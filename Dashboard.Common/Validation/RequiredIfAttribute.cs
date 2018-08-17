using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Dashboard.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        #region Constants

        private const string _defaultErrorMessage = "{0} is required.";

        #endregion

        #region Fields

        private string _basePropertyName;
        private object _targetValue;
        private RequiredAttribute _requiredAttribute;

        #endregion

        #region Constructors

        public RequiredIfAttribute(string basePropertyName, object targetValue)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
            _targetValue = targetValue;
            _requiredAttribute = new RequiredAttribute();
        }

        #endregion

        #region Properties

        public string BasePropertyName
        {
            get { return _basePropertyName; }
        }

        public object TargetValue
        {
            get { return _targetValue; }
        }

        #endregion

        #region Overrides - ValidationAttribute

        public override string FormatErrorMessage(string name)
        {
            string errorMessage = String.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
            return String.Format(CultureInfo.InvariantCulture, errorMessage, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            if (IsRequired(validationContext))
            {
                if (!_requiredAttribute.IsValid(value))
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    result = new ValidationResult(message);
                }
            }

            return result;
        }

        #endregion

        #region Private Methods

        private bool IsRequired(ValidationContext validationContext)
        {
            bool result = false;

            PropertyInfo basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);
            object basePropertyValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (basePropertyValue == null && _targetValue == null)
            {
                result = true;
            }
            else
            {
                result = (basePropertyValue != null && basePropertyValue.Equals(_targetValue));
            }

            return result;
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public string GetRequiredTargetValue()
        {
            string result = (_targetValue ?? String.Empty).ToString();

            if (_targetValue != null && _targetValue.GetType() == typeof(bool))
            {
                result = result.ToLowerInvariant();
            }

            return result;
        }

        #endregion
    }
}
