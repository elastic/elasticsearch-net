using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataFrequencyFilter
	{
		[JsonProperty("min")]
		public double? Min { get; set; }

		[JsonProperty("max")]
		public double? Max { get; set; }

		[JsonProperty("min_segment_size")]
		public int? MinSegmentSize { get; set; }
	}

	public class FieldDataFrequencyQueryDescriptor
	{
		internal FieldDataFrequencyFilter FrequencyFilter { get; private set; }

		public FieldDataFrequencyQueryDescriptor()
		{
			this.FrequencyFilter = new FieldDataFrequencyFilter();
		}

		public FieldDataFrequencyQueryDescriptor Min(double min)
		{
			this.FrequencyFilter.Min = min;
			return this;
		}

		public FieldDataFrequencyQueryDescriptor Max(double max)
		{
			this.FrequencyFilter.Max = max;
			return this;
		}

		public FieldDataFrequencyQueryDescriptor MinSegmentSize(int minSegmentSize)
		{
			this.FrequencyFilter.MinSegmentSize = minSegmentSize;
			return this;
		}
	}
}
