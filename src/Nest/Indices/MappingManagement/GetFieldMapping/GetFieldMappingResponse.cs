using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using static Nest.Infer;

namespace Nest
{
	internal class FieldMappingPropertiesJsonConverter
		: ResolvableDictionaryJsonConverterBase<FieldMappingProperties, Field, FieldMapping>
	{
		protected override FieldMappingProperties Create(IConnectionSettingsValues s, Dictionary<Field, FieldMapping> d) =>
			new FieldMappingProperties(s, d);
	}
	[JsonConverter(typeof(FieldMappingPropertiesJsonConverter))]
	public class FieldMappingProperties : ResolvableDictionaryProxy<Field, FieldMapping> {
		internal FieldMappingProperties(IConnectionConfigurationValues connectionSettings, IReadOnlyDictionary<Field, FieldMapping> backingDictionary)
			: base(connectionSettings, backingDictionary) { }
	}

	public class TypeFieldMappings
	{
		[JsonProperty("mappings")]
		[JsonConverter(typeof(ResolvableDictionaryJsonConverter<TypeName, FieldMappingProperties>))]
		public IReadOnlyDictionary<TypeName, FieldMappingProperties> Mappings { get; internal set; } = EmptyReadOnly<TypeName, FieldMappingProperties>.Dictionary;
	}

	public class FieldMapping
	{
		[JsonProperty("full_name")]
		public string FullName { get; internal set; }

		[JsonProperty("mapping")]
		[JsonConverter(typeof(FieldMappingJsonConverter))]
		public IReadOnlyDictionary<Field, IFieldMapping> Mapping { get; internal set; } = EmptyReadOnly<Field, IFieldMapping>.Dictionary;
	}

	public interface IGetFieldMappingResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, TypeFieldMappings> Indices { get; }

		IFieldMapping GetMapping(IndexName index, TypeName type, Field property);

		IFieldMapping MappingFor<T>(Field property, IndexName index = null, TypeName type = null);

		IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null, TypeName type = null) where T : class;
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetFieldMappingResponse, IndexName, TypeFieldMappings>))]
	public class GetFieldMappingResponse : DictionaryResponseBase<IndexName, TypeFieldMappings>, IGetFieldMappingResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, TypeFieldMappings> Indices => Self.BackingDictionary;

		//if you call get mapping on an existing type and index but no fields match you still get back a 200.
		public override bool IsValid => base.IsValid && this.Indices.HasAny();

		private FieldMappingProperties MappingsFor(IndexName index, TypeName type)
		{
			if (!this.Indices.TryGetValue(index, out var indexMapping) || indexMapping.Mappings == null) return null;
			return !indexMapping.Mappings.TryGetValue(type, out var typeFieldMapping) ? null : typeFieldMapping;
		}

		public IFieldMapping GetMapping(IndexName index, TypeName type, Field property)
		{
			if (property == null) return null;

			var mappings = this.MappingsFor(index, type);
			if (mappings == null) return null;

			if (!mappings.TryGetValue(property, out var fieldMapping) || fieldMapping.Mapping == null) return null;
			return fieldMapping.Mapping.TryGetValue(property, out var field) ? field : null;
		}

		public IFieldMapping MappingFor<T>(Field property, IndexName index, TypeName type) =>
			this.GetMapping(index ?? Index<T>(), type ?? Type<T>(), property);

		public IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null, TypeName type = null)
			where T : class =>
			this.GetMapping(index ?? Index<T>(), type ?? Type<T>(), Field(objectPath));
	}
}
