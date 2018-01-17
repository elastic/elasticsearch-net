using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateResponse : IResponse
	{
		string Id { get; }
		string Index { get; }
		string Type { get; }
		long Version { get; }
		bool Created { get; }
		Result Result { get; }
	}

	[JsonObject]
	public class CreateResponse : ResponseBase, ICreateResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("created")]
		[Obsolete("Removed in 6.0")]
		public bool Created { get; internal set; }

		[JsonProperty("result")]
		public Result Result { get; internal set; }
	}
}
