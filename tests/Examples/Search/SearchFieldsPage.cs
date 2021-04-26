/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class SearchFieldsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/search-fields.asciidoc:49")]
		public void Line49()
		{
			// tag::27e19bcb00dd4a192920eb809f6299e4[]
			var response0 = new SearchResponse<object>();
			// end::27e19bcb00dd4a192920eb809f6299e4[]

			response0.MatchesExample(@"GET /_search
			{
			  ""_source"": false,
			  ""query"": {
			    ""term"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-fields.asciidoc:66")]
		public void Line66()
		{
			// tag::e12cf31e59a19ee8453909c237809a8f[]
			var response0 = new SearchResponse<object>();
			// end::e12cf31e59a19ee8453909c237809a8f[]

			response0.MatchesExample(@"GET /_search
			{
			  ""_source"": ""obj.*"",
			  ""query"": {
			    ""term"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-fields.asciidoc:83")]
		public void Line83()
		{
			// tag::536d54ca973cd7b19b797a4b25c2e404[]
			var response0 = new SearchResponse<object>();
			// end::536d54ca973cd7b19b797a4b25c2e404[]

			response0.MatchesExample(@"GET /_search
			{
			  ""_source"": [ ""obj1.*"", ""obj2.*"" ],
			  ""query"": {
			    ""term"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-fields.asciidoc:109")]
		public void Line109()
		{
			// tag::85e2c00ea9adf834952a54e69b14cbf5[]
			var response0 = new SearchResponse<object>();
			// end::85e2c00ea9adf834952a54e69b14cbf5[]

			response0.MatchesExample(@"GET /_search
			{
			  ""_source"": {
			    ""includes"": [ ""obj1.*"", ""obj2.*"" ],
			    ""excludes"": [ ""*.description"" ]
			  },
			  ""query"": {
			    ""term"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-fields.asciidoc:153")]
		public void Line153()
		{
			// tag::e443e6c04bd38e54205f6485e67105d8[]
			var response0 = new SearchResponse<object>();
			// end::e443e6c04bd38e54205f6485e67105d8[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""match_all"": {}
			  },
			  ""docvalue_fields"": [
			    ""my_ip*"",                     <1>
			    {
			      ""field"": ""my_keyword_field"" <2>
			    },
			    {
			      ""field"": ""*_date_field"",
			      ""format"": ""epoch_millis""    <3>
			    }
			  ]
			}");
		}
	}
}