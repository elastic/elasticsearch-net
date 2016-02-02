using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<GeoBoundsAggregation>))]
	public interface IGeoBoundsAggregation : IMetricAggregation
	{
		[JsonProperty("wrap_longitude")]
		bool? WrapLongitude { get; set; }
	}

	public class GeoBoundsAggregation : MetricAggregationBase, IGeoBoundsAggregation
	{
		public bool? WrapLongitude { get; set; }

		internal GeoBoundsAggregation() { }

		public GeoBoundsAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoBounds = this;
	}

	public class GeoBoundsAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<GeoBoundsAggregationDescriptor<T>, IGeoBoundsAggregation, T>
			, IGeoBoundsAggregation
		where T : class
	{
		bool? IGeoBoundsAggregation.WrapLongitude { get; set; }

		public GeoBoundsAggregationDescriptor<T> WrapLongitude(bool wrapLongitude = true) =>
			Assign(a => a.WrapLongitude = wrapLongitude);
	}
}
