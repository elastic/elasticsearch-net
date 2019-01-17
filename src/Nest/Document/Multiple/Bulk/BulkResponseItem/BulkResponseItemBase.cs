using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// An item within a bulk response
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(BulkResponseItemFormatter))]
	public interface IBulkResponseItem
	{
		/// <summary>
		/// The error associated with the bulk operation
		/// </summary>
		[DataMember(Name = "error")]
		BulkError Error { get; }

		/// <summary>
		/// The id of the document for the bulk operation
		/// </summary>
		[DataMember(Name = "_id")]
		string Id { get; }

		/// <summary>
		/// The index against which the bulk operation ran
		/// </summary>
		[DataMember(Name = "_index")]
		string Index { get; }

		/// <summary>
		/// Specifies whether this particular bulk operation succeeded or not
		/// </summary>
		bool IsValid { get; }

		/// <summary>
		/// The type of bulk operation
		/// </summary>
		string Operation { get; }

		[DataMember(Name = "_primary_term")]
		long PrimaryTerm { get; }

		/// <summary>
		/// The result of the bulk operation
		/// </summary>
		[DataMember(Name = "result")]
		string Result { get; }

		[DataMember(Name = "_seq_no")]
		long SequenceNumber { get; }

		/// <summary>
		/// The shards associated with the bulk operation
		/// </summary>
		[DataMember(Name = "_shards")]
		ShardStatistics Shards { get; }

		/// <summary>
		/// The status of the bulk operation
		/// </summary>
		[DataMember(Name = "status")]
		int Status { get; }

		/// <summary>
		/// The type against which the bulk operation ran
		/// </summary>
		[DataMember(Name = "_type")]
		string Type { get; }

		/// <summary>
		/// The version of the document
		/// </summary>
		[DataMember(Name = "_version")]
		long Version { get; }
	}

	/// <inheritdoc />
	public abstract class BulkResponseItemBase : IBulkResponseItem
	{
		/// <inheritdoc />
		public BulkError Error { get; internal set; }

		/// <inheritdoc />
		public string Id { get; internal set; }

		/// <inheritdoc />
		public string Index { get; internal set; }

		/// <inheritdoc />
		public bool IsValid
		{
			get
			{
				if (Error != null || Type.IsNullOrEmpty()) return false;

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

		/// <inheritdoc />
		public abstract string Operation { get; }

		/// <inheritdoc />
		public long PrimaryTerm { get; internal set; }

		/// <inheritdoc />
		public string Result { get; internal set; }

		/// <inheritdoc />
		public long SequenceNumber { get; internal set; }

		/// <inheritdoc />
		public ShardStatistics Shards { get; internal set; }

		/// <inheritdoc />
		public int Status { get; internal set; }

		/// <inheritdoc />
		public string Type { get; internal set; }

		/// <inheritdoc />
		public long Version { get; internal set; }

		public override string ToString() =>
			$"{Operation} returned {Status} _index: {Index} _type: {Type} _id: {Id} _version: {Version} error: {Error}";
	}
}
