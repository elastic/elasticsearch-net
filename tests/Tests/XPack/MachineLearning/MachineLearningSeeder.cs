// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.XPack.MachineLearning
{
	public class MachineLearningSeeder
	{
		public const string MachineLearningTestsIndexTemplateName = "server-metrics";

		public MachineLearningSeeder(IElasticClient client, INodeFileSystem fileSystem)
		{
			RoamingFolder = fileSystem.LocalFolder;
			Client = client;
		}

		public string RoamingFolder { get; }

		private IElasticClient Client { get; }

		// Sometimes we run against an manually started Elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If template exists assume this cluster is already seeded with the machine learning data.
		private bool AlreadySeeded() => Client.Indices.TemplateExists(MachineLearningTestsIndexTemplateName).Exists;

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;

			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			DeleteIndicesAndTemplates();
			// and now recreate everything
			CreateIndicesAndSeedIndexData();
		}

		public void DeleteIndicesAndTemplates()
		{
			if (Client.Indices.TemplateExists(MachineLearningTestsIndexTemplateName).Exists)
				Client.Indices.DeleteTemplate(MachineLearningTestsIndexTemplateName);
		}

		private void CreateIndicesAndSeedIndexData()
		{
			CreateIndices();
			SeedIndexData();
		}

		public void CreateIndices()
		{
			CreateIndexTemplate();
			CreateMetricIndex();
		}

		private void SeedIndexData()
		{
			Console.WriteLine("Bulk importing starting ...");
			var folder = Path.Combine(RoamingFolder, "server_metrics");
			Enumerable.Range(1, 4)
				.ToList()
				.ForEach(i =>
				{
					var metricsFile = Path.Combine(folder, $"server-metrics_{i}.json");

					// TODO: Remove metric type from server-metrics files. Remove this when example is patched for 7.x
					var fileContents = File.ReadAllText(metricsFile);
					fileContents = fileContents.Replace(",\"_type\":\"metric\",", ",");

					var bulkResponse = Client.LowLevel.Bulk<BulkResponse>(
						fileContents,
						new BulkRequestParameters
						{
							RequestConfiguration = new RequestConfiguration
							{
								RequestTimeout = TimeSpan.FromMinutes(3)
							}
						});

					if (!bulkResponse.ApiCall.Success || !bulkResponse.IsValid)
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
			var createProjectIndex = Client.Indices.Create(MachineLearningTestsIndexTemplateName, c => c
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
			);
			createProjectIndex.ShouldBeValid();
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = Client.Indices.PutTemplate(new PutIndexTemplateRequest(MachineLearningTestsIndexTemplateName)
			{
				IndexPatterns = new ReadOnlyCollection<string>(new[] { "*" }),
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
