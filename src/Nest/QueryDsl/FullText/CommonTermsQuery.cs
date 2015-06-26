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
		ICommonTermsQuery Self => this;

		bool IQuery.IsConditionless => Self.Field.IsConditionless() || Self.Query.IsNullOrEmpty();

		CommonTermsQueryDescriptor<T> Assign(Action<ICommonTermsQuery> assign)
		{
			assign(Self);
			return this;
		}

		string IQuery.Name { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Name(string name) => Assign(a => a.Name = name);

		double? ICommonTermsQuery.Boost { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Boost(double boost) => Assign(a=>a.Boost = boost);

		PropertyPathMarker ICommonTermsQuery.Field { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> OnField(string field) => Assign(a => a.Field = field);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		void IFieldNameQuery.SetFieldName(string fieldName) => Self.Field = fieldName;

		PropertyPathMarker IFieldNameQuery.GetFieldName() => Self.Field;

		string ICommonTermsQuery.Query { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator op) => Assign(a => a.LowFrequencyOperator = op);

		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op) => Assign(a => a.HighFrequencyOperator = op);

		string ICommonTermsQuery.Analyzer { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> CutoffFrequency(double cutOffFrequency) => 
			Assign(a => a.CutoffFrequency = cutOffFrequency);

		string ICommonTermsQuery.MinimumShouldMatch { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatch) =>
			Assign(a=>a.MinimumShouldMatch = minimumShouldMatch.ToString(CultureInfo.InvariantCulture));

		bool? ICommonTermsQuery.DisableCoord { get; set; }
		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true) => Assign(a => a.DisableCoord = disable);
	}
}
