using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface IElasticType 
	{
		string Name { get; set; }
		string Type { get; }
    string Similarity { get; set; }
	}
}
