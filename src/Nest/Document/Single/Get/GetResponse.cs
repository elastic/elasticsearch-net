using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetResponse<out TDocument> : IResponse where TDocument : class
	{
		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		[DataMember(Name = "found")]
		bool Found { get; }

		[DataMember(Name = "_id")]
		string Id { get; }

		[DataMember(Name = "_index")]
		string Index { get; }

		[DataMember(Name = "_parent")]
		[Obsolete("No longer returned on indices created in Elasticsearch 6.0")]
		string Parent { get; }

		[DataMember(Name = "_routing")]
		string Routing { get; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }

		[DataMember(Name = "_type")]
		string Type { get; }

		[DataMember(Name = "_version")]
		long Version { get; }
	}

	[DataContract]
	public class GetResponse<TDocument> : ResponseBase, IGetResponse<TDocument> where TDocument : class
	{
		public FieldValues Fields { get; internal set; } = FieldValues.Empty;
		public bool Found { get; internal set; }
		public string Id { get; internal set; }
		public string Index { get; internal set; }
		public string Parent { get; internal set; }
		public string Routing { get; internal set; }
		public TDocument Source { get; internal set; }
		public string Type { get; internal set; }
		public long Version { get; internal set; }
	}
}
