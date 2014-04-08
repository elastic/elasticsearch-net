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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CommonTermsQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal string _QueryString { get; set; }

		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }
		
		[JsonProperty(PropertyName = "cutoff_frequency")]
		internal double? _CutoffFrequency { get; set; }
		
		[JsonProperty(PropertyName = "low_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _LowFrequencyOperator { get; set; }
		
		[JsonProperty(PropertyName = "high_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _HighFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		internal int? _MinimumShouldMatch { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }
		
		[JsonProperty(PropertyName = "disable_coord")]
		internal bool? _DisableCoord { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() || this._QueryString.IsNullOrEmpty();
			}
		}


		public CommonTermsQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public CommonTermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public CommonTermsQueryDescriptor<T> Query(string query)
		{
			this._QueryString = query;
			return this;
		}
		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator op)
		{
			this._LowFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op)
		{
			this._HighFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
		
		public CommonTermsQueryDescriptor<T> CutOffFrequency(double cutOffFrequency)
		{
			this._CutoffFrequency = cutOffFrequency;
			return this;
		}
		public CommonTermsQueryDescriptor<T> MinumumShouldMatch(int minimumShouldMatch)
		{
			this._MinimumShouldMatch = minimumShouldMatch;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true)
		{
			this._DisableCoord = disable;
			return this;
		}

	}
}
