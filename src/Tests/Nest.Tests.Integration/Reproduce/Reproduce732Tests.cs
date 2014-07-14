using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce732Tests : IntegrationTests
	{
		[Test]
		public void UpdateUsingDynamicObject()
		{
			var id = NestTestData.Data.Last().Id;
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			Assert.NotNull(project);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject, dynamic>(u => u
				.Id(id)
				.Document(new
				{
					Id = project.Id,
					LOC = project.LOC + 10
				})
				.Refresh()
			);
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
