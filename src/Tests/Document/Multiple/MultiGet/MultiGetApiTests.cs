using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.MultiGet
{
	public class MultiGetSimplifiedApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetSimplifiedApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		private IEnumerable<long> _ids = Developer.Developers.Select(d => (long)d.Id).Take(10);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/devs/developer/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			ids = this._ids
		};

		protected override void ExpectResponse(IMultiGetResponse response)
		{
			response.Hits.Should().NotBeEmpty().And.HaveCount(10);
			foreach (var document in response.Hits)
			{
				document.Index.Should().NotBeNullOrWhiteSpace();
				document.Type.Should().NotBeNullOrWhiteSpace();
				document.Id.Should().NotBeNullOrWhiteSpace();
				document.Found.Should().BeTrue();
			}
		}

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Developer>()
			.Type<Developer>()
			.GetMany<Developer>(this._ids);


		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Developer>(), Type<Developer>())
		{
			Documents = this._ids
				.Select(n=>new MultiGetOperation<Developer>(n))
		};
	}

	public class MultiGetApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		private IEnumerable<long> _ids = Developer.Developers.Select(d => (long)d.Id).Take(10);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/devs/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			docs = Developer.Developers.Select(p => new { _type = "developer", _id = p.Id, _routing = p.Id.ToString(), _source = false }).Take(10)
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Developer>()
			.GetMany<Developer>(this._ids, (g, i) => g.Routing(i.ToString()).Source(false));

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Developer>())
		{
			Documents = this._ids
				.Select(n=>new MultiGetOperation<Developer>(n) { Routing = n.ToString(), Source = false })
		};

		protected override void ExpectResponse(IMultiGetResponse response)
		{
			response.Hits.Should().NotBeEmpty().And.HaveCount(10);
			foreach (var hit in response.Hits)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
			foreach (var document in response.SourceMany<Project>(this._ids))
			{
				document.ShouldAdhereToSourceSerializerWhenSet();
			}
		}
	}


	public class MultiGetMetadataApiTests : ApiIntegrationTestBase<ReadOnlyCluster,IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetMetadataApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		private IEnumerable<string> _ids = Project.Projects.Select(d => d.Name).Take(10);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/doc/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			ids = this._ids
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Project>()
			.Type<Project>()
			.GetMany<Project>(this._ids);

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Project>(), Type<Project>())
		{
			Documents = this._ids.Select(n => new MultiGetOperation<Project>(n))
		};

		protected override void ExpectResponse(IMultiGetResponse response)
		{
			response.Hits.Should().NotBeEmpty().And.HaveCount(10);

			foreach (var hit in response.GetMany<Project>(_ids))
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
				hit.Version.Should().Be(1);
				hit.Source.ShouldAdhereToSourceSerializerWhenSet();
			}
		}
	}

	public class MultiGetParentApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
	{
		public MultiGetParentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiGet(f),
			fluentAsync: (client, f) => client.MultiGetAsync(f),
			request: (client, r) => client.MultiGet(r),
			requestAsync: (client, r) => client.MultiGetAsync(r)
		);

		private IEnumerable<CommitActivity> _activities = CommitActivity.CommitActivities.Take(10);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/doc/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			docs = _activities.Select(p => new { _id = p.Id, _routing = p.ProjectName })
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Project>()
			.Type<CommitActivity>()
			.GetMany<CommitActivity>(this._activities.Select(c => c.Id), (m, id) => m.Routing(_activities.Single(a => a.Id == id).ProjectName));

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Project>(), Type<CommitActivity>())
		{
			Documents = this._activities.Select(n => new MultiGetOperation<CommitActivity>(n.Id) { Routing = n.ProjectName })
		};

		protected override void ExpectResponse(IMultiGetResponse response)
		{
			response.Hits.Should().NotBeEmpty().And.HaveCount(10);

			foreach (var hit in response.GetMany<CommitActivity>(_activities.Select(c => c.Id)))
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
				hit.Version.Should().Be(1);
#pragma warning disable 618
				hit.Parent.Should().BeNull();
#pragma warning restore 618
				hit.Routing.Should().NotBeNullOrEmpty();
			}
		}
	}
}
