using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SuggestBucket>))]
	public interface ISuggestBucket
	{
		[JsonProperty("completion")]
		ICompletionSuggester Completion { get; set; }

		[JsonProperty("phrase")]
		IPhraseSuggester Phrase { get; set; }

		[JsonProperty("prefix")]
		string Prefix { get; set; }

		[JsonProperty("regex")]
		string Regex { get; set; }

		[JsonProperty("term")]
		ITermSuggester Term { get; set; }

		[JsonProperty("text")]
		string Text { get; set; }
	}

	public class SuggestBucket : ISuggestBucket
	{
		public ICompletionSuggester Completion { get; set; }

		public IPhraseSuggester Phrase { get; set; }

		public string Prefix { get; set; }

		public string Regex { get; set; }

		public ITermSuggester Term { get; set; }
		public string Text { get; set; }
	}
}
