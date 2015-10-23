using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermVectorsResponse : IResponse
	{
		bool Found { get; }
		IDictionary<string, TermVector> TermVectors { get; }
	}

	[JsonObject]
	public class TermVectorsResponse : BaseResponse, ITermVectorsResponse
	{
		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty("term_vectors")]
		public IDictionary<string, TermVector> TermVectors { get; internal set; } =  new Dictionary<string, TermVector>();
	}
}
