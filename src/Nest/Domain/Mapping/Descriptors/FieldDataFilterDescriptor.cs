using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class FieldDataFilterDescriptor
	{
		internal FieldDataFilter Filter { get; private set; }

		public FieldDataFilterDescriptor()
		{
			this.Filter = new FieldDataFilter();
		}

		public FieldDataFilterDescriptor Frequency(
			Func<FieldDataFrequencyFilterDescriptor, FieldDataFrequencyFilterDescriptor> frequencyFilterSelector)
		{
			var selector = frequencyFilterSelector(new FieldDataFrequencyFilterDescriptor());
			this.Filter.Frequency = selector.FrequencyFilter;
			return this;
		}

		public FieldDataFilterDescriptor Regex(
			Func<FieldDataRegexFilterDescriptor, FieldDataRegexFilterDescriptor> regexFilterSelector)
		{
			var selector = regexFilterSelector(new FieldDataRegexFilterDescriptor());
			this.Filter.Regex = selector.RegexFilter;
			return this;
		}
	}
}
