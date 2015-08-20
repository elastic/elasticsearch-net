using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexSettingsConverter))]
	public interface IIndexSettings : IDynamicIndexSettings
	{
		/// <summary>
		/// The number of primary shards that an index should have. Defaults to 5. 
		/// This setting can only be set at index creation time. It cannot be changed on a closed index. 
		/// </summary>
		int? NumberOfShards { get; set; }

		/// <summary>
		/// The store module allows you to control how index data is stored and accessed on disk.
		/// <para>EXPERT MODE toggle</para>
		/// </summary>
		FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }
	}

	public class IndexSettings: DynamicIndexSettings, IIndexSettings
	{
		public IndexSettings() : base() { }
		public IndexSettings(IDictionary<string, object> container) : base(container) { }
		public IndexSettings(Dictionary<string, object> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public int? NumberOfShards { get; set; }
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		//public void Add(string setting, object value) => _backingDictionary.Add(setting, value);
	}

	public class IndexSettingsDescriptor: DynamicIndexSettingsDescriptor<IndexSettingsDescriptor>, IIndexSettings
	{
		IndexSettingsDescriptor Assign(Action<IIndexSettings> assigner) => Fluent.Assign(this, assigner);

		int? IIndexSettings.NumberOfShards { get; set; }
		FileSystemStorageImplementation? IIndexSettings.FileSystemStorageImplementation { get; set; }

		public IndexSettingsDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(a => a.NumberOfShards = numberOfShards);

		public IndexSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(a => a.FileSystemStorageImplementation = fs);

	}

}
