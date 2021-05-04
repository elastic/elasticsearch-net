// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
