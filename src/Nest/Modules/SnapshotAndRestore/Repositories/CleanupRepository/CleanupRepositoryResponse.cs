// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CleanupRepositoryResponse : ResponseBase
	{
		[DataMember(Name ="results")]
		public CleanupRepositoryResults Results { get; internal set; }
	}

	public class CleanupRepositoryResults
	{
		[DataMember(Name ="deleted_bytes")]
		public long DeletedBytes { get; internal set; }

		[DataMember(Name ="deleted_blobs")]
		public long DeletedBlobs { get; internal set; }
	}
}
