using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class FieldDataQueryDescriptor
	{
		internal FieldDataFilter Filter { get; private set; }

		public FieldDataQueryDescriptor()
		{
			this.Filter = new FieldDataFilter();
		}

		public FieldDataQueryDescriptor Frequency(
			Func<FieldDataFrequencyQueryDescriptor, FieldDataFrequencyQueryDescriptor> frequencyFilterSelector)
		{
			var selector = frequencyFilterSelector(new FieldDataFrequencyQueryDescriptor());
			this.Filter.Frequency = selector.FrequencyFilter;
			return this;
		}

		public FieldDataQueryDescriptor Regex(
			Func<FieldDataRegexQueryDescriptor, FieldDataRegexQueryDescriptor> regexFilterSelector)
		{
			var selector = regexFilterSelector(new FieldDataRegexQueryDescriptor());
			this.Filter.Regex = selector.RegexFilter;
			return this;
		}
	}
}
