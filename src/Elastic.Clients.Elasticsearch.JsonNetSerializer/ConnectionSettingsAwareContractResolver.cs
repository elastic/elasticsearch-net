// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace Elastic.Clients.Elasticsearch.JsonNetSerializer;

public class ConnectionSettingsAwareContractResolver : DefaultContractResolver
{
	public ConnectionSettingsAwareContractResolver(IElasticsearchClientSettings connectionSettings) =>
		ConnectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));

	protected IElasticsearchClientSettings ConnectionSettings { get; }

	protected override string ResolvePropertyName(string fieldName) =>
		ConnectionSettings.DefaultFieldNameInferrer != null
			? ConnectionSettings.DefaultFieldNameInferrer(fieldName)
			: base.ResolvePropertyName(fieldName);

	protected override JsonContract CreateContract(Type objectType)
	{
		var contract = base.CreateContract(objectType);
		if (objectType.IsEnum && objectType.GetCustomAttribute<StringEnumAttribute>() != null)
			contract.Converter = new StringEnumConverter();

		return contract;
	}

	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
	{
		var property = base.CreateProperty(member, memberSerialization);
		ApplyShouldSerializer(property);
		ApplyPropertyOverrides(member, property);
		return property;
	}

	/// <summary> Renames/Ignores a property based on the connection settings mapping or custom attributes for the property </summary>
	private void ApplyPropertyOverrides(MemberInfo member, JsonProperty property)
	{
		//if (!ConnectionSettings.PropertyMappings.TryGetValue(member, out var propertyMapping))
		//	propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

		var serializerMapping = ConnectionSettings.PropertyMappingProvider?.CreatePropertyMapping(member);

		var nameOverride = /*propertyMapping?.Name ??*/ serializerMapping?.Name;
		if (!string.IsNullOrWhiteSpace(nameOverride))
			property.PropertyName = nameOverride;

		var overrideIgnore = /*propertyMapping?.Ignore ??*/ serializerMapping?.Ignore;
		if (overrideIgnore.HasValue)
			property.Ignored = overrideIgnore.Value;
	}

	private static void ApplyShouldSerializer(JsonProperty property)
	{
		if (property.PropertyType == typeof(Query))
			property.ShouldSerialize = o => ShouldSerializeQuery(o, property);
		else if (property.PropertyType == typeof(IEnumerable<Query>))
			property.ShouldSerialize = o => ShouldSerializeQuerys(o, property);
	}

	private static bool ShouldSerializeQuery(object o, JsonProperty prop)
	{
		if (o == null)
			return false;
		if (prop.ValueProvider.GetValue(o) is not Query q)
			return false;
		//return q.IsWritable;
		return true;
	}

	private static bool ShouldSerializeQuerys(object o, JsonProperty prop)
	{
		if (o == null)
			return false;
		if (prop.ValueProvider.GetValue(o) is not IEnumerable<Query> q)
			return false;

		var queryContainers = q as Query[] ?? q.ToArray();
		//return queryContainers.Any(qq => qq != null && ((Query)qq).IsWritable);
		return queryContainers.Any(qq => qq != null);
	}
}
