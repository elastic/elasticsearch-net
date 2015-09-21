using System;
using System.Linq.Expressions;

namespace Nest.Resolvers
{
	public static class Property
	{
		/// <summary>
		/// Create a strongly typed string representation of the path to a property
		/// <para>i.e p => p.Array.First().SubProperty.Field will return 'array.subProperty.field'</para>
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="path">The path we want to specify</param>
		/// <param name="boost">An optional ^boost postfix, only make sense with queries</param>
		public static PropertyPathMarker Path<T>(Expression<Func<T, object>> path, double? boost = null) 
			where T : class
		{
			return PropertyPathMarker.Create(path, boost);
		}
		
		/// <summary>
		/// Create a strongly typed string representation of the name to a property
		/// <para>i.e p => p.Array.First().SubProperty.Field will return 'field'</para>
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="path">The path we want to specify</param>
		/// <param name="boost">An optional ^boost postfix, only make sense with queries</param>
		public static PropertyNameMarker Name<T>(Expression<Func<T, object>> path, double? boost = null) 
			where T : class
		{
			return PropertyNameMarker.Create(path, boost);
		}
	}
}