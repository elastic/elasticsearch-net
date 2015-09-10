using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISourceRequest : IDocumentOptionalPath<SourceRequestParameters> { }

	public interface ISourceRequest<T> : ISourceRequest where T : class { }

	public partial class SourceRequest : DocumentPathBase<SourceRequestParameters>, ISourceRequest
	{
		public SourceRequest(IndexName indexName, TypeName typeName, string id) : base(indexName, typeName, id) { }
	}

	public partial class SourceRequest<T> : DocumentPathBase<SourceRequestParameters, T>, ISourceRequest<T>
		where T : class
	{
		public SourceRequest(string id) : base(id) { }

		public SourceRequest(long id) : base(id) { }

		public SourceRequest(T document) : base(document) { }
	}

	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> : DocumentPathDescriptor<SourceDescriptor<T>, SourceRequestParameters, T>
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
