using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;
using static Nest.Infer;

namespace Nest
{
	internal class FieldMappingPropertiesFormatter
		: ResolvableDictionaryFormatterBase<FieldMappingProperties, Field, FieldMapping>
	{
		protected override FieldMappingProperties Create(IConnectionSettingsValues s, Dictionary<Field, FieldMapping> d) =>
			new FieldMappingProperties(s, d);
	}

	[JsonFormatter(typeof(FieldMappingPropertiesFormatter))]
	public class FieldMappingProperties : ResolvableDictionaryProxy<Field, FieldMapping>
	{
		internal FieldMappingProperties(IConnectionConfigurationValues connectionSettings, IReadOnlyDictionary<Field, FieldMapping> backingDictionary)
			: base(connectionSettings, backingDictionary) { }
	}

	public class TypeFieldMappings
	{
		[DataMember(Name = "mappings")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<TypeName, FieldMappingProperties>))]
		public IReadOnlyDictionary<TypeName, FieldMappingProperties> Mappings { get; internal set; } =
			EmptyReadOnly<TypeName, FieldMappingProperties>.Dictionary;
	}

	public class FieldMapping
	{
		[DataMember(Name = "full_name")]
		public string FullName { get; internal set; }

		[DataMember(Name = "mapping")]
		[JsonFormatter(typeof(FieldMappingFormatter))]
		public IReadOnlyDictionary<Field, IFieldMapping> Mapping { get; internal set; } = EmptyReadOnly<Field, IFieldMapping>.Dictionary;
	}

	public interface IGetFieldMappingResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, TypeFieldMappings> Indices { get; }

		IFieldMapping GetMapping(IndexName index, TypeName type, Field property);

		IFieldMapping MappingFor<T>(Field property, IndexName index = null, TypeName type = null);

		IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null, TypeName type = null) where T : class;
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetFieldMappingResponse, IndexName, TypeFieldMappings>))]
	public class GetFieldMappingResponse : DictionaryResponseBase<IndexName, TypeFieldMappings>, IGetFieldMappingResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, TypeFieldMappings> Indices => Self.BackingDictionary;

		//if you call get mapping on an existing type and index but no fields match you still get back a 200.
		public override bool IsValid => base.IsValid && Indices.HasAny();

		public IFieldMapping GetMapping(IndexName index, TypeName type, Field property)
		{
			if (property == null) return null;

			var mappings = MappingsFor(index, type);
			if (mappings == null) return null;

			if (!mappings.TryGetValue(property, out var fieldMapping) || fieldMapping.Mapping == null) return null;

			return fieldMapping.Mapping.TryGetValue(property, out var field) ? field : null;
		}

		public IFieldMapping MappingFor<T>(Field property, IndexName index, TypeName type) =>
			GetMapping(index ?? Index<T>(), type ?? Type<T>(), property);

		public IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null, TypeName type = null)
			where T : class =>
			GetMapping(index ?? Index<T>(), type ?? Type<T>(), Field(objectPath));

		private FieldMappingProperties MappingsFor(IndexName index, TypeName type)
		{
			if (!Indices.TryGetValue(index, out var indexMapping) || indexMapping.Mappings == null) return null;

			return !indexMapping.Mappings.TryGetValue(type, out var typeFieldMapping) ? null : typeFieldMapping;
		}
	}
}
