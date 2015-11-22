using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IDeleteRequest : IRequest<DeleteRequestParameters> { }

	public interface IDeleteRequest<T> : IDeleteRequest where T : class { }

	public partial class DeleteRequest { }

	public partial class DeleteRequest<T> : RequestBase<DeleteRequestParameters>, IDeleteRequest<T>
		where T : class
	{
	}

	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> where T : class { }
}
