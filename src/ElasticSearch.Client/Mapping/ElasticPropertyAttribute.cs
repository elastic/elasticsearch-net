using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Mapping
{
	[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public class ElasticPropertyAttribute : Attribute
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
		public object NullValue { get; set; }

		public bool OmitNorms { get; set; }
		public bool OmitTermFrequencyAndPositions { get; set; }
		public bool IncludeInAll { get; set;}
		public StoreOption Store { get; set; }

		/// <summary>
		/// Defaults to float so be sure to set this correctly!
		/// </summary>
		public NumericType NumericType { get; set; }
		public int PrecisionStep { get; set; }

		/// <summary>
		/// http://www.elasticsearch.org/guide/reference/mapping/date-format.html
		/// </summary>
		public string DateFormat { get; set; }

		public ElasticPropertyAttribute()
		{
			//make sure we match ES's defaults
			this.Boost = 1;
			this.TermVector = TermVectorOption.No; 
			this.Index = FieldIndexOption.Analyzed;
			this.Store = StoreOption.No;
			
			this.IncludeInAll = true;

			this.NumericType = NumericType.Float;
			this.PrecisionStep = 4;
		}
	}

	public enum DateFormatOption
	{

	}

	public enum NumericType
	{
		Float,
		Double, 
		Integer,
		Long,
		Short,
		Byte
	}


	public enum StoreOption
	{
		No, 
		Yes
	}
	public enum FieldIndexOption
	{
		Analyzed,
		NotAnalyzed ,
		None = 2
	}
	public enum TermVectorOption
	{
		No, 
		Yes, 
		WithOffsets,
		WithPositions,
		WithPositionsOffsets

	}

	
}
