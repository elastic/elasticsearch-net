using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class AliasRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public AliasRequestTests()
		{
			var request = new AliasRequest()
			{
				Actions = new List<IAliasAction>
				{
					new AliasAddAction { Add = new AliasAddOperation { Index = "myindex-2014-2-2", Alias = "myindex"}},
					new AliasRemoveAction() { Remove = new AliasRemoveOperation() { Index = "myindex-2014-3-2", Alias = "myindex"}},
				}
			};
			var response = this._client.Alias(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_aliases");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void AliasBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
