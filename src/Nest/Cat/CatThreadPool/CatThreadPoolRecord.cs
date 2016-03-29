using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(CatThreadPoolRecordJsonConverter))]
	public class CatThreadPoolRecord : ICatRecord
	{
		public string Id { get; set; }
		public string Pid { get; set; }
		public string Host { get; set; }
		public string Ip { get; set; }
		public string Port { get; set; }

		public CatThreadPool Bulk { get; set; }
		public CatThreadPool Flush { get; set; }
		public CatThreadPool Generic { get; set; }
		public CatThreadPool Get { get; set; }
		public CatThreadPool Index { get; set; }
		public CatThreadPool Management { get; set; }
		public CatThreadPool Merge { get; set; }
		public CatThreadPool Optimize { get; set; }
		public CatThreadPool Percolate { get; set; }
		public CatThreadPool Refresh { get; set; }
		public CatThreadPool Search { get; set; }
		public CatThreadPool Snapshot { get; set; }
		public CatThreadPool Suggest { get; set; }
		public CatThreadPool Warmer { get; set; }
	}
	public class CatThreadPool
	{
		public string Type { get; set; }
		public string Active { get; set; }
		public string Size { get; set; }
		public string Queue { get; set; }
		public string QueueSize { get; set; }
		public string Rejected { get; set; }
		public string Largest { get; set; }
		public string Completed { get; set; }
		public string Min { get; set; }
		public string Max { get; set; }
		public string KeepAlive { get; set; }
	}
}