using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public abstract class BulkOperationResponseItem
	{
		public abstract string Operation { get; internal set; }
		public abstract string Index { get; internal set; }
		public abstract string Type { get; internal set; }
		public abstract string Id { get; internal set; }
		public abstract string Version { get; internal set; }
		public abstract bool OK { get; internal set; }
		public abstract string Error { get; internal set; }
	}
}