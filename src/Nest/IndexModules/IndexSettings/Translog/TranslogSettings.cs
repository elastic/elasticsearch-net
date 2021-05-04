// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public interface ITranslogSettings
	{
		/// <summary>
		/// Whether or not to fsync and commit the translog after every index, delete, update, or bulk request
		/// </summary>
		TranslogDurability? Durability { get; set; }

		/// <summary>
		/// Dynamically updatable flush settings
		/// </summary>
		ITranslogFlushSettings Flush { get; set; }

		/// <summary>
		/// How often the translog is fsynced to disk and committed, regardless of write operations. Defaults to 5s.
		/// </summary>
		Time SyncInterval { get; set; }
	}

	public class TranslogSettings : ITranslogSettings
	{
		/// <inheritdoc />
		public TranslogDurability? Durability { get; set; }

		/// <inheritdoc />
		public ITranslogFlushSettings Flush { get; set; }

		/// <inheritdoc />
		public Time SyncInterval { get; set; }
	}

	public class TranslogSettingsDescriptor : DescriptorBase<TranslogSettingsDescriptor, ITranslogSettings>, ITranslogSettings
	{
		TranslogDurability? ITranslogSettings.Durability { get; set; }
		ITranslogFlushSettings ITranslogSettings.Flush { get; set; }
		Time ITranslogSettings.SyncInterval { get; set; }

		/// <inheritdoc />
		public TranslogSettingsDescriptor Flush(Func<TranslogFlushSettingsDescriptor, ITranslogFlushSettings> selector) =>
			Assign(selector, (a, v) => a.Flush = v?.Invoke(new TranslogFlushSettingsDescriptor()));

		/// <inheritdoc />
		public TranslogSettingsDescriptor Durability(TranslogDurability? durability) =>
			Assign(durability, (a, v) => a.Durability = v);

		/// <inheritdoc />
		public TranslogSettingsDescriptor SyncInterval(Time time) =>
			Assign(time, (a, v) => a.SyncInterval = v);
	}
}
