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
		[Obsolete(@"Took field is an Int but the value in the response can exced the max value for Int.
					If you use this field instead of TookAsLong the value can wrap around if it is too big.")]
		int Took { get; }
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

		[JsonProperty(PropertyName = "took")]
		public long TookAsLong { get; internal set; }

		[Obsolete(@"Took field is an Int but the value in the response can exced the max value for Int.
					If you use this field instead of TookAsLong the value can wrap around if it is too big.")]
		public int Took
		{
			get
			{
				return unchecked((int)TookAsLong);
			}
			internal set
			{
				TookAsLong = value;
			}
		}

		[JsonProperty("term_vectors")]
		public IDictionary<string, TermVector> TermVectors { get; internal set; } =  new Dictionary<string, TermVector>();
	}
}
