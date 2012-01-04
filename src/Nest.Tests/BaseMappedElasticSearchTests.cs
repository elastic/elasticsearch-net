using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;

namespace Nest.Tests
{
	[TestFixture]
	public class BaseElasticSearchTests
	{
		[TestFixtureSetUp]
		protected void Initialize()
		{
			this.ResetIndexes();
		}

		private IConnectionSettings _settings;
		protected IConnectionSettings Settings
		{
			get
			{
				if (this._settings != null)
					return this._settings;

				this._settings = new ConnectionSettings(Test.Default.Host, Test.Default.Port)
								.SetDefaultIndex(Test.Default.DefaultIndex)
								.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
								.UsePrettyResponses();

				return this._settings;
			}
		}
		private ElasticClient _connectedClient;
		protected ElasticClient ConnectedClient
		{
			get 
			{
				if (this._connectedClient != null)
					return this._connectedClient;

				var client = new ElasticClient(this.Settings);
				if (client.IsValid)
				{ 
					this._connectedClient = client;
					return this._connectedClient;
				}
				return null;
			}
		}
		protected ElasticClient CreateClient()
		{
			return new ElasticClient(this.Settings);
		}
		protected void ResetIndexes()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var projects = NestTestData.Data;
				var cloneIndex = Test.Default.DefaultIndex + "_clone";
				var bulkParameters = new SimpleBulkParameters() { Refresh = true };
				client.DeleteMapping<ElasticSearchProject>();
				client.DeleteMapping<ElasticSearchProject>(cloneIndex);
				client.Map<ElasticSearchProject>();
				client.Index(projects, bulkParameters);
				client.Index(projects, cloneIndex, bulkParameters);

			}
		}
		protected void DeleteIndices()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var cloneIndex = Test.Default.DefaultIndex + "_clone";
				client.DeleteMapping<ElasticSearchProject>();
				client.DeleteMapping<ElasticSearchProject>(cloneIndex);

			}
		}
		protected void BulkIndexData()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var projects = NestTestData.Data;
				var cloneIndex = Test.Default.DefaultIndex + "_clone";
				var bulkParameters = new SimpleBulkParameters() { Refresh = true };
				client.Index(projects, bulkParameters);
				client.Index(projects, cloneIndex, bulkParameters);

			}
		}
	}
}
