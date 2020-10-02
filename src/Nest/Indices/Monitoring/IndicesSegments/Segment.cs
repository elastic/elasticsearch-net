// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class Segment
	{
		[DataMember(Name ="attributes")]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } =
			EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name ="committed")]
		public bool Committed { get; internal set; }

		[DataMember(Name ="compound")]
		public bool Compound { get; internal set; }

		[DataMember(Name ="deleted_docs")]
		public long DeletedDocuments { get; internal set; }

		[DataMember(Name ="generation")]
		public int Generation { get; internal set; }

		[DataMember(Name ="memory_in_bytes")]
		public double MemoryInBytes { get; internal set; }

		[DataMember(Name ="search")]
		public bool Search { get; internal set; }

		[DataMember(Name ="size_in_bytes")]
		public double SizeInBytes { get; internal set; }

		[DataMember(Name ="num_docs")]
		public long TotalDocuments { get; internal set; }

		[DataMember(Name ="version")]
		public string Version { get; internal set; }
	}
}
