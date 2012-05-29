using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.TestData.Domain;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class UpdateIntegrationTests : BaseElasticSearchTests
	{
		[Test]
		public void TestUpdate()
		{
			this.ResetIndexes();
			var project = this.ConnectedClient.Get<ElasticSearchProject>(1);
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.ConnectedClient.Update<ElasticSearchProject>(u => u
			  .Object(project)
			  .Script("ctx._source.loc += 10")
			  .RetriesOnConflict(5)
			  .Refresh()
			);
			project = this.ConnectedClient.Get<ElasticSearchProject>(1);
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
