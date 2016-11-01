using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public abstract class MatrixAggregateBase : IAggregate
	{
		public IDictionary<string, object> Meta { get; set; }
	}
}
