namespace Nest
{
	[MapsApi("transform.preview_transform.json")]
	public partial interface IPreviewTransformRequest { }

	public partial class PreviewTransformRequest { }

	public partial class PreviewTransformDescriptor : IPreviewTransformRequest { }
}
