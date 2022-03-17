// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

public sealed class NestedSort
{
	public QueryContainer Filter { get; set; }

	public NestedSort Nested { get; set; }

	public Field Path { get; set; }

	public int? MaxChildren { get; set; }
}

public sealed class NestedSortDescriptor : DescriptorBase<NestedSortDescriptor>
{
	private QueryContainer _filter;
	private QueryContainerDescriptor _queryContainerDescriptor;
	private Action<QueryContainerDescriptor> _queryContainerDescriptorAction;
	private Field _path;

	public NestedSortDescriptor Path(Field path) => Assign(path, (a, v) => a._path = v);

	public NestedSortDescriptor Path<T, TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._path = v);

	public NestedSortDescriptor Path<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a._path = v);

	public NestedSortDescriptor Filter(QueryContainer queryContainer) =>
		Assign(queryContainer, (a, v) => a._filter = v);

	public NestedSortDescriptor Filter(QueryContainerDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._queryContainerDescriptor = v);

	public NestedSortDescriptor Filter(Action<QueryContainerDescriptor> configure) =>
			Assign(configure, (a, v) => a._queryContainerDescriptorAction = v);

	// TODO - Complete this

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();

		if (_path is not null)
		{
			writer.WritePropertyName("path");
			JsonSerializer.Serialize(writer, _path, options);
		}

		if (_filter is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _filter, options);
		}
		else if (_queryContainerDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _queryContainerDescriptor, options);
		}
		else if (_queryContainerDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			var descriptor = new QueryContainerDescriptor();
			_queryContainerDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		writer.WriteEndObject();
	}
}

public sealed class NestedSortDescriptor<TDocument> : DescriptorBase<NestedSortDescriptor<TDocument>>
{
	private QueryContainer _filter;
	private QueryContainerDescriptor<TDocument> _queryContainerDescriptor;
	private Action<QueryContainerDescriptor<TDocument>> _queryContainerDescriptorAction;
	private Field _path;

	public NestedSortDescriptor<TDocument> Path(Field path) => Assign(path, (a, v) => a._path = v);

	public NestedSortDescriptor<TDocument> Path<TValue>(Expression<Func<TDocument, TValue>> objectPath) => Assign(objectPath, (a, v) => a._path = v);

	public NestedSortDescriptor<TDocument> Path<T, TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._path = v);

	public NestedSortDescriptor<TDocument> Filter(QueryContainer queryContainer) =>
		Assign(queryContainer, (a, v) => a._filter = v);

	public NestedSortDescriptor<TDocument> Filter(QueryContainerDescriptor<TDocument> descriptor) =>
		Assign(descriptor, (a, v) => a._queryContainerDescriptor = v);

	public NestedSortDescriptor<TDocument> Filter(Action<QueryContainerDescriptor<TDocument>> configure) =>
			Assign(configure, (a, v) => a._queryContainerDescriptorAction = v);

	// TODO - Complete this

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();

		if (_path is not null)
		{
			writer.WritePropertyName("path");
			JsonSerializer.Serialize(writer, _path, options);
		}

		if (_filter is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _filter, options);
		}
		else if (_queryContainerDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _queryContainerDescriptor, options);
		}
		else if (_queryContainerDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			var descriptor = new QueryContainerDescriptor<TDocument>();
			_queryContainerDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		writer.WriteEndObject();
	}
}
