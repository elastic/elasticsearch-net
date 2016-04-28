using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.License.PostLicense
{
	[Collection(IntegrationContext.ReadOnly)]
	[SkipVersion("<2.3.0", "")]
	public class PostLicenseApiTests : ApiTestBase<IPostLicenseResponse, IPostLicenseRequest, PostLicenseDescriptor, PostLicenseRequest>
	{
		public PostLicenseApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PostLicense(f),
			fluentAsync: (client, f) => client.PostLicenseAsync(f),
			request: (client, r) => client.PostLicense(r),
			requestAsync: (client, r) => client.PostLicenseAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_license?acknowledge=true";

		protected override bool SupportsDeserialization => false;

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

		protected override Func<PostLicenseDescriptor, IPostLicenseRequest> Fluent => d => d
			.Acknowledge()
			.License(this.FakeLicense);

		protected override PostLicenseRequest Initializer => new PostLicenseRequest
		{
			Acknowledge = true,
			License= this.FakeLicense
		};
	}
}
