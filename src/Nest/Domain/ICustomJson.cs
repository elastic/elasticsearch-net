using System.Collections.Generic;

namespace Nest
{
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