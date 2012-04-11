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
using Nest.TestData.Domain;

namespace Nest.Tests.DSL
{
	[TestFixture]
	public class SortTests
	{
		[Test]
		public void TestSort()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
        .Sort(sort=>sort
          .OnField(e=>e.Name)
          .MissingLast()
          .Descending()
      );
			var json = ElasticClient.Serialize(s);
			var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            name: {
              missing: ""_last"",
              order: ""desc""
            }
          }
        }";
			Assert.True(json.JsonEquals(expected), json);
		}
    [Test]
    public void TestSortGeo()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .SortGeoDistance(sort => sort
          .OnField(e => e.Name)
          .MissingLast()
          .Descending()
          .PinTo(40, -70)
          .Unit(GeoUnit.km)
      );
      var json = ElasticClient.Serialize(s);
      var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            name: {
              missing: ""_last"",
              order: ""desc"",
              ""pin.location"": ""40, -70"",
              unit: ""km""
            }
          }
        }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestSortScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .SortScript(sort => sort
          .MissingLast()
          .Descending()
          .Script("doc['field_name'].value * factor")
          .Params(p=>p
            .Add("factor", 1.1)
          )
          .Type("number")
      );
      var json = ElasticClient.Serialize(s);
      var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            _script: {
              missing: ""_last"",
              order: ""desc"",
              type: ""number"",
              script: ""doc['field_name'].value * factor"",
              params: {
                factor: 1.1
              }
            }
          }
        }";
      Assert.True(json.JsonEquals(expected), json);
    }
	}
}
