using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;

namespace Tests.Search
{
    // TODO: Additional sections on Paging results (from/size, scroll and search_after), Multisearch and Conditionless queries
    /**=== Writing queries
     *
     * Once you have data indexed within Elasticsearch, you're going to want to be able to search it. Elasticsearch
     * offers a powerful query DSL to define queries to execute agains Elasticsearch. This DSL is based on JSON
     * and is exposed in NEST in the form of both a Fluent API and an Object Initializer syntax
     *
     */
    public class WritingQueries : SerializationTestBase, IClusterFixture<ReadOnlyCluster>
    {
        private readonly ReadOnlyCluster _cluster;

        public WritingQueries(ReadOnlyCluster cluster)
        {
            _cluster = cluster;
        }

        private IElasticClient client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming());

        /**==== Match All query
         *
         * The simplest of queries is the {ref_current}/query-dsl-match-all-query.html[match_all] query;
         * this will return all documents, giving them all a `_score` of 1.0
         *
         * [NOTE]
         * --
         * Not _all_ of the matching documents are returned in the one response; by default, only the first ten documents
         * are returned. You can use `from` and `size` to paginate results.
         * --
         */
        [U]
        public void MatchAllQuery()
        {
            var searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .MatchAll()
                )
            );

            /**
             * which serializes to the following JSON
             */
            //json
            var expected = new
            {
                query = new
                {
                    match_all = new {}
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));

            /**Since `match_all` queries are common, the previous example also has a shorthand which
             * serializes to the same query DSL JSON
             */
            searchResponse = client.Search<Project>(s => s
                .MatchAll()
            );

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));

            /**
             * The two previous examples both used the Fluent API to express the query. NEST also exposes an
             * Object Initializer syntax to compose queries
             */
            var searchRequest = new SearchRequest<Project>
            {
                Query = new MatchAllQuery()
            };

            searchResponse = client.Search<Project>(searchRequest);

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));
        }

        /**==== Search request parameters
         *
         * There are several parameters available on a search request; take a look at the reference section
         * on <<reference-search, search>> for more details.
         *
         *[float]
         * === Common queries
         *
         * By default, documents will be returned in `_score` descending order, where the `_score` for each hit
         * is the relevancy score calculated for how well the document matched the query criteria.
         *
         * There are a number of search queries at your disposal, all of which are documented in
         * the <<query-dsl, Query DSL>> reference section. Here, we want to highlight the three types of query
         * operations that users typically want to perform
         *
         * - <<structured-search, Structured search>>
         * - <<unstructured-search, Unstructured search>>
         * - <<combining-queries, Combining queries>>
         *
         * [[structured-search]]
         * ==== Structured search
         *
         * Structured search is about querying data that has inherent structure. Dates, times and numbers
         * are all structured and it is common to want to query against fields of these types to look
         * for exact matches, values that fall within a range, etc. Text can also be structured, for example,
         * the keyword tags applied to a blog post.
         *
         * With structured search, the answer to a query is *always* yes or no; a document is either a match
         * for the query or it isn't.
         *
         * The <<term-level-queries, term level queries>> are typically used for structured search. Here's an
         * example that looks for documents whose started on date falls within a specified range
         */
        [U]
        public void StructuredSearch()
        {
            var searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .DateRange(r => r
                        .Field(f => f.StartedOn)
                        .GreaterThanOrEquals(new DateTime(2017, 01, 01))
                        .LessThan(new DateTime(2018, 01, 01))// <1> Find all the projects that have been started in 2017
                    )
                )
            );

            /**which yields the following query JSON
             *
             */
            // json
            var expected = new
            {
                query = new
                {
                    range = new
                    {
                        startedOn = new {
                            lt = "2018-01-01T00:00:00",
                            gte = "2017-01-01T00:00:00"
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));

            /**
             * Since the answer to this query is always yes or no, we don't want to _score_ the query. To do this,
             * we can get the query to be __executed in a filter context__ by wrapping it in a `bool` query `filter`
             * clause
             */
            searchResponse = client.Search<Project>(s => s
               .Query(q => q
                   .Bool(b => b
                       .Filter(bf => bf
                           .DateRange(r => r
                               .Field(f => f.StartedOn)
                               .GreaterThanOrEquals(new DateTime(2017, 01, 01))
                               .LessThan(new DateTime(2018, 01, 01))
                           )
                       )
                   )

               )
            );

            //json
            var expected2 = new
            {
                query = new
                {
                    @bool = new
                    {
                        filter = new object[]
                        {
                            new
                            {
                                range = new
                                {
                                    startedOn = new
                                    {
                                        lt = "2018-01-01T00:00:00",
                                        gte = "2017-01-01T00:00:00"
                                    }
                                }
                            }
                        }
                    }

                }
            };

            //hide
            Expect(expected2)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));
        }

         /**
         * The benefit of executing a query in a filter context is that Elasticsearch is able to
         * forgo calculating a relevancy score, as well as cache filters for faster subsequent performance.
         *
         * [IMPORTANT]
         * --
         * <<term-level-queries, term level queries>> have no analysis phase, that is, the query input
         * is not analyzed, and an *exact match* to the input is looked for in the inverted index. This can
         * trip many new users up when using a term level query against a field that is analyzed at index
         * time.
         *
         * When a field is _only_ to be used for exact matching, you should consider indexing it as a
         * {ref_current}/keyword.html[keyword] datatype. If a field is used for both exact matches and
         * full text search, you should consider indexing it with <<multi-fields, multi fields>>.
         * --
         *
         * [[unstructured-search]]
         * ==== Unstructured search
         *
         * Another common use case is to search within full text fields in order to find the most relevant documents.
         *
         * <<full-text-queries, Full text queries>> are used for unstructured search; here we use the `match` query
         * to find all documents that contain `"Russ"` in the lead developer first name field
         */
        [U]
        public void UnstructuredSearch()
        {
            var searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.LeadDeveloper.FirstName)
                        .Query("Russ")
                    )
                )
            );

            /**which yields the following query JSON
             *
             */
            // json
            var expected = new
            {
                query = new
                {
                    match = new JObject
                    {
                        {
                            "leadDeveloper.firstName", new JObject
                            {
                                { "query", "Russ" }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));
        }


        /**
         *
         * [IMPORTANT]
         * --
         * <<full-text-queries, full text queries>> have an analysis phase, that is, the query input
         * is analyzed, and the resulting terms from query analysis are compared to the terms in the inverted
         * index.
         *
         * You have full control over the analysis that is applied at both search time and index time, by applying
         * <<writing-analyzers, analyzers>> to {ref_current}/text.html[text] datatype fields through
         * <<mapping, mapping>>.
         * --
         *
         * [[combining-queries]]
        * ==== Combining queries
        *
        * An extremely common scenario is to combine separate queries together to form a
        * <<compound-queries, compound query>>, the most common of which is the `bool` query
        */
        [U]
        public void BoolQuery()
        {
            var searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(mu => mu
                            .Match(m => m // <1> match documents where lead developer first name contains Russ
                                .Field(f => f.LeadDeveloper.FirstName)
                                .Query("Russ")
                            ), mu => mu
                            .Match(m => m // <2> ...and where the lead developer last name contains Cam
                                .Field(f => f.LeadDeveloper.LastName)
                                .Query("Cam")
                            )
                        )
                        .Filter(fi => fi
                             .DateRange(r => r
                                .Field(f => f.StartedOn)
                                .GreaterThanOrEquals(new DateTime(2017, 01, 01))
                                .LessThan(new DateTime(2018, 01, 01)) // <3> ...and where the project started in 2017
                            )
                        )
                    )
                )
            );

            /**which yields the following query JSON
             *
             */
            // json
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
                                match = new JObject
                                {
                                    {
                                        "leadDeveloper.firstName", new JObject
                                        {
                                            { "query", "Russ" }
                                        }
                                    }
                                }
                            },
                            new
                            {
                                match = new JObject
                                {
                                    {
                                        "leadDeveloper.lastName", new JObject
                                        {
                                            { "query", "Cam" }
                                        }
                                    }
                                }
                            },
                        },
                        filter = new object[]
                        {
                            new
                            {
                                range = new
                                {
                                    startedOn = new
                                    {
                                        lt = "2018-01-01T00:00:00",
                                        gte = "2017-01-01T00:00:00"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));

            /**
             * A document must
             * satisfy all three queries in this example to be a match
             *
             * . the `match` queries on both first name and last name will contribute to
             * the relevancy score calculated, since both queries are running in a query context
             *
             * . the `range` query against the started on date is running in a filter context,
             * so no score is calculated for matching documents (all documents have the same score
             * of 1.0 for this query).
             *
             * Because `bool` queries are so common, NEST overloads operators on queries to make forming
             * `bool` queries much more succinct. The previous `bool` query can be more concisely
             * expressed as
             */
            searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.LeadDeveloper.FirstName)
                        .Query("Russ")
                    ) && q // <1> combine queries using the binary `&&` operator
                    .Match(m => m
                        .Field(f => f.LeadDeveloper.LastName)
                        .Query("Cam")
                    ) && +q // <2> wrap a query in a `bool` query filter clause using the unary `+` operator and combine using the binary `&&` operator
                    .DateRange(r => r
                        .Field(f => f.StartedOn)
                        .GreaterThanOrEquals(new DateTime(2017, 01, 01))
                        .LessThan(new DateTime(2018, 01, 01))
                    )
                )
            );

            /**
             * Take a look at the dedicated section on <<bool-queries, writing `bool` queries>> for more detail
             * and further examples.
             */
            //hide
            Expect(expected)
                .NoRoundTrip()
                .WhenSerializing(Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes));
        }

        /**
        * ==== Search response
        *
        * The response returned from a search query is an `ISearchResponse<T>`, where `T` is the
        * generic parameter type defined in the search method call. There are a fair few properties
		* on the response, but the most common you're likely to work with is `.Documents`,
		* which we'll demonstrate below.
        *
        * ==== Matching documents
        *
        * To get the documents in the response that match the search query is easy enough
        */
        [I]
        public void GettingDocuments()
        {
            // hide
            var client = _cluster.Client;

            var searchResponse = client.Search<Project>(s => s
                .Query(q => q
                    .MatchAll()
                )
            );

            var projects = searchResponse.Documents;

            /**
             * `.Documents` is a convenient shorthand for
             */
            searchResponse.HitsMetadata.Hits.Select(h => h.Source);

            /**
             * and it's possible to retrieve other metadata about each hit from the hits collection. Here's
             * an example that retrieves the highlights for a hit, when using <<highlighting-usage, highlighting>>
             */
            var highlights = searchResponse.HitsMetadata.Hits.Select(h => h
                .Highlights // <1> Get the highlights for the hit, when using highlighting
            );
        }
    }
}
