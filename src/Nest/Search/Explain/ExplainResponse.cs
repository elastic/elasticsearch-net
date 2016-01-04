using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IExplainResponse<T> : IResponse
		where T : class
	{
		bool Matched { get; }
		ExplanationDetail Explanation { get; }
		ExplainGet<T> Get { get; }

		T Source { get; }
		FieldValues Fields { get; }
	}

	[JsonObject]
	public class ExplainResponse<T> : BaseResponse, IExplainResponse<T>
		where T : class
	{
		[JsonProperty(PropertyName = "matched")]
		public bool Matched { get; internal set; }

		[JsonProperty(PropertyName = "explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[JsonProperty(PropertyName = "get")]
		public ExplainGet<T> Get { get; internal set; }

		public T Source => this.Get?.Source;

		[JsonProperty(PropertyName = "fields")]
		public FieldValues Fields { get; set; }
	}
}