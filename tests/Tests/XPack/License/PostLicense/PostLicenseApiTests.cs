// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.License.PostLicense
{
	[SkipVersion("<2.3.0", "")]
	public class PostLicenseApiTests : ApiTestBase<XPackCluster, PostLicenseResponse, IPostLicenseRequest, PostLicenseDescriptor, PostLicenseRequest>
	{
		public PostLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			license = new
			{
				expiry_date_in_millis = 1,
				issue_date_in_millis = 2,
				issued_to = "nest test framework",
				issuer = "martijn",
				max_nodes = 20,
				signature = "<redacted>",
				type = "gold",
				uid = "uuid"
			}
		};

		protected override Func<PostLicenseDescriptor, IPostLicenseRequest> Fluent => d => d
			.Acknowledge()
			.License(FakeLicense);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PostLicenseRequest Initializer => new PostLicenseRequest
		{
			Acknowledge = true,
			License = FakeLicense
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_license?acknowledge=true";

		private Nest.License FakeLicense { get; } = new Nest.License
		{
			UID = "uuid",
			ExpiryDateInMilliseconds = 1,
			IssueDateInMilliseconds = 2,
			IssuedTo = "nest test framework",
			Issuer = "martijn",
			Type = LicenseType.Gold,
			MaxNodes = 20,
			Signature = "<redacted>"
		};

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.Post(f),
			(client, f) => client.License.PostAsync(f),
			(client, r) => client.License.Post(r),
			(client, r) => client.License.PostAsync(r)
		);
	}
}
