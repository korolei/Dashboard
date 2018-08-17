
using System;
using Microsoft.AspNetCore.Authorization;

namespace Dashboard.Common.Security
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true)]
    public class CanExecuteAttribute : AuthorizeAttribute
    {
        public CanExecuteAttribute(string className, string methodName)
        {
            if (!AuthorizationHelper.CanExecute(className, methodName))
            {
                throw new AuthorizationException($"Invalid permissions for class name:'{className}' and method name:'{methodName}'");
            }
        }
        
        public CanExecuteAttribute(string className)
        {
            if (!AuthorizationHelper.CanExecute(className, string.Empty))
            {
                throw new AuthorizationException($"Invalid permissions for class name:'{className}'.");
            }
        }
    }
}