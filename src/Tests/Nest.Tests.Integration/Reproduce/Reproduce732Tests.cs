using System.Dynamic;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce732Tests : IntegrationTests
	{
		[ElasticType(Name = "load", IdProperty = "Id")]
		public class Load
		{
			public int Id { get; set; }

			public int CustomerId { get; set; }

			public string CustomerName { get; set; }

			public double Total { get; set; }
		}

		[Test]
		public void UpdateUsingDynamicObject()
		{
			var id = NestTestData.Data.Last().Id;
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			Assert.NotNull(project);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject, dynamic>(u => u
				.Id(id)
				.Doc(new
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

		[Test]
		public void TestUpdateUsingPartial()
		{
			Load load = new Load
			{
				Id = 1,
				CustomerId = 3,
				CustomerName = "Customer",
				Total = 0.00
			};

			this.Client.Index(load);

			dynamic partial_load = new ExpandoObject();
			partial_load.Id = load.Id;
			partial_load.CustomerId = 3;

			Assert.IsTrue(this.UpdateLoad(partial_load));
		}

		[Test]
		public void TestUpdateUsingPartialAndBulk()
		{
			Load load = new Load
			{
				Id = 2,
				CustomerId = 3,
				CustomerName = "Other",
				Total = 1.00
			};

			this.Client.Index(load);

			dynamic partial_load = new ExpandoObject();
			partial_load.Id = load.Id;
			partial_load.CustomerId = 3;

			Assert.IsTrue(this.UpdateViaBulk(partial_load));
		}

		public bool UpdateViaBulk(dynamic partial)
		{
			IBulkResponse response = Client.Bulk(b => b
				.Update<Load, dynamic>(u => u
					.Id(partial.Id)
					.Doc(partial)
				));

			return response.IsValid;
		}
		public bool UpdateLoad(dynamic partial)
		{
			IUpdateResponse response = Client.Update<Load, dynamic>(u => u
				.Id((int)partial.Id)
				.Doc((object)partial)
			);
			return response.IsValid;
		}
	}
}
