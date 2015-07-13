using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<GeoBoundsAggregator>))]
	public interface IGeoBoundsAggregator : IMetricAggregator
	{
		[JsonProperty("wrap_longitude")]
		bool? WrapLongitude { get; set; }
	}

	public class GeoBoundsAggregator : MetricAggregator, IGeoBoundsAggregator
	{
		public bool? WrapLongitude { get; set; }
	}

	public class GeoBoundsAgg : MetricAgg, IGeoBoundsAggregator
	{
		public bool? WrapLongitude { get; set; }

		public GeoBoundsAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoBounds = this;
	}

	public class GeoBoundsAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<GeoBoundsAggregatorDescriptor<T>, IGeoBoundsAggregator, T>
			, IGeoBoundsAggregator
		where T : class
	{
		bool? IGeoBoundsAggregator.WrapLongitude { get; set; }

		public GeoBoundsAggregatorDescriptor<T> WrapLongitude(bool wrapLongitude = true) =>
			Assign(a => a.WrapLongitude = wrapLongitude);

	}
}
