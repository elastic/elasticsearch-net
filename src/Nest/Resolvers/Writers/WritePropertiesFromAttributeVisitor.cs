using System;
using System.Globalization;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest.Resolvers.Writers {
    public class WritePropertiesFromAttributeVisitor : IElasticPropertyVisitor
    {
        private readonly JsonWriter _jsonWriter;
        private readonly string _propertyName;
        private readonly string _type;

        public WritePropertiesFromAttributeVisitor(JsonWriter jsonWriter, string propertyName, string type)
        {
            this._jsonWriter = jsonWriter;
            this._propertyName = propertyName;
            this._type = type;
        }

        public void Visit(ElasticPropertyAttribute attribute)
        {
            this.VisitBaseAttribute(attribute);
        }

        public void VisitBaseAttribute(IElasticPropertyAttribute att) {
            if (att.AddSortField)
            {
                this._jsonWriter.WritePropertyName("type");
                this._jsonWriter.WriteValue("multi_field");
                this._jsonWriter.WritePropertyName("fields");
                this._jsonWriter.WriteStartObject();
                this._jsonWriter.WritePropertyName(this._propertyName);
                this._jsonWriter.WriteStartObject();
            }
            if (att.NumericType != NumericType.Default)
            {
                this._jsonWriter.WritePropertyName("type");
                string numericType = Enum.GetName(typeof (NumericType), att.NumericType);
                this._jsonWriter.WriteValue(numericType.ToLowerInvariant());
            }
            else
            {
                this._jsonWriter.WritePropertyName("type");
                this._jsonWriter.WriteValue(this._type);
            }
            if (!att.Analyzer.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("analyzer");
                this._jsonWriter.WriteValue(att.Analyzer);
            }
            if (!att.IndexAnalyzer.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("index_analyzer");
                this._jsonWriter.WriteValue(att.IndexAnalyzer);
            }
            if (!att.IndexAnalyzer.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("index_analyzer");
                this._jsonWriter.WriteValue(att.IndexAnalyzer);
            }
            if (!att.NullValue.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("null_value");
                this._jsonWriter.WriteValue(att.NullValue);
            }
            if (!att.SearchAnalyzer.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("search_analyzer");
                this._jsonWriter.WriteValue(att.SearchAnalyzer);
            }
            if (!att.DateFormat.IsNullOrEmpty())
            {
                this._jsonWriter.WritePropertyName("format");
                this._jsonWriter.WriteValue(att.DateFormat);
            }
            if (att.Index != FieldIndexOption.analyzed)
            {
                this._jsonWriter.WritePropertyName("index");
                this._jsonWriter.WriteValue(Enum.GetName(typeof (FieldIndexOption), att.Index));
            }
            if (att.TermVector != TermVectorOption.no)
            {
                this._jsonWriter.WritePropertyName("term_vector");
                this._jsonWriter.WriteValue(Enum.GetName(typeof (TermVectorOption), att.TermVector));
            }
            if (att.OmitNorms)
            {
                this._jsonWriter.WritePropertyName("omit_norms");
                this._jsonWriter.WriteValue("true");
            }
            if (att.OmitTermFrequencyAndPositions)
            {
                this._jsonWriter.WritePropertyName("omit_term_freq_and_positions");
                this._jsonWriter.WriteValue("true");
            }
            if (!att.IncludeInAll)
            {
                this._jsonWriter.WritePropertyName("include_in_all");
                this._jsonWriter.WriteValue("false");
            }
            if (att.Store)
            {
                this._jsonWriter.WritePropertyName("store");
                this._jsonWriter.WriteValue("yes");
            }
            if (att.Boost != 1)
            {
                this._jsonWriter.WritePropertyName("boost");
                this._jsonWriter.WriteRawValue(att.Boost.ToString(CultureInfo.InvariantCulture));
            }
            if (att.PrecisionStep != 4)
            {
                this._jsonWriter.WritePropertyName("precision_step");
                this._jsonWriter.WriteRawValue(att.PrecisionStep.ToString(CultureInfo.InvariantCulture));
            }

            if (!att.Similarity.IsNullOrEmpty())
            {
              this._jsonWriter.WritePropertyName("similarity");
              this._jsonWriter.WriteValue(att.Similarity);
            }
            if (att.AddSortField)
            {
                this._jsonWriter.WriteEnd();
                this._jsonWriter.WritePropertyName("sort");
                this._jsonWriter.WriteStartObject();

                if (att.NumericType != NumericType.Default)
                {
                    this._jsonWriter.WritePropertyName("type");
                    string numericType = Enum.GetName(typeof (NumericType), att.NumericType);
                    this._jsonWriter.WriteValue(numericType.ToLowerInvariant());
                }
                else
                {
                    this._jsonWriter.WritePropertyName("type");
                    this._jsonWriter.WriteValue(this._type);
                }
                if (att.SortAnalyzer.IsNullOrEmpty())
                {
                    this._jsonWriter.WritePropertyName("index");
                    this._jsonWriter.WriteValue(Enum.GetName(typeof(FieldIndexOption), FieldIndexOption.not_analyzed));
                }
                else
                {
                    this._jsonWriter.WritePropertyName("index_analyzer");
                    this._jsonWriter.WriteValue(att.SortAnalyzer);
                }

                this._jsonWriter.WriteEnd();
                this._jsonWriter.WriteEnd();
            }
        }
    }
}