using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.MultiSearch
{
	[TestFixture]
	public class MultiSearchRequestTest : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public MultiSearchRequestTest()
		{
			var request = new MultiSearchRequest
			{
				Operations = new Dictionary<string, ISearchRequest>
				{
					{ "first-search", new SearchRequest
					{
						Indices = new IndexNameMarker[] {"my-index"},
						Query = Query<ElasticsearchProject>.Term(p=>p.Name, "NEST"),
						SearchType = SearchType.Scan,
						Preference = "local",
						Routing = new string[] {"routingvalue"}
						
					}},
					{ "second-search", new SearchRequest
					{
						Query = Query<ElasticsearchProject>.Term(p=>p.Contributors.First().LastName, "Laarman")
					}},
				}
			};
			var response = this._client.MultiSearch(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_msearch");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void MultiSearchBody()
		{
			this.BulkJsonEquals(this._status.Request.Utf8String(), MethodBase.GetCurrentMethod());
		}
	}
}
