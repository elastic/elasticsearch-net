using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class HotThreadInformation
	{
		public string NodeName { get; set; }
		public string NodeId { get; set; }
		public IEnumerable<string> Threads { get; set; } = Enumerable.Empty<string>();
		public IEnumerable<string> Hosts { get; set; } = Enumerable.Empty<string>();
	}

	public interface INodesHotThreadsResponse : IResponse
	{
		IEnumerable<HotThreadInformation> HotThreads { get; }
	}

	public class NodesHotThreadsResponse : BaseResponse, INodesHotThreadsResponse
	{
		public NodesHotThreadsResponse() { }

		internal NodesHotThreadsResponse(IEnumerable<HotThreadInformation> threadInfo)
		{
			this.HotThreads = threadInfo;
		}

		public IEnumerable<HotThreadInformation> HotThreads { get; } = Enumerable.Empty<HotThreadInformation>();
	}
}
