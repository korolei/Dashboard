using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using Dashboard.Common.Data;
using Dashboard.Common.Utilities;

namespace Dashboard.Common
{
    public interface IEntity
    {
        int Id { get; }
    }
    public abstract class EntityBase : IEntity
    {
        #region Fields

        private int _id;

        #endregion

        #region Constructors

        protected EntityBase()
        {
        }

        protected EntityBase(int id)
        {
            _id = id;
        }

        #endregion

        #region Properties

        [DisplayName("Id")]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        #endregion

        #region Overrides - System.Object

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj != null)
            {
                EntityBase entityBase = obj as EntityBase;

                if (entityBase != null)
                {
                    result = (this == entityBase);
                }
            }

            return result;
        }

        public static bool operator ==(EntityBase obj1, EntityBase obj2)
        {
            if ((object)obj1 == null && (object)obj2 == null)
            {
                return true;
            }

            if ((object)obj1 == null || (object)obj2 == null)
            {
                return false;
            }

            if (!obj1.Id.Equals(obj2.Id))
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(EntityBase obj1, EntityBase obj2)
        {
            return (!(obj1 == obj2));
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        #endregion

        #region Public Methods

        public bool CanEditProperty(string propertyName)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("propertyName", propertyName);

            PropertyInfo propertyInfo = GetType().GetProperty(propertyName);

            bool hasPublicSetMethod = propertyInfo.GetSetMethod() != null && propertyInfo.GetSetMethod().IsPublic;
            
            return propertyInfo.CanWrite && hasPublicSetMethod && !DenyEditProperty(propertyName);
        }

        public void AssertCanEditProperty(string propertyName)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("propertyName", propertyName);

            if (!CanEditProperty(propertyName))
            {
                string message = "An attempt to update the '{0}' property was denied by the system.";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, message, propertyName));
            }
        }

        public virtual bool DenyEditProperty(string propertyName)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("propertyName", propertyName);

            return false;
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IEnumerable<string> GetUpdatablePropertyNames()
        {
            Collection<string> result = new Collection<string>();

            foreach (PropertyInfo propertyInfo in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (CanEditProperty(propertyInfo.Name))
                {
                    result.Add(propertyInfo.Name);
                }
            }

            return result;
        }

        public virtual void AssertIsValid()
        {
            Collection<ValidationResult> validationResults = new Collection<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true))
            {
                throw new DataValidationException(GetDataValidationErrorMessage(validationResults));
            }
        }

        #endregion

        #region Private Methods

        private static string GetDataValidationErrorMessage(Collection<ValidationResult> validationResults)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Data is invalid. Please correct the following errors:");

            foreach (ValidationResult validationResult in validationResults)
            {
                builder.AppendLine(validationResult.ErrorMessage);
            }

            return builder.ToString();
        }

        #endregion
    }
}
