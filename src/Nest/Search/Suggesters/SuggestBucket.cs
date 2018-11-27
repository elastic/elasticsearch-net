using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(SuggestBucket))]
	public interface ISuggestBucket
	{
		[DataMember(Name ="completion")]
		ICompletionSuggester Completion { get; set; }

		[DataMember(Name ="phrase")]
		IPhraseSuggester Phrase { get; set; }

		[DataMember(Name ="prefix")]
		string Prefix { get; set; }

		[DataMember(Name ="regex")]
		string Regex { get; set; }

		[DataMember(Name ="term")]
		ITermSuggester Term { get; set; }

		[DataMember(Name ="text")]
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
