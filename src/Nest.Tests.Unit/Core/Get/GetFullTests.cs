using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Get
{
	[TestFixture]
	public class GetFullTests : BaseJsonTests
	{
		[Test]
		public void GetSimple()
		{
			var result = this._client.GetFull<ElasticSearchProject>(1);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/1", status.RequestUrl);
		}
		[Test]
		public void GetWithPathInfo()
		{
			var result = this._client.GetFull<ElasticSearchProject>("myindex", "mytype", 404);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/mytype/404", status.RequestUrl);
		}
		[Test]
		public void GetUsingDescriptor()
		{
			var result = this._client.GetFull<ElasticSearchProject>(g=>g
				.Index("myindex")
				.Id(404)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/elasticsearchprojects/404", status.RequestUrl);
		}
		[Test]
		public void GetUsingDescriptorEscapes()
		{
			var result = this._client.GetFull<ElasticSearchProject>(g => g
				.Index("myindex")
				.Id("myid/with/slashes")
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/elasticsearchprojects/myid%252Fwith%252Fslashes", status.RequestUrl);
		}
		[Test]
		public void GetUsingDescriptorWithType()
		{
			var result = this._client.GetFull<ElasticSearchProject>(g => g
				.Index("myindex")
				.Type("mytype")
				.Id(404)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/mytype/404", status.RequestUrl);
		}
		[Test]
		public void GetUsingDescriptorWithTypeAndFields()
		{
			var result = this._client.GetFull<ElasticSearchProject>(g => g
				.Index("myindex")
				.Type("mytype")
				.Id(404)
				.Fields(p=>p.Content, p=>p.Name)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/mytype/404?fields=content,name", status.RequestUrl);
		}
	}
}
