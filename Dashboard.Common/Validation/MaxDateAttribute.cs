using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Dashboard.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class MaxDateAttribute : ValidationAttribute
    {
        #region Constants

        private const string _defaultErrorMessage = "{0} must not be greater than {1}.";
        
        public const string DateFormat = "MM/dd/yyyy";
        public const string CurrentDate = "CurrentDate";

        #endregion

        #region Fields

        private DateTime _maxValue;

        #endregion

        #region Constructors

        public MaxDateAttribute(string maxValue)
            : base(_defaultErrorMessage)
        {
            _maxValue = ParseDate(maxValue);
        }

        #endregion

        #region Properties

        public string MaxValue
        {
            get { return _maxValue.ToShortDateString(); }
        }

        public DateTime MaxValueAsDate
        {
            get { return _maxValue; }
        }

        #endregion

        #region Overrides - ValidationAttribute

        public override string FormatErrorMessage(string name)
        {
            string maxValue = _maxValue.ToLongDateString();

            if (_maxValue == DateTime.Today)
            {
                maxValue = "today's date";
            }


            string errorMessage = String.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
            return String.Format(CultureInfo.InvariantCulture, errorMessage, name, maxValue);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;

            if (value != null && value is DateTime)
            {
                DateTime dateValue = (DateTime)value;

                if (dateValue.Date > _maxValue)
                {
                    string message = FormatErrorMessage(validationContext.DisplayName);
                    result = new ValidationResult(message);
                }
            }

            return result;
        }

        #endregion

        #region Private Methods

        private static DateTime ParseDate(string date)
        {
            DateTime result;

            if (date == CurrentDate)
            {
                result = DateTime.Today;
            }
            else
            {
                result = DateTime.ParseExact(date, DateFormat, CultureInfo.InvariantCulture);
            }

            return result;
        }

        #endregion
    }
}
