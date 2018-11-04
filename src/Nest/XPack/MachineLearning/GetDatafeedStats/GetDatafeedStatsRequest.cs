namespace Nest
{
	/// <summary>
	/// A request to retrieve configuration information for Machine Learning datafeeds.
	/// </summary>
	public partial interface IGetDatafeedStatsRequest { }

	/// <inheritdoc />
	public partial class GetDatafeedStatsRequest { }

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetDatafeedStats")]
	public partial class GetDatafeedStatsDescriptor { }
}
