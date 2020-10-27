// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An item within a bulk response
	/// </summary>
	[JsonFormatter(typeof(BulkResponseItemFormatter))]
	public abstract class BulkResponseItemBase
	{
		/// <summary>
		/// The error associated with the bulk operation
		/// </summary>
		[DataMember(Name = "error")]
		public Error Error { get; internal set; }

		/// <summary>
		/// The id of the document for the bulk operation
		/// </summary>
		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The index against which the bulk operation ran
		/// </summary>
		[DataMember(Name = "_index")]
		public string Index { get; internal set; }

		/// <summary> The type of bulk operation </summary>
		public abstract string Operation { get; }

		[DataMember(Name = "_primary_term")]
		public long PrimaryTerm { get; internal set; }

		[DataMember(Name = "get")]
		internal LazyDocument Get { get; set; }

		/// <summary>
		/// Deserialize the <see cref="Get"/> property as a GetResponse<TDocument> type, where TDocument is the document type.
		/// </summary>
		public GetResponse<TDocument> GetResponse<TDocument>() where TDocument : class => Get?.AsUsingRequestResponseSerializer<GetResponse<TDocument>>();

		/// <summary> The result of the bulk operation</summary>
		[DataMember(Name = "result")]
		public string Result { get; internal set; }

		[DataMember(Name = "_seq_no")]
		public long SequenceNumber { get; internal set; }

		/// <summary>
		/// The shards associated with the bulk operation
		/// </summary>
		[DataMember(Name = "_shards")]
		public ShardStatistics Shards { get; internal set; }

		/// <summary> The status of the bulk operation </summary>
		[DataMember(Name = "status")]
		public int Status { get; internal set; }

		/// <summary> The version of the document </summary>
		[DataMember(Name = "_version")]
		public long Version { get; internal set; }

		/// <summary>
		/// Specifies whether this particular bulk operation succeeded or not
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (Error != null) return false;

				switch (Operation.ToLowerInvariant())
				{
					case "delete": return Status == 200 || Status == 404;
					case "update":
					case "index":
					case "create":
						return Status == 200 || Status == 201;
					default:
						return false;
				}
			}
		}
		public override string ToString() =>
			$"{Operation} returned {Status} _index: {Index} _id: {Id} _version: {Version} error: {Error}";
	}

}
