using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using static Nest.Infer;

namespace Nest
{
	public class TypeFieldMappings
	{
		[DataMember(Name = "mappings")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<Field, FieldMapping>))]
		public IReadOnlyDictionary<Field, FieldMapping> Mappings { get; internal set; } = EmptyReadOnly<Field, FieldMapping>.Dictionary;
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

		IFieldMapping GetMapping(IndexName index, Field property);

		IFieldMapping MappingFor<T>(Field property, IndexName index = null);

		IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null) where T : class;
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetFieldMappingResponse, IndexName, TypeFieldMappings>))]
	public class GetFieldMappingResponse : DictionaryResponseBase<IndexName, TypeFieldMappings>, IGetFieldMappingResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, TypeFieldMappings> Indices => Self.BackingDictionary;

		//if you call get mapping on an existing type and index but no fields match you still get back a 200.
		public override bool IsValid => base.IsValid && Indices.HasAny();

		public IFieldMapping GetMapping(IndexName index, Field property)
		{
			if (property == null) return null;

			var mappings = MappingsFor(index);
			if (mappings == null) return null;

			if (!mappings.TryGetValue(property, out var fieldMapping) || fieldMapping.Mapping == null) return null;

			return fieldMapping.Mapping.TryGetValue(property, out var field) ? field : null;
		}

		public IFieldMapping MappingFor<T>(Field property, IndexName index) =>
			GetMapping(index ?? Index<T>(), property);

		public IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null)
			where T : class =>
			GetMapping(index ?? Index<T>(), Field(objectPath));

		private IReadOnlyDictionary<Field, FieldMapping> MappingsFor(IndexName index)
		{
			if (!Indices.TryGetValue(index, out var indexMapping) || indexMapping.Mappings == null) return null;

			return indexMapping.Mappings;
		}
	}
}
