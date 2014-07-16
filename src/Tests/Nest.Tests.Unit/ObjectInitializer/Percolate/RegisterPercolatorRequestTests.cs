using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Percolate
{
	[TestFixture]
	public class RegisterPercolatorRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public RegisterPercolatorRequestTests()
		{
			var request = new RegisterPercolatorRequest("index", "percolator-name")
			{
				MetaData = new Dictionary<string, object>
				{
					{ "color", "blue"}
				},
				Query = Query<ElasticsearchProject>.Term(p=>p.Name, "NEST")
			};
			var response = this._client.RegisterPercolator(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/index/.percolator/percolator-name");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void RegisterPercolatorBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}

}
