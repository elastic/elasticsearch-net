// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices.Apis
{
	public class UnfreezePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/apis/unfreeze.asciidoc:42")]
		public void Line42()
		{
			// tag::ffea06f77c9df5720412aa06be964118[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ffea06f77c9df5720412aa06be964118[]

			response0.MatchesExample(@"POST /my_index/_freeze");

			response1.MatchesExample(@"POST /my_index/_unfreeze");
		}
	}
}
