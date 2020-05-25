// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class FieldNamesFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::e4fc720e1f7f2f9a7edf48184fd4a0dd[]
			var response0 = new SearchResponse<object>();
			// end::e4fc720e1f7f2f9a7edf48184fd4a0dd[]

			response0.MatchesExample(@"PUT tweets
			{
			  ""mappings"": {
			    ""_field_names"": {
			      ""enabled"": false
			    }
			  }
			}");
		}
	}
}
