using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Document.Multiple.MultiGet
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiGetSimplifiedApiTests : ApiIntegrationTestBase<IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
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
		protected override string UrlPath => $"/dev/developer/_mget";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new 
		{
			ids = this._ids
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Developer>()
			.Type<Developer>()
			.GetMany<Developer>(this._ids);
			

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Developer>(), Type<Developer>())
		{
			Documents = this._ids
				.Select(n=>new MultiGetOperation<Developer>(n))
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.Documents.Should().NotBeEmpty().And.HaveCount(10);
			foreach (var hit in r.Documents)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		});
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class MultiGetApiTests : ApiIntegrationTestBase<IMultiGetResponse, IMultiGetRequest, MultiGetDescriptor, MultiGetRequest>
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
			docs = Developer.Developers.Select(p => new { _type = "project", _id = p.Id, _routing = p.Id, _source = false }).Take(10)
		};

		protected override Func<MultiGetDescriptor, IMultiGetRequest> Fluent => d => d
			.Index<Developer>()
			.GetMany<Developer>(this._ids, (g, i) => g.Routing(i.ToString()).Source(false));

		protected override MultiGetRequest Initializer => new MultiGetRequest(Index<Developer>())
		{
			Documents = this._ids
				.Select(n=>new MultiGetOperation<Developer>(n) { Routing = n.ToString(), Source = false })
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.Documents.Should().NotBeEmpty().And.HaveCount(10);
			foreach (var hit in r.Documents)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		});
	}
}
