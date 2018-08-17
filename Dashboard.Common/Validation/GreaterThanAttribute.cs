using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Dashboard.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class GreaterThanAttribute : ValidationAttribute
    {
        #region Constants

        private const string _defaultErrorMessage = "{0} must be greater than {1}.";

        #endregion

        #region Fields

        private string _basePropertyName;

        #endregion

        #region Constructors

        public GreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        #endregion

        #region Properties

        public string BasePropertyName
        {
            get { return _basePropertyName; }
        }

        #endregion

        #region Overrides - ValidationAttribute

        public override string FormatErrorMessage(string name)
        {
            string errorMessage = String.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
            return String.Format(CultureInfo.InvariantCulture, errorMessage, name, _basePropertyName);
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
                    if (thisValue.CompareTo(baseValue) < 1)
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
