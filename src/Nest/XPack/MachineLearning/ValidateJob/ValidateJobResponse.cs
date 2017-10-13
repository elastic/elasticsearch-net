namespace Nest
{
	public interface IValidateJobResponse : IAcknowledgedResponse {}

	public class ValidateJobResponse : AcknowledgedResponseBase, IValidateJobResponse {}
}
