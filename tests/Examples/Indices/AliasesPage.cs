// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class AliasesPage : ExampleBase
	{
		[U]
		[Description("indices/aliases.asciidoc:12")]
		public void Line12()
		{
			// tag::d3016e4e8025362ad9a05ee86bb2061f[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Index("twitter")
					.Alias("alias1")
				)
			);
			// end::d3016e4e8025362ad9a05ee86bb2061f[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""twitter"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:170")]
		public void Line170()
		{
			// tag::b4392116f2cc57ce8064ccbad30318d5[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Index("test1")
					.Alias("alias1")
				)
			);
			// end::b4392116f2cc57ce8064ccbad30318d5[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:186")]
		public void Line186()
		{
			// tag::3653567181f43a5f64c74f934aa821c2[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Remove(al => al
					.Index("test1")
					.Alias("alias1")
				)
			);
			// end::3653567181f43a5f64c74f934aa821c2[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""remove"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:204")]
		public void Line204()
		{
			// tag::af3fb9fa5691a7b37a6dc2a69ff66e64[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Remove(al => al
					.Index("test1")
					.Alias("alias1")
				)
				.Add(al => al
					.Index("test1")
					.Alias("alias2")
				)
			);
			// end::af3fb9fa5691a7b37a6dc2a69ff66e64[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""remove"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } },
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias2"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:222")]
		public void Line222()
		{
			// tag::f0e21e03a07c8fa0209b0aafdb3791e6[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Index("test1")
					.Alias("alias1")
				)
				.Add(al => al
					.Index("test2")
					.Alias("alias1")
				)
			);
			// end::f0e21e03a07c8fa0209b0aafdb3791e6[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } },
			        { ""add"" : { ""index"" : ""test2"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Waiting on PR to implement: https://github.com/elastic/elasticsearch-net/issues/4721")]
		[Description("indices/aliases.asciidoc:236")]
		public void Line236()
		{
			// tag::5f210f74725ea0c9265190346edfa246[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Alias("alias1")
				)
			);
			// end::5f210f74725ea0c9265190346edfa246[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""indices"" : [""test1"", ""test2""], ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:253")]
		public void Line253()
		{
			// tag::6799d132c1c7ca3970763acde2337ef9[]
			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Index("test*")
					.Alias("all_test_indices")
				)
			);
			// end::6799d132c1c7ca3970763acde2337ef9[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test*"", ""alias"" : ""all_test_indices"" } }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:276")]
		public void Line276()
		{
			// tag::de176bc4788ea286fff9e92418a43ea8[]
			var createIndexResponse = client.Indices.Create("test");

			var createIndexResponse2 = client.Indices.Create("test_2");

			var aliasResponse = client.Indices.BulkAlias(a => a
				.Add(al => al
					.Index("test_2")
					.Alias("test")
				)
				.RemoveIndex(al => al
					.Index("test")
				)
			);
			// end::de176bc4788ea286fff9e92418a43ea8[]

			createIndexResponse.MatchesExample(@"PUT test     \<1>");

			createIndexResponse2.MatchesExample(@"PUT test_2   \<2>");

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"":  { ""index"": ""test_2"", ""alias"": ""test"" } },
			        { ""remove_index"": { ""index"": ""test"" } }  \<3>
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:304")]
		public void Line304()
		{
			// tag::23ab0f1023b1b2cd5cdf2a8f9ccfd57b[]
			var createIndexResponse = client.Indices.Create("test1", c => c
				.Map(m => m
					.Properties(p => p
						.Keyword(k => k
							.Name("user")
						)
					)
				)
			);
			// end::23ab0f1023b1b2cd5cdf2a8f9ccfd57b[]

			createIndexResponse.MatchesExample(@"PUT /test1
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user"" : {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:320")]
		public void Line320()
		{
			// tag::7cf71671859be7c1ecf673396db377cd[]
			var aliasResponse = client.Indices.BulkAlias(b => b
				.Add(al => al
					.Index("test1")
					.Alias("alias2")
					.Filter<object>(f => f
						.Term("user", "kimchy")
					)
				)
			);
			// end::7cf71671859be7c1ecf673396db377cd[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test1"",
			                 ""alias"" : ""alias2"",
			                 ""filter"" : { ""term"" : { ""user"" : ""kimchy"" } }
			            }
			        }
			    ]
			}", e => e.ApplyBodyChanges(json => json["actions"][0]["add"]["filter"]["term"]["user"].ToLongFormTermQuery()));
		}

		[U]
		[Description("indices/aliases.asciidoc:348")]
		public void Line348()
		{
			// tag::bc1ad5cc6d3eab98e3ce01f209ba7094[]
			var aliasResponse = client.Indices.BulkAlias(b => b
				.Add(al => al
					.Index("test")
					.Alias("alias1")
					.Routing("1")
				)
			);
			// end::bc1ad5cc6d3eab98e3ce01f209ba7094[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""routing"" : ""1""
			            }
			        }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:368")]
		public void Line368()
		{
			// tag::fa0f4485cd48f986b7ae8cbb24e331c4[]
			var aliasResponse = client.Indices.BulkAlias(b => b
				.Add(al => al
					.Index("test")
					.Alias("alias2")
					.SearchRouting("1,2")
					.IndexRouting("2")
				)
			);
			// end::fa0f4485cd48f986b7ae8cbb24e331c4[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias2"",
			                 ""search_routing"" : ""1,2"",
			                 ""index_routing"" : ""2""
			            }
			        }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:394")]
		public void Line394()
		{
			// tag::427f6b5c5376cbf0f71f242a60ca3d9e[]
			var searchResponse = client.Search<object>(s => s
				.Index("alias2")
				.QueryOnQueryString("user:kimchy")
				.Routing("2,3")
			);
			// end::427f6b5c5376cbf0f71f242a60ca3d9e[]

			searchResponse.MatchesExample(@"GET /alias2/_search?q=user:kimchy&routing=2,3");
		}

		[U]
		[Description("indices/aliases.asciidoc:415")]
		public void Line415()
		{
			// tag::f6d6889667f56b8f49d2858070571a6b[]
			var aliasResponse = client.Indices.BulkAlias(b => b
				.Add(al => al
					.Index("test")
					.Alias("alias1")
					.IsWriteIndex()
				)
				.Add(al => al
					.Index("test2")
					.Alias("alias1")
				)
			);
			// end::f6d6889667f56b8f49d2858070571a6b[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : true
			            }
			        },
			        {
			            ""add"" : {
			                 ""index"" : ""test2"",
			                 ""alias"" : ""alias1""
			            }
			        }
			    ]
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:441")]
		public void Line441()
		{
			// tag::b0ec418bf416c62bed602b0a32a6d5f5[]
			var indexResponse = client.Index(
				new { foo = "bar" },
				i => i.Id(1).Index("alias1"));
			// end::b0ec418bf416c62bed602b0a32a6d5f5[]

			indexResponse.MatchesExample(@"PUT /alias1/_doc/1
			{
			    ""foo"": ""bar""
			}");
		}

		[U]
		[Description("indices/aliases.asciidoc:453")]
		public void Line453()
		{
			// tag::67bba546d835bca8f31df13e3587c348[]
			var getResponse = client.Get<object>(1, g => g.Index("test"));
			// end::67bba546d835bca8f31df13e3587c348[]

			getResponse.MatchesExample(@"GET /test/_doc/1");
		}

		[U]
		[Description("indices/aliases.asciidoc:462")]
		public void Line462()
		{
			// tag::ad79228630684d950fe9792a768d24c5[]
			var aliasResponse = client.Indices.BulkAlias(b => b
				.Add(al => al
					.Index("test")
					.Alias("alias1")
					.IsWriteIndex(false)
				)
				.Add(al => al
					.Index("test2")
					.Alias("alias1")
					.IsWriteIndex()
				)
			);
			// end::ad79228630684d950fe9792a768d24c5[]

			aliasResponse.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : false
			            }
			        }, {
			            ""add"" : {
			                 ""index"" : ""test2"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : true
			            }
			        }
			    ]
			}");
		}
	}
}
