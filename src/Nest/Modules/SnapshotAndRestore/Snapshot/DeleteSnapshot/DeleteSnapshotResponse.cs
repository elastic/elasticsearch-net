using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IDeleteSnapshotResponse : IAcknowledgedResponse { }

	public class DeleteSnapshotResponse : AcknowledgedResponseBase, IDeleteSnapshotResponse { }
}
