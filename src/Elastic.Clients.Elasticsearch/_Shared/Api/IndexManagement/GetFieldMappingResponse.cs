// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Elastic.Clients.Elasticsearch.Mapping;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class GetFieldMappingResponse
{
	public IProperty? GetProperty(string index, string field)
	{
		if (index is null)
			throw new ArgumentNullException(nameof(index));

		if (field is null)
			throw new ArgumentNullException(nameof(field));

		var mappings = MappingsFor(index);

		if (mappings is null)
			return null;

		if (!mappings.TryGetValue(field, out var fieldMapping))
			return null;

		return string.Equals(fieldMapping.Mapping.Key, field, StringComparison.Ordinal) ? fieldMapping.Mapping.Value : null;
	}

	public bool TryGetProperty(string index, string field, [NotNullWhen(true)] out IProperty? property)
	{
		property = null;

		if (index is null)
			throw new ArgumentNullException(nameof(index));

		if (field is null)
			throw new ArgumentNullException(nameof(field));

		var mappings = MappingsFor(index);

		if (mappings is null)
			return false;

		if (!mappings.TryGetValue(field, out var fieldMapping))
			return false;

		if (string.Equals(fieldMapping.Mapping.Key, field, StringComparison.Ordinal))
		{
			property = fieldMapping.Mapping.Value;
			return true;
		}

		return false;
	}

	private IReadOnlyDictionary<string, FieldMapping>? MappingsFor(string index)
	{
		if (!FieldMappings.TryGetValue(index, out var indexMapping))
			return null;

		return indexMapping.Mappings;
	}
}
