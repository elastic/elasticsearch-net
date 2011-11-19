using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Mapping
{
	public interface IElasticType 
	{
		string Name { get; set; }
		string IndexAnalyzer { get; set; }
		string SearchAnalyzer { get; set; }
		string[] DynamicDateFormats { get; set; }
		bool DateDetection { get; set; }
		bool NumericDetection { get; set; }
		
	}

	public class ElasticType : IElasticType
	{
		public string Name { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string[] DynamicDateFormats { get; set; }
		public bool DateDetection { get; set; }
		public bool NumericDetection { get; set; }
		public ElasticType()
		{
			this.DateDetection = true;
		}
	}

}
