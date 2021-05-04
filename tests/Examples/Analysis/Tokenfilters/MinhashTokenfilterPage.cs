// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class MinhashTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/minhash-tokenfilter.asciidoc:134")]
		public void Line134()
		{
			// tag::7fc0039434c1833d7efd65d85870abfb[]
			var response0 = new SearchResponse<object>();
			// end::7fc0039434c1833d7efd65d85870abfb[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""my_shingle_filter"": {      <1>
			          ""type"": ""shingle"",
			          ""min_shingle_size"": 5,
			          ""max_shingle_size"": 5,
			          ""output_unigrams"": false
			        },
			        ""my_minhash_filter"": {
			          ""type"": ""min_hash"",
			          ""hash_count"": 1,          <2>
			          ""bucket_count"": 512,      <3>
			          ""hash_set_size"": 1,       <4>
			          ""with_rotation"": true     <5>
			        }
			      },
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""my_shingle_filter"",
			            ""my_minhash_filter""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""fingerprint"": {
			        ""type"": ""text"",
			        ""analyzer"": ""my_analyzer""
			      }
			    }
			  }
			}");
		}
	}
}