using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public interface IAnalyzeResponse : IResponse
    {
        IEnumerable<AnalyzeToken> Tokens { get; }
    }

    [JsonObject]
	public class AnalyzeResponse : BaseResponse, IAnalyzeResponse
    {
		public AnalyzeResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "tokens")]
		public IEnumerable<AnalyzeToken> Tokens { get; internal set; }
	}
}