// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class PutWatchResponse : ResponseBase
	{
		[DataMember(Name = "created")]
		public bool Created { get; internal set; }

		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		[DataMember(Name = "_version")]
		public int Version { get; internal set; }

		[DataMember(Name = "_seq_no")]
		public long SequenceNumber { get; internal set; }

		[DataMember(Name = "_primary_term")]
		public long PrimaryTerm { get; internal set; }
	}
}
