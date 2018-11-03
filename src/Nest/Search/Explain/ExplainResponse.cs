using Newtonsoft.Json;

namespace Nest
{
	public interface IExplainResponse<TDocument> : IResponse
		where TDocument : class
	{
		ExplanationDetail Explanation { get; }
		InstantGet<TDocument> Get { get; }
		bool Matched { get; }
	}

	[JsonObject]
	public class ExplainResponse<TDocument> : ResponseBase, IExplainResponse<TDocument>
		where TDocument : class
	{
		[JsonProperty("explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[JsonProperty("get")]
		public InstantGet<TDocument> Get { get; internal set; }

		[JsonProperty("matched")]
		public bool Matched { get; internal set; }
	}
}
