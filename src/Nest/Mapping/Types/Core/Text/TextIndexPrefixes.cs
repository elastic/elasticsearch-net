using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TextIndexPrefixes>))]
	public interface ITextIndexPrefixes
	{
		[JsonProperty("min_chars")]
		int? MinCharacters { get; set; }

		[JsonProperty("max_chars")]
		int? MaxCharacters { get; set; }
	}

	public class TextIndexPrefixes : ITextIndexPrefixes
	{
		public int? MinCharacters { get; set; }
		public int? MaxCharacters { get; set; }
	}

	public class TextIndexPrefixesDescriptor
		: DescriptorBase<TextIndexPrefixesDescriptor, ITextIndexPrefixes>, ITextIndexPrefixes
	{
		int? ITextIndexPrefixes.MinCharacters { get; set; }
		int? ITextIndexPrefixes.MaxCharacters { get; set; }

		public TextIndexPrefixesDescriptor MinCharacters(int? min) => Assign(a => a.MinCharacters = min);

		public TextIndexPrefixesDescriptor MaxCharacters(int? max) => Assign(a => a.MaxCharacters = max);
	}
}
