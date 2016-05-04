using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	public static class Infer
	{
		public static IndexName Index(IndexName index) => index;
		public static IndexName Index<T>() => typeof(T);

		public static Indices Indices<T>() => typeof(T);
		public static Indices Indices(params IndexName[] indices) => indices;
		public static Indices Indices(IEnumerable<IndexName> indices) => indices.ToArray();
		public static Indices AllIndices = Nest.Indices.All;

		public static TypeName Type(TypeName type) => type;
		public static TypeName Type<T>() => typeof(T);
		public static Types Type(IEnumerable<TypeName> types) => new Types.ManyTypes(types);
		public static Types Type(params TypeName[] types) => new Types.ManyTypes(types);
		public static Types AllTypes = Nest.Types.All;

		public static Names Names(params string[] names) => string.Join(",", names);
		public static Names Names(IEnumerable<string> names) => string.Join(",", names);

		public static Id Id<T>(T document) where T : class => Nest.Id.From(document);

		public static Fields Fields<T>(params Expression<Func<T, object>>[] fields) where T : class =>
			new Fields(fields.Select(f=>(Field)f));

		public static Fields Fields<T>(params string[] fields) where T : class =>
			new Fields(fields.Select(f=>(Field)f));

		/// <summary>
		/// Create a strongly typed string field name representation of the path to a property
		/// <para>i.e p => p.Array.First().SubProperty.Field will return 'array.subProperty.field'</para>
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="path">The path we want to specify</param>
		/// <param name="boost">An optional ^boost postfix, only make sense with certain queries</param>
		public static Field Field<T>(Expression<Func<T, object>> path, double? boost = null)
			where T : class =>
			Nest.Field.Create(path, boost);

		public static Field Field(string field, double? boost = null) => Nest.Field.Create(field, boost);

		public static PropertyName Property(string property) => property;

		public static PropertyName Property<T>(Expression<Func<T, object>> path)
			where T : class => path;
	}
}
