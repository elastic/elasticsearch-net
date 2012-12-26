using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class BaseElasticSearchTests
	{
		[TestFixtureSetUp]
		public void Initialize()
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
		protected virtual void ResetIndexes()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var projects = NestTestData.Data;
				var people = NestTestData.People;

				this.ResetType<ElasticSearchProject>(client, projects);
				this.ResetType<Person>(client, people);

				
			}
		}
		private void ResetType<T>(IElasticClient client, IEnumerable<T> objects) where T : class {
			var cloneIndex = Test.Default.DefaultIndex + "_clone";
			var bulkParameters = new SimpleBulkParameters() { Refresh = true };
			client.DeleteMapping<T>();
			client.DeleteMapping<T>(cloneIndex);
			client.MapFromAttributes<T>();
			client.MapFromAttributes<T>(cloneIndex);
			this.ConnectedClient.OpenIndex<T>();
			this.ConnectedClient.OpenIndex(cloneIndex);
			client.IndexMany(objects, bulkParameters);
			client.IndexMany(objects, cloneIndex, bulkParameters);
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
				client.IndexMany(projects, bulkParameters);
				client.IndexMany(projects, cloneIndex, bulkParameters);

			}
		}

		/// <summary>
		/// Execute a filter test and assert the results.
		/// </summary>
		/// <param name="project">Document to be search</param>
		/// <param name="filter">Filter to be test</param>
		/// <param name="queryMustHaveResults">If the execution of search must return results</param>
		public void DoFilterTest(Func<FilterDescriptor<ElasticSearchProject>, Nest.BaseFilter>  filter, ElasticSearchProject project, bool queryMustHaveResults)
		{
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, project.Id.ToString());

			var results = this.ConnectedClient.Search<ElasticSearchProject>(
				s => s.Filter(ff => ff.And(
						f => f.Term(e => e.Id, project.Id.ToString()),
						filter
					))
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(queryMustHaveResults ? 1 : 0, results.Total);
		}
	}
}
