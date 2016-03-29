using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IBulkAliasResponse : IAcknowledgedResponse { }

	public class BulkAliasResponse : AcknowledgedResponseBase, IBulkAliasResponse { }
}
