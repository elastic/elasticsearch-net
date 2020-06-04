// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class AnomalyDetectorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/anomaly-detectors.asciidoc:270")]
		public void Line270()
		{
			// tag::bb03aeaba69c110f86505fa0e4e5035d[]
			var response0 = new SearchResponse<object>();
			// end::bb03aeaba69c110f86505fa0e4e5035d[]

			response0.MatchesExample(@"GET _cat/ml/anomaly_detectors?h=id,s,dpr,mb&v");
		}
	}
}
