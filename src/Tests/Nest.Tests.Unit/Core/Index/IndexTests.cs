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

namespace Nest.Tests.Unit.Core.Index
{
  [TestFixture]
  public class IndexTests : BaseJsonTests
  {
    [Test]
    public void IndexParameters()
    {
      var o = new ElasticsearchProject { Id = 1, Name = "Test" };
      var result = this._client.Index(o, i=>i.Version(1));
      var status = result.ConnectionStatus;
      StringAssert.Contains("version=1", status.RequestUrl);
    }

    [Test]
    public void IndexingDictionaryRespectsCasing()
    {
      var x = new
      {
        FirstDictionary = new Dictionary<string, object>
				{
					{"ALLCAPS", 1 },
					{"PascalCase", "should work as well"},
					{"camelCase", DateTime.Now}
				}
      };
      var result = this._client.Index(x);

      var request = result.ConnectionStatus.Request;
      StringAssert.Contains("ALLCAPS", request);
      StringAssert.Contains("PascalCase", request);
      StringAssert.Contains("camelCase", request);
      StringAssert.Contains("firstDictionary", request);
    }
  }
}
