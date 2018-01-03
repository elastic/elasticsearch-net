using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAliasResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexAliases> Indices { get; }
	}

	public class IndexAliases
	{
		[JsonProperty("aliases")]
		public IReadOnlyDictionary<string, AliasDefinition> Aliases { get; internal set; } = EmptyReadOnly<string, AliasDefinition>.Dictionary;
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetAliasResponse, IndexName, IndexAliases>))]
	public class GetAliasResponse : DictionaryResponseBase<IndexName, IndexAliases>, IGetAliasResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexAliases> Indices => Self.BackingDictionary;

		public override bool IsValid => this.Indices.Count > 0;
	}
}
