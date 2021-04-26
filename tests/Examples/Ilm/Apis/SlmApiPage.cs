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
using Nest;

namespace Examples.Ilm.Apis
{
	public class SlmApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line185()
		{
			// tag::aa7cf5df36b867aee5e3314ac4b4fa68[]
			var response0 = new SearchResponse<object>();
			// end::aa7cf5df36b867aee5e3314ac4b4fa68[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"", <1>
			  ""name"": ""<daily-snap-{now/d}>"", <2>
			  ""repository"": ""my_repository"", <3>
			  ""config"": { <4>
			    ""indices"": [""data-*"", ""important""], <5>
			    ""ignore_unavailable"": false,
			    ""include_global_state"": false
			  },
			  ""retention"": { <6>
			    ""expire_after"": ""30d"", <7>
			    ""min_count"": 5, <8>
			    ""max_count"": 50 <9>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line261()
		{
			// tag::b4f9fe8808cb27a210b162e7aaba261d[]
			var response0 = new SearchResponse<object>();
			// end::b4f9fe8808cb27a210b162e7aaba261d[]

			response0.MatchesExample(@"GET /_slm/policy/daily-snapshots?human");
		}

		[U(Skip = "Example not implemented")]
		public void Line312()
		{
			// tag::bc2dd9e5ed37f98016ecf53f968d2211[]
			var response0 = new SearchResponse<object>();
			// end::bc2dd9e5ed37f98016ecf53f968d2211[]

			response0.MatchesExample(@"GET /_slm/policy");
		}

		[U(Skip = "Example not implemented")]
		public void Line357()
		{
			// tag::0ab002c6618af75e1041a23c692327ad[]
			var response0 = new SearchResponse<object>();
			// end::0ab002c6618af75e1041a23c692327ad[]

			response0.MatchesExample(@"POST /_slm/policy/daily-snapshots/_execute");
		}

		[U(Skip = "Example not implemented")]
		public void Line437()
		{
			// tag::df620765f4ee59012ebb9f7a474d9e5d[]
			var response0 = new SearchResponse<object>();
			// end::df620765f4ee59012ebb9f7a474d9e5d[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"",
			  ""name"": ""<daily-snap-{now/d}>"",
			  ""repository"": ""my_repository"",
			  ""config"": {
			    ""indices"": [""data-*"", ""important""],
			    ""ignore_unavailable"": true,
			    ""include_global_state"": false
			  },
			  ""retention"": {
			    ""expire_after"": ""30d"",
			    ""min_count"": 5,
			    ""max_count"": 50
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line554()
		{
			// tag::55e8ddf643726dec51531ada0bec7143[]
			var response0 = new SearchResponse<object>();
			// end::55e8ddf643726dec51531ada0bec7143[]

			response0.MatchesExample(@"GET /_slm/stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line614()
		{
			// tag::1a1f3421717ff744ed83232729289bb0[]
			var response0 = new SearchResponse<object>();
			// end::1a1f3421717ff744ed83232729289bb0[]

			response0.MatchesExample(@"DELETE /_slm/policy/daily-snapshots");
		}

		[U(Skip = "Example not implemented")]
		public void Line649()
		{
			// tag::e71d300cd87f09a9527cf45395dd7eb1[]
			var response0 = new SearchResponse<object>();
			// end::e71d300cd87f09a9527cf45395dd7eb1[]

			response0.MatchesExample(@"POST /_slm/_execute_retention");
		}

		[U(Skip = "Example not implemented")]
		public void Line708()
		{
			// tag::41195ef13af0465cdee1ae18f6c00fde[]
			var response0 = new SearchResponse<object>();
			// end::41195ef13af0465cdee1ae18f6c00fde[]

			response0.MatchesExample(@"POST _slm/stop");
		}

		[U(Skip = "Example not implemented")]
		public void Line759()
		{
			// tag::371962cf63e65c10026177c6a1bad0b6[]
			var response0 = new SearchResponse<object>();
			// end::371962cf63e65c10026177c6a1bad0b6[]

			response0.MatchesExample(@"POST _slm/start");
		}

		[U(Skip = "Example not implemented")]
		public void Line811()
		{
			// tag::cde4104a29dfe942d55863cdd8718627[]
			var response0 = new SearchResponse<object>();
			// end::cde4104a29dfe942d55863cdd8718627[]

			response0.MatchesExample(@"GET _slm/status");
		}
	}
}
