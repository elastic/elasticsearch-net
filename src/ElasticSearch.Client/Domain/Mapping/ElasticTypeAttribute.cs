using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Mapping
{
	//TODO dynamic template support
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class ElasticTypeAttribute : Attribute, IElasticType
	{
		public string Name { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string[] DynamicDateFormats { get; set; }
		public bool DateDetection { get; set; }
		public bool NumericDetection { get; set; }
		public string ParentType { get; set; }
		public ElasticTypeAttribute()
		{
			this.DateDetection = true;
		}
	}

}
