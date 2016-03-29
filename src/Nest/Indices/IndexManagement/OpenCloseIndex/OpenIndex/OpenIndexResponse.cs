using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IOpenIndexResponse : IAcknowledgedResponse { }

	public class OpenIndexResponse : AcknowledgedResponseBase, IOpenIndexResponse { }
}
