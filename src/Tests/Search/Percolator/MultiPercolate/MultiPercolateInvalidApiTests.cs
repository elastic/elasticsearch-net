using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Percolator.MultiPercolate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiPercolateInvalidApiTests
		: ApiIntegrationTestBase<IMultiPercolateResponse, IMultiPercolateRequest, MultiPercolateDescriptor, MultiPercolateRequest>
	{
		public MultiPercolateInvalidApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiPercolate(f),
			fluentAsync: (c, f) => c.MultiPercolateAsync(f),
			request: (c, r) => c.MultiPercolate(r),
			requestAsync: (c, r) => c.MultiPercolateAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => false; //three out of 4 responses have an error
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_mpercolate";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new object[]
		{
			new Dictionary<string, object>{ { "percolate", new {} } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object>{ { "percolate", new {  index = "otherindex", id = 1 } } },
			new { },
			new Dictionary<string, object> { { "count", new { index = "otherindex", type = "othertype" } } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object> { { "count", new { id = 2 } } },
			new { }
		};

		protected override Func<MultiPercolateDescriptor, IMultiPercolateRequest> Fluent => m => m
			.Index(typeof(Project))
			.Type(typeof(Project))
			.Percolate<Project>(p => p.Document(Project.Instance))
			.Percolate<Project>(p => p.Index("otherindex").Id(1))
			.Count<Project>(p => p.Index("otherindex").Type("othertype").Document(Project.Instance))
			.Count<Project>(p => p.Id(2));

		protected override MultiPercolateRequest Initializer => new MultiPercolateRequest(typeof(Project), typeof(Project))
		{
			Percolations = new List<IPercolateOperation>
			{
				new PercolateRequest<Project>(Project.Instance),
				new PercolateRequest<Project>("otherindex", typeof(Project), 1),
				new PercolateCountRequest<Project>("otherindex", "othertype") { Document = Project.Instance },
				new PercolateCountRequest<Project>(2)
			}
		};

		protected override void ExpectResponse(IMultiPercolateResponse response)
		{
			var responses = response.Responses.ToList();
			responses.Should().HaveCount(4);
			foreach (var r in responses.Skip(1))
			{
				r.IsValid.Should().BeFalse();
				r.ServerError.Should().NotBeNull();
				r.ServerError.Error.Should().NotBeNull();

			}
			responses[1].ServerError.Error.Reason.Should().Be("no such index");
			responses[1].ServerError.Error.Type.Should().Be("index_not_found_exception");

			var validResponse = responses.First();
			validResponse.IsValid.Should().BeTrue();
			validResponse.Matches.Should().NotBeNull().And.BeEmpty();
			validResponse.Shards.Should().NotBeNull();
			validResponse.Shards.Total.Should().Be(validResponse.Shards.Successful);
		}
	}
}
