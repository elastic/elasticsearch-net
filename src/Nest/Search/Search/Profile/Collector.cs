// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class Collector
	{
		[DataMember(Name ="children")]
		public IReadOnlyCollection<Collector> Children { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="reason")]
		public string Reason { get; internal set; }

		[DataMember(Name ="time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }
	}
}
