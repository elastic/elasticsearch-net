using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataFilter
	{
		[JsonProperty("frequency")]
		public FieldDataFrequencyFilter Frequency { get; set; }

		[JsonProperty("regex")]
		public FieldDataRegexFilter Regex { get; set; }
	}

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
