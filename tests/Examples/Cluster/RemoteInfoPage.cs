// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class RemoteInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::cc0cca5556ec6224c7134c233734beed[]
			var response0 = new SearchResponse<object>();
			// end::cc0cca5556ec6224c7134c233734beed[]

			response0.MatchesExample(@"GET /_remote/info");
		}
	}
}
