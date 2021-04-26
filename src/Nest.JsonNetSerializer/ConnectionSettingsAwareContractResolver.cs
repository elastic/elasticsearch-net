/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace Nest.JsonNetSerializer
{
	public class ConnectionSettingsAwareContractResolver : DefaultContractResolver
	{
		public ConnectionSettingsAwareContractResolver(IConnectionSettingsValues connectionSettings) =>
			ConnectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));

		protected IConnectionSettingsValues ConnectionSettings { get; }

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
			if (!ConnectionSettings.PropertyMappings.TryGetValue(member, out var propertyMapping))
				propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

			var serializerMapping = ConnectionSettings.PropertyMappingProvider?.CreatePropertyMapping(member);

			var nameOverride = propertyMapping?.Name ?? serializerMapping?.Name;
			if (!string.IsNullOrWhiteSpace(nameOverride)) property.PropertyName = nameOverride;

			var overrideIgnore = propertyMapping?.Ignore ?? serializerMapping?.Ignore;
			if (overrideIgnore.HasValue)
				property.Ignored = overrideIgnore.Value;
		}

		private static void ApplyShouldSerializer(JsonProperty property)
		{
			if (property.PropertyType == typeof(QueryContainer))
				property.ShouldSerialize = o => ShouldSerializeQueryContainer(o, property);
			else if (property.PropertyType == typeof(IEnumerable<QueryContainer>))
				property.ShouldSerialize = o => ShouldSerializeQueryContainers(o, property);
		}

		private static bool ShouldSerializeQueryContainer(object o, JsonProperty prop)
		{
			if (o == null) return false;
			if (!(prop.ValueProvider.GetValue(o) is IQueryContainer q)) return false;

			return q.IsWritable;
		}

		private static bool ShouldSerializeQueryContainers(object o, JsonProperty prop)
		{
			if (o == null) return false;
			if (!(prop.ValueProvider.GetValue(o) is IEnumerable<QueryContainer> q)) return false;

			var queryContainers = q as QueryContainer[] ?? q.ToArray();
			return queryContainers.Any(qq => qq != null && ((IQueryContainer)qq).IsWritable);
		}
	}
}
