// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class PendingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::aa814309ad5f1630886ba75255b444f5[]
			var response0 = new SearchResponse<object>();
			// end::aa814309ad5f1630886ba75255b444f5[]

			response0.MatchesExample(@"GET /_cluster/pending_tasks");
		}
	}
}
