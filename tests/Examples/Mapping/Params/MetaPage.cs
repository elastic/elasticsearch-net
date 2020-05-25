// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class MetaPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/meta.asciidoc:9")]
		public void Line9()
		{
			// tag::bb49cbbeef6afe2dae0db46c4a10df3b[]
			var response0 = new SearchResponse<object>();
			// end::bb49cbbeef6afe2dae0db46c4a10df3b[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""latency"": {
			        ""type"": ""long"",
			        ""meta"": {
			          ""unit"": ""ms""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
