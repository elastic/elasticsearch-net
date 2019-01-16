using System.Runtime.Serialization;

namespace Nest
{
	[StringEnum]
	public enum SynonymFormat
	{
		[EnumMember(Value = "solr")]
		Solr,

		[EnumMember(Value = "wordnet")]
		WordNet
	}
}
