using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public abstract class BulkResponseItemBase
	{
		[JsonProperty("error")]
		public BulkError Error { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		/// <summary>
		/// Specifies wheter this particular bulk operation succeeded or not
		/// </summary>
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

		public abstract string Operation { get; internal set; }

		[JsonProperty("_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty("status")]
		public int Status { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		public override string ToString() =>
			$"{Operation} returned {Status} _index: {Index} _type: {Type} _id: {Id} _version: {Version} error: {Error}";
	}
}
