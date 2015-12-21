using Elasticsearch.Net;

namespace Nest
{
	public interface IUnregisterPercolatorRequest : IRequest<DeleteRequestParameters> { }

	//TODO port complex route values logic

	//internal static class UnregisterPercolatorPathInfo
	//{
	//	public static void Update(IConnectionSettingsValues settings, RouteValues pathInfo)
	//	{
	//		//deleting a percolator in elasticsearch < 1.0 is actually deleting a document in a 
	//		//special _percolator index where the passed index is actually a type
	//		//the name is actually the id, we rectify that here
	//		pathInfo.Index = pathInfo.Index;
	//		pathInfo.Id = pathInfo.Name;
	//		pathInfo.Type = ".percolator";
	//		pathInfo.HttpMethod = HttpMethod.DELETE;
	//	}
	//}

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
