using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	public interface ITermVectorsResponse : IResponse
	{
		string Index { get; }
		string Type { get; }
		string Id { get; }
		long Version { get; }
		bool Found { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		int Took { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		long TookAsLong { get; }

		IDictionary<string, TermVector> TermVectors { get; }
	}

	[JsonObject]
	public class TermVectorsResponse : ResponseBase, ITermVectorsResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("found")]
		public bool Found { get; internal set; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[JsonProperty("took")]
		public long TookAsLong { get; internal set; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		[JsonIgnore]
		public int Took => TookAsLong > int.MaxValue ? int.MaxValue : (int)TookAsLong;

		[JsonProperty("term_vectors")]
		public IDictionary<string, TermVector> TermVectors { get; internal set; } =  new Dictionary<string, TermVector>();
	}
}
