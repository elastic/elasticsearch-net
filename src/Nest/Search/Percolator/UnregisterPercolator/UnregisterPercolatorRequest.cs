using Elasticsearch.Net;

namespace Nest
{
	public interface IUnregisterPercolatorRequest : IRequest<DeleteRequestParameters> { }

	public class UnregisterPercolatorRequest : RequestBase<DeleteRequestParameters>, IUnregisterPercolatorRequest
	{
		public UnregisterPercolatorRequest(IndexName index, Name name) 
			: base(r=>r.Required("index", index).Required("type", (TypeName)".percolator").Required("id", name)) { }
	}

	public class UnregisterPercolatorDescriptor<T>
		: RequestDescriptorBase<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, IUnregisterPercolatorRequest>, IUnregisterPercolatorRequest
		where T : class
	{
		public UnregisterPercolatorDescriptor(Name name) 
			: base(r=>r.Required("index", (IndexName)typeof(T)).Required("type", (TypeName)".percolator").Required("id", name)) { }

		public UnregisterPercolatorDescriptor<T> Index(IndexName index) => Assign(a => a.RouteValues.Required("index", index));
	}
}
