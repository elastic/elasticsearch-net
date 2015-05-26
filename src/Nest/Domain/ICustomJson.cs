using System.Collections.Generic;

namespace Nest
{
	//If an object implements this then it can handle its own json representation
	public interface ICustomJson
	{
		object GetCustomJson();
	}

	public interface IDomainObject
	{
		
	}
}