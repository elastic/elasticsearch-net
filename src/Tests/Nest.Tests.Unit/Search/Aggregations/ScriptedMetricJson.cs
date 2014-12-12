
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Search.Aggregations
{
	[TestFixture]
	public class ScriptedMetricJson
	{
		[Test]
		public void ScriptedMetric()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Aggregations(a => a
					.ScriptedMetric("profit", sm => sm
						.Language("groovy")
						.InitScript("init script")
						.MapScript("map script")
						.CombineScript("combine script")
						.ReduceScript("reduce script")
						.ReduceParams(rp => rp
							.Add("param1", "value1")
							.Add("param2", "value2")
						)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"
				{ 
					from: 0, 
					size: 10, 
					aggs :  {
						""profit"" :  {
							scripted_metric : {
							lang: ""groovy"",
							init_script : ""init script"",
							map_script : ""map script"",
							combine_script: ""combine script"",
							reduce_script: ""reduce script"",
							reduce_params: {
								param1: ""value1"",
								param2: ""value2""
							}
						} 
					}
				}
			}";

			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
