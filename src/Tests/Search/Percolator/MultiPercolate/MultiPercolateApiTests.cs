using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;

namespace Tests.Search.Percolator.MultiPercolate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiPercolateApiTests
		: ApiIntegrationTestBase<IMultiPercolateResponse, IMultiPercolateRequest, MultiPercolateDescriptor, MultiPercolateRequest>
	{
		public MultiPercolateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiPercolate(f),
			fluentAsync: (c, f) => c.MultiPercolateAsync(f),
			request: (c, r) => c.MultiPercolate(r),
			requestAsync: (c, r) => c.MultiPercolateAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_mpercolate";

		protected override bool SupportsDeserialization => false;
	
		protected override object ExpectJson => new object[]
		{
			new Dictionary<string, object>{ { "percolate", new {} } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object>{ { "percolate", new {  index = "otherindex", id = "1" } } },
			new { },
			new Dictionary<string, object> { { "count", new { index = "otherindex", type = "othertype" } } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object> { { "count", new { id = "2" } } },
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
	}
}
