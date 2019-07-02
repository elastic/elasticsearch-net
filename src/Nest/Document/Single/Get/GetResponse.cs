using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public interface IGetResponse<out TDocument> : IResponse where TDocument : class
	{
		TDocument Source { get; }
	}
	public class GetResponse<TDocument> : ResponseBase, IGetResponse<TDocument> where TDocument : class
	{
		[DataMember(Name = "fields")]
		public FieldValues Fields { get; internal set; }

		[DataMember(Name = "found")]
		public bool Found { get; internal set; }

		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		[DataMember(Name = "_index")]
		public string Index { get; internal set; }

		[DataMember(Name = "_primary_term")]
		public long? PrimaryTerm { get; internal set; }

		[DataMember(Name = "_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name = "_seq_no")]
		public long? SequenceNumber { get; internal set; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		[DataMember(Name = "_type")]
		public string Type { get; internal set; }

		[DataMember(Name = "_version")]
		public long Version { get; internal set; }
	}
}
