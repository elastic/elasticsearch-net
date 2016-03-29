using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface ISyncedFlushResponse : IShardsOperationResponse { }

	public class SyncedFlushResponse : ShardsOperationResponseBase, ISyncedFlushResponse { }
}
