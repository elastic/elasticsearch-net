using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISourceRequest : IRequest<SourceRequestParameters> { }

	public interface ISourceRequest<T> : ISourceRequest where T : class { }

	public partial class SourceRequest : RequestBase<SourceRequestParameters>, ISourceRequest
	{
	}

	public partial class SourceRequest<T> : RequestBase<SourceRequestParameters>, ISourceRequest<T>
		where T : class
	{
	}

	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> : RequestDescriptorBase<SourceDescriptor<T>, SourceRequestParameters>
		where T : class
	{
		public SourceDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public SourceDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
	}
}
