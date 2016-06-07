using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<GeoCentroidAggregation>))]
	public interface IGeoCentroidAggregation : IMetricAggregation { }

	public class GeoCentroidAggregation : MetricAggregationBase, IGeoCentroidAggregation
	{
		internal GeoCentroidAggregation() { }

		public GeoCentroidAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoCentroid = this;
	}

	public class GeoCentroidAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<GeoCentroidAggregationDescriptor<T>, IGeoCentroidAggregation, T>
			, IGeoCentroidAggregation
		where T : class
	{
	}
}
