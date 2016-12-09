using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class HotThreadInformation
	{
		public string NodeName { get; internal set; }
		public string NodeId { get; internal set; }
		public IReadOnlyCollection<string> Threads { get; internal set; } = EmptyReadOnly<string>.Collection;
		public IReadOnlyCollection<string> Hosts { get; internal set; } = EmptyReadOnly<string>.Collection;
	}

	public interface INodesHotThreadsResponse : IResponse
	{
		IReadOnlyCollection<HotThreadInformation> HotThreads { get; }
	}

	public class NodesHotThreadsResponse : ResponseBase, INodesHotThreadsResponse
	{
		public NodesHotThreadsResponse() { }

		internal NodesHotThreadsResponse(IReadOnlyCollection<HotThreadInformation> threadInfo)
		{
			this.HotThreads = threadInfo;
		}

		public IReadOnlyCollection<HotThreadInformation> HotThreads { get; internal set; } = EmptyReadOnly<HotThreadInformation>.Collection;
	}
}
