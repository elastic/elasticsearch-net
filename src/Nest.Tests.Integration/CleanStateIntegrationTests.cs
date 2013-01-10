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
	public class CleanStateIntegrationTests : IntegrationTests
	{
		protected override void ResetIndexes()
		{
			var cloneIndex = Test.Default.DefaultIndex + "_clone";
			if (_client.IsValid)
			{
				var projects = NestTestData.Data;
				var people = NestTestData.People;

				_client.DeleteIndex(Test.Default.DefaultIndex);
				//_client.DeleteIndex(Test.Default.DefaultIndex + "_clone");

				_client.CreateIndex(Test.Default.DefaultIndex, new IndexSettings());
				//_client.CreateIndex(Test.Default.DefaultIndex + "_clone", new IndexSettings());

				this.ResetType<ElasticSearchProject>(_client, projects);
				this.ResetType<Person>(_client, people);			
			}
		}
	
		private void ResetType<T>(IElasticClient client, IEnumerable<T> objects) where T : class {
			var cloneIndex = Test.Default.DefaultIndex + "_clone";
			var bulkParameters = new SimpleBulkParameters() { Refresh = true };

			_client.MapFromAttributes<T>();
			_client.MapFromAttributes<T>(cloneIndex);

			_client.IndexMany(objects, bulkParameters);
			_client.IndexMany(objects, cloneIndex, bulkParameters);
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

			var results = this._client.Search<ElasticSearchProject>(
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
