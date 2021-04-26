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

using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue5432
	{
		[U]
		public void DeserializesAnalyzers()
		{
			const string json = @"{
    ""analyzer-test"": {
        ""aliases"": {},
        ""mappings"": {},
        ""settings"": {
            ""index"": {
                ""routing"": {
                    ""allocation"": {
                        ""include"": {
                            ""_tier_preference"": ""data_content""
                        }
                    }
                },
                ""number_of_shards"": ""1"",
                ""provided_name"": ""analyzer-test"",
                ""creation_date"": ""1616482422981"",
                ""analysis"": {
                    ""filter"": {
                        ""autocomplete_ngram"": {
                            ""type"": ""edge_ngram"",
                            ""min_gram"": ""3"",
                            ""max_gram"": ""20""
                        }
                    },
                    ""analyzer"": {
                        ""search_autocomplete"": {
                            ""filter"": [
                                ""lowercase""
                            ],
                            ""tokenizer"": ""keyword""
                        },
                        ""index_autocomplete"": {
                            ""filter"": [
                                ""lowercase"",
                                ""autocomplete_ngram""
                            ],
                            ""tokenizer"": ""keyword""
                        }
                    }
                },
                ""number_of_replicas"": ""1"",
                ""uuid"": ""g8qnTA5wTveRCQnQN6xa3A"",
                ""version"": {
                    ""created"": ""7110099""
                }
            }
        }
    }
}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var client = TestClient.FixedInMemoryClient(bytes);
			var response = client.Indices.Get("analyzer-test");

			var analyzers = response.Indices.Values.First().Settings.Analysis.Analyzers.Values;
			analyzers.Count.Should().Be(2);

			foreach(var a in analyzers)
			{
				a.Should().NotBeNull();
			}

			if (response.Indices.Values.First().Settings.Analysis.Analyzers.TryGetValue("search_autocomplete", out var analyzerOne))
			{
				var customAnalyzer = analyzerOne as CustomAnalyzer;
				customAnalyzer.Should().NotBeNull();
				customAnalyzer!.Filter.Count().Should().Be(1);
			}
			else
			{
				throw new Exception("Expected index_autocomplete analyzer was not found.");
			}

			if (response.Indices.Values.First().Settings.Analysis.Analyzers.TryGetValue("index_autocomplete", out var analyzerTwo))
			{
				var customAnalyzer = analyzerTwo as CustomAnalyzer;
				customAnalyzer.Should().NotBeNull();
				customAnalyzer!.Filter.Count().Should().Be(2);
			}
			else
			{
				throw new Exception("Expected index_autocomplete analyzer was not found.");
			}
		}
	}
}
