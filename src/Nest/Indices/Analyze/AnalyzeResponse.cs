using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalyzeResponse : IResponse
	{
		IEnumerable<AnalyzeToken> Tokens { get; }
	}

	[JsonObject]
	public class AnalyzeResponse : ResponseBase, IAnalyzeResponse
	{
		[JsonProperty(PropertyName = "tokens")]
		public IEnumerable<AnalyzeToken> Tokens { get; internal set; }
	}
}