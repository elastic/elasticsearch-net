using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddataFrequencyFilter
	{
		[JsonProperty("min")]
		double? Min { get; set; }

		[JsonProperty("max")]
		double? Max { get; set; }

		[JsonProperty("min_segment_size")]
		int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilter : IFielddataFrequencyFilter
	{
		public double? Min { get; set; }
		public double? Max { get; set; }
		public int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilterDescriptor
		: DescriptorBase<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter>, IFielddataFrequencyFilter
	{
		double? IFielddataFrequencyFilter.Min { get; set; }
		double? IFielddataFrequencyFilter.Max { get; set; }
		int? IFielddataFrequencyFilter.MinSegmentSize { get; set; }

		public FielddataFrequencyFilterDescriptor Min(double min) => Assign(a => a.Min = min);

		public FielddataFrequencyFilterDescriptor Max(double max) => Assign(a => a.Max = max);

		public FielddataFrequencyFilterDescriptor MinSegmentSize(int minSegmentSize) => Assign(a => a.MinSegmentSize = minSegmentSize);
	}
}
