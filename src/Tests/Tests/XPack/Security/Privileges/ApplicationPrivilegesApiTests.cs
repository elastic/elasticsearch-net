using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.Privileges
{
	public class ApplicationPrivilegesApiTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutPrivilegesStep = nameof(PutPrivilegesStep);

		public ApplicationPrivilegesApiTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutPrivilegesStep, u => u.Calls<PutPrivilegesDescriptor, PutPrivilegesRequest, IPutPrivilegesRequest, IPutPrivilegesResponse>(
					v => new PutPrivilegesRequest
					{
						Applications = new AppPrivileges
						{
							{
								"app", new Nest.Privileges
								{
									{
										"p1", new PrivilegesActions
										{
											Actions = new[] { "data:read/*", "action:login" },
											Metadata = new Dictionary<string, object>
											{
												{ "key1", "val1a" },
												{ "key2", "val2a" }
											}
										}
									}
								}
							}
						}
					},
					(v, d) => d
						.Applications(a => a
							.Application("app", pr => pr
								.Privilege("p1", ac => ac
									.Actions("data:read/*", "action:login")
									.Metadata(m => m
										.Add("key1", "val1a")
										.Add("key2", "val2a")
									)
								)
							)
						),
					(v, c, f) => c.PutPrivileges(f),
					(v, c, f) => c.PutPrivilegesAsync(f),
					(v, c, r) => c.PutPrivileges(r),
					(v, c, r) => c.PutPrivilegesAsync(r)
				)
			}
		}) { }
	}
}
