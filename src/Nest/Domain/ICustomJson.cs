using System.Collections.Generic;

namespace Nest
{
	// If an object implements this then it can handle its own json representation
	public interface ICustomJson
	{
		object GetCustomJson();
	}

	// Any object that implements this will automatically be added to the serialization contract
	public interface INestSerializable { }
}