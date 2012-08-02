using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class ElasticProperty : IElasticProperty
	{
		public bool OptOut { get; set; }
		public string Name { get; set; }

		public FieldType Type { get; set; }

		public TermVectorOption TermVector { get; set; }
		public FieldIndexOption Index { get; set; }

		public decimal Boost { get; set; }


		public string Analyzer { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string NullValue { get; set; }

		public bool OmitNorms { get; set; }
		public bool OmitTermFrequencyAndPositions { get; set; }
		public bool IncludeInAll { get; set; }
		public bool Store { get; set; }

		/// <summary>
		/// Defaults to float so be sure to set this correctly!
		/// </summary>
		public NumericType NumericType { get; set; }
		public int PrecisionStep { get; set; }

		/// <summary>
		/// http://www.elasticsearch.org/guide/reference/mapping/date-format.html
		/// </summary>
		public string DateFormat { get; set; }

		public ElasticProperty()
		{
			//make sure we match ES's defaults
			this.Boost = 1;
			this.TermVector = TermVectorOption.no; 
			this.Index = FieldIndexOption.analyzed;
		
			this.IncludeInAll = true;
			this.PrecisionStep = 4;
		}
	}



	
	
	

	
}
