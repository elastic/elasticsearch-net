using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddataRegexFilter
	{
		[JsonProperty("pattern")]
		string Pattern { get; set; }
	}

	public class FielddataRegexFilter : IFielddataRegexFilter
	{
		public string Pattern { get; set; }
	}

	public class FielddataRegexFilterDescriptor
		: DescriptorBase<FielddataRegexFilterDescriptor, IFielddataRegexFilter>, IFielddataRegexFilter
	{
		string IFielddataRegexFilter.Pattern { get; set; }

		public FielddataRegexFilterDescriptor Pattern(string pattern) => Assign(a => a.Pattern = pattern);
	}
}
