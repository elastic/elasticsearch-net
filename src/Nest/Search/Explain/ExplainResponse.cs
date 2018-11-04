using Newtonsoft.Json;

namespace Nest
{
	public interface IExplainResponse<T> : IResponse
		where T : class
	{
		ExplanationDetail Explanation { get; }
		InstantGet<T> Get { get; }
		bool Matched { get; }
	}

	[JsonObject]
	public class ExplainResponse<T> : ResponseBase, IExplainResponse<T>
		where T : class
	{
		[JsonProperty(PropertyName = "explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[JsonProperty(PropertyName = "get")]
		public InstantGet<T> Get { get; internal set; }

		[JsonProperty(PropertyName = "matched")]
		public bool Matched { get; internal set; }
	}
}
