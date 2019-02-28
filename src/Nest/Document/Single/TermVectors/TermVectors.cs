using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[ReadAs(typeof(TermVectorsResult))]
	public interface ITermVectors
	{
		bool Found { get; }
		string Id { get; }
		string Index { get; }
		IReadOnlyDictionary<Field, TermVector> TermVectors { get; }
		long Took { get; }
		string Type { get; }
		long Version { get; }
	}

	public class TermVectorsResult : ITermVectors
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="term_vectors")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<Field, TermVector>))]
		public IReadOnlyDictionary<Field, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<Field, TermVector>.Dictionary;

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
