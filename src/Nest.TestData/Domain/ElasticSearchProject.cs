using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using ElasticSearch.Client.Mapping;

namespace Nest.TestData.Domain
{
	[ElasticType(
		Name = "elasticsearchprojects",
		DateDetection = true,
		NumericDetection = true,
		SearchAnalyzer = "standard",
		IndexAnalyzer = "standard",
		DynamicDateFormats = new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" }
	)]
	public class ElasticSearchProject
	{
		public int Id { get; set;  }
		public string Name { get; set; }
		public string Country { get; set; }
		public string Content { get; set; }
		[ElasticProperty(Name="loc")]
		public int LOC { get; set; }
		public List<Person> Followers { get; set; }

		[ElasticProperty(Type=FieldType.geo_point)]
		public GeoLocation Origin { get; set; }
		public DateTime StartedOn { get; set; }


		//excuse the lame properties i needed some numerics !
		public long LongValue { get; set; }
		public float FloatValue { get; set; }
		public double DoubleValue { get; set; }

	}
}
