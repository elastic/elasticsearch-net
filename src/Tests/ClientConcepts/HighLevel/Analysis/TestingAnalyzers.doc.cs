using System;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Analysis
{
    /**[[testing-analyzers]]
     * === Testing analyzers
     *
     * When <<writing-analyzers, building your own analyzers>>, it's useful to test that the analyzer
     * does what we expect it to. This is where the {ref_current}/indices-analyze.html[Analyze API] comes in.
     *
     */
    public class TestingAnalyzers : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
    {
        //hide
        public TestingAnalyzers(WritableCluster cluster) : base(cluster)
        {
        }

        //hide
        private static class Console
        {
            public static void WriteLine(string value) { }
        }

        /**
         * ==== Testing in-built analyzers
         *
         * To get started with the Analyze API, we can test to see how a built-in analyzer will analyze
         * a piece of text
         */
        [I] public void StandardAnalyzer()
        {
            //hide
            var client = Client;

            var analyzeResponse = client.Analyze(a => a
                .Analyzer("standard") // <1> Use the `standard` analyzer
                .Text("F# is THE SUPERIOR language :)")
            );

            /**
             * This returns the following response from Elasticsearch
             */
            //json
            var expected = new
            {
                tokens = new object[]
                {
                    new
                    {
                        token = "f",
                        start_offset = 0,
                        end_offset = 1,
                        type = "<ALPHANUM>",
                        position = 0
                    },
                    new
                    {
                        token = "is",
                        start_offset = 3,
                        end_offset = 5,
                        type = "<ALPHANUM>",
                        position = 1
                    },
                    new
                    {
                        token = "the",
                        start_offset = 6,
                        end_offset = 9,
                        type = "<ALPHANUM>",
                        position = 2
                    },
                    new
                    {
                        token = "superior",
                        start_offset = 10,
                        end_offset = 18,
                        type = "<ALPHANUM>",
                        position = 3
                    },
                    new
                    {
                        token = "language",
                        start_offset = 19,
                        end_offset = 27,
                        type = "<ALPHANUM>",
                        position = 4
                    }
                }
            };

            //hide
            Expect(expected).WhenSerializing(analyzeResponse as AnalyzeResponse);

            /**which is deserialized to an instance of `IAnalyzeResponse` by NEST
             * that we can work with
             */
            foreach (var analyzeToken in analyzeResponse.Tokens)
            {
                Console.WriteLine($"{analyzeToken.Token}");
            }
        }

        /**
         * In testing the `standard` analyzer on our text, we've noticed that
         *
         * - `F#` is tokenized as `"f"`
         * - stop word tokens `"is"` and `"the"` are included
         * - `"superior"` is included but we'd also like to tokenize `"great"` as a synonym for superior
         *
         * We'll look at how we can test a combination of built-in analysis components next to
         * build an analyzer to fit our needs.
         *
         * ==== Testing built-in analysis components
         *
         * A _transient_ analyzer can be composed from built-in analysis components to test
         * an analysis configuration
         */
        [I] public void TransientAnalyzer()
        {
            //hide
            var client = Client;

            var analyzeResponse = client.Analyze(a => a
                .Tokenizer("standard")
                .Filter("lowercase", "stop")
                .Text("F# is THE SUPERIOR language :)")
            );

            //json
            var expected = new
            {
                tokens = new object[]
                {
                    new
                    {
                        token = "f",
                        start_offset = 0,
                        end_offset = 1,
                        type = "<ALPHANUM>",
                        position = 0
                    },
                    new
                    {
                        token = "superior",
                        start_offset = 10,
                        end_offset = 18,
                        type = "<ALPHANUM>",
                        position = 3
                    },
                    new
                    {
                        token = "language",
                        start_offset = 19,
                        end_offset = 27,
                        type = "<ALPHANUM>",
                        position = 4
                    }
                }
            };

            /**
             * Great! This has removed stop words, but we still have `F#` tokenized as `"f"`
             * and no `"great"` synonym for `"superior"`.
             *
             * IMPORTANT: Character and Token filters are **applied in the order** in which they are specified.
             *
             * Let's build a custom analyzer with additional components to solve this.
             */
            //hide
            Expect(expected).WhenSerializing(analyzeResponse as AnalyzeResponse);
        }

        /**
         * ==== Testing a custom analyzer in an index
         *
         * A custom analyzer can be created within an index, either when creating the index or by
         * updating the settings on an existing index.
         *
         * IMPORTANT: When adding to an existing index, it needs to be closed first.
         *
         *
         */
        [I]public void CustomAnalyzer()
        {
            //hide
            var client = Client;
            //hide
            var createIndexResponse = client.CreateIndex("analysis-index", c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
            );
            //hide
            client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Green).Index("analysis-index").Timeout("5s"));

            /**
             * In this example, we'll add a custom analyzer to an existing index. First,
             * we need to close the index
             */
            client.CloseIndex("analysis-index");

            /**
             * Now, we can update the settings to add the analyzer
             */
            client.UpdateIndexSettings("analysis-index", i => i
                .IndexSettings(s => s
                    .Analysis(a => a
                        .CharFilters(cf => cf
                            .Mapping("my_char_filter", m => m
                                .Mappings("F# => FSharp")
                            )
                        )
                        .TokenFilters(tf => tf
                            .Synonym("my_synonym", sf => sf
                                .Synonyms("superior, great")

                            )
                        )
                        .Analyzers(an => an
                            .Custom("my_analyzer", ca => ca
                                .Tokenizer("standard")
                                .CharFilters("my_char_filter")
                                .Filters("lowercase", "stop", "my_synonym")
                            )
                        )

                    )
                )
            );

            /**
             * And open the index again. Here, we also wait up to five seconds for the
             * status of the index to become green
             */
            client.OpenIndex("analysis-index");
            client.ClusterHealth(h => h
                .WaitForStatus(WaitForStatus.Green)
                .Index("analysis-index")
                .Timeout(TimeSpan.FromSeconds(5))
            );

            /**With the index open and ready, let's test the analyzer
             */
            var analyzeResponse = client.Analyze(a => a
                .Index("analysis-index") // <1> Since we added the custom analyzer to the "analysis-index" index, we need to target this index to test it
                .Analyzer("my_analyzer")
                .Text("F# is THE SUPERIOR language :)")
            );

            /** The output now looks like
             */
            //json
            var expected = new
            {
                tokens = new object[]
                {
                    new
                    {
                        token = "fsharp",
                        start_offset = 0,
                        end_offset = 2,
                        type = "<ALPHANUM>",
                        position = 0
                    },
                    new
                    {
                        token = "superior",
                        start_offset = 10,
                        end_offset = 18,
                        type = "<ALPHANUM>",
                        position = 3
                    },
                    new
                    {
                        token = "great",
                        start_offset = 10,
                        end_offset = 18,
                        type = "SYNONYM",
                        position = 3
                    },
                    new
                    {
                        token = "language",
                        start_offset = 19,
                        end_offset = 27,
                        type = "<ALPHANUM>",
                        position = 4
                    }
                }
            };

            /**
             * Exactly what we were after!
             */
            //hide
            Expect(expected).WhenSerializing(analyzeResponse as AnalyzeResponse);
        }

        /**
         * ==== Testing an analyzer on a field
         *
         * It's also possible to test the analyzer for a given field type mapping.
		 * Given an index created with the following settings and mappings
         */
        [I] public void CustomAnalyzerOnField()
        {
			// hide
            var client = Client;

            client.CreateIndex("project-index", i => i
                .Settings(s => s
                    .Analysis(a => a
                        .CharFilters(cf => cf
                            .Mapping("my_char_filter", m => m
                                .Mappings("F# => FSharp")
                            )
                        )
                        .TokenFilters(tf => tf
                            .Synonym("my_synonym", sf => sf
                                .Synonyms("superior, great")

                            )
                        )
                        .Analyzers(an => an
                            .Custom("my_analyzer", ca => ca
                                .Tokenizer("standard")
                                .CharFilters("my_char_filter")
                                .Filters("lowercase", "stop", "my_synonym")
                            )
                        )

                    )
                )
                .Mappings(m => m
                    .Map<Project>(mm => mm
                        .Properties(p => p
                            .Text(t => t
                                .Name(n => n.Name)
                                .Analyzer("my_analyzer")
                            )
                        )
                    )
                )
            );

            /**
             * The analyzer on the `name` field can be tested with
             */
            var analyzeResponse = client.Analyze(a => a
                .Index("project-index")
                .Field<Project>(f => f.Name)
                .Text("F# is THE SUPERIOR language :)")
            );
        }

        /////**
        //// * ==== Advanced details with Explain
        //// *
        //// * It's possible to get more advanced details about analysis by setting `Explain()` on
        //// * the request.
        //// *
        //// * For this example, we'll use Object Initializer syntax instead of the Fluent API; choose
        //// * whichever one you're most comfortable with!
        //// */
        ////[I]
        ////public void UsingExplain()
        ////{
        ////    //hide
        ////    var client = Client;

        ////    var analyzeRequest = new AnalyzeRequest
        ////    {
        ////        Analyzer = "standard",
        ////        Text = new [] { "F# is THE SUPERIOR language :)" },
        ////        Explain = true
        ////    };

        ////    var analyzeResponse = client.Analyze(analyzeRequest);

        ////    /**
        ////     * We now get further details back in the response
        ////     */
        ////    //json
        ////    var expected = new
        ////    {
        ////        detail = new
        ////        {
        ////            custom_analyzer = false,
        ////            analyzer = new
        ////            {
        ////                name = "standard",
        ////                tokens = new object[]
        ////                {
        ////                    new
        ////                    {
        ////                        token = "f",
        ////                        start_offset = 0,
        ////                        end_offset = 1,
        ////                        type = "<ALPHANUM>",
        ////                        position = 0,
        ////                        bytes = "[66]",
        ////                        positionLength = 1
        ////                    },
        ////                    new
        ////                    {
        ////                        token = "is",
        ////                        start_offset = 3,
        ////                        end_offset = 5,
        ////                        type = "<ALPHANUM>",
        ////                        position = 1,
        ////                        bytes = "[69 73]",
        ////                        positionLength = 1
        ////                    },
        ////                    new
        ////                    {
        ////                        token = "the",
        ////                        start_offset = 6,
        ////                        end_offset = 9,
        ////                        type = "<ALPHANUM>",
        ////                        position = 2,
        ////                        bytes = "[74 68 65]",
        ////                        positionLength = 1
        ////                    },
        ////                    new
        ////                    {
        ////                        token = "superior",
        ////                        start_offset = 10,
        ////                        end_offset = 18,
        ////                        type = "<ALPHANUM>",
        ////                        position = 3,
        ////                        bytes = "[73 75 70 65 72 69 6f 72]",
        ////                        positionLength = 1
        ////                    },
        ////                    new
        ////                    {
        ////                        token = "language",
        ////                        start_offset = 19,
        ////                        end_offset = 27,
        ////                        type = "<ALPHANUM>",
        ////                        position = 4,
        ////                        bytes = "[6c 61 6e 67 75 61 67 65]",
        ////                        positionLength = 1
        ////                    }
        ////                }
        ////            }
        ////        }

        ////    };
        ////}
    }
}
