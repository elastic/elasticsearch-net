using System.Collections.Generic;

namespace Nest
{
	public class HotThreadInformation
	{
		public HotThreadInformation()
		{
			this.Threads = new List<string>();
		}

		public string Node { get; set; }
		public List<string> Threads { get; set; }
	}

	public interface INodesHotThreadsResponse : IResponse
	{
		List<HotThreadInformation> HotThreads { get; }
	}

	public class NodesHotThreadsResponse : BaseResponse, INodesHotThreadsResponse
	{
		public NodesHotThreadsResponse()
		{
			this.HotThreads = new List<HotThreadInformation>();
		}

		public List<HotThreadInformation> HotThreads { get; set; }
	}
}
