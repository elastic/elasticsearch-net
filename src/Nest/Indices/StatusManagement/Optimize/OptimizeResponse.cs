using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IOptimizeResponse : IShardsOperationResponse { }

	public class OptimizeResponse : ShardsOperationResponseBase, IOptimizeResponse { }
}
