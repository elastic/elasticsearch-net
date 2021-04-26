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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue2886 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2886(WritableCluster cluster) => _cluster = cluster;

		[I]
		public void CanReadSingleOrMultipleCommonGramsCommonWordsItem()
		{
			var client = _cluster.Client;

			var json = @"
				{
				  ""settings"": {
					""analysis"": {
					  ""filter"": {
						""single_common_words"": {
						  ""type"":         ""common_grams"",
						  ""common_words"": ""_english_""
						},
						""multiple_common_words"": {
						  ""type"":         ""common_grams"",
						  ""common_words"": [""_english_"", ""_french_""]
						}
					  }
					}
				  }
				}";

			var response = client.LowLevel.Indices.Create<StringResponse>("common_words_token_filter", json);
			response.Success.Should().BeTrue();

			var settingsResponse = client.Indices.Get("common_words_token_filter");

			var indexState = settingsResponse.Indices["common_words_token_filter"];
			indexState.Should().NotBeNull();

			var tokenFilters = indexState.Settings.Analysis.TokenFilters;
			tokenFilters.Should().HaveCount(2);

			var commonGramsTokenFilter = tokenFilters["single_common_words"] as ICommonGramsTokenFilter;
			commonGramsTokenFilter.Should().NotBeNull();
			commonGramsTokenFilter.CommonWords.Should().NotBeNull().And.HaveCount(1);

			commonGramsTokenFilter = tokenFilters["multiple_common_words"] as ICommonGramsTokenFilter;
			commonGramsTokenFilter.Should().NotBeNull();
			commonGramsTokenFilter.CommonWords.Should().NotBeNull().And.HaveCount(2);
		}
	}
}
