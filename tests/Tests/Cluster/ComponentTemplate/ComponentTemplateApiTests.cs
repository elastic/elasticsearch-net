using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ComponentTemplate
{
	[SkipVersion("<7.8.0", "Introduced in 7.8.0 ")]
	public class ComponentTemplateApiTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string InitialExistsStep = nameof(InitialExistsStep);
		private const string PutStep = nameof(PutStep);
		private const string ExistsStep = nameof(ExistsStep);
		private const string GetStep = nameof(GetStep);
		private const string DeleteStep = nameof(DeleteStep);

		public ComponentTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
		{
			{InitialExistsStep, u =>
				u.Calls<ComponentTemplateExistsDescriptor, ComponentTemplateExistsRequest, IComponentTemplateExistsRequest, ExistsResponse>(
					v => new ComponentTemplateExistsRequest(v),
					(v, d) => d,
					(v, c, f) => c.Cluster.ComponentTemplateExists(v, f),
					(v, c, f) => c.Cluster.ComponentTemplateExistsAsync(v, f),
					(v, c, r) => c.Cluster.ComponentTemplateExists(r),
					(v, c, r) => c.Cluster.ComponentTemplateExistsAsync(r)
				)
			},
			{PutStep, u =>
				u.Calls<PutComponentTemplateDescriptor, PutComponentTemplateRequest, IPutComponentTemplateRequest, PutComponentTemplateResponse>(
					v => new PutComponentTemplateRequest(v)
					{
						Version = 2,
						Meta = new Dictionary<string, object>
						{
							{ "foo", "bar" }
						},
						Template = new Nest.Template
						{
							Settings = new IndexSettings
							{
								NumberOfShards = 2,
								NumberOfReplicas = 0
							},
							Aliases = new Aliases
							{
								{ v + "-alias", new Alias() }
							},
							Mappings = new TypeMapping
							{
								Properties = new Properties<Project>
								{
									{ p => p.Name, new KeywordProperty() }
								}
							}
						}
					},
					(v, d) => d
						.Version(2)
						.Meta(d => d
							.Add("foo", "bar")
						)
						.Template(t => t
							.Settings(s => s
								.NumberOfShards(2)
								.NumberOfReplicas(0)
							)
							.Aliases(a => a
								.Alias(v + "-alias", al => al)
							)
							.Mappings<Project>(m => m
								.Properties(p => p
									.Keyword(k => k
										.Name(n => n.Name)
									)
								)
							)
						),
					(v, c, f) => c.Cluster.PutComponentTemplate(v, f),
					(v, c, f) => c.Cluster.PutComponentTemplateAsync(v, f),
					(v, c, r) => c.Cluster.PutComponentTemplate(r),
					(v, c, r) => c.Cluster.PutComponentTemplateAsync(r)
				)
			},
			{ExistsStep, u =>
				u.Calls<ComponentTemplateExistsDescriptor, ComponentTemplateExistsRequest, IComponentTemplateExistsRequest, ExistsResponse>(
					v => new ComponentTemplateExistsRequest(v),
					(v, d) => d,
					(v, c, f) => c.Cluster.ComponentTemplateExists(v, f),
					(v, c, f) => c.Cluster.ComponentTemplateExistsAsync(v, f),
					(v, c, r) => c.Cluster.ComponentTemplateExists(r),
					(v, c, r) => c.Cluster.ComponentTemplateExistsAsync(r)
				)
			},
			{GetStep, u =>
				u.Calls<GetComponentTemplateDescriptor, GetComponentTemplateRequest, IGetComponentTemplateRequest, GetComponentTemplateResponse>(
					v => new GetComponentTemplateRequest(v),
					(v, d) => d,
					(v, c, f) => c.Cluster.GetComponentTemplate(v, f),
					(v, c, f) => c.Cluster.GetComponentTemplateAsync(v, f),
					(v, c, r) => c.Cluster.GetComponentTemplate(r),
					(v, c, r) => c.Cluster.GetComponentTemplateAsync(r)
				)
			},
			{DeleteStep, u =>
				u.Calls<DeleteComponentTemplateDescriptor, DeleteComponentTemplateRequest, IDeleteComponentTemplateRequest, DeleteComponentTemplateResponse>(
					v => new DeleteComponentTemplateRequest(v),
					(v, d) => d,
					(v, c, f) => c.Cluster.DeleteComponentTemplate(v, f),
					(v, c, f) => c.Cluster.DeleteComponentTemplateAsync(v, f),
					(v, c, r) => c.Cluster.DeleteComponentTemplate(r),
					(v, c, r) => c.Cluster.DeleteComponentTemplateAsync(r)
				)
			},
		}) { }

		[I] public async Task InitialExistsResponse() => await Assert<ExistsResponse>(InitialExistsStep, (v, r) =>
		{
			r.IsValid.Should().BeFalse();
			r.Exists.Should().BeFalse();
		});

		[I] public async Task PutResponse() => await Assert<PutComponentTemplateResponse>(PutStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task ExistsResponse() => await Assert<ExistsResponse>(ExistsStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Exists.Should().BeTrue();
		});

		[I] public async Task GetResponse() => await Assert<GetComponentTemplateResponse>(GetStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.ComponentTemplates.Should().NotBeNull().And.NotBeEmpty();

			var namedComponentTemplate = r.ComponentTemplates.FirstOrDefault(c => c.Name == v);
			namedComponentTemplate.Should().NotBeNull();
			namedComponentTemplate.Name.Should().Be(v);
			namedComponentTemplate.ComponentTemplate.Should().NotBeNull();
			namedComponentTemplate.ComponentTemplate.Template.Should().NotBeNull();
			namedComponentTemplate.ComponentTemplate.Version.Should().NotBeNull();
			namedComponentTemplate.ComponentTemplate.Meta.Should().NotBeNull().And.ContainKey("foo");
			namedComponentTemplate.ComponentTemplate.Template.Settings.Should().NotBeNull();
			namedComponentTemplate.ComponentTemplate.Template.Aliases.Should().NotBeNull();
			namedComponentTemplate.ComponentTemplate.Template.Mappings.Should().NotBeNull();
		});

		[I] public async Task DeleteResponse() => await Assert<DeleteComponentTemplateResponse>(DeleteStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}
