// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardSequenceNumber
	{
		[DataMember(Name ="global_checkpoint")]
		public long GlobalCheckpoint { get; internal set; }

		[DataMember(Name ="local_checkpoint")]
		public long LocalCheckpoint { get; internal set; }

		[DataMember(Name ="max_seq_no")]
		public long MaximumSequenceNumber { get; internal set; }
	}
}
