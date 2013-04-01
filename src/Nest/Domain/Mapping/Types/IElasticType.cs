using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public interface IElasticType 
	{
		TypeNameMarker TypeNameMarker { get; set; }
		TypeNameMarker Type { get; }
	}
}
