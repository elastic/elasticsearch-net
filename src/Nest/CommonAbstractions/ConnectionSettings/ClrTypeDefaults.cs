// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		/// The property for the given <see cref="ClrType" /> to resolve ids from.
		/// </summary>
		string IdPropertyName { get; set; }

		/// <summary>
		/// The default Elasticsearch index name for the given <see cref="ClrType" />
		/// </summary>
		string IndexName { get; set; }

		/// <summary>
		/// The relation name for the given <see cref="ClrType" /> to resolve to.
		/// </summary>
		string RelationName { get; set; }

		/// <summary>Disables Id inference for the given <see cref="ClrType"/>.
		/// By default, the _id value for a document is inferred from a property named Id,
		/// or from the property named by <see cref="IdPropertyName"/>, if set.
		/// </summary>
		bool DisableIdInference { get; set; }
	}

	public interface IClrTypeMapping<TDocument> : IClrTypeMapping where TDocument : class
	{
		/// <summary> Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate </summary>
		Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <summary>
		/// Ignore or rename certain properties of CLR type <typeparamref name="TDocument" />
		/// </summary>
		IList<IClrPropertyMapping<TDocument>> Properties { get; set; }

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		Expression<Func<TDocument, object>> RoutingProperty { get; set; }
	}

	public class ClrTypeMapping : IClrTypeMapping
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ClrTypeMapping" />
		/// </summary>
		public ClrTypeMapping(Type type) => ClrType = type;

		/// <inheritdoc />
		public Type ClrType { get; }

		/// <inheritdoc />
		public string IdPropertyName { get; set; }

		/// <inheritdoc />
		public string IndexName { get; set; }

		/// <inheritdoc />
		public string RelationName { get; set; }

		/// <inheritdoc />
		public bool DisableIdInference { get; set; }
	}

	public class ClrTypeMapping<TDocument> : ClrTypeMapping, IClrTypeMapping<TDocument> where TDocument : class
	{
		public ClrTypeMapping() : base(typeof(TDocument)) { }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <inheritdoc />
		public IList<IClrPropertyMapping<TDocument>> Properties { get; set; }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> RoutingProperty { get; set; }
	}

	public class ClrTypeMappingDescriptor : DescriptorBase<ClrTypeMappingDescriptor, IClrTypeMapping>, IClrTypeMapping
	{
		private readonly Type _type;

		/// <summary>
		/// Instantiates a new instance of <see cref="ClrTypeMappingDescriptor" />
		/// </summary>
		/// <param name="type">The CLR type to map</param>
		public ClrTypeMappingDescriptor(Type type) => _type = type;

		Type IClrTypeMapping.ClrType => _type;
		string IClrTypeMapping.IdPropertyName { get; set; }
		string IClrTypeMapping.IndexName { get; set; }
		string IClrTypeMapping.RelationName { get; set; }
		bool IClrTypeMapping.DisableIdInference { get; set; }

		/// <inheritdoc cref="IClrTypeMapping.IndexName"/>
		public ClrTypeMappingDescriptor IndexName(string indexName) => Assign(indexName, (a, v) => a.IndexName = v);

		/// <inheritdoc cref="IClrTypeMapping.RelationName"/>
		public ClrTypeMappingDescriptor RelationName(string relationName) => Assign(relationName, (a, v) => a.RelationName = v);

		/// <inheritdoc cref="IClrTypeMapping.IdPropertyName"/>
		public ClrTypeMappingDescriptor IdProperty(string idProperty) => Assign(idProperty, (a, v) => a.IdPropertyName = v);

		/// <inheritdoc cref="IClrTypeMapping.DisableIdInference"/>
		public ClrTypeMappingDescriptor DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a.DisableIdInference = v);
	}

	public class ClrTypeMappingDescriptor<TDocument>
		: DescriptorBase<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>>, IClrTypeMapping<TDocument>
		where TDocument : class
	{
		Type IClrTypeMapping.ClrType { get; } = typeof(TDocument);
		Expression<Func<TDocument, object>> IClrTypeMapping<TDocument>.IdProperty { get; set; }
		string IClrTypeMapping.IdPropertyName { get; set; }
		string IClrTypeMapping.IndexName { get; set; }
		IList<IClrPropertyMapping<TDocument>> IClrTypeMapping<TDocument>.Properties { get; set; } = new List<IClrPropertyMapping<TDocument>>();
		string IClrTypeMapping.RelationName { get; set; }
		Expression<Func<TDocument, object>> IClrTypeMapping<TDocument>.RoutingProperty { get; set; }
		bool IClrTypeMapping.DisableIdInference { get; set; }

		/// <summary>
		/// The default Elasticsearch index name for <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IndexName(string indexName) => Assign(indexName, (a, v) => a.IndexName = v);

		/// <summary>
		/// The relation name for <typeparamref name="TDocument" /> to resolve to.
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> RelationName(string relationName) => Assign(relationName, (a, v) => a.RelationName = v);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(property, (a, v) => a.IdProperty = v);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(string property) => Assign(property, (a, v) => a.IdPropertyName = v);

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		public ClrTypeMappingDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) =>
			Assign(property, (a, v) => a.RoutingProperty = v);

		/// <summary>
		/// Ignore <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property) =>
			Assign(property, (a, v) => a.Properties.Add(new IgnoreClrPropertyMapping<TDocument>(v)));

		/// <summary>
		/// Rename <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> PropertyName(Expression<Func<TDocument, object>> property, string newName) =>
			Assign(new RenameClrPropertyMapping<TDocument>(property, newName), (a, v) => a.Properties.Add(v));

		/// <inheritdoc cref="IClrTypeMapping.DisableIdInference"/>
		public ClrTypeMappingDescriptor<TDocument> DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a.DisableIdInference = v);
	}
}
