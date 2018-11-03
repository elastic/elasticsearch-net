namespace Nest
{
	public interface ICloseIndexResponse : IAcknowledgedResponse { }

	public class CloseIndexResponse : AcknowledgedResponseBase, ICloseIndexResponse { }
}
