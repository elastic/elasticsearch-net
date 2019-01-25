using System;

namespace Nest
{
	public interface ISoftDeleteSettings
	{
		/// <summary> Enables soft deletes on the index</summary>
		bool? Enabled { get; set; }
		/// <summary> Configure the retention of soft deletes on the index</summary>
		ISoftDeleteRetentionSettings Retention { get; set; }
	}

	public class SoftDeleteSettings : ISoftDeleteSettings
	{
		/// <inheritdoc see cref="ISoftDeleteSettings.Retention"/>
		public ISoftDeleteRetentionSettings Retention { get; set; }

		/// <inheritdoc see cref="ISoftDeleteSettings.Enabled"/>
		public bool? Enabled { get; set; }
	}

	public class SoftDeleteSettingsDescriptor : DescriptorBase<SoftDeleteSettingsDescriptor, ISoftDeleteSettings>, ISoftDeleteSettings
	{
		bool? ISoftDeleteSettings.Enabled { get; set; }
		ISoftDeleteRetentionSettings ISoftDeleteSettings.Retention { get; set; }

		/// <inheritdoc see cref="ISoftDeleteSettings.Retention"/>
		public SoftDeleteSettingsDescriptor Retention(Func<SoftDeleteRetentionSettingsDescriptor, ISoftDeleteRetentionSettings> selector) =>
			Assign(a => a.Retention = selector.Invoke(new SoftDeleteRetentionSettingsDescriptor()));

		/// <inheritdoc see cref="ISoftDeleteSettings.Enabled"/>
		public SoftDeleteSettingsDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);
	}
}
