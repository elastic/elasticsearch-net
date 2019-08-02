using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples
{
	/// <summary>
	/// https://www.elastic.co/guide/en/elasticsearch/reference/master/docs-get.html
	/// </summary>
	public class DocsGet : ExampleBase
	{
		/// <summary>
		/// GET twitter/_doc/0
		/// </summary>
		[U] public void Get()
		{
			// tag::04385590b3364c057850cffb97e0a9a6[]
			var getResponse = client.Get<Tweet>(0, g => g.Index("twitter"));
			// end::04385590b3364c057850cffb97e0a9a6[]

			getResponse.MatchesExample("GET twitter/_doc/0");
		}

		/// <summary>
		/// HEAD twitter/_doc/0
		/// </summary>
		[U] public void Exists()
		{
			// tag::015dec7c846a973e994423984870eccf[]
			var existsResponse = client.DocumentExists<Tweet>(0, g => g.Index("twitter"));
			// end::015dec7c846a973e994423984870eccf[]

			existsResponse.MatchesExample("HEAD twitter/_doc/0");
		}

		/// <summary>
		/// GET twitter/_doc/0?_source=false
		/// </summary>
		[U] public void SourceFiltering()
		{
			// tag::8457d92d89c5dc0389cd8c1350a72af7[]
			var getResponse = client.Get<Tweet>(0, g => g
				.Index("twitter")
				.SourceEnabled(false)
			);
			// end::8457d92d89c5dc0389cd8c1350a72af7[]

			getResponse.MatchesExample("GET twitter/_doc/0?_source=false");
		}

		/// <summary>
		/// GET twitter/_doc/0?_source_includes=*.id&_source_excludes=entities
		/// </summary>
		[U] public void SourceIncludesExcludes()
		{
			// tag::c5d72a90df7052734423df8fe63dfbc4[]
			var getResponse = client.Get<Tweet>(0, g => g
				.Index("twitter")
				.SourceIncludes("*.id")
				.SourceExcludes("entities")
			);
			// end::c5d72a90df7052734423df8fe63dfbc4[]

			getResponse.MatchesExample("GET twitter/_doc/0?_source_includes=*.id&_source_excludes=entities");
		}

		/// <summary>
		/// GET twitter/_doc/0?_source=*.id,retweeted
		/// </summary>
		[U] public void SourceIncludesShorter()
		{
			// tag::0e526e3f058c9f1d8c9b5f7407918edb[]
			var getResponse = client.Get<Tweet>(0, g => g
				.Index("twitter")
				.SourceIncludes("*.id,retweeted")
			);
			// end::0e526e3f058c9f1d8c9b5f7407918edb[]

			getResponse.MatchesExample("GET twitter/_doc/0?_source=*.id,retweeted");
		}

		/// <summary>
		/// PUT twitter
		/// {
		/// 	"mappings": {
		/// 		"properties": {
		/// 			"counter": {
		/// 				"type": "integer",
		/// 				"store": false
		/// 			},
		/// 			"tags": {
		/// 				"type": "keyword",
		/// 				"store": true
		/// 			}
		/// 		}
		/// 	}
		/// }
		/// PUT twitter/_doc/1
		/// {
		/// 	"counter" : 1,
		/// 	"tags" : ["red"]
		/// }
		/// GET twitter/_doc/1?stored_fields=tags,counter
		/// </summary>
		[U] public void StoredFields()
		{
			// tag::99a849b50a0611bc05703791b9f3bb0e[]
			var createIndexResponse = client.Indices.Create("twitter", c => c
				.Map<Tweet>(m => m
					.Properties(p => p
						.Number(n => n
							.Name(f => f.Counter)
							.Type(NumberType.Integer)
							.Store(false)
						)
						.Keyword(k => k
							.Name(f => f.Tags)
							.Store(true)
						)
					)
				)
			);
			// end::99a849b50a0611bc05703791b9f3bb0e[]

			createIndexResponse.MatchesExample(@"PUT twitter
			{
			   ""mappings"": {
			       ""properties"": {
			          ""counter"": {
			             ""type"": ""integer"",
			             ""store"": false
			          },
			          ""tags"": {
			             ""type"": ""keyword"",
			             ""store"": true
			          }
			       }
			   }
			}");

			// tag::f294c64d36ba2b5cbfcdab5e48cbbf61[]
			var indexResponse = client.Index(new Tweet { Id = 1, Counter = 1, Tags = new[] { "red" } }, i => i.Index("twitter"));
			// end::f294c64d36ba2b5cbfcdab5e48cbbf61[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/1
			{
			    ""counter"" : 1,
				""tags"" : [""red""]
			}");

			// tag::159b20511bd6b07471780c716d170ff2[]
			var getResponse = client.Get<Tweet>(1, g => g
				.Index("twitter")
				.StoredFields(
					f => f.Tags,
					f => f.Counter)
			);
			// end::159b20511bd6b07471780c716d170ff2[]

			getResponse.MatchesExample("GET twitter/_doc/1?stored_fields=tags,counter");
		}

		/// <summary>
		/// PUT twitter/_doc/2?routing=user1
		/// {
		/// 	"counter" : 1,
		/// 	"tags" : ["white"]
		/// }
		/// </summary>
		/// <returns></returns>
		[U] public void Routing()
		{
			// tag::c10f7ccb6e568d09dbb2a00bbc2a8524[]
			var indexResponse = client.Index(new Tweet { Id = 2, Counter = 1, Tags = new[] { "white" } }, g => g
				.Index("twitter")
				.Routing("user1")
			);
			// end::c10f7ccb6e568d09dbb2a00bbc2a8524[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/2?routing=user1
			{
			    ""counter"" : 1,
			    ""tags"" : [""white""]
			}");

			// tag::3a1137042dd7f0b247bea769b13851f8[]
			var getResponse = client.Get<Tweet>(2, g => g
				.Index("twitter")
				.Routing("user1")
				.StoredFields(
					f => f.Tags,
					f => f.Counter)
			);
			// end::3a1137042dd7f0b247bea769b13851f8[]

			getResponse.MatchesExample("GET twitter/_doc/2?routing=user1&stored_fields=tags,counter");
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		[U] public void SourceDirectly()
		{
			// tag::d7c22f2745bc072915aca889cc6a9309[]
			var sourceResponse = client.Source<Tweet>(1, s => s.Index("twitter"));
			// end::d7c22f2745bc072915aca889cc6a9309[]

			sourceResponse.MatchesExample("GET twitter/_source/1");

			// tag::1170c9a6124587b0a6c622d2ec3b7aec[]
			var sourceFilteringResponse = client.Source<Tweet>(1, s => s
				.Index("twitter")
				.SourceIncludes("*.id")
				.SourceExcludes("entities")
			);
			// end::1170c9a6124587b0a6c622d2ec3b7aec[]

			sourceFilteringResponse.MatchesExample("GET twitter/_source/1/?_source_includes=*.id&_source_excludes=entities");

			// tag::f18aa8d89c6f1f1438ab682459d75712[]
			var sourceExistsResponse = client.SourceExists<Tweet>(1, s => s.Index("twitter"));
			// end::f18aa8d89c6f1f1438ab682459d75712[]

			sourceExistsResponse.MatchesExample("HEAD twitter/_source/1");
		}

		[U] public void Routing2()
		{
			// tag::6809e8bd715b837d6dcb651da1465c05[]
			var getResponse = client.Get<Tweet>(2, g => g
				.Index("twitter")
				.Routing("user1")
			);
			// end::6809e8bd715b837d6dcb651da1465c05[]

			getResponse.MatchesExample("GET twitter/_doc/2?routing=user1");
		}
	}
}
