using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FielddataFilter>))]
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
			Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> frequencyFilterSelector
		) =>
			Assign(frequencyFilterSelector(new FielddataFrequencyFilterDescriptor()), (a, v) => a.Frequency = v);

		public FielddataFilterDescriptor Regex(
			Func<FielddataRegexFilterDescriptor, IFielddataRegexFilter> regexFilterSelector
		) =>
			Assign(regexFilterSelector(new FielddataRegexFilterDescriptor()), (a, v) => a.Regex = v);
	}
}
