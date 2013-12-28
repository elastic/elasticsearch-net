using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SuggestDescriptorBucket<T> where T : class
	{
		[JsonProperty(PropertyName = "text")]
		internal string _Text { get; set; }

		[JsonProperty(PropertyName = "term")]
		public TermSuggestDescriptor<T> TermSuggest { get; set; }

		[JsonProperty(PropertyName = "phrase")]
		public PhraseSuggestDescriptor<T> PhraseSuggest { get; set; }

        [JsonProperty(PropertyName = "completion")]
        public CompletionSuggestDescriptor<T> CompletionSuggest { get; set; }
	}
}
