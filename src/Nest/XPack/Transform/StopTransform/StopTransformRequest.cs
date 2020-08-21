namespace Nest
{
	[MapsApi("transform.stop_transform.json")]
	public partial interface IStopTransformRequest { }

	public partial class StopTransformRequest { }

	public partial class StopTransformDescriptor : IStopTransformRequest { }
}
