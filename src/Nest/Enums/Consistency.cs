using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public enum Consistency
	{
		Quorum, //default in ES
		One, 
		All
	}
}
