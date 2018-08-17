using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dashboard.Common.Utilities;

namespace Dashboard.Common
{
	public class PaginatedList<T> : PaginatedListBase<T>
	{
		#region Constructors

		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "pageNumber-1", 
			Justification = "The pageNumber is checked during parameter validation")]
		public PaginatedList(IQueryable<T> source, int pageNumber, int pageSize, string orderByFieldName, bool orderAscending)
		{
			ParameterValidator.AssertIsNotNull("source", source);
			ParameterValidator.AssertIsPositiveValue("pageNumber", pageNumber);
            ParameterValidator.AssertIsPositiveValue("pageSize", pageSize);
			ParameterValidator.AssertIsNotNullOrWhiteSpace("orderByFieldName", orderByFieldName);

			ItemTotal = source.Count();

			string methodName = orderAscending ? "OrderBy" : "OrderByDescending";
			IOrderedQueryable<T> sortedList = ApplySort(source, orderByFieldName, methodName);

			Items = sortedList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		#endregion

		#region Private Helper Methods

		private static IOrderedQueryable<T> ApplySort(IQueryable<T> source, string propertyName, string methodName)
		{
			string[] propertyParts = propertyName.Split('.');
			
			Type type = typeof(T);

			ParameterExpression parameterExpression = Expression.Parameter(type, "x");
			Expression expression = parameterExpression;

			foreach (string propertyPart in propertyParts)
			{
				PropertyInfo propertyInfo = type.GetProperty(propertyPart);
				expression = Expression.Property(expression, propertyInfo);
				type = propertyInfo.PropertyType;
			}

			Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

			LambdaExpression lambda = Expression.Lambda(delegateType, expression, parameterExpression);

			object result = typeof(Queryable).GetMethods().Single(
				method => method.Name == methodName
					&& method.IsGenericMethodDefinition
					&& method.GetGenericArguments().Length == 2
					&& method.GetParameters().Length == 2)
				.MakeGenericMethod(typeof(T), type)
				.Invoke(null, new object[] { source, lambda });

			return ((IOrderedQueryable<T>)result);
		}

		#endregion
	}  
}
