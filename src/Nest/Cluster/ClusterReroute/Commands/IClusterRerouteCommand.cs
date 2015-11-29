using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(ClusterRerouteCommandJsonConverter))]
	public interface IClusterRerouteCommand
	{
		[JsonIgnore]
		string Name { get; }
	}
}