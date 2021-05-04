// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.SearchTemplate.RenderSearchTemplate
{
	public class RenderSearchTemplateApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, RenderSearchTemplateResponse, IRenderSearchTemplateRequest, RenderSearchTemplateDescriptor,
			RenderSearchTemplateRequest>
	{
		private static readonly string inlineSearchTemplate = @"
{
	""query"": {
	  ""terms"": {
		""status"": [
		  ""{{#status}}"",
		  ""{{.}}"",
		  ""{{/status}}""
		]
	  }
	}
  }";

		private readonly string[] _statusValues = { "pending", "published" };

		public RenderSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> Fluent => s => s
			.Source(inlineSearchTemplate)
			.Params(p => p
				.Add("status", _statusValues)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;


		protected override RenderSearchTemplateRequest Initializer => new RenderSearchTemplateRequest
		{
			Source = inlineSearchTemplate,
			Params = new Dictionary<string, object>
			{
				{ "status", _statusValues }
			}
		};

		protected override string UrlPath => $"/_render/template";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.RenderSearchTemplate(f),
			(c, f) => c.RenderSearchTemplateAsync(f),
			(c, r) => c.RenderSearchTemplate(r),
			(c, r) => c.RenderSearchTemplateAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.TemplateOutput.Should().NotBeNull();

			//TODO: 7.x this fails on As with random source serializer we need to come up wit a better API here in 7.x
			// build.bat seed:36985 integrate 7.0.0-beta1 "readonly" "rendersearchtemplate"

			if (TestConfiguration.Instance.Random.SourceSerializer) return;
			var searchRequest = r.TemplateOutput.As<ISearchRequest>();
			searchRequest.Should().NotBeNull();

			searchRequest.Query.Should().NotBeNull();
		});
	}
}
