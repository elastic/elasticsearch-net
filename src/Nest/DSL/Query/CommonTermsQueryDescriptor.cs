using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;
using Nest.Resolvers;

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
		int? MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "disable_coord")]
		bool? DisableCoord { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CommonTermsQueryDescriptor<T> : ICommonTermsQuery where T : class
	{
		string ICommonTermsQuery.Query { get; set; }

		PropertyPathMarker ICommonTermsQuery.Field { get; set; }
		
		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		
		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		
		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }

		int? ICommonTermsQuery.MinimumShouldMatch { get; set; }
		
		double? ICommonTermsQuery.Boost { get; set; }
		
		string ICommonTermsQuery.Analyzer { get; set; }
		
		bool? ICommonTermsQuery.DisableCoord { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICommonTermsQuery)this).Field.IsConditionless() || ((ICommonTermsQuery)this).Query.IsNullOrEmpty();
			}
		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((ICommonTermsQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((ICommonTermsQuery)this).Field;
		}

		public CommonTermsQueryDescriptor<T> OnField(string field)
		{
			((ICommonTermsQuery)this).Field = field;
			return this;
		}
		public CommonTermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((ICommonTermsQuery)this).Field = objectPath;
			return this;
		}

		public CommonTermsQueryDescriptor<T> Query(string query)
		{
			((ICommonTermsQuery)this).Query = query;
			return this;
		}
		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator op)
		{
			((ICommonTermsQuery)this).LowFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op)
		{
			((ICommonTermsQuery)this).HighFrequencyOperator = op;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer)
		{
			((ICommonTermsQuery)this).Analyzer = analyzer;
			return this;
		}
		
		public CommonTermsQueryDescriptor<T> CutOffFrequency(double cutOffFrequency)
		{
			((ICommonTermsQuery)this).CutoffFrequency = cutOffFrequency;
			return this;
		}
		public CommonTermsQueryDescriptor<T> MinumumShouldMatch(int minimumShouldMatch)
		{
			((ICommonTermsQuery)this).MinimumShouldMatch = minimumShouldMatch;
			return this;
		}
		public CommonTermsQueryDescriptor<T> Boost(double boost)
		{
			((ICommonTermsQuery)this).Boost = boost;
			return this;
		}
		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true)
		{
			((ICommonTermsQuery)this).DisableCoord = disable;
			return this;
		}

	}
}
