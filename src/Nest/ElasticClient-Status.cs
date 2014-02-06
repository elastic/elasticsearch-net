using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IStatusResponse Status(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<IndicesStatusDescriptor, IndicesStatusQueryString, StatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatusDispatch(p)
			);
		}
		public Task<IStatusResponse> StatusAsync(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<IndicesStatusDescriptor, IndicesStatusQueryString, StatusResponse, IStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatusDispatchAsync(p)
			);
		}
	}
}
