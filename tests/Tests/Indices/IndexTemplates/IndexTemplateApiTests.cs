// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

namespace Tests.Indices.IndexTemplates
{
	[SkipVersion("<7.8.0", "Introduced in 7.8.0 ")]
	public class IndexTemplateApiTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string InitialExistsStep = nameof(InitialExistsStep);
		private const string PutStep = nameof(PutStep);
		private const string ExistsStep = nameof(ExistsStep);
		private const string GetStep = nameof(GetStep);
		private const string DeleteStep = nameof(DeleteStep);

		public IndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
		{
			{InitialExistsStep, u =>
				u.Calls<IndexTemplateV2ExistsDescriptor, IndexTemplateV2ExistsRequest, IIndexTemplateV2ExistsRequest, ExistsResponse>(
					v => new IndexTemplateV2ExistsRequest(v),
					(v, d) => d,
					(v, c, f) => c.Indices.TemplateV2Exists(v, f),
					(v, c, f) => c.Indices.TemplateV2ExistsAsync(v, f),
					(v, c, r) => c.Indices.TemplateV2Exists(r),
					(v, c, r) => c.Indices.TemplateV2ExistsAsync(r)
				)
			},
			{PutStep, u =>
				u.Calls<PutIndexTemplateV2Descriptor, PutIndexTemplateV2Request, IPutIndexTemplateV2Request, PutIndexTemplateV2Response>(
					v => new PutIndexTemplateV2Request(v)
					{
						IndexPatterns = new [] {"foo", "bar"},
						Version = 2,
						Meta = new Dictionary<string, object>
						{
							{ "foo", "bar" }
						},
						Template = new Template
						{
							Settings = new Nest.IndexSettings
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
						.IndexPatterns("foo", "bar")
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
					(v, c, f) => c.Indices.PutTemplateV2(v, f),
					(v, c, f) => c.Indices.PutTemplateV2Async(v, f),
					(v, c, r) => c.Indices.PutTemplateV2(r),
					(v, c, r) => c.Indices.PutTemplateV2Async(r)
				)
			},
			{"WaitStep", u => u.Call(async (v, c) =>
			{
				await Task.Delay(500); // allow template to be fully created
			})},
			{ExistsStep, u =>
				u.Calls<IndexTemplateV2ExistsDescriptor, IndexTemplateV2ExistsRequest, IIndexTemplateV2ExistsRequest, ExistsResponse>(
					v => new IndexTemplateV2ExistsRequest(v),
					(v, d) => d,
					(v, c, f) => c.Indices.TemplateV2Exists(v, f),
					(v, c, f) => c.Indices.TemplateV2ExistsAsync(v, f),
					(v, c, r) => c.Indices.TemplateV2Exists(r),
					(v, c, r) => c.Indices.TemplateV2ExistsAsync(r)
				)
			},
			{GetStep, u =>
				u.Calls<GetIndexTemplateV2Descriptor, GetIndexTemplateV2Request, IGetIndexTemplateV2Request, GetIndexTemplateV2Response>(
					v => new GetIndexTemplateV2Request(v),
					(v, d) => d,
					(v, c, f) => c.Indices.GetTemplateV2(v, f),
					(v, c, f) => c.Indices.GetTemplateV2Async(v, f),
					(v, c, r) => c.Indices.GetTemplateV2(r),
					(v, c, r) => c.Indices.GetTemplateV2Async(r)
				)
			},
			{DeleteStep, u =>
				u.Calls<DeleteIndexTemplateV2Descriptor, DeleteIndexTemplateV2Request, IDeleteIndexTemplateV2Request, DeleteIndexTemplateV2Response>(
					v => new DeleteIndexTemplateV2Request(v),
					(v, d) => d,
					(v, c, f) => c.Indices.DeleteTemplateV2(v, f),
					(v, c, f) => c.Indices.DeleteTemplateV2Async(v, f),
					(v, c, r) => c.Indices.DeleteTemplateV2(r),
					(v, c, r) => c.Indices.DeleteTemplateV2Async(r)
				)
			},
		}) { }

		[I] public async Task InitialExistsResponse() => await Assert<ExistsResponse>(InitialExistsStep, (v, r) =>
		{
			r.IsValid.Should().BeFalse();
			r.Exists.Should().BeFalse();
		});

		[I] public async Task PutResponse() => await Assert<PutIndexTemplateV2Response>(PutStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task ExistsResponse() => await Assert<ExistsResponse>(ExistsStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Exists.Should().BeTrue();
		});

		[I] public async Task GetResponse() => await Assert<GetIndexTemplateV2Response>(GetStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.IndexTemplates.Should().NotBeNull().And.NotBeEmpty();

			var indexTemplate = r.IndexTemplates.FirstOrDefault(c => c.Name == v);
			indexTemplate.Should().NotBeNull();
			indexTemplate.Name.Should().Be(v);
			indexTemplate.IndexTemplate.Should().NotBeNull();
			indexTemplate.IndexTemplate.Template.Should().NotBeNull();
			indexTemplate.IndexTemplate.Version.Should().NotBeNull();
			indexTemplate.IndexTemplate.Meta.Should().NotBeNull().And.ContainKey("foo");
			indexTemplate.IndexTemplate.Template.Settings.Should().NotBeNull();
			indexTemplate.IndexTemplate.Template.Aliases.Should().NotBeNull();
			indexTemplate.IndexTemplate.Template.Mappings.Should().NotBeNull();
		});

		[I] public async Task DeleteResponse() => await Assert<DeleteIndexTemplateV2Response>(DeleteStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}
