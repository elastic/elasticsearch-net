using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICommonTermsQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "low_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? LowFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "high_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? HighFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "disable_coord")]
		bool? DisableCoord { get; set; }
	}

	public class CommonTermsQuery : PlainQuery, ICommonTermsQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public string Query { get; set; }
		public PropertyPathMarker Field { get; set; }
		public double? CutoffFrequency { get; set; }
		public Operator? LowFrequencyOperator { get; set; }
		public Operator? HighFrequencyOperator { get; set; }
		public string MinimumShouldMatch { get; set; }
		public double? Boost { get; set; }
		public string Analyzer { get; set; }
		public bool? DisableCoord { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.CommonTerms = this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}
	}

	public class CommonTermsQueryDescriptor<T> : ICommonTermsQuery where T : class
	{
		private ICommonTermsQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Query.IsNullOrEmpty();
			}
		}
		string ICommonTermsQuery.Query { get; set; }
		PropertyPathMarker ICommonTermsQuery.Field { get; set; }
		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }
		string ICommonTermsQuery.MinimumShouldMatch { get; set; }
		double? ICommonTermsQuery.Boost { get; set; }
		string ICommonTermsQuery.Analyzer { get; set; }
		bool? ICommonTermsQuery.DisableCoord { get; set; }

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}

		public CommonTermsQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public CommonTermsQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public CommonTermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public CommonTermsQueryDescriptor<T> Query(string query)
		{
			Self.Query = query;
			return this;
		}

		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator op)
		{
			Self.LowFrequencyOperator = op;
			return this;
		}

		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op)
		{
			Self.HighFrequencyOperator = op;
			return this;
		}

		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}
		
		public CommonTermsQueryDescriptor<T> CutoffFrequency(double cutOffFrequency)
		{
			Self.CutoffFrequency = cutOffFrequency;
			return this;
		}

		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch)
		{
			Self.MinimumShouldMatch = minimumShouldMatch;
			return this;
		}

		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatch)
		{
			Self.MinimumShouldMatch = minimumShouldMatch.ToString(CultureInfo.InvariantCulture);
			return this;
		}

		public CommonTermsQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true)
		{
			Self.DisableCoord = disable;
			return this;
		}
	}
}
