using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ExtendedStatsAggregation>))]
	public interface IExtendedStatsAggregation : IMetricAggregation
	{
		[JsonProperty("sigma")]
		double? Sigma { get; set; }
	}

	public class ExtendedStatsAggregation : MetricAggregationBase, IExtendedStatsAggregation
	{
		internal ExtendedStatsAggregation() { }

		public ExtendedStatsAggregation(string name, Field field) : base(name, field) { }

		public double? Sigma { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStats = this;
	}

	public class ExtendedStatsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ExtendedStatsAggregationDescriptor<T>, IExtendedStatsAggregation, T>
			, IExtendedStatsAggregation
		where T : class
	{
		double? IExtendedStatsAggregation.Sigma { get; set; }

		public ExtendedStatsAggregationDescriptor<T> Sigma(double? sigma) =>
			Assign(a => a.Sigma = sigma);
	}
}
