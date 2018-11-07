namespace Nest
{
	public interface IDeleteScriptResponse : IAcknowledgedResponse { }

	public class DeleteScriptResponse : AcknowledgedResponseBase, IDeleteScriptResponse { }
}
