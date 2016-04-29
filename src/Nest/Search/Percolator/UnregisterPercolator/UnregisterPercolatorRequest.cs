using System;
using Elasticsearch.Net;

namespace Nest
{
	[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
	public interface IUnregisterPercolatorRequest : IRequest<DeleteRequestParameters> { }

	[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
	public class UnregisterPercolatorRequest : RequestBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
	{
		public UnregisterPercolatorRequest(IndexName index, Name name)
			: base(r=>r.Required("index", index).Required("type", (TypeName)".percolator").Required("id", name)) { }
	}

	[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
	public class UnregisterPercolatorDescriptor<T>
		: RequestDescriptorBase<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, IUnregisterPercolatorRequest>, IUnregisterPercolatorRequest
		where T : class
	{
		public UnregisterPercolatorDescriptor(Name name)
			: base(r=>r.Required("index", (IndexName)typeof(T)).Required("type", (TypeName)".percolator").Required("id", name)) { }

		public UnregisterPercolatorDescriptor<T> Index(IndexName index) => Assign(a => a.RouteValues.Required("index", index));
	}
}
