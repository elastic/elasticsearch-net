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

namespace Tests.Search.Percolator.RegisterPercolator
{
	[Collection(IntegrationContext.ReadOnly)]
	public class RegisterPercolatorApiTests
		: ApiIntegrationTestBase<IRegisterPercolateResponse, IRegisterPercolatorRequest, RegisterPercolatorDescriptor<Project>, RegisterPercolatorRequest>
	{
		public RegisterPercolatorApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.RegisterPercolator(_name, f),
			fluentAsync: (c, f) => c.RegisterPercolatorAsync(_name, f),
			request: (c, r) => c.RegisterPercolator(r),
			requestAsync: (c, r) => c.RegisterPercolatorAsync(r)
		);

		private string _name = "name-of-perc";

		protected override int ExpectStatusCode => 201;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/.percolator/{_name}";

		protected override RegisterPercolatorDescriptor<Project> NewDescriptor() => new RegisterPercolatorDescriptor<Project>(_name);
		
		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = "nest"
					}
				}
			},
			language = "c#",
			commits = 5000
		};

		protected override Func<RegisterPercolatorDescriptor<Project>, IRegisterPercolatorRequest> Fluent => r => r
			.Query(q => q
				.Match(m => m
					.OnField(p => p.Name)
					.Query("nest")
				)
			)
			.Metadata(md => md
				.Add("language", "c#")
				.Add("commits", 5000)
			);

		protected override RegisterPercolatorRequest Initializer => new RegisterPercolatorRequest(typeof(Project), _name)
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "nest"
			}),
			Metadata = new Dictionary<string, object>
			{
				{ "language", "c#" },
				{ "commits", 5000 }
			}
		};
	}
}
