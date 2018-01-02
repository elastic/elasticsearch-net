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
		public IReadOnlyDictionary<IndexName, IndexMappings> Mappings => Indices;

		[Obsolete("Use GetMappingFor explicitly instead this is a leaky abstraction that returns the mapping of the first index's first type on the response")]
		public TypeMapping Mapping => this.Indices.FirstOrDefault().Value?.Mappings?.FirstOrDefault().Value;

		public TypeMapping GetMappingFor<T>() => this.Indices[typeof(T)]?[typeof(T)];
		public TypeMapping GetMappingFor(string index, string type) => this.Indices[index]?[type];
		public TypeMapping GetMappingFor(string index) => this.Indices[index]?.Mappings?.FirstOrDefault().Value;

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
