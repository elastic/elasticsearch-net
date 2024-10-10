// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// TODO: Move away from shared project

#if !ELASTICSEARCH_SERVERLESS

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Mapping;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class GetFieldMappingResponse
{
	[JsonIgnore]
	public IReadOnlyDictionary<IndexName, TypeFieldMappings> FieldMappings => BackingDictionary;

	public IProperty? GetProperty(IndexName index, Field field)
	{
		if (index is null)
			throw new ArgumentNullException(nameof(index));

		if (field is null)
			throw new ArgumentNullException(nameof(field));

		var mappings = MappingsFor(index);

		if (mappings is null)
			return null;

		if (!mappings.TryGetValue(field, out var fieldMapping) || fieldMapping.Mapping is null)
			return null;

		return fieldMapping.Mapping.TryGetProperty(PropertyName.FromField(field), out var property) ? property : null;
	}

	public bool TryGetProperty(IndexName index, Field field, out IProperty property)
	{
		property = null;

		if (index is null)
			throw new ArgumentNullException(nameof(index));

		if (field is null)
			throw new ArgumentNullException(nameof(field));

		var mappings = MappingsFor(index);

		if (mappings is null)
			return false;

		if (!mappings.TryGetValue(field, out var fieldMapping) || fieldMapping.Mapping is null)
			return false;

		if (fieldMapping.Mapping.TryGetProperty(PropertyName.FromField(field), out var matched))
		{
			property = matched;
			return true;
		}

		return false;
	}

	public IProperty? PropertyFor<T>(Field field) => PropertyFor<T>(field, null);

	public IProperty? PropertyFor<T>(Field field, IndexName index) =>
		GetProperty(index ?? Infer.Index<T>(), field);

	public IProperty? PropertyFor<T, TValue>(Expression<Func<T, TValue>> objectPath)
		where T : class =>
			GetProperty(Infer.Index<T>(), Infer.Field(objectPath));

	public IProperty? PropertyFor<T, TValue>(Expression<Func<T, TValue>> objectPath, IndexName index)
		where T : class =>
			GetProperty(index, Infer.Field(objectPath));

	public IProperty? PropertyFor<T>(Expression<Func<T, object>> objectPath)
		where T : class =>
			GetProperty(Infer.Index<T>(), Infer.Field(objectPath));

	public IProperty? PropertyFor<T>(Expression<Func<T, object>> objectPath, IndexName index)
		where T : class =>
			GetProperty(index, Infer.Field(objectPath));

	private IReadOnlyDictionary<Field, FieldMapping> MappingsFor(IndexName index)
	{
		if (!FieldMappings.TryGetValue(index, out var indexMapping) || indexMapping.Mappings == null)
			return null;

		return indexMapping.Mappings;
	}
}

#endif
