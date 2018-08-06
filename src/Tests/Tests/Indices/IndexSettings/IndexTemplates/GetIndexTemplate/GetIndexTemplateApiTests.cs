using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.GetIndexTemplate
{
	public class GetIndexTemplateApiTests
		: ApiIntegrationTestBase<WritableCluster, IGetIndexTemplateResponse, IGetIndexTemplateRequest, GetIndexTemplateDescriptor, GetIndexTemplateRequest>
	{
		public GetIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndexTemplate(f),
			fluentAsync: (client, f) => client.GetIndexTemplateAsync(f),
			request: (client, r) => client.GetIndexTemplate(r),
			requestAsync: (client, r) => client.GetIndexTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> Fluent => d => d
			.Name(CallIsolatedValue);

		protected override int ExpectStatusCode => 200;

		protected override bool ExpectIsValid => true;

		protected override GetIndexTemplateRequest Initializer => new GetIndexTemplateRequest(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putTemplateResponse = client.PutIndexTemplate(callUniqueValue.Value, d =>
					d.IndexPatterns("startingwiththis-*")
				     .Settings(s => s.NumberOfShards(2))
					 .Version(1)
					);

				if (!putTemplateResponse.IsValid)
					throw new Exception($"Problem putting index template for integration test: {putTemplateResponse.DebugInformation}");
			}
		}

		protected override void ExpectResponse(IGetIndexTemplateResponse response)
		{
			response.ShouldBeValid();

			response.TemplateMappings.Should().NotBeNull();
			response.TemplateMappings.Should().HaveCount(1);

			var responseTemplateMapping = response.TemplateMappings[CallIsolatedValue];

			responseTemplateMapping.IndexPatterns.Should().NotBeNull();
			responseTemplateMapping.IndexPatterns.Should().HaveCount(1);
			responseTemplateMapping.IndexPatterns.First().Should().Be("startingwiththis-*");

			responseTemplateMapping.Version.Should().Be(1);

			responseTemplateMapping.Settings.Should().NotBeNull();
			responseTemplateMapping.Settings.NumberOfShards.Should().Be(2);
		}
	}
}
