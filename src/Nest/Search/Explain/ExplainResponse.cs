using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IExplainResponse<TDocument> : IResponse
		where TDocument : class
	{
		bool Matched { get; }
		ExplanationDetail Explanation { get; }
		InstantGet<TDocument> Get { get; }
	}

	[JsonObject]
	public class ExplainResponse<TDocument> : ResponseBase, IExplainResponse<TDocument>
		where TDocument : class
	{
		[JsonProperty("matched")]
		public bool Matched { get; internal set; }

		[JsonProperty("explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[JsonProperty("get")]
		public InstantGet<TDocument> Get { get; internal set; }
	}
}
