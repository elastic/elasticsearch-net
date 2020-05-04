// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			var alreadySeeded = false;
			if (!TestClient.Configuration.ForceReseed && (alreadySeeded = AlreadySeeded())) return;

			var t = Task.Run(async () => await SeedNodeAsync(alreadySeeded).ConfigureAwait(false));

			t.Wait(TimeSpan.FromSeconds(40));
		}

		public void SeedNodeNoData()
		{
			var alreadySeeded = false;
			if (!TestClient.Configuration.ForceReseed && (alreadySeeded = AlreadySeeded())) return;

			var t = Task.Run(async () => await SeedNodeNoDataAsync(alreadySeeded).ConfigureAwait(false));

			t.Wait(TimeSpan.FromSeconds(40));
		}

		// Sometimes we run against an manually started Elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If raw_fields exists assume this cluster is already seeded.
		private bool AlreadySeeded() => Client.Indices.TemplateExists(TestsIndexTemplateName).Exists;

		private async Task SeedNodeAsync(bool alreadySeeded)
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await DeleteIndicesAndTemplatesAsync(alreadySeeded).ConfigureAwait(false);
			await ClusterSettingsAsync().ConfigureAwait(false);
			await PutPipeline().ConfigureAwait(false);
			// and now recreate everything
			await CreateIndicesAndSeedIndexDataAsync().ConfigureAwait(false);
		}

		private async Task SeedNodeNoDataAsync(bool alreadySeeded)
		{
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			await DeleteIndicesAndTemplatesAsync(alreadySeeded).ConfigureAwait(false);
			await ClusterSettingsAsync().ConfigureAwait(false);
			// and now recreate everything
			await CreateIndicesAsync().ConfigureAwait(false);
		}

		public async Task ClusterSettingsAsync()
		{
			if (TestConfiguration.Instance.InRange("<6.1.0")) return;

			var clusterConfiguration = new Dictionary<string, object>()
			{
				{ "cluster.routing.use_adaptive_replica_selection", true }
			};

			if (TestConfiguration.Instance.InRange(">=6.5.0"))
				clusterConfiguration += new RemoteClusterConfiguration
				{
					{ RemoteClusterName, "127.0.0.1:9300" }
				};

			var putSettingsResponse = await Client.Cluster.PutSettingsAsync(new ClusterPutSettingsRequest
			{
				Transient = clusterConfiguration
			}).ConfigureAwait(false);

			putSettingsResponse.ShouldBeValid();
		}

		public async Task PutPipeline()
		{
			if (TestConfiguration.Instance.InRange("<6.1.0")) return;

			var putProcessors = await Client.Ingest.PutPipelineAsync(PipelineName, pi => pi
				.Description("A pipeline registered by the NEST test framework")
				.Processors(pp => pp
					.Set<Project>(s => s.Field(p => p.Metadata).Value(new { x = "y" }))
				)
			).ConfigureAwait(false);
			putProcessors.ShouldBeValid();
		}


		public async Task DeleteIndicesAndTemplatesAsync(bool alreadySeeded)
		{
			var tasks = new List<Task>
			{
				Client.Indices.DeleteAsync(typeof(Project)),
				Client.Indices.DeleteAsync(typeof(Developer)),
				Client.Indices.DeleteAsync(typeof(ProjectPercolation))
			};

			if (alreadySeeded)
				tasks.Add(Client.Indices.DeleteTemplateAsync(TestsIndexTemplateName));

			await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);
		}

		private async Task CreateIndicesAndSeedIndexDataAsync()
		{
			await CreateIndicesAsync().ConfigureAwait(false);
			await SeedIndexDataAsync().ConfigureAwait(false);
		}

		public async Task CreateIndicesAsync()
		{
			var indexTemplateResponse = await CreateIndexTemplateAsync().ConfigureAwait(false);
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
				}).ConfigureAwait(false);
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
				) };
			await Task.WhenAll(tasks).ConfigureAwait(false);
			await Client.Indices.RefreshAsync(Indices.Index(typeof(Project), typeof(Developer), typeof(ProjectPercolation))).ConfigureAwait(false);
		}

		private Task<PutIndexTemplateResponse> CreateIndexTemplateAsync() => Client.Indices.PutTemplateAsync(
			new PutIndexTemplateRequest(TestsIndexTemplateName)
			{
				IndexPatterns = new[] { "*" },
				Settings = IndexSettings
			});

		private Task<CreateIndexResponse> CreateDeveloperIndexAsync() => Client.Indices.CreateAsync(Infer.Index<Developer>(), c => c
			.Map<Developer>(m => m
				.AutoMap()
				.Properties(DeveloperProperties)
			)
		);

#pragma warning disable 618
		private Task<CreateIndexResponse> CreateProjectIndexAsync() => Client.Indices.CreateAsync(typeof(Project), c => c
			.Settings(settings => settings.Analysis(ProjectAnalysisSettings))
			// this uses obsolete overload somewhat on purpose to make sure it works just as the rest
			// TODO 8.0 remove with once the overloads are gone too
			.Mappings(ProjectMappings)
			.Aliases(aliases => aliases
				.Alias(ProjectsAliasName)
				.Alias(ProjectsAliasFilter, a => a
					.Filter<Project>(f => f.Term(p => p.Join, Infer.Relation<Project>()))
				)
				.Alias(CommitsAliasFilter, a => a
					.Filter<CommitActivity>(f => f.Term(p => p.Join, Infer.Relation<CommitActivity>()))
				)
			)
		);
#pragma warning restore 618

#pragma warning disable 618
		public static ITypeMapping ProjectMappings(MappingsDescriptor map) => map
			.Map<Project>(ProjectTypeMappings);
#pragma warning restore 618

		public static ITypeMapping ProjectTypeMappings(TypeMappingDescriptor<Project> m) => m
			.RoutingField(r => r.Required())
			.AutoMap()
			.Properties(ProjectProperties)
			.Properties<CommitActivity>(props => props
				.Object<Developer>(o => o
					.AutoMap()
					.Name(p => p.Committer)
					.Properties(DeveloperProperties)
					.Dynamic()
				)
				.Text(t => t
					.Name(p => p.ProjectName)
					.Index(false)
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
						.Filters("shingle")
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


		private Task<CreateIndexResponse> CreatePercolatorIndexAsync() => Client.Indices.CreateAsync(typeof(ProjectPercolation), c => c
			.Settings(s => s
				.AutoExpandReplicas("0-all")
				.Analysis(ProjectAnalysisSettings)
			)
			.Map<ProjectPercolation>(m => m
				.AutoMap()
				.Properties(PercolatedQueryProperties)
			)
		);

		public static PropertiesDescriptor<TProject> ProjectProperties<TProject>(PropertiesDescriptor<TProject> props)
			where TProject : Project
		{
			props
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
					.Name(p => p.LocationPoint)
				)
				.GeoShape(g => g
					.Name(p => p.LocationShape)
				)
				.Completion(cm => cm
					.Name(p => p.Suggest)
					.Contexts(cx => cx
						.Category(c => c
							.Name("color")
						)
						.GeoLocation(c => c
							.Name("geo")
							.Precision("1")
						)
					)
				)
				.Scalar(p => p.NumberOfCommits, n => n.Store())
				.Scalar(p => p.NumberOfContributors, n => n.Store())
				.Object<Dictionary<string, Metadata>>(o => o
					.Name(p => p.Metadata)
				)
				.RankFeature(rf => rf
					.Name(p => p.Rank)
					.PositiveScoreImpact()
				);

			if (TestConfiguration.Instance.InRange(">=7.3.0"))
				props.Flattened(f => f
					.Name(p => p.Labels)
				);
			else
				props.Object<Labels>(f => f
					.Name(p => p.Labels)
					.Enabled(false)
				);

			if (TestConfiguration.Instance.InRange(">=7.4.0"))
				props.Shape(g => g
					.Name(p => p.ArbitraryShape)
				);

			if (TestConfiguration.Instance.InRange(">=7.7.0"))
				props.ConstantKeyword(f => f
					.Name(p => p.VersionControl)
				);
			else
				props.Keyword(f => f
					.Name(p => p.VersionControl)
				);

			return props;
		}

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
