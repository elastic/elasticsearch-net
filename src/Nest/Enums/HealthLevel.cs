using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public enum HealthLevel
	{
		Cluster, //default in ES
		Indices,
        Shards
	}
}
