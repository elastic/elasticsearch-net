namespace Nest
{
	[MapsApi("ml.preview_datafeed.json")]
	[ResponseBuilderWithGeneric("PreviewDatafeedResponseBuilder<TDocument>.Instance")]
	public partial interface IPreviewDatafeedRequest { }

	/// <inheritdoc />
	public partial class PreviewDatafeedRequest { }

	/// <inheritdoc />
	public partial class PreviewDatafeedDescriptor { }
}
