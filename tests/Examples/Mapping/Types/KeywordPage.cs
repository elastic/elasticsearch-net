// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class KeywordPage : ExampleBase
	{
		[U]
		[Description("mapping/types/keyword.asciidoc:20")]
		public void Line20()
		{
			// tag::46c4b0dfb674825f9579203d41e7f404[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Keyword(k => k
							.Name("tags")
						)
					)
				)
			);
			// end::46c4b0dfb674825f9579203d41e7f404[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""tags"": {
			        ""type"":  ""keyword""
			      }
			    }
			  }
			}");
		}
	}
}
