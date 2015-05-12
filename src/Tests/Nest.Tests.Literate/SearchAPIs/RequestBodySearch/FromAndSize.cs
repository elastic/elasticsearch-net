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
		public abstract class SearchUsageBase : EndpointUsageTests<ISearchResponse<object>, ISearchRequest, SearchDescriptor<object>, SearchRequest>
		{
			protected override void ClientUsage() =>
				this.Calls(
					fluent: (client, f) => client.Search<object>(f),
					fluentAsync: (client, f) => client.SearchAsync<object>(f),
					request: (client, r) => client.Search<object>(r),
					requestAsync: (client, r) => client.SearchAsync<object>(r)
				);
		}

		public class Usage : SearchUsageBase
		{
			protected override object ExpectedJson { get; } =
				new {from = 10, size = 12};

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(string url) => url.Should().EndWith("");

			protected override SearchRequest Initializer =>
				new SearchRequest()
				{ 
					From = 10,
					Size = 12
				};

			protected override Func<SearchDescriptor<object>, ISearchRequest> Fluent => s => s
					.From(10)
					.Size(12);
		}
	}
}
