using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.MultiGet
{
	[TestFixture]
	public class MultiGetRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public MultiGetRequestTests()
		{
			var request = new MultiGetRequest
			{
				GetOperations = new List<IMultiGetOperation>
				{
					{ new MultiGetOperation<ElasticsearchProject>(12) { Index = "some-other-index"}},
					{
						new MultiGetOperation<ElasticsearchProject>("composite-id")
						{
							Fields = new []
							{
								Property.Path<ElasticsearchProject>(p=>p.Name)
							},
							Routing = "routing-value",
						}
					}
				}

			};
			var response = this._client.MultiGet(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_mget");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void MultiGetBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
