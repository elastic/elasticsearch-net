// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	public class InnerHitsMetadata
	{
		[DataMember(Name ="hits")]
		public List<IHit<ILazyDocument>> Hits { get; internal set; }

		[DataMember(Name ="max_score")]
		public double? MaxScore { get; internal set; }

		[DataMember(Name = "total")]
		public TotalHits Total { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			if (Hits == null || Hits.Count == 0)
				return Enumerable.Empty<T>();

			return Hits.Select(hit => hit.Source.As<T>()).ToList();
		}
	}
}
