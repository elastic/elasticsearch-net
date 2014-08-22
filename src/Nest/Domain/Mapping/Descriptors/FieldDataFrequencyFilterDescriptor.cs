using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class FieldDataFrequencyFilterDescriptor
	{
		internal FieldDataFrequencyFilter FrequencyFilter { get; private set; }

		public FieldDataFrequencyFilterDescriptor()
		{
			this.FrequencyFilter = new FieldDataFrequencyFilter();
		}

		public FieldDataFrequencyFilterDescriptor Min(double min)
		{
			this.FrequencyFilter.Min = min;
			return this;
		}

		public FieldDataFrequencyFilterDescriptor Max(double max)
		{
			this.FrequencyFilter.Max = max;
			return this;
		}

		public FieldDataFrequencyFilterDescriptor MinSegmentSize(int minSegmentSize)
		{
			this.FrequencyFilter.MinSegmentSize = minSegmentSize;
			return this;
		}
	}
}
