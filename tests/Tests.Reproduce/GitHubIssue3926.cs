using System;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue3926
	{
		public class DocumentModel
		{
			[JsonProperty("@timestamp")]
			public DateTime Timestamp { get; set; }

			[JsonProperty("document_name")]
			public string DocumentName { get; set; }
		}

		[U]
		public void FieldInferenceUsesJsonPropertyName()
		{
			var client = TestClient.InMemoryWithJsonNetSerializer;

			var searchResponse = client.Search<DocumentModel>(descriptor => descriptor
				.Index("my-index")
				.Query(q => q
					.DateRange(range => range
						.Field(model => model.Timestamp)
						.GreaterThan("1970-01-01"))
				)
			);

			Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes).Should().Contain("@timestamp");
		}
	}
}
