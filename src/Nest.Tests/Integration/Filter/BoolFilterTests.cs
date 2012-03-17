using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Integration.Filter
{
	[TestFixture]
	public class BoolFilterTests : BaseElasticSearchTests
	{
		[Test]
		public void BoolFilter()
		{
			var results = this.ConnectedClient.Search<ElasticSearchProject>(s=>s
				.From(0)
				.Size(10)
				.Filter(filter=>filter
					.Bool(b=>b
						.Must(
							f => f.MatchAll()
						)
						.MustNot(
							f => f.Term(e => e.Name, "elasticflume")
						)
						.Should(
							f=> f.Exists(p => p.LOC)
						)
					)
				)
			);
			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.Greater(results.Total, 0);
			Assert.Greater(results.Total, 10);
			
			// assert we actually filtered on something
			var totalInIndex = this.ConnectedClient.Count<ElasticSearchProject>(q=>q.MatchAll()).Count;
			Assert.Less(results.Total, totalInIndex);
		}
	}
}
