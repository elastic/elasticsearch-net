using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Nest
{
	public interface IReadOnlyUrlRepository : IRepository
	{
		[JsonProperty("settings")]
		IReadOnlyUrlRepositorySettings Settings { get; set; }
	}

	public interface IReadOnlyUrlRepositorySettings : INestSerializable
	{
		[JsonProperty("location")]
		string Location { get; set; }

		[JsonProperty("concurrent_streams")]
		int ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepository : IReadOnlyUrlRepository
	{
		public ReadOnlyUrlRepository(ReadOnlyUrlRepositorySettings settings)
		{
			this.Settings = settings;
		}

		string IRepository.Type { get { return "url"; } }
		public IReadOnlyUrlRepositorySettings Settings { get; set; }
	}

	public class ReadOnlyUrlRepositorySettings : IReadOnlyUrlRepositorySettings
	{
		public ReadOnlyUrlRepositorySettings(string location)
		{
			this.Location = location;
		}

		public string Location { get; set; }
		public int ConcurrentStreams { get; set; }
	}

	public class ReadOnlyUrlRepositorySettingsDescriptor
		: DescriptorBase<ReadOnlyUrlRepositorySettingsDescriptor, IReadOnlyUrlRepositorySettings>, IReadOnlyUrlRepositorySettings
	{
		int IReadOnlyUrlRepositorySettings.ConcurrentStreams { get; set; }
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
		string IRepository.Type { get { return "url"; } }
		IReadOnlyUrlRepositorySettings IReadOnlyUrlRepository.Settings { get; set; }

		public ReadOnlyUrlRepositoryDescriptor Settings(string location, Func<ReadOnlyUrlRepositorySettingsDescriptor, IReadOnlyUrlRepositorySettings> settingsSelector = null) =>
			Assign(a => a.Settings = settingsSelector.InvokeOrDefault(new ReadOnlyUrlRepositorySettingsDescriptor().Location(location)));
	}
}