using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IInput { }

	public abstract class InputBase : IInput
	{
		internal abstract void WrapInContainer(IInputContainer container);
	}
}
