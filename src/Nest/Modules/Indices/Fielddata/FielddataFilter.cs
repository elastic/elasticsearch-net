using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddataFilter
	{
		[JsonProperty("frequency")]
		IFielddataFrequencyFilter Frequency { get; set; }

		[JsonProperty("regex")]
		IFielddataRegexFilter Regex { get; set; }
	}

	public class FielddataFilter : IFielddataFilter
	{
		public IFielddataFrequencyFilter Frequency { get; set; }
		public IFielddataRegexFilter Regex { get; set; }
	}

	public class FielddataFilterDescriptor
		: DescriptorBase<FielddataFilterDescriptor, IFielddataFilter>, IFielddataFilter
	{
		IFielddataFrequencyFilter IFielddataFilter.Frequency { get; set; }
		IFielddataRegexFilter IFielddataFilter.Regex { get; set; }

		public FielddataFilterDescriptor Frequency(
			Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> frequencyFilterSelector) =>
			Assign(a => a.Frequency = frequencyFilterSelector(new FielddataFrequencyFilterDescriptor()));

		public FielddataFilterDescriptor Regex(
			Func<FielddataRegexFilterDescriptor, IFielddataRegexFilter> regexFilterSelector) =>
			Assign(a => a.Regex = regexFilterSelector(new FielddataRegexFilterDescriptor()));
	}
}
