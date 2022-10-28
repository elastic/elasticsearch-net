// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;
using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch;

public class ClrTypeMapping
{
	/// <summary>
	/// Initializes a new instance of <see cref="ClrTypeMapping" />.
	/// </summary>
	public ClrTypeMapping(Type type) => ClrType = type;

	/// <summary>
	/// The CLR type the mapping relates to.
	/// </summary>
	public Type ClrType { get; }

	/// <summary>
	/// The property for the given <see cref="ClrType" /> to resolve IDs from.
	/// </summary>
	public string IdPropertyName { get; set; }

	/// <summary>
	/// The default Elasticsearch index name for the given <see cref="ClrType" />.
	/// </summary>
	public string IndexName { get; set; }

	/// <summary>
	/// The relation name for the given <see cref="ClrType" /> to resolve to.
	/// </summary>
	public string RelationName { get; set; }

	/// <summary>
	/// Disables ID inference for the given <see cref="ClrType"/>.
	/// By default, the _id value for a document is inferred from a property named Id,
	/// or from the property named by <see cref="IdPropertyName"/>, if set.
	/// </summary>
	public bool DisableIdInference { get; set; }
}

public sealed class ClrTypeMapping<TDocument> : ClrTypeMapping
{
	public ClrTypeMapping() : base(typeof(TDocument)) { }

	/// <summary>
	/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that NEST will evaluate
	/// </summary>
	public Expression<Func<TDocument, object>> IdProperty { get; set; }

	/// <summary>
	/// Provide a default routing parameter lookup based on <typeparamref name="TDocument" />
	/// </summary>
	public Expression<Func<TDocument, object>> RoutingProperty { get; set; }
}

public sealed class ClrTypeMappingDescriptor : Descriptor<ClrTypeMappingDescriptor>
{
	internal Type _clrType;
	internal string _indexName;
	internal string _relationName;
	internal string _idProperty;
	internal bool _disableIdInference;

	/// <summary>
	/// Instantiates a new instance of <see cref="ClrTypeMappingDescriptor" />
	/// </summary>
	/// <param name="type">The CLR type to map</param>
	public ClrTypeMappingDescriptor(Type type) => _clrType = type;

	/// <inheritdoc cref="ClrTypeMapping.IndexName"/>
	public ClrTypeMappingDescriptor IndexName(string indexName) => Assign(indexName, (a, v) => a._indexName = v);

	/// <inheritdoc cref="ClrTypeMapping.RelationName"/>
	public ClrTypeMappingDescriptor RelationName(string relationName) => Assign(relationName, (a, v) => a._relationName = v);

	/// <inheritdoc cref="ClrTypeMapping{T}.IdProperty"/>
	public ClrTypeMappingDescriptor IdProperty(string idProperty) => Assign(idProperty, (a, v) => a._idProperty = v);

	/// <inheritdoc cref="ClrTypeMapping.DisableIdInference"/>
	public ClrTypeMappingDescriptor DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a._disableIdInference = v);
}

public sealed class ClrTypeMappingDescriptor<TDocument> : Descriptor<ClrTypeMappingDescriptor<TDocument>>
{
	internal Type _clrType = typeof(TDocument);
	internal string _indexName;
	internal string _relationName;
	internal string _idProperty;
	internal bool _disableIdInference;

	internal Expression<Func<TDocument, object>> _idPropertyExpression;
	internal Expression<Func<TDocument, object>> _routingPropertyExpression;

	/// <inheritdoc cref="ClrTypeMapping.IndexName"/>
	public ClrTypeMappingDescriptor<TDocument> IndexName(string indexName) => Assign(indexName, (a, v) => a._indexName = v);

	/// <inheritdoc cref="ClrTypeMapping.RelationName"/>
	public ClrTypeMappingDescriptor<TDocument> RelationName(string relationName) => Assign(relationName, (a, v) => a._relationName = v);

	/// <inheritdoc cref="ClrTypeMapping{T}.IdProperty"/>
	public ClrTypeMappingDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(property, (a, v) => a._idPropertyExpression = v);

	/// <inheritdoc cref="ClrTypeMapping{T}.IdProperty"/>
	public ClrTypeMappingDescriptor<TDocument> IdProperty(string property) => Assign(property, (a, v) => a._idProperty = v);

	/// <inheritdoc cref="ClrTypeMapping{T}.RoutingProperty"/>
	public ClrTypeMappingDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) =>
		Assign(property, (a, v) => a._routingPropertyExpression = v);

	/// <inheritdoc cref="ClrTypeMapping.DisableIdInference"/>
	public ClrTypeMappingDescriptor<TDocument> DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a._disableIdInference = v);
}
