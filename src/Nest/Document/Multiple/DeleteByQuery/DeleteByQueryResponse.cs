using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteByQueryResponse : IResponse
	{
		/// <summary>
		/// The delete by query details for each affected index.
		/// </summary>
		[JsonProperty("_indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, DeleteByQueryIndicesResult> Indices { get; }

		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

	}

	public class DeleteByQueryResponse : ResponseBase, IDeleteByQueryResponse
	{
		public IDictionary<string, DeleteByQueryIndicesResult> Indices { get; set; }
		public long Took { get; internal set; }
		public bool TimedOut { get; internal set; }
	}

	[JsonObject]
	public class DeleteByQueryIndicesResult
	{
		[JsonProperty("found")]
		public long Found { get; internal set; }

		[JsonProperty("deleted")]
		public long Deleted { get; internal set; }

		[JsonProperty("missing")]
		public long Missing { get; internal set; }

		[JsonProperty("failed")]
		public long Failed { get; internal set; }

	}
}
