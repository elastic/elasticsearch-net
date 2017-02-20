using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Framework.ManagedElasticsearch.NodeSeeders
{
	public class DefaultSeeder
	{
		public const string TestsIndexTemplateName = "nest_tests";
		public const string ProjectsAliasName = "projects-alias";

		private IElasticClient Client { get; }

		private readonly IIndexSettings _defaultIndexSettings = new IndexSettings
		{
			NumberOfShards = 2,
			NumberOfReplicas = 0
		};

		private IIndexSettings IndexSettings { get; }

		public DefaultSeeder(ElasticsearchNode node, IIndexSettings indexSettings)
		{
			this.Client = node.Client;
			this.IndexSettings = indexSettings ?? _defaultIndexSettings;
		}

		public DefaultSeeder(ElasticsearchNode node) : this(node, null) { }

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			this.DeleteIndicesAndTemplates();
			// and now recreate everything
			this.CreateIndicesAndSeedIndexData();
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If raw_fields exists assume this cluster is already seeded.
		private bool AlreadySeeded() => this.Client.IndexTemplateExists(TestsIndexTemplateName).Exists;

		public void DeleteIndicesAndTemplates()
		{
			if (this.Client.IndexTemplateExists(TestsIndexTemplateName).Exists)
				this.Client.DeleteIndexTemplate(TestsIndexTemplateName);
			if (this.Client.IndexExists(Infer.Indices<Project>()).Exists)
				this.Client.DeleteIndex(typeof(Project));
			if (this.Client.IndexExists(Infer.Indices<Developer>()).Exists)
				this.Client.DeleteIndex(typeof(Developer));
		}

		public void CreateIndices()
		{
			CreateRawFieldsIndexTemplate();
			CreateIndexTemplate();
			CreateProjectIndex();
			CreateDeveloperIndex();
		}

		private void SeedIndexData()
		{
			this.Client.IndexMany(Project.Projects);
			this.Client.IndexMany(Developer.Developers);
			this.Client.Bulk(b => b
				.IndexMany(
					CommitActivity.CommitActivities,
					(d, c) => d.Document(c).Parent(c.ProjectName)
				)
			);
			this.Client.Refresh(Nest.Indices.Index(typeof(Project), typeof(Developer)));
		}
		private void WaitForIndexCreation(IndexName index)
		{
			var wait = this.Client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow).Index(index));
			wait.ShouldBeValid();
		}
		private void CreateIndicesAndSeedIndexData()
		{
			this.CreateIndices();
			this.SeedIndexData();
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate(new PutIndexTemplateRequest(TestsIndexTemplateName)
			{
				Template = "*",
				Settings = this.IndexSettings
			});
			putTemplateResult.IsValid.Should().BeTrue();
		}

		private void CreateDeveloperIndex()
		{
			var createDeveloperIndex = this.Client.CreateIndex(Infer.Index<Developer>(), c => c
				.Mappings(map => map
					.Map<Developer>(m => m
						.AutoMap()
						.Properties(DeveloperProperties)
					)
				)
			);
			createDeveloperIndex.IsValid.Should().BeTrue();
			this.WaitForIndexCreation(Index<Developer>());
		}

		private void CreateRawFieldsIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate("raw_fields", p => p
				.Template("*") //match on all created indices
				.Settings(s => s
					.NumberOfReplicas(0)
					.NumberOfShards(2)
				)
				.Mappings(pm => pm
					.Map("_default_", m => m
						.DynamicTemplates(dt => dt
							.DynamicTemplate("raw_field", dtt => dtt
								.Match("*") //matches all fields
								.MatchMappingType("string") //that are a string
								.Mapping(tm => tm
									.String(sm => sm //map as string
										.Fields(f => f //with a multifield 'raw' that is not analyzed
											.String(ssm => ssm.Name("raw").Index(FieldIndexOption.NotAnalyzed))
										)
									)
								)
							)
						)
					)
				)
			);
			putTemplateResult.ShouldBeValid();
		}


		private void CreateProjectIndex()
		{
			var createProjectIndex = this.Client.CreateIndex(typeof(Project), c => c
				.Settings(settings => settings
					.Analysis(ProjectAnalysisSettings)
				)
				.Aliases(a => a
					.Alias(ProjectsAliasName)
				)
				.Mappings(map => map
#pragma warning disable 618
					.Map<Project>(m => m
						.AutoMap()
						.TimestampField(t => t
							.Enabled()
						)
						.TtlField(t => t
							.Enable()
							.Default("7d")
						)
#pragma warning restore 618
						.Properties(ProjectProperties)
					)
#pragma warning disable 618
					.Map<CommitActivity>(m => m
						.AutoMap()
						.Parent<Project>()
						.TimestampField(t => t
							.Enabled()
						)
#pragma warning restore 618
						.Properties(props => props
							.Object<Developer>(o => o
								.AutoMap()
								.Name(p => p.Committer)
								.Properties(DeveloperProperties)
							)
							.String(t => t
								.Name(p => p.ProjectName)
								.NotAnalyzed()
							)
						)
					)
				)
			);
			createProjectIndex.IsValid.Should().BeTrue();
			this.WaitForIndexCreation(Index<Project>());
		}

		public static IAnalysis ProjectAnalysisSettings(AnalysisDescriptor analysis)
		{
			analysis
				.TokenFilters(tokenFilters => tokenFilters
					.Shingle("shingle", shingle => shingle
						.MinShingleSize(2)
						.MaxShingleSize(4)
					)
				)
				.Analyzers(analyzers => analyzers
					.Custom("shingle", shingle => shingle
						.Filters("standard", "shingle")
						.Tokenizer("standard")
					)
				);
			return analysis;
		}


		public static PropertiesDescriptor<Project> ProjectProperties(PropertiesDescriptor<Project> props) => props
			.String(s => s
				.Name(p => p.Name)
				.NotAnalyzed()
				.Store()
				.Fields(fs => fs
					.String(ss => ss
						.Name("standard")
						.Analyzer("standard")
					)
					.Completion(cm => cm
						.Name("suggest")
					)
				)
			)
			.String(s => s
				.Name(p => p.Description)
				.Fields(f => f
					.String(t => t
						.Name("shingle")
						.Analyzer("shingle")
					)
				)
			)
			.Date(d => d
				.Store()
				.Name(p => p.StartedOn)
			)
			.String(d => d
				.Store()
				.Name(p => p.DateString)
			)
			.String(d => d
				.NotAnalyzed()
				.Name(p => p.State)
				.Fields(fs => fs
					.String(st => st
						.Name("offsets")
						.IndexOptions(IndexOptions.Offsets)
					)
				)
			)
			.Nested<Tag>(mo => mo
				.AutoMap()
				.Name(p => p.Tags)
				.Properties(TagProperties)
			)
			.Object<Developer>(o => o
				.AutoMap()
				.Name(p => p.LeadDeveloper)
				.Properties(DeveloperProperties)
			)
			.GeoPoint(g => g
				.Name(p => p.Location)
			)
			.Completion(cm => cm
				.Name(p => p.Suggest)
				.Payloads()
				.Context(cx => cx
					.Category("color", c => c
						.Default("color")
					)
				)
			)
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.Integer)
				.Store()
			)
			.Object<Dictionary<string, Metadata>>(o => o
				.Name(p => p.Metadata)
			);

		private static PropertiesDescriptor<Tag> TagProperties(PropertiesDescriptor<Tag> props) => props
			.String(s => s
				.NotAnalyzed()
				.Name(p => p.Name)
				.Fields(f => f
					.String(st => st
						.Name("vectors")
						.TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
					)
				)
			);

		private static PropertiesDescriptor<Developer> DeveloperProperties(PropertiesDescriptor<Developer> props) => props
			.String(s => s
				.NotAnalyzed()
				.Name(p => p.OnlineHandle)
			)
			.String(s => s
				.NotAnalyzed()
				.Name(p => p.Gender)
			)
			.String(s => s
				.Name(p => p.FirstName)
				.TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
			)
			.Ip(s => s
				.Name(p => p.IPAddress)
			)
			.GeoPoint(g => g
				.Name(p => p.Location)
			);

	}
}
