using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ExtendedStatsAggregation>))]
	public interface IExtendedStatsAggregation : IMetricAggregation { }

	public class ExtendedStatsAggregation : MetricAggregationBase, IExtendedStatsAggregation
	{
		public override string TypeName => "extended_stats";

		internal ExtendedStatsAggregation() { }

		public ExtendedStatsAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStats = this;
	}

	public class ExtendedStatsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ExtendedStatsAggregationDescriptor<T>, IExtendedStatsAggregation, T>
			, IExtendedStatsAggregation
		where T : class
	{
		public override string TypeName => "extended_stats";
	}
}
