using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class GetIdFromElasticSearchTests : BaseElasticSearchTests
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
      var response = this.ConnectedClient.Index(newProject);
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
      var response = this.ConnectedClient.IndexMany(newProjects);
      Assert.IsTrue(response.IsValid, response.ConnectionStatus.Result);
      Assert.IsNotEmpty(response.Items);
      Assert.True(response.Items.All(i => i.OK), response.ConnectionStatus.Result);
      Assert.True(response.Items.All(i => !i.Id.IsNullOrEmpty()));
    }
	}
}
