// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;
using static Nest.Infer;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetFieldMappingResponse, IndexName, TypeFieldMappings>))]
	public class GetFieldMappingResponse : DictionaryResponseBase<IndexName, TypeFieldMappings>
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

		public IFieldMapping MappingFor<T>(Field property) => MappingFor<T>(property, null);

		public IFieldMapping MappingFor<T>(Field property, IndexName index) =>
			GetMapping(index ?? Index<T>(), property);

		public IFieldMapping MappingFor<T, TValue>(Expression<Func<T, TValue>> objectPath, IndexName index = null)
			where T : class =>
			GetMapping(index ?? Index<T>(), Field(objectPath));

		public IFieldMapping MappingFor<T>(Expression<Func<T, object>> objectPath, IndexName index = null)
			where T : class =>
			GetMapping(index ?? Index<T>(), Field(objectPath));


		private IReadOnlyDictionary<Field, FieldMapping> MappingsFor(IndexName index)
		{
			if (!Indices.TryGetValue(index, out var indexMapping) || indexMapping.Mappings == null) return null;

			return indexMapping.Mappings;
		}
	}

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

}
