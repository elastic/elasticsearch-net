using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IGetAliasResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexAliases> Indices { get; }
	}

	public class IndexAliases
	{
		[DataMember(Name ="aliases")]
		public IReadOnlyDictionary<string, AliasDefinition> Aliases { get; internal set; } = EmptyReadOnly<string, AliasDefinition>.Dictionary;
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetAliasResponse, IndexName, IndexAliases>))]
	public class GetAliasResponse : DictionaryResponseBase<IndexName, IndexAliases>, IGetAliasResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexAliases> Indices => Self.BackingDictionary;

		public override bool IsValid => Indices.Count > 0;
	}
}
