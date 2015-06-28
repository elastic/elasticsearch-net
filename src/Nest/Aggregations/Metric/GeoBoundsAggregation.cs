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

	public class GeoBoundsAggregationDescriptor<T> 
		: MetricAggregationBaseDescriptor<GeoBoundsAggregationDescriptor<T>, T>, IGeoBoundsAggregator
		where T : class
	{
		IGeoBoundsAggregator Self => this;
		bool? IGeoBoundsAggregator.WrapLongitude { get; set; }

		public GeoBoundsAggregationDescriptor<T> WrapLongitude(bool wrapLongitude = true)
		{
			this.Self.WrapLongitude = wrapLongitude;
			return this;
		}
	}
}
