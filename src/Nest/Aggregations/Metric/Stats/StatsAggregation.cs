using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<StatsAggregation>))]
	public interface IStatsAggregator : IMetricAggregation { }

	public class StatsAggregation : MetricAggregation, IStatsAggregator
	{
		public StatsAggregation(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Stats = this;
	}

	public class StatsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<StatsAggregationDescriptor<T>, IStatsAggregator, T>
			, IStatsAggregator 
		where T : class { }
}
