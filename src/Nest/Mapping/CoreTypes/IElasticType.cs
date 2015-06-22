using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticType : IFieldMapping
	{
		PropertyNameMarker Name { get; set; }
		TypeNameMarker Type { get; }
	}
}
