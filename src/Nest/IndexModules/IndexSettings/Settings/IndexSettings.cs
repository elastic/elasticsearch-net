using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexSettingsConverter))]
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

	/// <inheritdoc />
	public class IndexSettings: DynamicIndexSettings, IIndexSettings
	{
		public IndexSettings() : base() { }
		public IndexSettings(IDictionary<string, object> container) : base(container) { }
		public IndexSettings(Dictionary<string, object> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <inheritdoc />
		public int? NumberOfShards { get; set; }

		/// <inheritdoc />
		public FileSystemStorageImplementation? FileSystemStorageImplementation { get; set; }

		//public void Add(string setting, object value) => _backingDictionary.Add(setting, value);
	}

	/// <inheritdoc />
	public class IndexSettingsDescriptor: DynamicIndexSettingsDescriptorBase<IndexSettingsDescriptor, IndexSettings>
	{
		public IndexSettingsDescriptor() : base(new IndexSettings()) { }

		/// <inheritdoc />
		public IndexSettingsDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(a => a.NumberOfShards = numberOfShards);

		/// <inheritdoc />
		public IndexSettingsDescriptor FileSystemStorageImplementation(FileSystemStorageImplementation? fs) =>
			Assign(a => a.FileSystemStorageImplementation = fs);

	}

}
