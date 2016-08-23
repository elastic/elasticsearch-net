using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Serialization;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce2208Tests : BaseJsonTests
	{
		[Test]
		public void SerializesFormat()
		{
			var weight = 1;
			var earliestDate = new DateTime(2016,1,1);
			var farthestDate = new DateTime(2016,8,1);

			var function = new FunctionScoreFunction<GroupListSearchDataContract>()
				.Weight(weight)
				.Filter(fi => fi
					.Bool(b => b
						.Should(s => s
							.Range(r => r
								.OnField(f => f.RoleCriteria.TargetMoveIn)
								.GreaterOrEquals(earliestDate)
								.LowerOrEquals(farthestDate)
								.Format("yyyy-MM-dd'T'HH:mm:ss.SSS")
							)
						)
					)
				);

			var bytes = _client.Serializer.Serialize(function);
			var json = Encoding.UTF8.GetString(bytes);
			Assert.IsTrue(json.Contains("\"format\""));
		}

		[Test]
		public void DeserializesFormatAndTimezone()
		{
			var json = @"{
			  ""filter"": {
				""bool"": {
				  ""should"": [
					{
					  ""range"": {
						""roleCriteria.targetMoveIn"": {
						  ""lte"": ""2016-08-01T00:00:00.000"",
						  ""gte"": ""2016-01-01T00:00:00.000"",
						  ""format"": ""yyyy-MM-dd'T'HH:mm:ss.SSS"",
						  ""time_zone"": ""+10:00""
						}
					  }
					}
				  ]
				}
			  },
			  ""weight"": 1.0
			}";

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				var deserializedFunction = _client.Serializer.Deserialize<IFunctionScoreFunction>(stream);
				var rangeFilter = ((IFilterContainer)deserializedFunction.Filter).Bool.Should.First().Range;

				Assert.IsNotNullOrEmpty(rangeFilter.Format);
				Assert.IsNotNullOrEmpty(rangeFilter.TimeZone);
			}
		}

		public class GroupListSearchDataContract
		{
			public RoleCriteria RoleCriteria { get; set; }
		}

		public class RoleCriteria
		{
			public DateTime TargetMoveIn { get; set; }
		}
	}
}