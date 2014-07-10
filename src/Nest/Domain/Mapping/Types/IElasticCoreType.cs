using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IElasticCoreType 
	{
		string IndexName { get; set; }
	}
}
