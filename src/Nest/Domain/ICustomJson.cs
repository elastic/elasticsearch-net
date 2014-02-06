using System.Collections.Generic;

namespace Nest
{

	//If an object implements this then it can handle its own json representation
	internal interface ICustomJson
	{
		IDictionary<object, object> GetCustomJson();
	}
}