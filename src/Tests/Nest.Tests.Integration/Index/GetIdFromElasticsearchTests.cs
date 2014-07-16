using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class GetIdFromElasticSearchTests : IntegrationTests
	{

		public class LogClass
		{
			public string Message { get; set; }
		}
		[Test]
		public void IndexWithoutIdShouldSetIdFromElasticseach()
		{
			var newProject = new LogClass
			{
				Message = "Some Message",
			};
			var response = this.Client.Index(newProject);
			Assert.IsTrue(response.IsValid);
			Assert.IsNotNullOrEmpty(response.Id);
			Assert.IsNotNullOrEmpty(response.Type);
			Assert.IsNotNullOrEmpty(response.Index);
			Assert.IsNotNullOrEmpty(response.Version);
		}
		[Test]
		public void IndexmanyWithoutIdShouldSetIdFromElasticseach()
		{
			var newProjects = new List<LogClass>
			{
				new LogClass { Message = "Some Message1" },
				new LogClass { Message = "Some Message2" },
				new LogClass { Message = "Some Message3" }
			};
			var response = this.Client.IndexMany(newProjects);
			Assert.IsTrue(response.IsValid, response.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.IsFalse(response.Errors, response.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.IsNotEmpty(response.Items);
			Assert.True(response.Items.All(i => !i.Id.IsNullOrEmpty()));
		}
	}
}
