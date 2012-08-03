using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class IElasticProperty 
	{
		bool OptOut { get; set; }
		string Name { get; set; }

		FieldType Type { get; set; }

		TermVectorOption TermVector { get; set; }
		FieldIndexOption Index { get; set; }

		decimal Boost { get; set; }


		string Analyzer { get; set; }
		string IndexAnalyzer { get; set; }
		string SearchAnalyzer { get; set; }
		string NullValue { get; set; }

		bool OmitNorms { get; set; }
		bool OmitTermFrequencyAndPositions { get; set; }
		bool IncludeInAll { get; set; }
		bool Store { get; set; }

		/// <summary>
		/// Defaults to float so be sure to set this correctly!
		/// </summary>
		NumericType NumericType { get; set; }
		int PrecisionStep { get; set; }

		/// <summary>
		/// http://www.elasticsearch.org/guide/reference/mapping/date-format.html
		/// </summary>
		string DateFormat { get; set; }
	}



	
	
	

	
}
