using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Repository
{
	[TestFixture]
	public class CreateRepositoryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public CreateRepositoryRequestTests()
		{
			var request = new CreateRepositoryRequest("my-new-repos")
			{
				Repository = new AzureRepository
				{
					Settings = new Dictionary<string, object>
					{
						{"bucket", "ad4423423fasf"}
					}
				}
			};
			var response = this._client.CreateRepository(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/my-new-repos");
			this._status.RequestMethod.Should().Be("PUT");
		}
		
		[Test]
		public void CreateRepositoryBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}

}
