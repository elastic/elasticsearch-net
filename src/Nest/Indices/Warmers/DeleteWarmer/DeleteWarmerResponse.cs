using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IDeleteWarmerResponse : IAcknowledgedResponse { }

	public class DeleteWarmerResponse  : AcknowledgedResponseBase, IDeleteWarmerResponse { }
}
