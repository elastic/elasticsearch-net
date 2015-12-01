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
}