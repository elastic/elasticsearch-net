using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class TermVectorTerm
	{
		[DataMember(Name ="doc_freq")]
		public int DocumentFrequency { get; internal set; }

		[DataMember(Name ="term_freq")]
		public int TermFrequency { get; internal set; }

		[DataMember(Name ="tokens")]
		public IReadOnlyCollection<Token> Tokens { get; internal set; } =
			EmptyReadOnly<Token>.Collection;

		[DataMember(Name ="ttf")]
		public int TotalTermFrequency { get; internal set; }
	}
}
