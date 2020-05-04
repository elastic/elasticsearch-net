// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardTransactionLog
	{
		[DataMember(Name ="operations")]
		public long Operations { get; internal set; }

		[DataMember(Name ="size_in_bytes")]
		public long SizeInBytes { get; internal set; }

		[DataMember(Name ="uncommitted_operations")]
		public long UncommittedOperations { get; internal set; }

		[DataMember(Name ="uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes { get; internal set; }
	}
}
