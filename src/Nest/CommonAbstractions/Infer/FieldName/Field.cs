using System;
using System.Linq.Expressions;

namespace Nest
{
	public static class Property
	{
		/// <summary>
		/// Create a strongly typed string field name representation of the path to a property
		/// <para>i.e p => p.Arrary.First().SubProperty.Field will return 'array.subProperty.field'</para>
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="path">The path we want to specify</param>
		/// <param name="boost">An optional ^boost postfix, only make sense with certain queries</param>
		public static FieldName Field<T>(Expression<Func<T, object>> path, double? boost = null) 
			where T : class
		{
			return FieldName.Create(path, boost);
		}
		
	}
}