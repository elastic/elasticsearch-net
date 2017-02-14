using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Framework.Integration
{
	public class Seeder
	{
		private IElasticClient Client { get; }

		private readonly IIndexSettings DefaultIndexSettings = new IndexSettings
		{
			NumberOfShards = 2,
			NumberOfReplicas = 0
		};

		private IIndexSettings IndexSettings { get; }

		public Seeder(ElasticsearchNode node, IIndexSettings indexSettings)
		{
			this.Client = node.Client;
			this.IndexSettings = indexSettings ?? DefaultIndexSettings;
		}

		public Seeder(ElasticsearchNode node) : this(node, null) { }

		public void SeedNode()
		{
			if (TestClient.Configuration.ForceReseed || !AlreadySeeded())
			{
				// Ensure a clean slate by deleting everything regardless of whether they may already exist
				this.DeleteIndicesAndTemplates();
				// and now recreate everything
				this.CreateIndicesAndSeedIndexData();
			}
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If raw_fields exists assume this cluster is already seeded.
		private bool AlreadySeeded() => this.Client.IndexTemplateExists("nest_tests").Exists;

		public void DeleteIndicesAndTemplates()
		{
			if (this.Client.IndexTemplateExists("nest_tests").Exists)
				this.Client.DeleteIndexTemplate("nest_tests");
			if (this.Client.IndexExists(Indices<Project>()).Exists)
				this.Client.DeleteIndex(typeof(Project));
			if (this.Client.IndexExists(Indices<Developer>()).Exists)
				this.Client.DeleteIndex(typeof(Developer));
			if (this.Client.IndexExists(Indices<PercolatedQuery>()).Exists)
				this.Client.DeleteIndex(typeof(PercolatedQuery));
		}

		public void CreateIndices()
		{
			CreateIndexTemplate();
			CreateProjectIndex();
			CreateDeveloperIndex();
			CreatePercolatorIndex();
		}

		private void SeedIndexData()
		{
			this.Client.IndexMany(Project.Projects);
			this.Client.IndexMany(Developer.Developers);
			this.Client.Index(new PercolatedQuery
			{
				Id = "1",
				Query = new MatchAllQuery()
			});
			this.Client.Bulk(b => b
				.IndexMany(
					CommitActivity.CommitActivities,
					(d, c) => d.Document(c).Parent(c.ProjectName)
				)
			);
			this.Client.Refresh(Nest.Indices.Index(typeof(Project), typeof(Developer), typeof(PercolatedQuery)));
		}

		private void CreateIndicesAndSeedIndexData()
		{
			this.CreateIndices();
			this.SeedIndexData();
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate(new PutIndexTemplateRequest("nest_test")
			{
				Template = "*",
				Settings = this.IndexSettings
			});
			putTemplateResult.IsValid.Should().BeTrue();
		}

		private void CreateDeveloperIndex()
		{
			var createDeveloperIndex = this.Client.CreateIndex(Index<Developer>(), c => c
				.Mappings(map => map
					.Map<Developer>(m => m
						.AutoMap()
						.Properties(DeveloperProperties)
					)
				)
			);
			createDeveloperIndex.IsValid.Should().BeTrue();
		}

		private void CreateProjectIndex()
		{
			var createProjectIndex = this.Client.CreateIndex(typeof(Project), c => c
				.Settings(settings=>settings
					.Analysis(ProjectAnalysisSettings)
				)
				.Aliases(a => a
					.Alias("projects-alias")
				)
				.Mappings(map => map
					.Map<Project>(m => m
						.AutoMap()
						.Properties(ProjectProperties)
					)
					.Map<CommitActivity>(m => m
						.AutoMap()
						.Parent<Project>()
						.Properties(props => props
							.Object<Developer>(o => o
								.AutoMap()
								.Name(p => p.Committer)
								.Properties(DeveloperProperties)
							)
							.Text(t => t
								.Name(p => p.ProjectName)
								.Index(false)
							)
						)
					)
				)
				);
			createProjectIndex.IsValid.Should().BeTrue();
		}

		public static IAnalysis ProjectAnalysisSettings(AnalysisDescriptor analysis)
		{
			analysis
                .TokenFilters(tokenFilters => tokenFilters
                    .Shingle("shingle", shingle=>shingle
                        .MinShingleSize(2)
                        .MaxShingleSize(4)
                    )
                )
                .Analyzers(analyzers=>analyzers
                    .Custom("shingle", shingle=>shingle
                        .Filters("standard", "shingle")
                        .Tokenizer("standard")
                    )
                );
			if (new SemVer.Range(">=5.2.0").IsSatisfied(TestClient.Configuration.ElasticsearchVersion))
				analysis.Normalizers(analyzers => analyzers
					.Custom("my_normalizer", n => n
						.Filters("lowercase", "asciifolding")
					)
				);
			return analysis;
		}


		private void CreatePercolatorIndex()
		{
			var createPercolatedIndex = this.Client.CreateIndex(typeof(PercolatedQuery), c => c
				.Settings(s => s
					.AutoExpandReplicas("0-all")
				)
				.Mappings(map => map
					.Map<PercolatedQuery>(m => m
						.AutoMap()
						.Properties(PercolatedQueryProperties)
					)
				)
			);

			createPercolatedIndex.IsValid.Should().BeTrue();
		}

		public static PropertiesDescriptor<Project> ProjectProperties(PropertiesDescriptor<Project> props) => props
				.Keyword(s => s
					.Name(p => p.Name)
					.Store()
					.Fields(fs => fs
						.Text(ss => ss
							.Name("standard")
							.Analyzer("standard")
						)
						.Completion(cm => cm
							.Name("suggest")
						)
					)
				)
				.Text(s=>s
					.Name(p=>p.Description)
					.Fielddata()
					.Fields(f=>f
						.Text(t=>t
							.Name("shingle")
							.Analyzer("shingle")
						)
					)
				)
				.Date(d => d
					.Store()
					.Name(p => p.StartedOn)
				)
				.Text(d => d
					.Store()
					.Name(p => p.DateString)
				)
				.Keyword(d => d
					.Name(p => p.State)
					.Fields(fs => fs
						.Text(st => st
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
					.Contexts(cx => cx
						.Category(c => c
							.Name("color")
						)
					)
				)
				.Number(n => n
					.Name(p => p.NumberOfCommits)
					.Store()
				)
				.Object<Dictionary<string,Metadata>>(o => o
					.Name(p => p.Metadata)
				);

		private static PropertiesDescriptor<Tag> TagProperties(PropertiesDescriptor<Tag> props) => props
			.Keyword(s => s
				.Name(p => p.Name)
				.Fields(f => f
					.Text(st => st
						.Name("vectors")
						.TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
					)
				)
			);

		private static PropertiesDescriptor<Developer> DeveloperProperties(PropertiesDescriptor<Developer> props) => props
			.Keyword(s => s
				.Name(p => p.OnlineHandle)
			)
			.Keyword(s => s
				.Name(p => p.Gender)
			)
			.Text(s => s
				.Name(p => p.FirstName)
				.TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
			)
			.Ip(s => s
				.Name(p => p.IPAddress)
			)
			.GeoPoint(g => g
				.Name(p => p.Location)
			)
			.Object<GeoIp>(o => o
				.Name(p => p.GeoIp)
			);

		public static PropertiesDescriptor<PercolatedQuery> PercolatedQueryProperties(PropertiesDescriptor<PercolatedQuery> props) => props
			.Percolator(pp => pp
				.Name(n => n.Query)
			);
	}
}
