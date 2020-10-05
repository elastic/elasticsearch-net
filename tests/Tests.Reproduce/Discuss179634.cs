// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
