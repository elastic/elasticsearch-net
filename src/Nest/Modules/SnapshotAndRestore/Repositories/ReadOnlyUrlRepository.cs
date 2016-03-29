using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IReadOnlyUrlRepository : IRepository<IReadOnlyUrlRepositorySettings> { }

	public class ReadOnlyUrlRepository : IReadOnlyUrlRepository
	{
		public ReadOnlyUrlRepository(ReadOnlyUrlRepositorySettings settings)
		{
			Settings = settings;
		}

		public IReadOnlyUrlRepositorySettings Settings { get; set; }
		public string Type { get; } = "url";
	}

	public interface IReadOnlyUrlRepositorySettings : IRepositorySettings
	{
		[JsonProperty("location")]
		string Location { get; set; }

		[JsonProperty("concurrent_streams")]
		int? ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepositorySettings : IReadOnlyUrlRepositorySettings
	{
		internal ReadOnlyUrlRepositorySettings() { }

		public ReadOnlyUrlRepositorySettings(string location)
		{
			this.Location = location;
		}

		public string Location { get; set; }
		public int? ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepositorySettingsDescriptor
		: DescriptorBase<ReadOnlyUrlRepositorySettingsDescriptor, IReadOnlyUrlRepositorySettings>, IReadOnlyUrlRepositorySettings
	{
		int? IReadOnlyUrlRepositorySettings.ConcurrentStreams { get; set; }
		string IReadOnlyUrlRepositorySettings.Location { get;set; }

		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public ReadOnlyUrlRepositorySettingsDescriptor Location(string location) => Assign(a => a.Location = location);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public ReadOnlyUrlRepositorySettingsDescriptor ConcurrentStreams(int concurrentStreams) =>
			Assign(a => a.ConcurrentStreams = concurrentStreams);
	}

	public class ReadOnlyUrlRepositoryDescriptor 
		: DescriptorBase<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepository>, IReadOnlyUrlRepository
	{
		string ISnapshotRepository.Type { get { return "url"; } }
		IReadOnlyUrlRepositorySettings IRepository<IReadOnlyUrlRepositorySettings>.Settings { get; set; }

		public ReadOnlyUrlRepositoryDescriptor Settings(string location, Func<ReadOnlyUrlRepositorySettingsDescriptor, IReadOnlyUrlRepositorySettings> settingsSelector = null) =>
			Assign(a => a.Settings = settingsSelector.InvokeOrDefault(new ReadOnlyUrlRepositorySettingsDescriptor().Location(location)));
	}
}