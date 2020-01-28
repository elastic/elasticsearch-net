using Elastic.Xunit.XunitPlumbing;
using Nest;
using Newtonsoft.Json.Linq;

namespace Examples.Indices
{
	public class CreateIndexPage : ExampleBase
	{
		[U]
		public void Line10()
		{
			// tag::1c23507edd7a3c18538b68223378e4ab[]
			var createIndexResponse = client.Indices.Create("twitter");
			// end::1c23507edd7a3c18538b68223378e4ab[]

			createIndexResponse.MatchesExample(@"PUT /twitter");
		}

		[U]
		public void Line82()
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

				return e;
			});
		}

		[U]
		public void Line100()
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

				return e;
			});
		}

		[U]
		public void Line124()
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

				return e;
			});
		}

		[U]
		public void Line148()
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
				return e;
			});
		}

		[U]
		public void Line195()
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
		public void Line208()
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
				return e;
			});
		}
	}
}
