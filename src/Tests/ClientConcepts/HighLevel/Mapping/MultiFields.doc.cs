using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
    /**[[multi-fields]]
    * === Multi fields
    *
    * It is often useful to index the same field in Elasticsearch in different ways, to
    * serve different purposes, for example, mapping a POCO `string` property as a
    * `text` datatype for full text search as well as mapping as a `keyword` datatype for
    * structured search, sorting and aggregations. Another example is mapping a POCO `string`
    * property to use different analyzers, to serve different full text search needs.
    *
    *
    */
    public class MultiFields
    {
        public class Person
        {
            public string Name { get; set; }
        }

		/**
        *
        * ==== Creating Multi fields
        *
		* Let's look at an example, using the following simple POCO
		*
        * Multi fields can be created on a mapping using the `.Fields()` method within a field mapping
        */
		[U]
        public void CreatingMultiFields()
        {
            var descriptor = new CreateIndexDescriptor("myindex")
                .Mappings(ms => ms
                    .Map<Person>(m => m
                        .Properties(p => p
                            .String(t => t
                                .Name(n => n.Name)
                                .Fields(ff => ff
                                    .String(tt => tt
                                        .Name("stop") // <1> Use the stop analyzer on this sub field
                                        .Analyzer("stop")
                                    )
                                    .String(tt => tt
                                        .Name("shingles")
                                        .Analyzer("name_shingles") // <2> Use a custom analyzer named "named_shingles" that is configured in the index
                                    )
                                    .String(k => k
                                        .Name("keyword") // <3> Index as not analyzed
                                        .IgnoreAbove(256)
										.NotAnalyzed()
                                    )
                                )
                            )
                        )
                    )
                );

            /**
             */
            //json
            var expected = new
            {
                mappings = new
                {
                    person = new
                    {
                        properties = new
                        {
                            name = new
                            {
                                type = "string",
                                fields = new
                                {
                                    stop = new
                                    {
                                        type = "string",
                                        analyzer = "stop"
                                    },
                                    shingles = new
                                    {
                                        type = "string",
                                        analyzer = "name_shingles"
                                    },
                                    keyword = new
                                    {
                                        type = "string",
                                        ignore_above = 256,
										index = "not_analyzed"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected).WhenSerializing((ICreateIndexRequest)descriptor);
        }

		/**
		 * [NOTE]
         * --
         * Multi fields do not change the original `_source` field in Elasticsearch; they affect only how
         * a field is indexed.
         *
         * New multi fields can be added to existing fields using the Put Mapping API.
         * --
		 *
		 * This is useful because the property can be used for both full text search
		 * as well as for structured search, sorting and aggregations
		 */

		[U]
		public void Searching()
		{
			// hide
			var client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming().PrettyJson());

			var searchResponse = client.Search<Person>(s => s
				.Query(q => q
					.Match(m => m
						.Field(f => f.Name)
						.Query("Russ Cam")
					) && q
					.Match(m => m
						.Field(f => f.Name.Suffix("shingles")) // <1> Use the shingles subfield on `Name`
						.Query("Russ Cam")
						.Boost(1.2)
					)
				)
				.Sort(ss => ss
					.Descending(f => f.Name.Suffix("keyword")) // <2> Use the keyword subfield on `Name`
				)
				.Aggregations(a => a
					.Terms("peoples_names", t => t
						.Field(f => f.Name.Suffix("keyword"))
					)
				)
			);

			/**
             */
			//json
			var expected = new
			{
				query = new
				{
					@bool = new
					{
						must = new object[]
						{
							new
							{
								match = new
								{
									name = new
									{
										query = "Russ Cam"
									}
								}
							},
							new
							{
								match = new JObject
								{
									{
										"name.shingles", new JObject
										{
											{ "query", "Russ Cam" },
											{ "boost", 1.2 }
										}
									}
								}
							}
						}
					}
				},
				sort = new object[]
				{
					new JObject
					{
						{ "name.keyword", new JObject { { "order", "desc" } } }
					}
				},
				aggs = new
				{
					peoples_names = new
					{
						terms = new
						{
							field = "name.keyword"
						}
					}
				}
			};

			// hide
			JObject.DeepEquals(JObject.FromObject(expected), JObject.Parse(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes))).Should().BeTrue();
		}
	}
}
