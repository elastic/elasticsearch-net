namespace Nest
{
	public interface IDeletePipelineResponse : IAcknowledgedResponse { }

	public class DeletePipelineResponse : AcknowledgedResponseBase, IDeletePipelineResponse { }
}
