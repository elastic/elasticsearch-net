namespace Nest
{
	/// <summary>
	/// A request to retrieve configuration information for Machine Learning datafeeds.
	/// </summary>
	public partial interface IGetDatafeedsRequest { }

	/// <inheritdoc />
	public partial class GetDatafeedsRequest { }

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetDatafeeds")]
	public partial class GetDatafeedsDescriptor { }
}
