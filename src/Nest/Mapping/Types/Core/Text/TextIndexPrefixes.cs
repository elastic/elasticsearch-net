using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TextIndexPrefixes>))]
	public interface ITextIndexPrefixes
	{
		[JsonProperty("max_chars")]
		int? MaxCharacters { get; set; }

		[JsonProperty("min_chars")]
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

		public TextIndexPrefixesDescriptor MaxCharacters(int? max) => Assign(a => a.MaxCharacters = max);

		public TextIndexPrefixesDescriptor MinCharacters(int? min) => Assign(a => a.MinCharacters = min);
	}
}
