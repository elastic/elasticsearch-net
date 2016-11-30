using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorsResponse : IResponse
	{
		IReadOnlyCollection<ITermVectors> Documents { get; }
	}

	[JsonObject]
	public class MultiTermVectorsResponse : ResponseBase, IMultiTermVectorsResponse
	{
		[JsonProperty("docs")]
		[JsonConverter(typeof(ReadOnlyCollectionJsonConverter<TermVectorsResult, ITermVectors>))]
		public IReadOnlyCollection<ITermVectors> Documents { get; internal set; } = EmptyReadOnly<ITermVectors>.Collection;
	}
}
