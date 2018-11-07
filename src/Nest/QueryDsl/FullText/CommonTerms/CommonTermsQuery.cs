using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<CommonTermsQuery>))]
	public interface ICommonTermsQuery : IFieldNameQuery
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty("high_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? HighFrequencyOperator { get; set; }

		[JsonProperty("low_freq_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? LowFrequencyOperator { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("query")]
		string Query { get; set; }
	}

	public class CommonTermsQuery : FieldNameQueryBase, ICommonTermsQuery
	{
		public string Analyzer { get; set; }
		public double? CutoffFrequency { get; set; }
		public Operator? HighFrequencyOperator { get; set; }
		public Operator? LowFrequencyOperator { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public string Query { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.CommonTerms = this;

		internal static bool IsConditionless(ICommonTermsQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class CommonTermsQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<CommonTermsQueryDescriptor<T>, ICommonTermsQuery, T>
			, ICommonTermsQuery
		where T : class
	{
		protected override bool Conditionless => CommonTermsQuery.IsConditionless(this);
		string ICommonTermsQuery.Analyzer { get; set; }
		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		Field IFieldNameQuery.Field { get; set; }
		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }
		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		MinimumShouldMatch ICommonTermsQuery.MinimumShouldMatch { get; set; }
		string IQuery.Name { get; set; }
		string ICommonTermsQuery.Query { get; set; }

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator? op) => Assign(a => a.HighFrequencyOperator = op);

		public CommonTermsQueryDescriptor<T> LowFrequencyOperator(Operator? op) => Assign(a => a.LowFrequencyOperator = op);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> CutoffFrequency(double? cutOffFrequency) => Assign(a => a.CutoffFrequency = cutOffFrequency);

		/// <inheritdoc />
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);
	}
}
