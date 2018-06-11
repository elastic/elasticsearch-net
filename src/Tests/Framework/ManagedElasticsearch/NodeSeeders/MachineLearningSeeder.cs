using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Managed.FileSystem;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework.ManagedElasticsearch.NodeSeeders
{
	public class MachineLearningSeeder
	{
		public const string MachineLearningTestsIndexTemplateName = "server-metrics";

		private IElasticClient Client { get; }
		public string RoamingFolder { get; }

		public MachineLearningSeeder(IElasticClient client, INodeFileSystem fileSystem)
		{
			this.RoamingFolder = fileSystem.LocalFolder;
			this.Client = client;
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If template exists assume this cluster is already seeded with the machine learning data.
		private bool AlreadySeeded() => this.Client.IndexTemplateExists(MachineLearningTestsIndexTemplateName).Exists;

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			this.DeleteIndicesAndTemplates();
			// and now recreate everything
			this.CreateIndicesAndSeedIndexData();
		}

		public void DeleteIndicesAndTemplates()
		{
			if (this.Client.IndexTemplateExists(MachineLearningTestsIndexTemplateName).Exists)
				this.Client.DeleteIndexTemplate(MachineLearningTestsIndexTemplateName);
		}

		private void CreateIndicesAndSeedIndexData()
		{
			this.CreateIndices();
			this.SeedIndexData();
		}

		public void CreateIndices()
		{
			CreateIndexTemplate();
			CreateMetricIndex();
		}

		private void SeedIndexData()
		{
			Console.WriteLine("Bulk importing starting ...");
			var folder = Path.Combine(this.RoamingFolder, "server_metrics");
			Enumerable.Range(1, 4).ToList().ForEach(i =>
			{
				var metricsFile = Path.Combine(folder, $"server-metrics_{i}.json");
				var bulkResponse = this.Client.LowLevel.Bulk<BulkResponse>(
					File.ReadAllBytes(metricsFile),
					new BulkRequestParameters
					{
						RequestConfiguration = new RequestConfiguration
						{
							RequestTimeout = TimeSpan.FromMinutes(3)
						}
					});

				if (!bulkResponse.ApiCall.Success || (!bulkResponse.IsValid))
				{
					// only use the Audit trail as failed bulk items will be YUGE
					var sb = new StringBuilder();
					ResponseStatics.DebugAuditTrail(bulkResponse.ApiCall.AuditTrail, sb);
					throw new Exception($"Problem seeding server-metrics data for machine learning: {sb}");
				}

				Console.WriteLine($"Indexed docs from {metricsFile}");
			});
			Console.WriteLine("Bulk importing finished.");
		}

		private void CreateMetricIndex()
		{
			var createProjectIndex = this.Client.CreateIndex(MachineLearningTestsIndexTemplateName, c => c
				.Mappings(map => map
					.Map<Metric>(m => m
						.AutoMap()
						.Properties(props => props
							.Keyword(t => t
								.Name(p => p.Host)
							)
							.Keyword(t => t
								.Name(p => p.Service)
							)
						)
					)
				)
			);
			createProjectIndex.ShouldBeValid();
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate(new PutIndexTemplateRequest(MachineLearningTestsIndexTemplateName)
			{
				IndexPatterns = new ReadOnlyCollection<string>(new [] { "*" }),
				Settings = new IndexSettings
				{
					NumberOfShards = 1,
					NumberOfReplicas = 0
				}
			});
			putTemplateResult.ShouldBeValid();
		}
	}
}
