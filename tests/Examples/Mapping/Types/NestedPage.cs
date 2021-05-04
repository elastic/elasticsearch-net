// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Examples.Models;

namespace Examples.Mapping.Types
{
	public class NestedPage : ExampleBase
	{
		[U]
		[Description("mapping/types/nested.asciidoc:22")]
		public void Line22()
		{
			// tag::8baccd8688a6bad1749b8935f9601ea4[]
			var indexResponse = client.Index(new GroupDoc
			{
				Group = "fans",
				User = new List<User>
					{
						new User { First = "John", Last = "Smith" },
						new User { First = "Alice", Last = "White" }
					}
			}, i => i
			.Index("my_index")
			.Id(1)
			);
			// end::8baccd8688a6bad1749b8935f9601ea4[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""group"" : ""fans"",
			  ""user"" : [ \<1>
			    {
			      ""first"" : ""John"",
			      ""last"" :  ""Smith""
			    },
			    {
			      ""first"" : ""Alice"",
			      ""last"" :  ""White""
			    }
			  ]
			}");
		}

		[U]
		[Description("mapping/types/nested.asciidoc:58")]
		public void Line58()
		{
			// tag::b214942b938e47f2c486e523546cb574[]
			var searchResponse = client.Search<GroupDoc>(s => s
				.Index("my_index")
				.Query(q => q
					.Match(m => m
						.Field(f => f.User[0].First) // <1> An expression to build a path to the field `user.first` from the `GroupDoc` type.
						.Query("Alice")
					) && q
					.Match(m => m
						.Field(f => f.User[0].Last) // <2> An expression to build a path to the field `user.last` from the `GroupDoc` type.
						.Query("Smith")
					)
				)
			);
			// end::b214942b938e47f2c486e523546cb574[]

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        { ""match"": { ""user.first"": ""Alice"" }},
			        { ""match"": { ""user.last"":  ""Smith"" }}
			      ]
			    }
			  }
			}", (example, body) =>
			{
				body["query"]["bool"]["must"][0]["match"]["user.first"].ToLongFormQuery();
				body["query"]["bool"]["must"][1]["match"]["user.last"].ToLongFormQuery();
			});
		}

		[U]
		[Description("mapping/types/nested.asciidoc:85")]
		public void Line85()
		{
			// tag::b919f88e6f47a40d5793479440a90ba6[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map<GroupDoc>(m => m
					.Properties(p => p
						.Nested<User>(n => n
							.Name(nn => nn.User)
						)
					)
				)
			);

			var indexResponse = client.Index(new GroupDoc
			{
				Group = "fans",
				User = new List<User>
					{
						new User { First = "John", Last = "Smith" },
						new User { First = "Alice", Last = "White" }
					}
			}, i => i
				.Index("my_index")
				.Id(1)
			);

			var searchResponse = client.Search<GroupDoc>(s => s
				.Index("my_index")
				.Query(q => q
					.Nested(n => n
						.Path(p => p.User)
						.Query(nq => nq
							.Match(m => m
								.Field(f => f.User[0].First)
								.Query("Alice")
							) && nq
							.Match(m => m
								.Field(f => f.User[0].Last)
								.Query("Smith")
							)
						)
					)
				)
			);

			var searchResponse2 = client.Search<GroupDoc>(s => s
				.Index("my_index")
				.Query(q => q
					.Nested(n => n
						.Path(p => p.User)
						.Query(nq => nq
							.Match(m => m
								.Field(f => f.User[0].First)
								.Query("Alice")
							) && nq
							.Match(m => m
								.Field(f => f.User[0].Last)
								.Query("White")
							)
						)
						.InnerHits(i => i
							.Highlight(h => h
								.Fields(hf => hf
									.Field(f => f.User[0].First)
								)
							)
						)
					)
				)
			);
			// end::b919f88e6f47a40d5793479440a90ba6[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user"": {
			        ""type"": ""nested"" \<1>
			      }
			    }
			  }
			}");

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""group"" : ""fans"",
			  ""user"" : [
			    {
			      ""first"" : ""John"",
			      ""last"" :  ""Smith""
			    },
			    {
			      ""first"" : ""Alice"",
			      ""last"" :  ""White""
			    }
			  ]
			}");

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""user"",
			      ""query"": {
			        ""bool"": {
			          ""must"": [
			            { ""match"": { ""user.first"": ""Alice"" }},
			            { ""match"": { ""user.last"":  ""Smith"" }} \<2>
			          ]
			        }
			      }
			    }
			  }
			}", (example, body) =>
			{
				body["query"]["nested"]["query"]["bool"]["must"][0]["match"]["user.first"].ToLongFormQuery();
				body["query"]["nested"]["query"]["bool"]["must"][1]["match"]["user.last"].ToLongFormQuery();
			});

			searchResponse2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""user"",
			      ""query"": {
			        ""bool"": {
			          ""must"": [
			            { ""match"": { ""user.first"": ""Alice"" }},
			            { ""match"": { ""user.last"":  ""White"" }} \<3>
			          ]
			        }
			      },
			      ""inner_hits"": { \<4>
			        ""highlight"": {
			          ""fields"": {
			            ""user.first"": {}
			          }
			        }
			      }
			    }
			  }
			}", (example, body) =>
			{
				body["query"]["nested"]["query"]["bool"]["must"][0]["match"]["user.first"].ToLongFormQuery();
				body["query"]["nested"]["query"]["bool"]["must"][1]["match"]["user.last"].ToLongFormQuery();
			});
		}
	}
}
