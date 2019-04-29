using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.Ilm
{
	[SkipVersion("<6.7.0", "All APIs exist in Elasticsearch 6.7.0")]
	public class IlmApiTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string IlmPutLifecycleStep = nameof(IlmPutLifecycleStep);
		public IlmApiTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				IlmPutLifecycleStep, u => u.Calls<IlmPutLifecycleDescriptor, IlmPutLifecycleRequest, IIlmPutLifecycleRequest, IIlmPutLifecycleResponse>(
					v => new IlmPutLifecycleRequest("policy" + v)
					{
						Policy = new Policy
						{
							Phases = new Phases
							{
								Warm = new Phase
								{
									MinimumAge = "10d",
									Actions = new LifecycleActions
									{
										new ForceMergeLifecycleAction
										{
											MaximumNumberSegments = 1
										}
									}
								},
								Delete = new Phase
								{
									MinimumAge = "30d",
									Actions = new LifecycleActions
									{
										new DeleteLifecycleAction()
									}
								}
							}
						}
					},
					(v, d) => d
						.Policy(p => p.Phases(a => a.Warm(w => w.MinimumAge("10d")
																.Actions(ac => ac.ForceMerge(f => f.MaximumNumberSegments(1))))
													.Delete(w => w.MinimumAge("30d")
		       													  .Actions(ac => ac.Delete(f => f)))))
					,
					(v, c, f) => c.IlmPutLifecycle("policy" + v, f),
					(v, c, f) => c.IlmPutLifecycleAsync("policy" + v, f),
					(v, c, r) => c.IlmPutLifecycle(r),
					(v, c, r) => c.IlmPutLifecycleAsync(r)
				)
			}
		}) { }

		[I] public async Task IlmPutLifecycleResponse() => await Assert<IlmPutLifecycleResponse>(IlmPutLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Acknowledged.Should().BeTrue();
		});
	}
}
