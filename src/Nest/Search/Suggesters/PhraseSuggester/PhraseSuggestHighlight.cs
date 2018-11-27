using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IPhraseSuggestHighlight
	{
		[DataMember(Name ="post_tag")]
		string PostTag { get; set; }

		[DataMember(Name ="pre_tag")]
		string PreTag { get; set; }
	}

	public class PhraseSuggestHighlight : IPhraseSuggestHighlight
	{
		public string PostTag { get; set; }
		public string PreTag { get; set; }
	}

	public class PhraseSuggestHighlightDescriptor : DescriptorBase<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight>, IPhraseSuggestHighlight
	{
		string IPhraseSuggestHighlight.PostTag { get; set; }
		string IPhraseSuggestHighlight.PreTag { get; set; }

		public PhraseSuggestHighlightDescriptor PreTag(string preTag) => Assign(a => a.PreTag = preTag);

		public PhraseSuggestHighlightDescriptor PostTag(string postTag) => Assign(a => a.PostTag = postTag);
	}
}
