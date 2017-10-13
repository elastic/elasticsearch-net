using Newtonsoft.Json;

namespace Nest
{
	public interface IValidateDetectorResponse : IAcknowledgedResponse {}

	public class ValidateDetectorResponse : AcknowledgedResponseBase, IValidateDetectorResponse {}
}
