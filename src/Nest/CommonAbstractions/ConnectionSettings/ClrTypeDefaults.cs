using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public interface IClrTypeDefault
	{
		/// <summary>
		/// The CLR type the mapping relates to
		/// </summary>
		Type ClrType { get; }

		/// <summary>
		/// The default Elasticsearch index name for <see cref="ClrType"/>
		/// </summary>
		string IndexName { get; set; }

		/// <summary>
		/// The default Elasticsearch type name for <see cref="ClrType"/>
		/// </summary>
		string TypeName { get; set; }

		/// <summary>
		/// The relation name for <see cref="ClrType"/> to resolve to.
		/// </summary>
		string RelationName { get; set; }

		/// <summary>
		/// The default query to inject if constraint to
		/// </summary>
		QueryContainer Query { get; set; }
	}

	public interface IClrTypeDefaults<TDocument> : IClrTypeDefault where TDocument : class
	{
		/// <summary> Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate </summary>
		Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		Expression<Func<TDocument, object>> RoutingProperty { get; set; }

		/// <summary>
		/// Ignore or rename certain properties of CLR type <typeparamref name="TDocument" />
		/// </summary>
		IList<IClrPropertyMapping<TDocument>> Properties { get; set; }
	}

	public class ClrTypeDefaults : IClrTypeDefault
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ClrTypeDefaults"/>
		/// </summary>
		public ClrTypeDefaults(Type type) => ClrType = type;

		/// <inheritdoc />
		public Type ClrType { get; }

		/// <inheritdoc />
		public string IndexName { get; set; }

		/// <inheritdoc />
		public string TypeName { get; set; }

		/// <inheritdoc />
		public string RelationName { get; set; }

	}
	public class ClrTypeDefaults<TDocument> : ClrTypeDefaults, IClrTypeDefaults<TDocument> where TDocument : class
	{
		public ClrTypeDefaults() : base(typeof(TDocument)) { }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> RoutingProperty { get; set; }

		/// <inheritdoc />
		public IList<IClrPropertyMapping<TDocument>> Properties { get; set; }
	}

	public class ClrTypeDefaultsDescriptor : DescriptorBase<ClrTypeDefaultsDescriptor,IClrTypeDefault> , IClrTypeDefault
	{
		private readonly Type _type;

		/// <summary>
		/// Instantiates a new instance of <see cref="ClrTypeDefaultsDescriptor"/>
		/// </summary>
		/// <param name="type">The CLR type to map</param>
		public ClrTypeDefaultsDescriptor(Type type) => _type = type;

		Type IClrTypeDefault.ClrType => _type;
		string IClrTypeDefault.IndexName { get; set; }
		string IClrTypeDefault.TypeName { get; set; }
		string IClrTypeDefault.RelationName { get; set; }

		/// <summary>
		/// The default Elasticsearch index name for the CLR type
		/// </summary>
		public ClrTypeDefaultsDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for the CLR type
		/// </summary>
		public ClrTypeDefaultsDescriptor TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for the CLR type to resolve to.
		/// </summary>
		public ClrTypeDefaultsDescriptor RelationName(string relationName) => Assign(a => a.RelationName = relationName);
	}

	public class ClrTypeDefaultsDescriptor<TDocument>
		: DescriptorBase<ClrTypeDefaultsDescriptor<TDocument>,IClrTypeDefaults<TDocument>>, IClrTypeDefaults<TDocument>
		where TDocument : class
	{
		Type IClrTypeDefault.ClrType { get; } = typeof (TDocument);
		string IClrTypeDefault.IndexName { get; set; }
		string IClrTypeDefault.TypeName { get; set; }
		string IClrTypeDefault.RelationName { get; set; }
		Expression<Func<TDocument, object>> IClrTypeDefaults<TDocument>.IdProperty { get; set; }
		Expression<Func<TDocument, object>> IClrTypeDefaults<TDocument>.RoutingProperty { get; set; }
		IList<IClrPropertyMapping<TDocument>> IClrTypeDefaults<TDocument>.Properties { get; set; } = new List<IClrPropertyMapping<TDocument>>();

		/// <summary>
		/// The default Elasticsearch index name for <typeparamref name="TDocument"/>
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for <typeparamref name="TDocument" /> to resolve to.
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> RelationName(string relationName) => Assign(a => a.RelationName = relationName);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.IdProperty = property);

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		public ClrTypeDefaultsDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.RoutingProperty = property);

		/// <summary>
		/// Ignore <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property) =>
			Assign(a => a.Properties.Add(new IgnoreClrPropertyMapping<TDocument>(property)));

		/// <summary>
		/// Rename <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeDefaultsDescriptor<TDocument> Rename(Expression<Func<TDocument, object>> property, string newName) =>
			Assign(a => a.Properties.Add(new RenameClrPropertyMapping<TDocument>(property, newName)));

	}
}
