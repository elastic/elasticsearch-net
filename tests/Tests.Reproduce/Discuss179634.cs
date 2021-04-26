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
using System.Globalization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Serialization;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class Discuss179634
	{
		[U]
		public void SerializeCompletionSuggesterFieldsCorrectlyWhenDefaultFieldNameInferrerUsed()
		{
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
				.DefaultFieldNameInferrer(p => p.ToUpper(CultureInfo.CurrentCulture))
				.DisableDirectStreaming();

			var tester = new SerializationTester(new ElasticClient(settings));

			var suggest = new SearchDescriptor<Project>()
				.Suggest(ss => ss
					.Completion("title", cs => cs
						.Field(f => f.Suggest)
						.Prefix("keyword")
						.Fuzzy(f => f
							.Fuzziness(Fuzziness.Auto)
						)
						.Size(5)
					)
				);

			var expected = @"{""suggest"":{""title"":{""completion"":{""fuzzy"":{""fuzziness"":""AUTO""},""field"":""SUGGEST"",""size"":5},""prefix"":""keyword""}}}";

			var result = tester.Serializes(suggest, expected);
			result.Success.Should().Be(true, result.DiffFromExpected);
		}
	}
}
