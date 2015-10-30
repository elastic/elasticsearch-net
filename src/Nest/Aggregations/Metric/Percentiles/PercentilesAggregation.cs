using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PercentilesAggregation>))]
	public interface IPercentilesAggregation : IMetricAggregation
	{
		[JsonProperty("percents")]
		IEnumerable<double> Percentages { get; set; }

		[JsonProperty("compression")]
		int? Compression { get; set; }
	}

	public class PercentilesAggregation : MetricAggregation, IPercentilesAggregation
	{
		public IEnumerable<double> Percentages { get; set; }
		public int? Compression { get; set; }

		public PercentilesAggregation(string name, FieldName field) : base(name, field) { } 

		internal override void WrapInContainer(AggregationContainer c) => c.Percentiles = this;
	}

	public class PercentilesAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<PercentilesAggregationDescriptor<T>, IPercentilesAggregation, T>
			, IPercentilesAggregation 
		where T : class
	{
		IEnumerable<double> IPercentilesAggregation.Percentages { get; set; }

		int? IPercentilesAggregation.Compression { get; set; }

		public PercentilesAggregationDescriptor<T> Percentages(params double[] percentages) => Assign(a => a.Percentages = percentages);

		public PercentilesAggregationDescriptor<T> Compression(int compression) => Assign(a => a.Compression = compression);

	}
}