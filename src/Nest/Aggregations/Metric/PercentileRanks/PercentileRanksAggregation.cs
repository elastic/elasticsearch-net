using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PercentileRanksAggregation>))]
	public interface IPercentileRanksAggregation : IMetricAggregation
	{
		[JsonProperty("values")]
		IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAggregation : MetricAggregationBase, IPercentileRanksAggregation
	{
		public IEnumerable<double> Values { get; set; }

		internal PercentileRanksAggregation() { }

		public PercentileRanksAggregation(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation, T>, IPercentileRanksAggregation
		where T : class
	{
		IEnumerable<double> IPercentileRanksAggregation.Values { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(a => a.Values = values.ToListOrNullIfEmpty());

	}
}
