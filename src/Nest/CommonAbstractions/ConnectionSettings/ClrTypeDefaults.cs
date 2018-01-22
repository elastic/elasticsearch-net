using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public interface IClrTypeMappings
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

	}

	public interface IClrTypeMappings<TDocument> : IClrTypeMappings where TDocument : class
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

	public class ClrTypeMappings : IClrTypeMappings
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ClrTypeMappings"/>
		/// </summary>
		public ClrTypeMappings(Type type) => ClrType = type;

		/// <inheritdoc />
		public Type ClrType { get; }

		/// <inheritdoc />
		public string IndexName { get; set; }

		/// <inheritdoc />
		public string TypeName { get; set; }

		/// <inheritdoc />
		public string RelationName { get; set; }

	}
	public class ClrTypeMappings<TDocument> : ClrTypeMappings, IClrTypeMappings<TDocument> where TDocument : class
	{
		public ClrTypeMappings() : base(typeof(TDocument)) { }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> RoutingProperty { get; set; }

		/// <inheritdoc />
		public IList<IClrPropertyMapping<TDocument>> Properties { get; set; }
	}

	public class ClrTypeMappingsDescriptor : DescriptorBase<ClrTypeMappingsDescriptor,IClrTypeMappings> , IClrTypeMappings
	{
		private readonly Type _type;

		/// <summary>
		/// Instantiates a new instance of <see cref="ClrTypeMappingsDescriptor"/>
		/// </summary>
		/// <param name="type">The CLR type to map</param>
		public ClrTypeMappingsDescriptor(Type type) => _type = type;

		Type IClrTypeMappings.ClrType => _type;
		string IClrTypeMappings.IndexName { get; set; }
		string IClrTypeMappings.TypeName { get; set; }
		string IClrTypeMappings.RelationName { get; set; }

		/// <summary>
		/// The default Elasticsearch index name for the CLR type
		/// </summary>
		public ClrTypeMappingsDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for the CLR type
		/// </summary>
		public ClrTypeMappingsDescriptor TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for the CLR type to resolve to.
		/// </summary>
		public ClrTypeMappingsDescriptor RelationName(string relationName) => Assign(a => a.RelationName = relationName);
	}

	public class ClrTypeMappingsDescriptor<TDocument>
		: DescriptorBase<ClrTypeMappingsDescriptor<TDocument>,IClrTypeMappings<TDocument>>, IClrTypeMappings<TDocument>
		where TDocument : class
	{
		Type IClrTypeMappings.ClrType { get; } = typeof (TDocument);
		string IClrTypeMappings.IndexName { get; set; }
		string IClrTypeMappings.TypeName { get; set; }
		string IClrTypeMappings.RelationName { get; set; }
		Expression<Func<TDocument, object>> IClrTypeMappings<TDocument>.IdProperty { get; set; }
		Expression<Func<TDocument, object>> IClrTypeMappings<TDocument>.RoutingProperty { get; set; }
		IList<IClrPropertyMapping<TDocument>> IClrTypeMappings<TDocument>.Properties { get; set; } = new List<IClrPropertyMapping<TDocument>>();

		/// <summary>
		/// The default Elasticsearch index name for <typeparamref name="TDocument"/>
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// The default Elasticsearch type name for <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// The relation name for <typeparamref name="TDocument" /> to resolve to.
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> RelationName(string relationName) => Assign(a => a.RelationName = relationName);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.IdProperty = property);

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		public ClrTypeMappingsDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) => Assign(a => a.RoutingProperty = property);

		/// <summary>
		/// Ignore <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property) =>
			Assign(a => a.Properties.Add(new IgnoreClrPropertyMapping<TDocument>(property)));

		/// <summary>
		/// Rename <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingsDescriptor<TDocument> Rename(Expression<Func<TDocument, object>> property, string newName) =>
			Assign(a => a.Properties.Add(new RenameClrPropertyMapping<TDocument>(property, newName)));

	}
}
