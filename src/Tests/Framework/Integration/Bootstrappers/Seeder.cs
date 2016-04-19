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
		private bool AlreadySeeded() => this.Client.IndexTemplateExists("raw_fields").Exists;

		public void DeleteIndicesAndTemplates()
		{
			if (this.Client.IndexTemplateExists("raw_fields").Exists)
				this.Client.DeleteIndexTemplate("raw_fields");
			if (this.Client.IndexExists(Indices<Project>()).Exists)
				this.Client.DeleteIndex(typeof(Project));
			if (this.Client.IndexExists(Indices<Developer>()).Exists)
				this.Client.DeleteIndex(typeof(Developer));
		}

		public void CreateIndices()
		{
			CreateRawFieldsIndexTemplate();
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
					.Map<Project>(MapProject)
#pragma warning disable 618
					.Map<CommitActivity>(m => m
						.Parent<Project>()

						.TimestampField(t => t
							.Enabled()
						)
#pragma warning restore 618
						.Properties(props => props
							.Object<Developer>(o => o
								.Name(p => p.Committer)
								.Properties(DeveloperProperties)
							)
							.String(prop => prop.Name(p => p.ProjectName).NotAnalyzed())
						)
					)
				)
				);
			createProjectIndex.IsValid.Should().BeTrue();
		}

#pragma warning disable 618
		public static TypeMappingDescriptor<Project> MapProject(TypeMappingDescriptor<Project> m) => m

			.TimestampField(t => t
				.Enabled()
			)
#pragma warning restore 618
			.Properties(props => props
				.String(s => s
					.Name(p => p.Name)
					.NotAnalyzed()
					.Fields(fs => fs
						.String(ss => ss.Name("standard").Analyzer("standard"))
						.Completion(cm => cm.Name("suggest"))
					)
				)
				.Date(d => d.Name(p => p.StartedOn))
				.String(d => d
					.Name(p => p.State)
					.NotAnalyzed()
					.Fields(fs => fs
						.String(st => st.Name("offsets").IndexOptions(IndexOptions.Offsets))
					)
				)
				.Nested<Tag>(mo => mo
					.Name(p => p.Tags)
					.Properties(TagProperties)
				)
				.Object<Developer>(o => o
					.Name(p => p.LeadDeveloper)
					.Properties(DeveloperProperties)
				)
				.GeoPoint(g => g.Name(p => p.Location))
				.Completion(cm => cm
					.Name(p => p.Suggest)
					.Payloads()
					.Context(cnt => cnt
						.Category("color", cat => cat
							.Default("red")
						)
					)
				)
			);

		private static PropertiesDescriptor<Tag> TagProperties(PropertiesDescriptor<Tag> props) => props
			.String(s => s
				.Name(p => p.Name).NotAnalyzed()
				.Fields(f => f
					.String(st => st.Name("vectors").TermVector(TermVectorOption.WithPositionsOffsetsPayloads))
				)
			);

		private static PropertiesDescriptor<Developer> DeveloperProperties(PropertiesDescriptor<Developer> props) => props
			.String(s => s.Name(p => p.OnlineHandle).NotAnalyzed())
			.String(s => s.Name(p => p.Gender).NotAnalyzed())
			.String(s => s.Name(p => p.FirstName).TermVector(TermVectorOption.WithPositionsOffsetsPayloads))
			.Ip(s => s.Name(p => p.IPAddress))
			.GeoPoint(g => g.Name(p => p.Location).LatLon())
			;
	}
}
