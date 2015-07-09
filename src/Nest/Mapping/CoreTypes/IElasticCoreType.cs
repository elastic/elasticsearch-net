using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticCoreType : IElasticType
	{
		string IndexName { get; set; }
	}
}
