namespace Nest {
    public interface IElasticPropertyAttribute
    {
        bool AddSortField { get; set; }
        
        bool OptOut { get; set; }
        string Name { get; set; }

        FieldType Type { get; }

        TermVectorOption TermVector { get; set; }
        FieldIndexOption Index { get; set; }

        double Boost { get; set; }

        string Analyzer { get; set; }
        string IndexAnalyzer { get; set; }
        string SearchAnalyzer { get; set; }
        string SortAnalyzer { get; set; }
        string NullValue { get; set; }

        bool OmitNorms { get; set; }
        bool OmitTermFrequencyAndPositions { get; set; }
        bool IncludeInAll { get; set; }
        bool Store { get; set; }

        bool DocValues { get; set; }

        string Similarity { get; set; }

        /// <summary>
        /// Defaults to float so be sure to set this correctly!
        /// </summary>
        NumberType NumericType { get; set; }
        int PrecisionStep { get; set; }

        /// <summary>
        /// http://www.elasticsearch.org/guide/reference/mapping/date-format.html
        /// </summary>
        string DateFormat { get; set; }

        void Accept(IElasticPropertyVisitor visitor);
    }
}