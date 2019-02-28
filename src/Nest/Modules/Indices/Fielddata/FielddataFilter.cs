using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FielddataFilter))]
	public interface IFielddataFilter
	{
		[DataMember(Name ="frequency")]
		IFielddataFrequencyFilter Frequency { get; set; }

		[DataMember(Name ="regex")]
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
			Assign(a => a.Frequency = frequencyFilterSelector(new FielddataFrequencyFilterDescriptor()));

		public FielddataFilterDescriptor Regex(
			Func<FielddataRegexFilterDescriptor, IFielddataRegexFilter> regexFilterSelector
		) =>
			Assign(a => a.Regex = regexFilterSelector(new FielddataRegexFilterDescriptor()));
	}
}
