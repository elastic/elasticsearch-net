namespace Nest
{
	public interface IOpenIndexResponse : IAcknowledgedResponse { }

	public class OpenIndexResponse : AcknowledgedResponseBase, IOpenIndexResponse { }
}
