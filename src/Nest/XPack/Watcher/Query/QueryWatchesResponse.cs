// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class QueryWatchesResponse : ResponseBase
	{
		[DataMember(Name = "count")]
		public int Count { get; internal set; }

		[DataMember(Name = "watches")]
		public IReadOnlyCollection<WatchQueryResult> Watches { get; internal set; }
	}

	[DataContract]
	public class WatchQueryResult
	{
		[DataMember(Name = "_id")]
		public string Id { get; set; }

		[DataMember(Name = "_primary_term")]
		public int PrimaryTerm { get; set; }

		[DataMember(Name = "_seq_no")]
		public int SequenceNumber { get; set; }

		[DataMember(Name = "status")]
		public WatchStatus Status { get; set; }

		[DataMember(Name = "watch")]
		public IWatch Watch { get; set; }
	}
}
