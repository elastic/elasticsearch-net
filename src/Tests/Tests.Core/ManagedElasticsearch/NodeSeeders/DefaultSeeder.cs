using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Tests.Configuration;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.Core.ManagedElasticsearch.NodeSeeders
{
	public class DefaultSeeder
	{
		public const string CommitsAliasFilter = "commits-only";
		public const string ProjectsAliasFilter = "projects-only";

		public const string ProjectsAliasName = "projects-alias";
		public const string TestsIndexTemplateName = "nest_tests";

		public const string RemoteClusterName = "remote-cluster";

		public const string PipelineName = "nest-pipeline";

		private readonly IIndexSettings _defaultIndexSettings = new IndexSettings()
		{
			NumberOfShards = 2,
			NumberOfReplicas = 0,
		};

		public DefaultSeeder(IElasticClient client, IIndexSettings indexSettings)
		{
			Client = client;
			IndexSettings = indexSettings ?? _defaultIndexSettings;
		}

		public DefaultSeeder(IElasticClient client) : this(client, null) { }

		private IElasticClient Client { get; }

		private IIndexSettings IndexSettings { get; }

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;

			var t = Task.Run(async () => await SeedNodeAsync());

			t.Wait(TimeSpan.FromSeconds(40));
		}

		public void SeedNodeNoData()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;

			var t = Task.Run(async () => await SeedNodeNoDataAsync());

			t.Wait(TimeSpan.FromSeconds(40));
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If raw_fields exists assume this cluster is already seeded.
		private bool AlreadySeeded() => Client.IndexTemplateExists(TestsIndexTemplateName).Exists;

		private async Task SeedNodeAsync()
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await DeleteIndicesAndTemplatesAsync();
			await ClusterSettingsAsync();
			await PutPipeline();
			// and now recreate everything
			await CreateIndicesAndSeedIndexDataAsync();
		}

		private async Task SeedNodeNoDataAsync()
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await DeleteIndicesAndTemplatesAsync();
			await ClusterSettingsAsync();
			// and now recreate everything
			await CreateIndicesAsync();
		}

		public async Task ClusterSettingsAsync()
		{
			if (TestConfiguration.Instance.InRange("<6.1.0")) return;

			var clusterConfiguration = new Dictionary<string, object>()
			{
				{ "cluster.routing.use_adaptive_replica_selection", true }
			};

			if (TestConfiguration.Instance.InRange(">=6.5.0"))
				clusterConfiguration += new RemoteClusterConfiguration()
				{
					{ RemoteClusterName, "127.0.0.1:9300" }
				};

			var putSettingsResponse = await Client.ClusterPutSettingsAsync(new ClusterPutSettingsRequest
			{
				Transient = clusterConfiguration
			});

			putSettingsResponse.ShouldBeValid();
		}

		public async Task PutPipeline()
		{
			if (TestConfiguration.Instance.InRange("<6.1.0")) return;

			var putProcessors = await Client.PutPipelineAsync(PipelineName, pi => pi
				.Description("A pipeline registered by the NEST test framework")
				.Processors(pp => pp
					.Set<Project>(s => s.Field(p => p.Metadata).Value(new { x = "y" }))
				)
			);
			putProcessors.ShouldBeValid();
		}


		public async Task DeleteIndicesAndTemplatesAsync()
		{
			var tasks = new Task[]
			{
				Client.DeleteIndexTemplateAsync(TestsIndexTemplateName),
				Client.DeleteIndexAsync(typeof(Project)),
				Client.DeleteIndexAsync(typeof(Developer)),
				Client.DeleteIndexAsync(typeof(ProjectPercolation))
			};
			await Task.WhenAll(tasks);
		}

		private async Task CreateIndicesAndSeedIndexDataAsync()
		{
			await CreateIndicesAsync();
			await SeedIndexDataAsync();
		}

		public async Task CreateIndicesAsync()
		{
			var indexTemplateResponse = await CreateIndexTemplateAsync();
			indexTemplateResponse.ShouldBeValid();

			var tasks = new[]
			{
				CreateProjectIndexAsync(),
				CreateDeveloperIndexAsync(),
				CreatePercolatorIndexAsync(),
			};
			await Task.WhenAll(tasks)
				.ContinueWith(t =>
				{
					foreach (var r in t.Result)
						r.ShouldBeValid();
				});
		}

		private async Task SeedIndexDataAsync()
		{
			var tasks = new Task[]
			{
				Client.IndexManyAsync(Project.Projects),
				Client.IndexManyAsync(Developer.Developers),
				Client.IndexDocumentAsync(new ProjectPercolation
				{
					Id = "1",
					Query = new MatchAllQuery()
				}),
				Client.BulkAsync(b => b
					.IndexMany(
						CommitActivity.CommitActivities,
						(d, c) => d.Document(c).Routing(c.ProjectName)
					)
				)
			};
			await Task.WhenAll(tasks);
			await Client.RefreshAsync(Indices.Index(typeof(Project), typeof(Developer), typeof(ProjectPercolation)));
		}

		private Task<IPutIndexTemplateResponse> CreateIndexTemplateAsync() => Client.PutIndexTemplateAsync(
			new PutIndexTemplateRequest(TestsIndexTemplateName)
			{
				IndexPatterns = new[] { "*" },
				Settings = IndexSettings
			});

		private Task<ICreateIndexResponse> CreateDeveloperIndexAsync() => Client.CreateIndexAsync(Infer.Index<Developer>(), c => c
			.Mappings(map => map
				.Map<Developer>(m => m
					.AutoMap()
					.Properties(DeveloperProperties)
				)
			)
		);

		private Task<ICreateIndexResponse> CreateProjectIndexAsync() => Client.CreateIndexAsync(typeof(Project), c => c
			.Settings(settings => settings
				.Analysis(ProjectAnalysisSettings)
			)
			.Aliases(aliases => aliases
				.Alias(ProjectsAliasName)
				.Alias(ProjectsAliasFilter, a => a
					.Filter<Project>(f => f.Term(p => p.Join, Infer.Relation<Project>()))
				)
				.Alias(CommitsAliasFilter, a => a
					.Filter<CommitActivity>(f => f.Term(p => p.Join, Infer.Relation<CommitActivity>()))
				)
			)
			.Mappings(map => map
				.Map<Project>(m => m
					.RoutingField(r => r.Required())
					.AutoMap()
					.Properties(ProjectProperties)
					.Properties<CommitActivity>(props => props
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
			//normalizers are a new feature since 5.2.0
			if (TestConfiguration.Instance.InRange(">=5.2.0"))
				analysis.Normalizers(analyzers => analyzers
					.Custom("my_normalizer", n => n
						.Filters("lowercase", "asciifolding")
					)
				);
			return analysis;
		}


		private Task<ICreateIndexResponse> CreatePercolatorIndexAsync() => Client.CreateIndexAsync(typeof(ProjectPercolation), c => c
			.Settings(s => s
				.AutoExpandReplicas("0-all")
				.Analysis(ProjectAnalysisSettings)
			)
			.Mappings(map => map
				.Map<ProjectPercolation>(m => m
					.AutoMap()
					.Properties(PercolatedQueryProperties)
				)
			)
		);

		public static PropertiesDescriptor<TProject> ProjectProperties<TProject>(PropertiesDescriptor<TProject> props)
			where TProject : Project => props
			.Join(j => j
				.Name(n => n.Join)
				.Relations(r => r
					.Join<Project, CommitActivity>()
				)
			)
			.Keyword(d => d.Name(p => p.Type))
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
			.Text(s => s
				.Name(p => p.Description)
				.Fielddata()
				.Fields(f => f
					.Text(t => t
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
					.Keyword(sk => sk
						.Name("keyword")
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
			.Scalar(p => p.NumberOfCommits, n => n.Store())
			.Scalar(p => p.NumberOfContributors, n => n.Store())
			.Object<Dictionary<string, Metadata>>(o => o
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

		public static PropertiesDescriptor<Developer> DeveloperProperties(PropertiesDescriptor<Developer> props) => props
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
				.Name(p => p.IpAddress)
			)
			.GeoPoint(g => g
				.Name(p => p.Location)
			)
			.Object<GeoIp>(o => o
				.Name(p => p.GeoIp)
			);

		public static PropertiesDescriptor<ProjectPercolation> PercolatedQueryProperties(PropertiesDescriptor<ProjectPercolation> props) =>
			ProjectProperties(props.Percolator(pp => pp.Name(n => n.Query)));
	}
}
