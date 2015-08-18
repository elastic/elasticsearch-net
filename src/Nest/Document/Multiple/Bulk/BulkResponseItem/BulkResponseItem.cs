using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public abstract class BulkResponseItem
	{
		public abstract string Operation { get; internal set; }
		public abstract string Index { get; internal set; }
		public abstract string Type { get; internal set; }
		public abstract string Id { get; internal set; }
		public abstract long Version { get; internal set; }
		public abstract int Status { get; internal set; }
		public abstract string Error { get; internal set; }

		/// <summary>
		/// Specifies wheter this particular bulk operation succeeded or not
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (!this.Error.IsNullOrEmpty() || this.Type.IsNullOrEmpty())
					return false;
				switch (this.Operation.ToLowerInvariant())
				{
					case "delete": return this.Status == 200 || this.Status == 404;
					case "update": 
					case "index":
					case "create":
						return this.Status == 200 || this.Status == 201;
					default:
						return false;
				}
			}
		}
	}
}