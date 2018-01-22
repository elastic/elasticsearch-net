using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[Obsolete("Scheduled to be removed in 6.0")]
	public interface IDeleteSearchTemplateResponse : IAcknowledgedResponse { }

	[Obsolete("Scheduled to be removed in 6.0")]
	public class DeleteSearchTemplateResponse : AcknowledgedResponseBase, IDeleteSearchTemplateResponse { }
}
