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
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Examples.Indices
{
	public class CreateIndexPage : ExampleBase
	{
		[U]
		[Description("indices/create-index.asciidoc:10")]
		public void Line10()
		{
			// tag::1c23507edd7a3c18538b68223378e4ab[]
			var createIndexResponse = client.Indices.Create("twitter");
			// end::1c23507edd7a3c18538b68223378e4ab[]

			createIndexResponse.MatchesExample(@"PUT /twitter");
		}

		[U]
		[Description("indices/create-index.asciidoc:81")]
		public void Line81()
		{
			// tag::e5d2172b524332196cac0f031c043659[]
			var createIndexResponse = client.Indices.Create("twitter", c => c
				.Settings(s => s
					.NumberOfShards(3)
					.NumberOfReplicas(2)
				)
			);
			// end::e5d2172b524332196cac0f031c043659[]

			createIndexResponse.MatchesExample(@"PUT /twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""number_of_shards"" : 3, <1>
			            ""number_of_replicas"" : 2 <2>
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["settings"] = new JObject
					{
						{ "index.number_of_shards", 3 },
						{ "index.number_of_replicas", 2 }
					};
				});
			});
		}

		[U]
		[Description("indices/create-index.asciidoc:99")]
		public void Line99()
		{
			// tag::b9c5d7ca6ca9c6f747201f45337a4abf[]
			var createIndexResponse = client.Indices.Create("twitter", c => c
				.Settings(s => s
					.NumberOfShards(3)
					.NumberOfReplicas(2)
				)
			);
			// end::b9c5d7ca6ca9c6f747201f45337a4abf[]

			createIndexResponse.MatchesExample(@"PUT /twitter
			{
			    ""settings"" : {
			        ""number_of_shards"" : 3,
			        ""number_of_replicas"" : 2
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["settings"] = new JObject
					{
						{ "index.number_of_shards", 3 },
						{ "index.number_of_replicas", 2 },
					};
				});
			});
		}

		[U]
		[Description("indices/create-index.asciidoc:123")]
		public void Line123()
		{
			// tag::dfef545b1e2c247bafd1347e8e807ac1[]
			var createIndexResponse = client.Indices.Create("test", c => c
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("field1")
						)
					)
				)
			);
			// end::dfef545b1e2c247bafd1347e8e807ac1[]

			createIndexResponse.MatchesExample(@"PUT /test
			{
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""properties"" : {
			            ""field1"" : { ""type"" : ""text"" }
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["settings"] = new JObject
					{
						{ "index.number_of_shards", 1 },
					};
				});
			});
		}

		[U]
		[Description("indices/create-index.asciidoc:143")]
		public void Line143()
		{
			// tag::4d56b179242fed59e3d6476f817b6055[]
			var createIndexResponse = client.Indices.Create("test", c => c
				.Aliases(a => a
					.Alias("alias_1")
					.Alias("alias_2", aa => aa
						.Filter<object>(f => f
							.Term("user", "kimchy")
						)
						.Routing("kimchy")
					)
				)
			);
			// end::4d56b179242fed59e3d6476f817b6055[]

			createIndexResponse.MatchesExample(@"PUT /test
			{
			    ""aliases"" : {
			        ""alias_1"" : {},
			        ""alias_2"" : {
			            ""filter"" : {
			                ""term"" : {""user"" : ""kimchy"" }
			            },
			            ""routing"" : ""kimchy""
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aliases"]["alias_2"]["filter"]["term"]["user"].ToLongFormTermQuery(); });
			});
		}

		[U]
		[Description("indices/create-index.asciidoc:190")]
		public void Line190()
		{
			// tag::4d46dbb96125b27f46299547de9d8709[]
			var createIndexResponse = client.Indices.Create("test", c => c
				.Settings(s => s
					.Setting("index.write.wait_for_active_shards", "2")
				)
			);
			// end::4d46dbb96125b27f46299547de9d8709[]

			createIndexResponse.MatchesExample(@"PUT /test
			{
			    ""settings"": {
			        ""index.write.wait_for_active_shards"": ""2""
			    }
			}");
		}

		[U]
		[Description("indices/create-index.asciidoc:203")]
		public void Line203()
		{
			// tag::fabe14480624a99e8ee42c7338672058[]
			var createIndexResponse = client.Indices.Create("test", c => c
				.Settings(s => s
					.Setting("index.write.wait_for_active_shards", "2")
				)
			);
			// end::fabe14480624a99e8ee42c7338672058[]

			createIndexResponse.MatchesExample(@"PUT /test?wait_for_active_shards=2", e =>
			{
				e.Uri.Query = null;
				e.ApplyBodyChanges(b => { b["settings"] = new JObject { { "index.write.wait_for_active_shards", "2" } }; });
			});
		}
	}
}
