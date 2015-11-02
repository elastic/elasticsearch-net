using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<StatsAggregator>))]
	public interface IStatsAggregator : IMetricAggregator { }

	public class StatsAggregator : MetricAggregator, IStatsAggregator { }

	public class StatsAgg : MetricAgg, IStatsAggregator
	{
		public StatsAgg(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Stats = this;
	}

	public class StatsAggregatorDescriptor<T>
		: MetricAggregationBaseDescriptor<StatsAggregatorDescriptor<T>, IStatsAggregator, T>
			, IStatsAggregator 
		where T : class { }
}
