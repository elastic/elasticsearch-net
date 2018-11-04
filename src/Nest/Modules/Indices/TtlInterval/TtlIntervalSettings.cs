namespace Nest
{
	public interface ITtlIntervalSettings
	{
		/// <summary>
		/// The deletions are processed with a bulk request. The number of deletions processed can be configured with this settings. Defaults
		/// to 10000.
		/// </summary>
		int? BulkSize { get; set; }

		/// <summary>How often the deletion process runs. Defaults to 60s.</summary>
		Time Interval { get; set; }
	}

	public class TtlIntervalSettings : ITtlIntervalSettings
	{
		/// <summary> defaults to 2</summary>
		public int? BulkSize { get; set; }

		/// <inheritdoc />
		public Time Interval { get; set; }
	}

	public class TtlIntervalSettingsDescriptor
		: DescriptorBase<TtlIntervalSettingsDescriptor, ITtlIntervalSettings>, ITtlIntervalSettings
	{
		int? ITtlIntervalSettings.BulkSize { get; set; }
		Time ITtlIntervalSettings.Interval { get; set; }

		/// <inheritdoc />
		public TtlIntervalSettingsDescriptor Interval(Time interval) => Assign(a => a.Interval = interval);

		/// <inheritdoc />
		public TtlIntervalSettingsDescriptor BulkSize(int? bulkSize) => Assign(a => a.BulkSize = bulkSize);
	}
}
