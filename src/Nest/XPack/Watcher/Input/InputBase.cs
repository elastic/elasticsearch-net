using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IInput { }

	public abstract class InputBase : IInput
	{
		internal abstract void WrapInContainer(IInputContainer container);
	}
}
