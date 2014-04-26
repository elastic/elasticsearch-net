using System.Collections.Generic;

namespace Nest
{
	public class ReadOnlyUrlRepositoryDescriptor : IRepository
	{
		public string Type { get { return "url"; } }
		public IDictionary<string, object> Settings { get; private set; }

		public ReadOnlyUrlRepositoryDescriptor()
		{
			this.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public ReadOnlyUrlRepositoryDescriptor Location(string location)
		{
			this.Settings["location"] = location;
			return this;
		}
		
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public ReadOnlyUrlRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			this.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
	
	}
}