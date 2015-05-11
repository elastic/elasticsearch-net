using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Nest.Tests.Literate;
using Ploeh.AutoFixture;
using Xunit;
using FluentAssertions;

namespace SearchApis.RequestBody
{
	public class FromAndSize 
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */

		public class Usage : EndpointUsageTests<ISearchResponse<object>>
		{
			protected override object ExpectedJson { get; } =
				new {from = 10, size = 12};

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(string url) => url.Should().EndWith("");

			protected override ISearchResponse<object> Initializer(IElasticClient client) =>
				client.Search<object>(new SearchRequest()
				{ 
					From = 10,
					Size = 12
				});

			protected override ISearchResponse<object> Fluent(IElasticClient client) =>
				client.Search<object>(s => s
					.From(10)
					.Size(12)
				);

		}
		
		//[ESVM(single=true, nodes=3)]
		public class ClusterThing : EndpointUsageTests<IHealthResponse>
		{
			protected override object ExpectedJson { get; } = new object {};

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(string url) => url.Should().Be("");

			protected override IHealthResponse Initializer(IElasticClient client) =>
				client.ClusterHealth(new ClusterHealthRequest());

			protected override IHealthResponse Fluent(IElasticClient client) =>
				client.ClusterHealth(h => h);
		}
	}
}
