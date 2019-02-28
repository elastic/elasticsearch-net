using System.Runtime.Serialization;
using Elasticsearch.Net;

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
