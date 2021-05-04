// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;
using static Tests.Core.Serialization.SerializationTestHelper;

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

			var analyzeResponse = client.Indices.Analyze(a => a
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

			/**which is deserialized to an instance of `AnalyzeResponse` by NEST
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

			var analyzeResponse = client.Indices.Analyze(a => a
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
		/*
		 *   {
-   "tokens": [
+   "error": {
+     "reason": "failed to find analyzer [my_analyzer]",
+     "root_cause": [
        {
-       "token": "fsharp",
-       "start_offset": 0,
-       "end_offset": 2,
-       "type": "<ALPHANUM>",
-       "position": 0
+         "reason": "[writable-node-c9352e9200][127.0.0.1:9300][indices:admin/analyze[s]]",
+         "stack_trace": "[[writable-node-c9352e9200][127.0.0.1:9300][indices:admin/analyze[s]]]; nested: RemoteTransportException[[writable-node-c9352e9200][127.0.0.1:9300][indices:admin/analyze[s]]]; nested: IllegalArgumentException[failed to find analyzer [my_analyzer]];\n\tat org.elasticsearch.ElasticsearchException.guessRootCauses(ElasticsearchException.java:639)\n\tat org.elasticsearch.ElasticsearchException.generateFailureXContent(ElasticsearchException.java:567)\n\tat org.elasticsearch.rest.BytesRestResponse.build(BytesRestResponse.java:138)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:96)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:91)\n\tat org.elasticsearch.rest.action.RestActionListener.onFailure(RestActionListener.java:58)\n\tat org.elasticsearch.action.support.TransportAction$1.onFailure(TransportAction.java:79)\n\tat org.elasticsearch.action.support.single.shard.TransportSingleShardAction$AsyncSingleAction.perform(TransportSingleShardAction.java:229)\n\tat org.elasticsearch.action.support.single.shard.TransportSingleShardAction$AsyncSingleAction.onFailure(TransportSingleShardAction.java:210)\n\tat org.elasticsearch.action.support.single.shard.TransportSingleShardAction$AsyncSingleAction$2.handleException(TransportSingleShardAction.java:266)\n\tat org.elasticsearch.transport.TransportService$ContextRestoreResponseHandler.handleException(TransportService.java:1110)\n\tat org.elasticsearch.transport.TransportService$DirectResponseChannel.processException(TransportService.java:1219)\n\tat org.elasticsearch.transport.TransportService$DirectResponseChannel.sendResponse(TransportService.java:1193)\n\tat org.elasticsearch.transport.TaskTransportChannel.sendResponse(TaskTransportChannel.java:60)\n\tat org.elasticsearch.action.support.ChannelActionListener.onFailure(ChannelActionListener.java:56)\n\tat org.elasticsearch.action.ActionRunnable.onFailure(ActionRunnable.java:60)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingAbstractRunnable.onFailure(ThreadContext.java:754)\n\tat org.elasticsearch.common.util.concurrent.AbstractRunnable.run(AbstractRunnable.java:39)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\nCaused by: RemoteTransportException[[writable-node-c9352e9200][127.0.0.1:9300][indices:admin/analyze[s]]]; nested: IllegalArgumentException[failed to find analyzer [my_analyzer]];\nCaused by: java.lang.IllegalArgumentException: failed to find analyzer [my_analyzer]\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.getAnalyzer(TransportAnalyzeAction.java:160)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.analyze(TransportAnalyzeAction.java:138)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.shardOperation(TransportAnalyzeAction.java:121)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.shardOperation(TransportAnalyzeAction.java:73)\n\tat org.elasticsearch.action.support.single.shard.TransportSingleShardAction.lambda$asyncShardOperation(TransportSingleShardAction.java:110)\n\tat org.elasticsearch.action.ActionRunnable$1.doRun(ActionRunnable.java:45)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingAbstractRunnable.doRun(ThreadContext.java:769)\n\tat org.elasticsearch.common.util.concurrent.AbstractRunnable.run(AbstractRunnable.java:37)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n",
+         "type": "remote_transport_exception"
+       }
+     ],
+     "stack_trace": "java.lang.IllegalArgumentException: failed to find analyzer [my_analyzer]\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.getAnalyzer(TransportAnalyzeAction.java:160)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.analyze(TransportAnalyzeAction.java:138)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.shardOperation(TransportAnalyzeAction.java:121)\n\tat org.elasticsearch.action.admin.indices.analyze.TransportAnalyzeAction.shardOperation(Tra, but found False.
Stack Trace:
		 */
		[SkipVersion(">=8.0.0-SNAPSHOT", "")]
		[I]public void CustomAnalyzer()
		{
			//hide
			var client = Client;
			//hide
			var createIndexResponse = client.Indices.Create("analysis-index", c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
			);
			//hide
			client.Cluster.Health("analysis-index", h => h.WaitForStatus(WaitForStatus.Green).Timeout("5s"));

			/**
			 * In this example, we'll add a custom analyzer to an existing index. First,
			 * we need to close the index
			 */
			client.Indices.Close("analysis-index");

			/**
			 * Now, we can update the settings to add the analyzer
			 */
			client.Indices.UpdateSettings("analysis-index", i => i
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
			client.Indices.Open("analysis-index");
			client.Cluster.Health("analysis-index",h => h
				.WaitForStatus(WaitForStatus.Green)
				.Timeout(TimeSpan.FromSeconds(5))
			);

			/**With the index open and ready, let's test the analyzer
			 */
			var analyzeResponse = client.Indices.Analyze(a => a
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

			client.Indices.Create("project-index", i => i
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
				.Map<Project>(mm => mm
					.Properties(p => p
						.Text(t => t
							.Name(n => n.Name)
							.Analyzer("my_analyzer")
						)
					)
				)
			);

			/**
			 * The analyzer on the `name` field can be tested with
			 */
			var analyzeResponse = client.Indices.Analyze(a => a
				.Index("project-index")
				.Field<Project, string>(f => f.Name)
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
