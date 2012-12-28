using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IElasticType 
	{
		string Name { get; set; }
		string Type { get; }
	}
}
