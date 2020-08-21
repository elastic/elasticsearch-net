// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class TranslogStats
	{
		/// <remarks>
		/// Valid only for Elasticsearch 6.3.0+
		/// </remarks>
		[DataMember(Name = "earliest_last_modified_age")]
		public long EarliestLastModifiedAge { get; set; }

		[DataMember(Name = "operations")]
		public long Operations { get; set; }

		[DataMember(Name = "size")]
		public string Size { get; set; }

		[DataMember(Name = "size_in_bytes")]
		public long SizeInBytes { get; set; }

		[DataMember(Name = "uncommitted_operations")]
		public int UncommittedOperations { get; set; }

		[DataMember(Name = "uncommitted_size")]
		public string UncommittedSize { get; set; }

		[DataMember(Name = "uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes { get; set; }
	}
}
