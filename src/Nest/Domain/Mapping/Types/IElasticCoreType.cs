using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticCoreType 
	{
		string IndexName { get; set; }
	}
}
