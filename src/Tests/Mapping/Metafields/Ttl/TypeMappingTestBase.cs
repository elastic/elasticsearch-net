using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using System.Collections.Generic;
using Elasticsearch.Net;
using static Nest.Infer;

#pragma warning disable 618

namespace Tests.Mapping.Metafields.Ttl
{
	public class TtlMetafieldApiTest : MetafieldsMappingApiTestsBase
	{
		public TtlMetafieldApiTest(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			_ttl = new { @default = "30m", enabled = true }
		};

		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => d => d.Index(CallIsolatedValue)
			.TtlField(ttl => ttl
				.Default("30m")
				.Enable()
			);

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, Type<Project>())
		{
			TtlField = new TtlField {  Default = TimeSpan.FromMinutes(30), Enabled = true }
		};
	}
}
