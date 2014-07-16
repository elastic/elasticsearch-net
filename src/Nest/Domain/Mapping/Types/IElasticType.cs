using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticType 
	{
		PropertyNameMarker Name { get; set; }
		TypeNameMarker Type { get; }
	}
}
