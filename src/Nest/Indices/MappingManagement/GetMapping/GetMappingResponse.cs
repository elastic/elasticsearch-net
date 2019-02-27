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

		void Accept(IMappingVisitor visitor);
	}

	public class IndexMappings
	{
		public TypeMapping this[TypeName type] => Mappings?[type];

		[JsonProperty("mappings")]
		public TypeMappings Mappings { get; internal set; }
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

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}

	public static class GetMappingResponseExtensions
	{
		public static ITypeMapping GetMappingFor<T>(this IGetMappingResponse response) => response.GetMappingFor(typeof(T));

		public static ITypeMapping GetMappingFor(this IGetMappingResponse response, IndexName index)
		{
			if (index.IsNullOrEmpty()) return null;

			TypeMapping mapping = null;
			var hasValue = response.Indices.TryGetValue(index, out var typeMapping)
				&& typeMapping?.Mappings != null
				&& typeMapping.Mappings.TryGetValue(null, out mapping);
			return mapping;
		}
	}
}
