// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Nest.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(MultiGetHit<>))]
	public interface IMultiGetHit<out TDocument> where TDocument : class
	{
		Error Error { get; }

		bool Found { get; }

		string Id { get; }

		string Index { get; }

		string Routing { get; }

		TDocument Source { get; }

		long Version { get; }

		long? SequenceNumber { get; }

		long? PrimaryTerm { get; }
	}

	[DataContract]
	public class MultiGetHit<TDocument> : IMultiGetHit<TDocument>
		where TDocument : class
	{
		[DataMember(Name = "error")]
		public Error Error { get; internal set; }

		[DataMember(Name = "fields")]
		public FieldValues Fields { get; internal set; }

		[DataMember(Name = "found")]
		public bool Found { get; internal set; }

		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		[DataMember(Name = "_index")]
		public string Index { get; internal set; }

		[DataMember(Name = "_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		[DataMember(Name = "_version")]
		public long Version { get; internal set; }

		[DataMember(Name = "_seq_no")]
		public long? SequenceNumber { get; internal set; }

		[DataMember(Name = "_primary_term")]
		public long? PrimaryTerm { get; internal set; }
	}
}
