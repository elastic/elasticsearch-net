using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.Ilm
{
	[SkipVersion("<6.7.0", "All APIs exist in Elasticsearch 6.7.0")]
	public class IlmApiTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string IlmGetStatusStep = nameof(IlmGetStatusStep);
		private const string IlmPutLifecycleStep = nameof(IlmPutLifecycleStep);
		private const string IlmGetLifecycleStep = nameof(IlmGetLifecycleStep);
		private const string IlmDeleteLifecycleStep = nameof(IlmDeleteLifecycleStep);
		private const string PutDocumentStep = nameof(PutDocumentStep);
		private const string IlmExplainLifecycleStep = nameof(IlmExplainLifecycleStep);
		private const string IlmRemovePolicyStep = nameof(IlmRemovePolicyStep);
		private const string IlmStopStep = nameof(IlmStopStep);

		public IlmApiTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutDocumentStep, u => u.Calls<IndexDescriptor<Project>, IndexRequest<Project>, IIndexRequest<Project>, IIndexResponse>(
					v => new IndexRequest<Project>(Document),
					(v, d) => d,
					(v, c, f) => c.Index(Document, f),
					(v, c, f) => c.IndexAsync(Document, f),
					(v, c, r) => c.Index(r),
					(v, c, r) => c.IndexAsync(r)
				)
			},
			{
				IlmExplainLifecycleStep, u => u.Calls<IlmExplainLifecycleDescriptor, IlmExplainLifecycleRequest, IIlmExplainLifecycleRequest, IIlmExplainLifecycleResponse>(
					v => new IlmExplainLifecycleRequest("project"),
					(v, d) => d,
					(v, c, f) => c.IlmExplainLifecycle("project", f),
					(v, c, f) => c.IlmExplainLifecycleAsync("project", f),
					(v, c, r) => c.IlmExplainLifecycle(r),
					(v, c, r) => c.IlmExplainLifecycleAsync(r)
				)
			},
			{
				IlmGetStatusStep, u => u.Calls<IlmGetStatusDescriptor, IlmGetStatusRequest, IIlmGetStatusRequest, IIlmGetStatusResponse>(
					v => new IlmGetStatusRequest(),
					(v, d) => d,
					(v, c, f) => c.IlmGetStatus(f),
					(v, c, f) => c.IlmGetStatusAsync(f),
					(v, c, r) => c.IlmGetStatus(r),
					(v, c, r) => c.IlmGetStatusAsync(r)
				)
			},
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
			},
			{
				IlmRemovePolicyStep, u => u.Calls<IlmRemovePolicyDescriptor, IlmRemovePolicyRequest, IIlmRemovePolicyRequest, IIlmRemovePolicyResponse>(
					v => new IlmRemovePolicyRequest("project"),
					(v, d) => d,
					(v, c, f) => c.IlmRemovePolicy("project", f),
					(v, c, f) => c.IlmRemovePolicyAsync("project", f),
					(v, c, r) => c.IlmRemovePolicy(r),
					(v, c, r) => c.IlmRemovePolicyAsync(r)
				)
			},
			{
				IlmGetLifecycleStep, u => u.Calls<IlmGetLifecycleDescriptor, IlmGetLifecycleRequest, IIlmGetLifecycleRequest, IIlmGetLifecycleResponse>(
					v => new IlmGetLifecycleRequest("policy" + v),
					(v, d) => d.PolicyId("policy" + v),
					(v, c, f) => c.IlmGetLifecycle(f),
					(v, c, f) => c.IlmGetLifecycleAsync(f),
					(v, c, r) => c.IlmGetLifecycle(r),
					(v, c, r) => c.IlmGetLifecycleAsync(r)
				)
			},
			{
				IlmDeleteLifecycleStep, u => u.Calls<IlmDeleteLifecycleDescriptor, IlmDeleteLifecycleRequest, IIlmDeleteLifecycleRequest, IIlmDeleteLifecycleResponse>(
					v => new IlmDeleteLifecycleRequest("policy" + v),
					(v, d) => d,
					(v, c, f) => c.IlmDeleteLifecycle("policy" + v, f),
					(v, c, f) => c.IlmDeleteLifecycleAsync("policy" + v, f),
					(v, c, r) => c.IlmDeleteLifecycle(r),
					(v, c, r) => c.IlmDeleteLifecycleAsync(r)
				)
			},
			{
				IlmStopStep, u => u.Calls<IlmStopDescriptor, IlmStopRequest, IIlmStopRequest, IIlmStopResponse>(
					v => new IlmStopRequest(),
					(v, d) => d,
					(v, c, f) => c.IlmStop(f),
					(v, c, f) => c.IlmStopAsync(f),
					(v, c, r) => c.IlmStop(r),
					(v, c, r) => c.IlmStopAsync(r)
				)
			},
		}) { }

		private static Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = "Name",
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } },
			SourceOnly = null
		};

		private static DateTime FixedDate { get; } = new DateTime(2015, 06, 06, 12, 01, 02, 123);

		[I] public async Task IlmExplainLifecycleResponse() => await Assert<IlmExplainLifecycleResponse>(IlmExplainLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);

			var index = $"project";
			var hasIndex = r.Indices.TryGetValue(index, out var indexDict);

			hasIndex.Should().BeTrue($"expect `{index}` to be returned");
			indexDict.Should().NotBeNull($"expect `{index}`'s value not to be null");

			indexDict.Index.Should().Be("project");
			indexDict.Managed.Should().Be(false);

		});

		[I] public async Task IlmStopResponse() => await Assert<IlmStopResponse>(IlmStopStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task IlmRemovePolicyResponse() => await Assert<IlmRemovePolicyResponse>(IlmRemovePolicyStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.HasFailures.Should().Be(false);
			r.FailedIndexes.Should().BeEmpty();
		});

		[I] public async Task IlmGetStatusResponse() => await Assert<IlmGetStatusResponse>(IlmGetStatusStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.OperationMode.Should().Be(OperationMode.Running);
		});

		[I] public async Task IlmPutLifecycleResponse() => await Assert<IlmPutLifecycleResponse>(IlmPutLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task IlmGetLifecycleResponse() => await Assert<IlmGetLifecycleResponse>(IlmGetLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Policies.Should().NotBeEmpty();

			var policy = $"policy{v}";
			var hasPolicy = r.Policies.TryGetValue(policy, out var policyDict);

			hasPolicy.Should().BeTrue($"expect `{policy}` to be returned");
			policyDict.Should().NotBeNull($"expect `{policy}`'s value not to be null");

			policyDict.Version.Should().Be(1);
			policyDict.ModifiedDate.Should().BeBefore(DateTimeOffset.UtcNow);
			policyDict.Policy.Phases.Should().NotBe(null);

			policyDict.Policy.Phases.Warm.Should().NotBe(null);
			policyDict.Policy.Phases.Warm.MinimumAge.Should().Be(new Time("10d"));
			policyDict.Policy.Phases.Warm.Actions.Should().NotBeEmpty();
			policyDict.Policy.Phases.Warm.Actions.Should().HaveCount(1);

			var warmAction = policyDict.Policy.Phases.Warm.Actions.First();
			warmAction.Key.Should().Be("forcemerge");
			warmAction.Value.Should().BeOfType<ForceMergeLifecycleAction>();
			var forceMerge = warmAction.Value.As<ForceMergeLifecycleAction>();
			forceMerge.MaximumNumberSegments.Should().Be(1);

			policyDict.Policy.Phases.Delete.Should().NotBe(null);
			policyDict.Policy.Phases.Delete.MinimumAge.Should().Be(new Time("30d"));
			policyDict.Policy.Phases.Delete.Actions.Should().NotBeEmpty();
			policyDict.Policy.Phases.Delete.Actions.Should().HaveCount(1);

			var deleteAction = policyDict.Policy.Phases.Delete.Actions.First();
			deleteAction.Key.Should().Be("delete");
			deleteAction.Value.Should().BeOfType<DeleteLifecycleAction>();
		});

		[I] public async Task IlmDeleteLifecycleResponse() => await Assert<IlmDeleteLifecycleResponse>(IlmDeleteLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Acknowledged.Should().BeTrue();
		});
	}
}
