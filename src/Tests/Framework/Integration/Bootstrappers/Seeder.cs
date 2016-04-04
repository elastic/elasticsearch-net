using System;
using FluentAssertions;
using Nest;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Framework.Integration
{
	public class Seeder
	{
		private IElasticClient Client { get; }

		public Seeder(ElasticsearchNode node)
		{
			this.Client = node.Client();
		}

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
		}

		public void CreateIndices()
		{
			CreateIndexTemplate();
			CreateProjectIndex();
			CreateDeveloperIndex();
		}

		private void SeedIndexData()
		{
			this.Client.IndexMany(Project.Projects);
			this.Client.IndexMany(Developer.Developers);
			this.Client.Refresh(Nest.Indices.Index<Project>().And<Developer>());
		}

		private void CreateIndicesAndSeedIndexData()
		{
			this.CreateIndices();
			this.SeedIndexData();
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate("nest_tests", p => p
				.Template("*") //match on all created indices
				.Settings(s => s
					.NumberOfReplicas(0)
					.NumberOfShards(2)
				)
				//.Mappings(pm => pm
				//	.Map("_default_", m => m
				//		.DynamicTemplates(dt => dt
				//			.DynamicTemplate("raw_field", dtt => dtt
				//				.Match("*") //matches all fields
				//				.MatchMappingType("string") //that are a string
				//				.Mapping(tm => tm
				//					.Text(sm => sm //map as string
				//						.Fields(f => f //with a multifield 'raw' that is not analyzed
				//							.Keyword(ssm => ssm.Name("raw"))
				//						)
				//					)
				//				)
				//			)
				//		)
				//	)
				//)
			);
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
						.TimestampField(t => t
							.Enabled()
						)
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
				.Date(d => d
					.Name(p => p.StartedOn)
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
				.LatLon()
			);
	}
}
