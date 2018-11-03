namespace Nest
{
	public interface IPutPipelineResponse : IAcknowledgedResponse { }

	public class PutPipelineResponse : AcknowledgedResponseBase, IPutPipelineResponse { }
}
