// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4228
	{
		[U]
		public void CanSerializeDateMathDateTimeMinValue() {

			var searchResponse = TestClient.DefaultInMemoryClient.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.DateRange(d => d
						.GreaterThanOrEquals(DateTime.MinValue)
						.Field("date")
					)
				)
			);

			Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes)
				.Should().Be("{\"query\":{\"range\":{\"date\":{\"gte\":\"0001-01-01T00:00:00\"}}}}");
		}
	}
}
