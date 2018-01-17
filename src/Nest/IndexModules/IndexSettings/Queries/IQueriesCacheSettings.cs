namespace Nest
{
	public interface IQueriesCacheSettings
	{
		/// <summary>
		/// Whether the query cache is enabled. <c>True</c> by default.
		/// </summary>
		bool? Enabled { get; set; }
	}

	public class QueriesCacheSettings : IQueriesCacheSettings
	{
		public bool? Enabled { get; set; }
	}

	public class QueriesCacheSettingsDescriptor : DescriptorBase<QueriesCacheSettingsDescriptor, IQueriesCacheSettings>, IQueriesCacheSettings
	{
		bool? IQueriesCacheSettings.Enabled { get; set; }

		/// <inheritdoc/>
		public QueriesCacheSettingsDescriptor Enabled(bool? enabled = true) =>
			Assign(a => a.Enabled = enabled);
	}
}
