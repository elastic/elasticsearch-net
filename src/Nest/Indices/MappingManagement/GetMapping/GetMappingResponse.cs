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

	internal class TypeMappingsJsonConverter : ResolvableDictionaryJsonConverterBase<TypeMappings, TypeName, TypeMapping>
	{
		protected override TypeMappings Create(IConnectionSettingsValues s, Dictionary<TypeName, TypeMapping> d) =>
			new TypeMappings(s, d);
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
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetMappingResponse, IndexName, IndexMappings>))]
	public class GetMappingResponse : DictionaryResponseBase<IndexName, IndexMappings>, IGetMappingResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexMappings> Indices => Self.BackingDictionary;
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexMappings> Mappings => Indices;

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
