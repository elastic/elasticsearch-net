// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetOverallBucketsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="overall_buckets")]
		public IReadOnlyCollection<OverallBucket> OverallBuckets { get; internal set; } = EmptyReadOnly<OverallBucket>.Collection;
	}
}
