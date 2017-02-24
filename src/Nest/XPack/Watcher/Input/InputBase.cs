using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface IInput {}

	public abstract class InputBase : IInput
	{
		internal abstract void WrapInContainer(IInputContainer container);
	}
}
