using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Nest.Tests.Literate;

namespace SearchApis
{
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
}
