using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public interface IClrTypeMapping
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
		/// The property for <see cref="ClrType"/> to resolve ids from.
		/// </summary>
		string IdPropertyName { get; set; }

	}

	public interface IClrTypeMapping<TDocument> : IClrTypeMapping where TDocument : class
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

	public class ClrTypeMapping : IClrTypeMapping
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ClrTypeMapping"/>
		/// </summary>
		public ClrTypeMapping(Type type) => ClrType = type;

		/// <inheritdoc />
		public Type ClrType { get; }

		/// <inheritdoc />
		public string IndexName { get; set; }

		/// <inheritdoc />
		public string TypeName { get; set; }

		/// <inheritdoc />
		public string RelationName { get; set; }

		/// <inheritdoc />
		public string IdPropertyName { get; set; }
	}
	public class ClrTypeMapping<TDocument> : ClrTypeMapping, IClrTypeMapping<TDocument> where TDocument : class
	{
		public ClrTypeMapping() : base(typeof(TDocument)) { }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> RoutingProperty { get; set; }

		/// <inheritdoc />
		public IList<IClrPropertyMapping<TDocument>> Properties { get; set; }
	}

	public class ClrTypeMappingDescriptor : DescriptorBase<ClrTypeMappingDescriptor,IClrTypeMapping> , IClrTypeMapping
	{
		private readonly Type _type;

		/// <summary>
		/// Instantiates a new instance of <see cref="ClrTypeMappingDescriptor"/>
		/// </summary>
		/// <param name="type">The CLR type to map</param>
		public ClrTypeMappingDescriptor(Type type) => _type = type;

		Type IClrTypeMapping.ClrType => _type;
		string IClrTypeMapping.IndexName { get; set; }
		string IClrTypeMapping.TypeName { get; set; }
		string IClrTypeMapping.RelationName { get; set; }
		string IClrTypeMapping.IdPropertyName { get; set; }

		/// <summary>
		/// The default Elasticsearch index name for the CLR type
		/// </summary>
		public ClrTypeMappingDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for the CLR type
		/// </summary>
		public ClrTypeMappingDescriptor TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for the CLR type to resolve to.
		/// </summary>
		public ClrTypeMappingDescriptor RelationName(string relationName) => Assign(a => a.RelationName = relationName);

		/// <summary>
		/// The name of the property on the CLR type to resolve an Id from.
		/// </summary>
		public ClrTypeMappingDescriptor IdProperty(string idProperty) => Assign(a => a.IdPropertyName = idProperty);
	}

	public class ClrTypeMappingDescriptor<TDocument>
		: DescriptorBase<ClrTypeMappingDescriptor<TDocument>,IClrTypeMapping<TDocument>>, IClrTypeMapping<TDocument>
		where TDocument : class
	{
		Type IClrTypeMapping.ClrType { get; } = typeof (TDocument);
		string IClrTypeMapping.IndexName { get; set; }
		string IClrTypeMapping.TypeName { get; set; }
		string IClrTypeMapping.RelationName { get; set; }
		string IClrTypeMapping.IdPropertyName { get; set; }
		Expression<Func<TDocument, object>> IClrTypeMapping<TDocument>.IdProperty { get; set; }
		Expression<Func<TDocument, object>> IClrTypeMapping<TDocument>.RoutingProperty { get; set; }
		IList<IClrPropertyMapping<TDocument>> IClrTypeMapping<TDocument>.Properties { get; set; } = new List<IClrPropertyMapping<TDocument>>();

		/// <summary>
		/// The default Elasticsearch index name for <typeparamref name="TDocument"/>
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for <typeparamref name="TDocument" /> to resolve to.
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> RelationName(string relationName) => Assign(a => a.RelationName = relationName);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.IdProperty = property);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(string property) => Assign(a => a.IdPropertyName = property);

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		public ClrTypeMappingDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.RoutingProperty = property);

		/// <summary>
		/// Ignore <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property) =>
			Assign(a => a.Properties.Add(new IgnoreClrPropertyMapping<TDocument>(property)));

		/// <summary>
		/// Rename <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> PropertyName(Expression<Func<TDocument, object>> property, string newName) =>
			Assign(a => a.Properties.Add(new RenameClrPropertyMapping<TDocument>(property, newName)));

	}
}
