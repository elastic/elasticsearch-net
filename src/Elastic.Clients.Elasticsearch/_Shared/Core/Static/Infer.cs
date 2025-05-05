// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Elastic.Clients.Elasticsearch;

public static class Infer
{
	//public static readonly Indices AllIndices = Elastic.Clients.Elasticsearch.Indices.All;

	public static IndexName Index(IndexName index) => index;

	public static IndexName Index<T>() => typeof(T);

	public static IndexName Index(Type type) => type;

	public static IndexName Index<T>(string clusterName) => IndexName.From<T>(clusterName);

	public static Indices Indices<T>() => typeof(T);

	public static Indices Indices(params IndexName[] indices) => indices;

	public static Indices Indices(IEnumerable<IndexName> indices) => indices.ToArray();

	public static RelationName Relation(string type) => type;

	public static RelationName Relation(Type type) => type;

	public static RelationName Relation<T>() => typeof(T);

	public static Routing Route<T>(T instance) where T : class => Routing.From(instance);

	public static Names Names(params string[] names) => string.Join(",", names);

	public static Names Names(IEnumerable<string> names) => string.Join(",", names);

	public static Id Id<T>(T document) where T : class => Elasticsearch.Id.From(document);

	public static Fields Fields<T>(params Expression<Func<T, object?>>[] fields) where T : class =>
		new(fields.Select(f => new Field(f)));

	public static Fields Fields(params string[] fields) => new(fields.Select(f => new Field(f)));

	public static Fields Fields(params PropertyInfo[] properties) => new(properties.Select(f => new Field(f)));

	/// <summary>
	///     Create a strongly typed string field name representation of the path to a property
	///     <para>e.g. p => p.Array.First().SubProperty.Field will return 'array.subProperty.field'</para>
	/// </summary>
	public static Field Field<T, TValue>(Expression<Func<T, TValue>> path, double? boost = null,
		string format = null)
		 => new(path, boost, format);

	/// <inheritdoc cref="Field{T, TValue}" />
	public static Field Field<T>(Expression<Func<T, object?>> path, double? boost = null, string format = null)
		 => new(path, boost, format);

	public static Field Field(string field, double? boost = null, string format = null) =>
		new(field, boost, format);

	public static Field Field(PropertyInfo property, double? boost = null, string format = null) =>
		new(property, boost, format);

	public static PropertyName Property(string property) => property;

	public static PropertyName Property<T, TValue>(Expression<Func<T, TValue>> path) where T : class => path;

	public static PropertyName Property<T>(Expression<Func<T, object?>> path) where T : class => path;
}
