namespace Nest
{
	public interface ISoftDeleteRetentionSettings
	{
		/// <summary> How many operations should be retained </summary>
		long? Operations { get; set; }
	}

	public class SoftDeleteRetentionSettings : ISoftDeleteRetentionSettings
	{
		/// <inheritdoc see cref="ISoftDeleteRetentionSettings.Operations"/>
		public long? Operations { get; set; }
	}

	public class SoftDeleteRetentionSettingsDescriptor : DescriptorBase<SoftDeleteRetentionSettingsDescriptor, ISoftDeleteRetentionSettings>, ISoftDeleteRetentionSettings
	{
		long? ISoftDeleteRetentionSettings.Operations { get; set; }

		/// <inheritdoc see cref="ISoftDeleteRetentionSettings.Operations"/>
		public SoftDeleteRetentionSettingsDescriptor Operations(long? operations) => Assign(a => a.Operations = operations);
	}
}
