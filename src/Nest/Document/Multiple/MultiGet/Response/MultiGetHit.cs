using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[ReadAs(typeof(MultiGetHit<>))]
	public interface IMultiGetHit<out TDocument> where TDocument : class
	{
		Error Error { get; }

		bool Found { get; }

		string Id { get; }

		string Index { get; }

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 6.x and up, use Routing instead.")]
		string Parent { get; }

		string Routing { get; }
		TDocument Source { get; }

		string Type { get; }

		long Version { get; }
	}

	[DataContract]
	public class MultiGetHit<TDocument> : IMultiGetHit<TDocument>
		where TDocument : class
	{
		[DataMember(Name ="error")]
		public Error Error { get; internal set; }

		[DataMember(Name = "fields")]
		public FieldValues Fields { get; internal set; }

		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="_parent")]
		public string Parent { get; internal set; }

		[DataMember(Name ="_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
