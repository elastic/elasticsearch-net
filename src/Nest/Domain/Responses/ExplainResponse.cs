using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IExplainResponse : IResponse
	{
		bool Matched { get; }
		ExplanationDetail Explanation { get; }
	}

	[JsonObject]
	public class ExplainResponse : BaseResponse, IExplainResponse
	{
		public ExplainResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "matched")]
		public bool Matched { get; internal set; }

		[JsonProperty(PropertyName = "explanation")]
		public ExplanationDetail Explanation { get; internal set;}
	}
}