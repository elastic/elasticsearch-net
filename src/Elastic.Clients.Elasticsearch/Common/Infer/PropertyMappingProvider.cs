// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	/// <inheritdoc />
	public class PropertyMappingProvider : IPropertyMappingProvider
	{
		protected readonly ConcurrentDictionary<string, PropertyMapping> Properties = new();

		/// <inheritdoc />
		public virtual PropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out var mapping))
				return mapping;

			mapping = PropertyMappingFromAttributes(memberInfo);
			Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		private static PropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
		{
			var jsonPropertyName = memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>(true);

			if (jsonPropertyName == null)
				return null;

			return new PropertyMapping
			{
				Name = jsonPropertyName?.Name
			};
		}
	}
}
