namespace Nest
{
	public interface IStartIlmResponse : IAcknowledgedResponse { }

	public class StartIlmResponse : AcknowledgedResponseBase, IStartIlmResponse { }
}
