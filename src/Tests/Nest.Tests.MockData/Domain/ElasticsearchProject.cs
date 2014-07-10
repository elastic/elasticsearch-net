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
			Index = FieldIndexOption.NotAnalyzed )]
		public string Country { get; set; }
		public string Content { get; set; }
		[ElasticProperty(Name="loc",AddSortField=true)]
		public int LOC { get; set; }
		public List<Person> Followers { get; set; }

		[ElasticProperty(Type=FieldType.Nested)]
		public List<Person> Contributors { get; set; }
		
		public List<Person> NestedFollowers { get; set; }

		[ElasticProperty(Type=FieldType.GeoPoint)]
		public GeoLocation Origin { get; set; }
		public DateTime StartedOn { get; set; }

		[ElasticProperty(Type=FieldType.Ip)]
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

		[ElasticProperty(NumericType=NumberType.Long)]
		public int StupidIntIWantAsLong { get; set; }

		public string MyAttachment { get; set; }

		public string MyBinaryField { get; set; }

		[ElasticProperty(Type=FieldType.Object)]
		public Product Product { get; set; }

        public string[] MyStringArrayField { get; set; }
	}
}
