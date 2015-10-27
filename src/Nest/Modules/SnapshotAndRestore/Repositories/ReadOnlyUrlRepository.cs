using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(RepositoryJsonConverter))]
	public interface IReadOnlyUrlRepistory : IRepository
	{
		[JsonProperty("location")]
		string Location { get; set; }

		[JsonProperty("concurrent_streams")]
		int ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepository : IReadOnlyUrlRepistory
	{
		public ReadOnlyUrlRepository(string location)
		{
			this.Location = location;
		}

		string IRepository.Type { get { return "url"; } }
		public string Location { get; set; }
		public int ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepositoryDescriptor 
		: DescriptorBase<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepistory>, IReadOnlyUrlRepistory
	{
		string IRepository.Type { get { return "url"; } }
		int IReadOnlyUrlRepistory.ConcurrentStreams { get; set; }
		string IReadOnlyUrlRepistory.Location { get; set; }

		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public ReadOnlyUrlRepositoryDescriptor Location(string location) => Assign(a => a.Location = location);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public ReadOnlyUrlRepositoryDescriptor ConcurrentStreams(int concurrentStreams) =>
			Assign(a => a.ConcurrentStreams = concurrentStreams);
	}
}