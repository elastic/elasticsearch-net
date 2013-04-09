using System;

namespace Nest
{
	[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class NestedElasticPropertyAttribute : Attribute, IElasticPropertyAttribute
    {
        public bool AddSortField { get; set; }
        public bool OptOut { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; private set; }
        public TermVectorOption TermVector { get; set; }
        public FieldIndexOption Index { get; set; }
        public double Boost { get; set; }
        public string Analyzer { get; set; }
        public string IndexAnalyzer { get; set; }
        public string SearchAnalyzer { get; set; }
        public string NullValue { get; set; }
        public bool OmitNorms { get; set; }
        public bool OmitTermFrequencyAndPositions { get; set; }
        public bool IncludeInAll { get; set; }
        public bool Store { get; set; }
        public NumericType NumericType { get; set; }
        public int PrecisionStep { get; set; }
        public string DateFormat { get; set; }

        public bool IncludeInParent { get; set; }
        public bool IncludeInRoot { get; set; }

        public NestedElasticPropertyAttribute()
        {
            Type = FieldType.nested;

            //make sure we match ES's defaults
			this.Boost = 1;
			this.TermVector = TermVectorOption.no; 
			this.Index = FieldIndexOption.analyzed;
		
			this.IncludeInAll = true;
			this.PrecisionStep = 4;
        }
        
        public void Accept(IElasticPropertyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}