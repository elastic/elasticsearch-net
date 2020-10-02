// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class SearchProfile
	{
		[DataMember(Name ="collector")]
		public IReadOnlyCollection<Collector> Collector { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;

		[DataMember(Name ="query")]
		public IReadOnlyCollection<QueryProfile> Query { get; internal set; } =
			EmptyReadOnly<QueryProfile>.Collection;

		[DataMember(Name ="rewrite_time")]
		public long RewriteTime { get; internal set; }
	}
}
