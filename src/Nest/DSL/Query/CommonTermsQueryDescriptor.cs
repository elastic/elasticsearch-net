using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public interface ICommonTermsQuery
	{
		[JsonProperty(PropertyName = "query")]
		string _QueryString { get; set; }

		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? _CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "low_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? _LowFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "high_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? _HighFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		int? _MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "disable_coord")]
		bool? _DisableCoord { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CommonTermsQueryDescriptor<T> : IQuery, ICommonTermsQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		string ICommonTermsQuery._QueryString { get; set; }

		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker ICommonTermsQuery._Field { get; set; }
		
		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? ICommonTermsQuery._CutoffFrequency { get; set; }
		
		[JsonProperty(PropertyName = "low_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? ICommonTermsQuery._LowFrequencyOperator { get; set; }
		
		[JsonProperty(PropertyName = "high_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? ICommonTermsQuery._HighFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		int? ICommonTermsQuery._MinimumShouldMatch { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		double? ICommonTermsQuery._Boost { get; set; }
		
		[JsonProperty(PropertyName = "analyzer")]
		string ICommonTermsQuery._Analyzer { get; set; }
		
		[JsonProperty(PropertyName = "disable_coord")]
		bool? ICommonTermsQuery._DisableCoord { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICommonTermsQuery)this)._Field.IsConditionless() || ((ICommonTermsQuery)this)._QueryString.IsNullOrEmpty();
			}
		}


		public CommonTermsQueryDescriptor<T> OnField(string field)
		{
			((ICommonTermsQuery)this)._Field = field;
			return this;
		}
		public CommonTermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((ICommonTermsQuery)this)._Field = objectPath;
			return this;
		}

		public CommonTermsQueryDescriptor<T> Query(string query)
		{
			((ICommonTermsQuery)this)._QueryString = query;
			return this;
		}
		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator op)
		{
			((ICommonTermsQuery)this)._LowFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op)
		{
			((ICommonTermsQuery)this)._HighFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer)
		{
			((ICommonTermsQuery)this)._Analyzer = analyzer;
			return this;
		}
		
		public CommonTermsQueryDescriptor<T> CutOffFrequency(double cutOffFrequency)
		{
			((ICommonTermsQuery)this)._CutoffFrequency = cutOffFrequency;
			return this;
		}
		public CommonTermsQueryDescriptor<T> MinumumShouldMatch(int minimumShouldMatch)
		{
			((ICommonTermsQuery)this)._MinimumShouldMatch = minimumShouldMatch;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Boost(double boost)
		{
			((ICommonTermsQuery)this)._Boost = boost;
			return this;
		}
		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true)
		{
			((ICommonTermsQuery)this)._DisableCoord = disable;
			return this;
		}

	}
}
