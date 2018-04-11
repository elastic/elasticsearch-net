using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Segment
	{
		[JsonProperty("generation")]
		public int Generation { get; internal set; }

		[JsonProperty("num_docs")]
		public long TotalDocuments { get; internal set; }

		[JsonProperty("deleted_docs")]
		public long DeletedDocuments { get; internal set; }

		[JsonProperty("size")]
		[Obsolete("Unused. Will be removed in the next major release")]
		public string Size { get; internal set; }

		[JsonProperty("size_in_bytes")]
		public double SizeInBytes { get; internal set; }

		[JsonProperty("memory_in_bytes")]
		public double MemoryInBytes { get; internal set; }

		[JsonProperty("committed")]
		public bool Committed { get; internal set; }

		[JsonProperty("search")]
		public bool Search { get; internal set; }

		[JsonProperty("version")]
		public string Version { get; internal set; }

		[JsonProperty("compound")]
		public bool Compound { get; internal set; }

		[JsonProperty("attributes")]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } =
			EmptyReadOnly<string, string>.Dictionary;
	}
}
