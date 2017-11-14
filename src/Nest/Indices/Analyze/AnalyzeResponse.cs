using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalyzeResponse : IResponse
	{
		IReadOnlyCollection<AnalyzeToken> Tokens { get; }
	}

	[JsonObject]
	public class AnalyzeResponse : ResponseBase, IAnalyzeResponse
	{
		[JsonProperty("tokens")]
		public IReadOnlyCollection<AnalyzeToken> Tokens { get; internal set; } = EmptyReadOnly<AnalyzeToken>.Collection;
	}
}
