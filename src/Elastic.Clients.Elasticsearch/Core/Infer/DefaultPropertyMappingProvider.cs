// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <inheritdoc />
public class DefaultPropertyMappingProvider : IPropertyMappingProvider
{
	protected readonly ConcurrentDictionary<string, PropertyMapping> Properties = new();

	/// <inheritdoc />
	public virtual PropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
	{
		// FUTURE: Perf: Can we avoid this string allocation by using a ValueTuple as the key?
		// This is only called once per property on each `PropertyName` as the `FieldResolver` also caches the final string.
		// We'd need to benchmark this before assuming a ValueTuple is better as we'd keep the FullName and Name strings alive in the
		// dictionary which may have more overall impact that the string creation.
		var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";

		if (Properties.TryGetValue(memberInfoString, out var mapping))
			return mapping;

		mapping = PropertyMappingFromAttributes(memberInfo);
		Properties.TryAdd(memberInfoString, mapping);
		return mapping;
	}

	private static PropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
	{
		// If the property includes a System.Text.Json `JsonPropertyName` attribute, grab the name from that.
		var jsonPropertyName = memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>(true);

		if (jsonPropertyName is null)
			return default;

		return new PropertyMapping
		{
			Name = jsonPropertyName?.Name
		};
	}
}
