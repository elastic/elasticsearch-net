using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(TextIndexPrefixes))]
	public interface ITextIndexPrefixes
	{
		[DataMember(Name ="max_chars")]
		int? MaxCharacters { get; set; }

		[DataMember(Name ="min_chars")]
		int? MinCharacters { get; set; }
	}

	public class TextIndexPrefixes : ITextIndexPrefixes
	{
		public int? MaxCharacters { get; set; }
		public int? MinCharacters { get; set; }
	}

	public class TextIndexPrefixesDescriptor
		: DescriptorBase<TextIndexPrefixesDescriptor, ITextIndexPrefixes>, ITextIndexPrefixes
	{
		int? ITextIndexPrefixes.MaxCharacters { get; set; }
		int? ITextIndexPrefixes.MinCharacters { get; set; }

		public TextIndexPrefixesDescriptor MinCharacters(int? min) => Assign(a => a.MinCharacters = min);

		public TextIndexPrefixesDescriptor MaxCharacters(int? max) => Assign(a => a.MaxCharacters = max);
	}
}
