using System.Collections.Generic;

namespace Nest
{
	//TODO the usage of this interface is a high hackage indicator go over implementations
	/// <summary>
	/// If an object implements this then it can handle its own json representation
	/// </summary>
	public interface ICustomJson
	{
		object GetCustomJson();
	}

	/// <summary>
	/// Any object that implements this interface will automatically have all 
	/// JsonProperties of all of its implementing interfaces discovered.
	/// </summary>
	public interface INestSerializable { }
}