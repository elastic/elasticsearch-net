using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexMappings> Indices { get; }
		[Obsolete("Renamed to Indices, will be deleted from NEST 7.x")]
		IReadOnlyDictionary<IndexName, IndexMappings> Mappings { get; }

		void Accept(IMappingVisitor visitor);
	}

	public class IndexMappings
	{
		[JsonProperty("mappings")]
		public TypeMappings Mappings { get; internal set; }

		public TypeMapping this[TypeName type] => this.Mappings?[type];
	}

	[JsonConverter(typeof(TypeMappingsJsonConverter))]
	public class TypeMappings : ResolvableDictionaryProxy<TypeName, TypeMapping>
	{
		internal TypeMappings(IConnectionConfigurationValues connectionSettings, IReadOnlyDictionary<TypeName, TypeMapping> backingDictionary)
			: base(connectionSettings, backingDictionary) { }

		internal class TypeMappingsJsonConverter : ResolvableDictionaryJsonConverterBase<TypeMappings, TypeName, TypeMapping>
		{
			protected override TypeMappings Create(IConnectionSettingsValues s, Dictionary<TypeName, TypeMapping> d) =>
				new TypeMappings(s, d);
		}
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetMappingResponse, IndexName, IndexMappings>))]
	public class GetMappingResponse : DictionaryResponseBase<IndexName, IndexMappings>, IGetMappingResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexMappings> Indices => Self.BackingDictionary;

		[JsonIgnore]
		[Obsolete("Renamed to Indices, will be deleted from NEST 7.x")]
		public IReadOnlyDictionary<IndexName, IndexMappings> Mappings => Indices;

		[Obsolete("Recommended to use GetMappingFor, this is a leaky abstraction that returns the mapping of the first index's first type on the response")]
		public ITypeMapping Mapping => this.Indices.FirstOrDefault().Value?.Mappings?.FirstOrDefault().Value;

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}

	public static class GetMappingResponseExtensions
	{

		public static ITypeMapping GetMappingFor<T>(this IGetMappingResponse response) => response.GetMappingFor(typeof(T), typeof(T));

		public static ITypeMapping GetMappingFor(this IGetMappingResponse response, IndexName index, TypeName type)
		{
			if (index.IsNullOrEmpty() || type.IsNullOrEmpty()) return null;
			TypeMapping mapping = null;
			var hasValue = response.Indices.TryGetValue(index, out var typeMapping)
			        && typeMapping?.Mappings != null
			        && typeMapping.Mappings.TryGetValue(type, out mapping);
			return mapping;
		}

		public static ITypeMapping GetMappingFor(this IGetMappingResponse response, IndexName index)
		{
			if (index.IsNullOrEmpty()) return null;
			if (!response.Indices.TryGetValue(index, out var typeMapping)) return null;
			return typeMapping?.Mappings?.FirstOrDefault().Value;
		}

	}
}
