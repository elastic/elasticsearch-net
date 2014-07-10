using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.CreateIndex
{
	[TestFixture]
	public class CreateIndexRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public CreateIndexRequestTests()
		{
			var request = new CreateIndexRequest("new-index-name")
			{
				IndexSettings = new IndexSettings
				{
					Settings = new Dictionary<string, object>
					{
						{"index.settings", "value"}
					},
					Mappings = new List<RootObjectMapping>
					{
						{ new RootObjectMapping
						{
							Name = "my_root_object",
							Properties = new Dictionary<PropertyNameMarker, IElasticType>
							{
								{"my_field", new StringMapping() { Name = "my_string_field "}}
							}
						}}
					}
				}
			};
			var response = this._client.CreateIndex(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/new-index-name");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void CreateIndexBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
