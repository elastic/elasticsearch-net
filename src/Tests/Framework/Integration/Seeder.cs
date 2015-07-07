using FluentAssertions;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework.Integration
{
	public class Seeder
	{
		private IElasticClient Client { get; }

		public Seeder(int port)
		{
			var client = TestClient.GetClient(seederSettings, port);
			this.Client = client;
		}

		private ConnectionSettings seederSettings(ConnectionSettings settings) => settings;
			
		public void SeedNode()
		{
			this.CreateIndicesAndMappings();
		}


		public void CreateIndicesAndMappings()
		{
			var putTemplateResult = this.Client.PutTemplate("raw_fields", p => p
				.Template("*") //match on all created indices
				.Settings(s=>s
					.Add("index.number_of_shards", "2")
					.Add("index.number_of_replicas", "0")
				)
				.AddMapping<object>(pm => pm
					.Type("_default_")
					.DynamicTemplates(dt => dt
						.Add(dtt => dtt
							.Name("raw_fields") //register a raw fields dynamic template
							.Match("*") //matches all fields
							.MatchMappingType("string") //that are a string
							.Mapping(m=>m
								.String(sm=>sm //map as string
									.Fields(f=>f //with a multifield 'raw' that is not analyzed
										.String(ssm=>ssm.Name("raw").Index(FieldIndexOption.NotAnalyzed))
									)
								)
							)
						)
					)
				)
			);
			putTemplateResult.IsValid.Should().BeTrue();

			var createProjectIndex = this.Client.CreateIndex(c => c
				.Index<Project>()
				.AddMapping<Project>(m=>m
					.Properties(props=>props
						.NestedObject<Tag>(mo=>mo
							.Name(p=>p.Tags)
							.Properties(TagProperties)
						)
						.Object<Developer>(o=>o
							.Name(p=>p.LeadDeveloper)
							.Properties(DeveloperProperties)
						)
					)
				)
				.AddMapping<CommitActivity>(m=>m
					.SetParent<Project>()
					.Properties(props=>props
						.Object<Developer>(o=>o
							.Name(p=>p.Committer)
							.Properties(DeveloperProperties)
						)
						.String(prop=>prop.Name(p=>p.ProjectName).NotAnalyzed())
					)
				)

			);
			createProjectIndex.IsValid.Should().BeTrue();

		}

		private static PropertiesDescriptor<Tag> TagProperties(PropertiesDescriptor<Tag> props) => props
			.String(s=>s.Name(p=>p.Name).NotAnalyzed());

		private static PropertiesDescriptor<Developer> DeveloperProperties(PropertiesDescriptor<Developer> props) => props
			.String(s=>s.Name(p=>p.OnlineHandle).NotAnalyzed())
			.String(s=>s.Name(p=>p.Gender).NotAnalyzed())
			//.GeoPoint(g=>g.Name(p=>p.Location))
			;

	}

	

}
