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

namespace Nest.Tests.Unit.Core.Template
{
	[TestFixture]
	public class PutWarmerRequestTests : BaseJsonTests
	{

		[Test]
		public void PathWithType()
		{
			var result = this._client.PutWarmer("warmer_pathwithtype", wd => wd
				.Index<ElasticSearchProject>()
				.Type<ElasticSearchProject>()
				.Search<ElasticSearchProject>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/_warmer/warmer_pathwithtype", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void PathWithDynamic()
		{
			var result = this._client.PutWarmer("warmer_pathwithdynamic", wd=> wd
				.Index("my_index")
				.Search<dynamic>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/my_index/_warmer/warmer_pathwithdynamic", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void PathWithAllIndices()
		{
			var result = this._client.PutWarmer("warmer_pathwithallindices", wd => wd
				.AllIndices()
				.Search<dynamic>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/_all/_warmer/warmer_pathwithallindices", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
	}
}
