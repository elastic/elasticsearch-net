using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace Nest.Tests.MockData.Domain
{
	[ElasticType(Name = "elasticsearchprojects")]
	public class ElasticsearchProject
	{
		public int Id { get; set;  }
		[ElasticProperty(AddSortField=true)]
		public string Name { get; set; }
		public string Version { get; set; }
		[ElasticProperty(
			OmitNorms = true, 
			Index = FieldIndexOption.not_analyzed )]
		public string Country { get; set; }
		public string Content { get; set; }
		[ElasticProperty(Name="loc",AddSortField=true)]
		public int LOC { get; set; }
		public List<Person> Followers { get; set; }

		[ElasticProperty(Type=FieldType.nested)]
		public List<Person> Contributors { get; set; }
		
		public List<Person> NestedFollowers { get; set; }

		[ElasticProperty(Type=FieldType.geo_point)]
		public GeoLocation Origin { get; set; }
		public DateTime StartedOn { get; set; }

		[ElasticProperty(Type=FieldType.ip)]
		public string PingIP { get; set; }

		public GeoShape MyGeoShape { get; set; }

		//excuse the lame properties i needed some numerics !
		public long LongValue { get; set; }
		public float FloatValue { get; set; }
		public double DoubleValue { get; set; }
		public bool BoolValue { get; set; }
		public List<int> IntValues { get; set; }
		public float[] FloatValues { get; set; }

		public int LocScriptField { get; set; }

		[ElasticProperty(NumericType=NumericType.Long)]
		public int StupidIntIWantAsLong { get; set; }

		public string MyAttachment { get; set; }

		public string MyBinaryField { get; set; }

	}
}
