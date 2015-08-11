using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Mapping
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class MapUsageBase
		: EndpointUsageBase<IIndicesResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		protected MapUsageBase(ReadOnlyCluster cluster, ApiUsage usage) : base(cluster, usage) { }

		public override bool ExpectIsValid => true;

		public override int ExpectStatusCode => 201;

		public override string UrlPath => "/project/project/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Map(f),
			fluentAsync: (client, f) => client.MapAsync(f),
			request: (client, r) => client.Map(r),
			requestAsync: (client, r) => client.MapAsync(r)
		);

		protected override PutMappingDescriptor<Project> Descriptor() => 
			new PutMappingDescriptor<Project>(TestClient.GetDefaultConnectionSettings());
	}
}
