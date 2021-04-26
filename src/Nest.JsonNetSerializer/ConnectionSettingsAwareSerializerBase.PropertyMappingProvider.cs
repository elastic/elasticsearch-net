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

using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase : IPropertyMappingProvider
	{
		protected readonly ConcurrentDictionary<string, IPropertyMapping> Properties = new ConcurrentDictionary<string, IPropertyMapping>();

		public IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out var mapping)) return mapping;
			mapping = FromAttributes(memberInfo);

			Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		private static IPropertyMapping FromAttributes(MemberInfo memberInfo)
		{
			var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>(true);
			var dataMemberProperty = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);
			var propertyName = memberInfo.GetCustomAttribute<PropertyNameAttribute>(true);
			var ignore = memberInfo.GetCustomAttribute<IgnoreAttribute>(true);
			var jsonIgnore = memberInfo.GetCustomAttribute<JsonIgnoreAttribute>(true);
			if (jsonProperty == null && ignore == null && propertyName == null && dataMemberProperty == null && jsonIgnore == null) return null;

			return new PropertyMapping
				{ Name = propertyName?.Name ?? jsonProperty?.PropertyName ?? dataMemberProperty?.Name, Ignore = ignore != null || jsonIgnore != null };
		}
	}
}
