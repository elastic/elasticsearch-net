// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Apis.Enrich
{
	public class DeleteEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/delete-enrich-policy.asciidoc:37")]
		public void Line37()
		{
			// tag::cdd29b01e730b3996de68a2788050021[]
			var response0 = new SearchResponse<object>();
			// end::cdd29b01e730b3996de68a2788050021[]

			response0.MatchesExample(@"DELETE /_enrich/policy/my-policy");
		}
	}
}
