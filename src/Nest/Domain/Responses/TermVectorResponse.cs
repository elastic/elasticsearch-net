using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermVectorResponse : IResponse
	{
		bool Found { get; }
		IDictionary<string, TermVector> TermVectors { get; }
	}

	public interface IMultiTermVectorHit : ITermVectorResponse
	{
		string Index { get; }
		string Type { get; }
		string Id { get; }
		long Version { get; }
		long Took { get; }
	}

	[JsonObject]
	public class TermVectorResponse : BaseResponse, ITermVectorResponse
	{
		public TermVectorResponse()
		{
			IsValid = true;
			TermVectors = new Dictionary<string, TermVector>();
		}

		[JsonProperty("found")]
		public bool Found { get; internal set; }


		[JsonProperty("term_vectors")]
		public IDictionary<string, TermVector> TermVectors { get; internal set; }
	}

	public class MultiTermVectorHit : TermVectorResponse, IMultiTermVectorHit
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("took")]
		public long Took { get; internal set; }
		
	}
}
