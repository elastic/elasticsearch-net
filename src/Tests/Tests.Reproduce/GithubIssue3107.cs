using System.Runtime.Serialization;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue3107
	{
		[U] public void FieldResolverRespectsDataMemberAttributes()
		{
			var client = TestClient.DefaultInMemoryClient;

			var document = new SourceEntity
			{
				Name = "name",
				DisplayName = "display name"
			};

			var indexResponse = client.IndexDocument(document);
			var requestJson = Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes);
			requestJson.Should().Contain("display_name");
			indexResponse.ApiCall.Uri.AbsoluteUri.Should().Contain("source_entity");

			var searchResponse = client.Search<SourceEntity>(s => s
				.Query(q => q
					.Terms(t => t
						.Field(f => f.DisplayName)
						.Terms("term")
					)
				)
			);

			requestJson = Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes);
			requestJson.Should().Contain("display_name");
		}

		[DataContract(Name = "source_entity")]
		public class SourceEntity
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "display_name")]
			public string DisplayName { get; set; }
		}
	}
}
