using System;

namespace Nest
{
	public interface ITranslogSettings
	{
		/// <summary>
		/// Dynamically updatable flush settings
		/// </summary>
		ITranslogFlushSettings Flush { get; set; }

		/// <summary>
		/// How often the translog is fsynced to disk and committed, regardless of write operations. Defaults to 5s.
		/// </summary>
		Time SyncInterval { get; set; }

		/// <summary>
		/// Whether or not to fsync and commit the translog after every index, delete, update, or bulk request
		/// </summary>
		TranslogDurability? Durability { get; set; }
	}

	public class TranslogSettings : ITranslogSettings
	{
		/// <inheritdoc/>
		public ITranslogFlushSettings Flush { get; set; }
		/// <inheritdoc/>
		public Time SyncInterval { get; set; }
		/// <inheritdoc/>
		public TranslogDurability? Durability { get; set; }
	}

	public class TranslogSettingsDescriptor: DescriptorBase<TranslogSettingsDescriptor, ITranslogSettings>, ITranslogSettings
	{
		ITranslogFlushSettings ITranslogSettings.Flush { get; set; }
		Time ITranslogSettings.SyncInterval { get; set; }
		TranslogDurability? ITranslogSettings.Durability { get; set; }

		/// <inheritdoc/>
		public TranslogSettingsDescriptor Flush(Func<TranslogFlushSettingsDescriptor, ITranslogFlushSettings> selector) =>
			Assign(a => a.Flush = selector?.Invoke(new TranslogFlushSettingsDescriptor()));

		/// <inheritdoc/>
		public TranslogSettingsDescriptor Durability(TranslogDurability? durability) =>
			Assign(a => a.Durability = durability);

		/// <inheritdoc/>
		public TranslogSettingsDescriptor SyncInterval(Time time) =>
			Assign(a => a.SyncInterval = time);
	}
}
