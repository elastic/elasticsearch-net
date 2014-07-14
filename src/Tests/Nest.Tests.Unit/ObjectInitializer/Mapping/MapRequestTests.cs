using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Mapping
{
	[TestFixture]
	public class MapRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public MapRequestTests()
		{
			var request = new PutMappingRequest("an-index","a-type")
			{
				ExpandWildcards = ExpandWildcards.Open,
				Mapping = new RootObjectMapping
				{
					Name = "my_root_object",
					Properties = new Dictionary<PropertyNameMarker, IElasticType>
					{
						{"my_field", new StringMapping() { Name = "my_string_field ", Boost = 1.2}}
					}
				}
			};
			var response = this._client.Map(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/an-index/a-type/_mapping?expand_wildcards=open");
			this._status.RequestMethod.Should().Be("PUT");
		}

		[Test]
		public void MapBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}

}
