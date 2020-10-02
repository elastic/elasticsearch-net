// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		[DataMember(Name = "score")]
		public double Score { get; internal set; }

		[DataMember(Name ="tokens")]
		public IReadOnlyCollection<Token> Tokens { get; internal set; } =
			EmptyReadOnly<Token>.Collection;

		[DataMember(Name ="ttf")]
		public int TotalTermFrequency { get; internal set; }
	}
}
