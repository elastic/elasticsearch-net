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
	public class IntegrationTests
	{
		protected readonly ElasticClient _client = ElasticsearchConfiguration.Client;
		protected readonly IConnectionSettings _settings = ElasticsearchConfiguration.Settings;
	
		protected virtual void ResetIndexes()
		{
			
		}


		public void DoFilterTest(Func<FilterDescriptor<ElasticSearchProject>, Nest.BaseFilter> filter, ElasticSearchProject project, bool queryMustHaveResults)
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
