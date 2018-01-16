using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest.JsonNetSerializer
{
	public class ConnectionSettingsAwareContractResolver : DefaultContractResolver
	{
		protected IConnectionSettingsValues ConnectionSettings { get; }

		public ConnectionSettingsAwareContractResolver(IConnectionSettingsValues connectionSettings)
		{
			ConnectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
		}

		protected override string ResolvePropertyName(string fieldName) =>
			this.ConnectionSettings.DefaultFieldNameInferrer != null
				? this.ConnectionSettings.DefaultFieldNameInferrer(fieldName)
				: base.ResolvePropertyName(fieldName);

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
			if (!this.ConnectionSettings.PropertyMappings.TryGetValue(member, out var propertyMapping))
				propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

			var serializerMapping = this.ConnectionSettings.PropertyMappingProvider?.CreatePropertyMapping(member);

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
			return queryContainers.Any(qq=>qq != null && ((IQueryContainer)qq).IsWritable);
		}

	}
}
