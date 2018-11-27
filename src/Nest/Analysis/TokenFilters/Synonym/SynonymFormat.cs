using System.Runtime.Serialization;

namespace Nest
{
	public enum SynonymFormat
	{
		[EnumMember(Value = "solr")]
		Solr,

		[EnumMember(Value = "wordnet")]
		WordNet
	}
}
