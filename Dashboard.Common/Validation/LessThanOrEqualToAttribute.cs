using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Dashboard.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class LessThanOrEqualToAttribute : ValidationAttribute
    {
        #region Constants

        private const string _defaultErrorMessage = "{0} must not exceed {1}.";

        #endregion

        #region Fields

        private string _basePropertyName;
        private string _basePropertyDisplayName;

        #endregion

        #region Constructors

        public LessThanOrEqualToAttribute(string basePropertyName, string basePropertyDisplayName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
            _basePropertyDisplayName = basePropertyDisplayName;
        }

        #endregion

        #region Properties

        public string BasePropertyName
        {
            get { return _basePropertyName; }
        }

        public string BasePropertyDisplayName
        {
            get { return _basePropertyDisplayName; }
        }

        #endregion

        #region Overrides - ValidationAttribute

        public override string FormatErrorMessage(string name)
        {
            string errorMessage = String.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
            return String.Format(CultureInfo.InvariantCulture, errorMessage, name, _basePropertyDisplayName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            PropertyInfo basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            if (basePropertyInfo.PropertyType.GetInterface("IComparable") != null)
            {
                IComparable baseValue = basePropertyInfo.GetValue(validationContext.ObjectInstance, null) as IComparable;
                IComparable thisValue = value as IComparable;

                if (baseValue != null && thisValue != null)
                {
                    if (thisValue.CompareTo(baseValue) > 0)
                    {
                        var message = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(message);
                    }
                }
            }
            else
            {
                string messageTemplate = "This validation attribute is not supported for properties of type {0}.";
                string message = String.Format(CultureInfo.InvariantCulture, messageTemplate, basePropertyInfo.PropertyType.Name);
                throw new NotSupportedException(message);
            }

            return result;
        }

        #endregion
    }
}
