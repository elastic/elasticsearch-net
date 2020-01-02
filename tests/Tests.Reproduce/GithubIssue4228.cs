using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.Serialization;
using Tests.Domain;

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
