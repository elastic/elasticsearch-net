using System;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public class ElasticPropertyAttribute : Attribute, IElasticPropertyAttribute
	{
		public bool AddSortField { get; set; }

		public bool OptOut { get; set; }

		public string Name { get; set; }

		public FieldType Type { get; set; }

		public TermVectorOption TermVector { get; set; }
		public FieldIndexOption Index { get; set; }

		public double Boost { get; set; }

		public string Analyzer { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string SortAnalyzer { get; set; }
		public string NullValue { get; set; }
		public string Similarity { get; set; }

		public bool DocValues { get; set; }

		public bool OmitNorms { get; set; }
		public bool OmitTermFrequencyAndPositions { get; set; }
		public bool IncludeInAll { get; set; }
		public bool Store { get; set; }

		/// <summary>
		/// Defaults to float so be sure to set this correctly!
		/// </summary>
		public NumberType NumericType { get; set; }
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

			this.IncludeInAll = true;
			this.PrecisionStep = 4;
		}

		public void Accept(IElasticPropertyVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
