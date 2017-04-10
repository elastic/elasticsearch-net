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
    * Let's look at a few examples. for each, we use the following simple POCO
    */

    public class MultiFields
    {
        public class Person
        {
            public string Name { get; set; }
        }

        /**
        * ==== Default mapping for String properties
        *
        * When using <<auto-map, Auto Mapping>>, the inferred mapping for a `string`
        * POCO type is a `text` datatype with multi fields including a `keyword` sub field
        */
        [U]
        public void DefaultMultiFields()
        {
            var descriptor = new CreateIndexDescriptor("myindex")
                .Mappings(ms => ms
                    .Map<Person>(m => m
                        .AutoMap()
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
                                type = "text",
                                fields = new
                                {
                                    keyword = new
                                    {
                                        type = "keyword",
                                        ignore_above = 256
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
        }

        /**
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
                        .Query("Russ")
                    )
                )
                .Sort(ss => ss
                    .Descending(f => f.Name.Suffix("keyword")) // <1> Use the keyword subfield on `Name`
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
                    match = new
                    {
                        name = new
                        {
                            query = "Russ"
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


        /**
        * [NOTE]
        * --
        * Multi fields do not change the original `_source` field in Elasticsearch; they affect only how
        * a field is indexed.
        *
        * New multi fields can be added to existing fields using the Put Mapping API.
        * --
        *
        * ==== Creating Multi fields
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
                            .Text(t => t
                                .Name(n => n.Name)
                                .Fields(ff => ff
                                    .Text(tt => tt
                                        .Name("stop") // <1> Use the stop analyzer on this sub field
                                        .Analyzer("stop")
                                    )
                                    .Text(tt => tt
                                        .Name("shingles")
                                        .Analyzer("name_shingles") // <2> Use a custom analyzer named "named_shingles" that is configured in the index
                                    )
                                    .Keyword(k => k
                                        .Name("keyword") // <3> Index as not analyzed
                                        .IgnoreAbove(256)
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
                                type = "text",
                                fields = new
                                {
                                    stop = new
                                    {
                                        type = "text",
                                        analyzer = "stop"
                                    },
                                    shingles = new
                                    {
                                        type = "text",
                                        analyzer = "name_shingles"
                                    },
                                    keyword = new
                                    {
                                        type = "keyword",
                                        ignore_above = 256
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
    }
}
