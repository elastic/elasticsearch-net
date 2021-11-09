// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch
{
	public class ClrTypeMapping
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

	public sealed class ClrTypeMapping<TDocument> : ClrTypeMapping where TDocument : class
	{
		public ClrTypeMapping() : base(typeof(TDocument)) { }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> IdProperty { get; set; }

		/// <inheritdoc />
		public IList<IClrPropertyMapping<TDocument>> Properties { get; set; }

		/// <inheritdoc />
		public Expression<Func<TDocument, object>> RoutingProperty { get; set; }
	}

	public sealed class ClrTypeMappingDescriptor : DescriptorBase<ClrTypeMappingDescriptor>
	{
		private readonly Type _type;

		internal string _indexName;
		internal string _relationName;
		internal string _idProperty;
		internal bool _disableIdInference;

		 // TODO - XML Comments

		/// <summary>
		/// Instantiates a new instance of <see cref="ClrTypeMappingDescriptor" />
		/// </summary>
		/// <param name="type">The CLR type to map</param>
		public ClrTypeMappingDescriptor(Type type) => _type = type;

		///// <inheritdoc cref="IClrTypeMapping.IndexName"/>
		public ClrTypeMappingDescriptor IndexName(string indexName) => Assign(indexName, (a, v) => a._indexName = v);

		///// <inheritdoc cref="IClrTypeMapping.RelationName"/>
		public ClrTypeMappingDescriptor RelationName(string relationName) => Assign(relationName, (a, v) => a._relationName = v);

		/// <summary>
		/// The property for the given <see cref="Type" /> to resolve IDs from.
		/// </summary>
		public ClrTypeMappingDescriptor IdProperty(string idProperty) => Assign(idProperty, (a, v) => a._idProperty = v);

		///// <inheritdoc cref="IClrTypeMapping.DisableIdInference"/>
		public ClrTypeMappingDescriptor DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a._disableIdInference = v);
	}

	public sealed class ClrTypeMappingDescriptor<TDocument>
		: DescriptorBase<ClrTypeMappingDescriptor<TDocument>>
			where TDocument : class
	{
		internal string _indexName;
		internal string _relationName;
		internal string _idProperty;
		internal bool _disableIdInference;

		internal Expression<Func<TDocument, object>> _idPropertyExpression;
		internal Expression<Func<TDocument, object>> _routingPropertyExpression;
		internal IList<IClrPropertyMapping<TDocument>> _properties;

		/// <summary>
		/// The default Elasticsearch index name for <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IndexName(string indexName) => Assign(indexName, (a, v) => a._indexName = v);

		/// <summary>
		/// The relation name for <typeparamref name="TDocument" /> to resolve to.
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> RelationName(string relationName) => Assign(relationName, (a, v) => a._relationName = v);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that Elastic.Clients.Elasticsearch will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(Expression<Func<TDocument, object>> property) => Assign(property, (a, v) => a._idPropertyExpression = v);

		/// <summary>
		/// Set a default Id property on CLR type <typeparamref name="TDocument" /> that Elastic.Clients.Elasticsearch will evaluate
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> IdProperty(string property) => Assign(property, (a, v) => a._idProperty = v);

		/// <summary> Provide a default routing parameter lookup based on <typeparamref name="TDocument" /> </summary>
		public ClrTypeMappingDescriptor<TDocument> RoutingProperty(Expression<Func<TDocument, object>> property) =>
			Assign(property, (a, v) => a._routingPropertyExpression = v);

		/// <summary>
		/// Ignore <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property) =>
			Assign(property, (a, v) => a._properties.Add(new IgnoreClrPropertyMapping<TDocument>(v)));

		/// <summary>
		/// Rename <paramref name="property" /> on CLR type <typeparamref name="TDocument" />
		/// </summary>
		public ClrTypeMappingDescriptor<TDocument> PropertyName(Expression<Func<TDocument, object>> property, string newName) =>
			Assign(new RenameClrPropertyMapping<TDocument>(property, newName), (a, v) => a._properties.Add(v));

		///// <inheritdoc cref="IClrTypeMapping.DisableIdInference"/>
		public ClrTypeMappingDescriptor<TDocument> DisableIdInference(bool disable = true) => Assign(disable, (a, v) => a._disableIdInference = v);
	}
}
