namespace Nest
{
	public interface IPutScriptResponse : IAcknowledgedResponse { }

	public class PutScriptResponse : AcknowledgedResponseBase, IPutScriptResponse { }
}
