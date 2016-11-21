using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorsResponse : IResponse
	{
		IReadOnlyCollection<TermVectorsResponse> Documents { get; }
	}

	[JsonObject]
	public class MultiTermVectorsResponse : ResponseBase, IMultiTermVectorsResponse
	{
		[JsonProperty("docs")]
		public IReadOnlyCollection<TermVectorsResponse> Documents { get; internal set; } = EmptyReadOnly<TermVectorsResponse>.Collection;
	}
}
