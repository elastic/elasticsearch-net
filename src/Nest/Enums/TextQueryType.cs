using System.Runtime.Serialization;

namespace Nest
{
    public enum TextQueryType
    {
		[EnumMember(Value = "best_fields")]
        BestFields,
		[EnumMember(Value = "most_fields")]
        MostFields,
		[EnumMember(Value = "cross_fields")]
        CrossFields,
		[EnumMember(Value = "phrase")]
        Phrase,
		[EnumMember(Value = "phrase_prefix")]
        PhrasePrefix
    }
}