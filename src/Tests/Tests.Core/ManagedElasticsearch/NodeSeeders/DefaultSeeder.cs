using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
		public const string TestsIndexTemplateName = "nest_tests";

		public const string ProjectsAliasName = "projects-alias";
		public const string ProjectsAliasFilter = "projects-only";
		public const string CommitsAliasFilter = "commits-only";

		private IElasticClient Client { get; }

		private readonly IIndexSettings _defaultIndexSettings = new IndexSettings()
		{
			NumberOfShards = 2,
			NumberOfReplicas = 0,
		};

		private IIndexSettings IndexSettings { get; }

		public DefaultSeeder(IElasticClient client, IIndexSettings indexSettings)
		{
			this.Client = client;
			this.IndexSettings = indexSettings ?? _defaultIndexSettings;
		}

		public DefaultSeeder(IElasticClient client) : this(client, null) { }

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && this.AlreadySeeded()) return;

			var t = Task.Run(async () => await this.SeedNodeAsync());

			t.Wait(TimeSpan.FromSeconds(40));
		}
		public void SeedNodeNoData()
		{
			if (!TestClient.Configuration.ForceReseed && this.AlreadySeeded()) return;

			var t = Task.Run(async () => await this.SeedNodeNoDataAsync());

			t.Wait(TimeSpan.FromSeconds(40));
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If raw_fields exists assume this cluster is already seeded.
		private bool AlreadySeeded() => this.Client.IndexTemplateExists(TestsIndexTemplateName).Exists;

		private async Task SeedNodeAsync()
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await this.DeleteIndicesAndTemplatesAsync();
			await this.ClusterSettingsAsync();
			// and now recreate everything
			await this.CreateIndicesAndSeedIndexDataAsync();
		}
		private async Task SeedNodeNoDataAsync()
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await this.DeleteIndicesAndTemplatesAsync();
			await this.ClusterSettingsAsync();
			// and now recreate everything
			await this.CreateIndicesAsync();
		}

		public async Task ClusterSettingsAsync()
		{
			if (TestConfiguration.Instance.InRange("<6.1.0")) return;
			var putSettingsResponse = await this.Client.ClusterPutSettingsAsync(s=>s
				.Transient(t=>t
					.Add("cluster.routing.use_adaptive_replica_selection", true)
				)
			);

			putSettingsResponse.ShouldBeValid();
		}

		public async Task DeleteIndicesAndTemplatesAsync()
		{
			var tasks = new Task[]
			{
				this.Client.DeleteIndexTemplateAsync(TestsIndexTemplateName),
				this.Client.DeleteIndexAsync(typeof(Project)),
				this.Client.DeleteIndexAsync(typeof(Developer)),
				this.Client.DeleteIndexAsync(typeof(ProjectPercolation))
			};
			await Task.WhenAll(tasks);
		}

		private async Task CreateIndicesAndSeedIndexDataAsync()
		{
			await this.CreateIndicesAsync();
			await this.SeedIndexDataAsync();
		}

		public async Task CreateIndicesAsync()
		{
			var indexTemplateResponse = await this.CreateIndexTemplateAsync();
			indexTemplateResponse.ShouldBeValid();

			var tasks = new []
			{
				this.CreateProjectIndexAsync(),
				this.CreateDeveloperIndexAsync(),
				this.CreatePercolatorIndexAsync(),
			};
			await Task.WhenAll(tasks).ContinueWith(t =>
			{
				foreach(var r in t.Result)
					r.ShouldBeValid();
			});
		}

		private async Task SeedIndexDataAsync()
		{
			var tasks = new Task[]
			{
				this.Client.IndexManyAsync(Project.Projects),
				this.Client.IndexManyAsync(Developer.Developers),
				this.Client.IndexDocumentAsync(new ProjectPercolation
				{
					Id = "1",
					Query = new MatchAllQuery()
				}),
				this.Client.BulkAsync(b => b
					.IndexMany(
						CommitActivity.CommitActivities,
						(d, c) => d.Document(c).Routing(c.ProjectName)
					)
				)
			};
			await Task.WhenAll(tasks);
			await this.Client.RefreshAsync(Indices.Index(typeof(Project), typeof(Developer), typeof(ProjectPercolation)));
		}

		private Task<IPutIndexTemplateResponse> CreateIndexTemplateAsync() => this.Client.PutIndexTemplateAsync(new PutIndexTemplateRequest(TestsIndexTemplateName)
		{
			IndexPatterns = new[] {"*"},
			Settings = this.IndexSettings
		});

		private Task<ICreateIndexResponse> CreateDeveloperIndexAsync() => this.Client.CreateIndexAsync(Infer.Index<Developer>(), c => c
			.Mappings(map => map
				.Map<Developer>(m => m
					.AutoMap()
					.Properties(DeveloperProperties)
				)
			)
		);

		private Task<ICreateIndexResponse> CreateProjectIndexAsync() => this.Client.CreateIndexAsync(typeof(Project), c => c
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
					.RoutingField(r=>r.Required())
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


		private Task<ICreateIndexResponse> CreatePercolatorIndexAsync() => this.Client.CreateIndexAsync(typeof(ProjectPercolation), c => c
			.Settings(s => s
				.AutoExpandReplicas("0-all")
				.Analysis(DefaultSeeder.ProjectAnalysisSettings)
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
