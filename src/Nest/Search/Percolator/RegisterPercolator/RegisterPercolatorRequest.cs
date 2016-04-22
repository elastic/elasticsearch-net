using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	// This does not represent a dedicated API in elasticsearch, its syntactic sugar for indexing percolate documents

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(RegisterPercolatorJsonConverter))]
	public interface IRegisterPercolatorRequest : IRequest<IndexRequestParameters> 
	{
		IDictionary<string, object> Metadata { get; set; }

		QueryContainer Query { get; set; }
	}

	public class RegisterPercolatorRequest : RequestBase<IndexRequestParameters>, IRegisterPercolatorRequest
	{
		internal RegisterPercolatorRequest() { }

		public IDictionary<string, object> Metadata { get; set; }
		public QueryContainer Query { get; set; }

		public RegisterPercolatorRequest(IndexName index, Name name) 
			: base(r=>r.Required("index", index).Required("type", (TypeName)".percolator").Required("id", name)) { }
	}

	public class RegisterPercolatorDescriptor<T> 
		: RequestDescriptorBase<RegisterPercolatorDescriptor<T>, IndexRequestParameters, IRegisterPercolatorRequest>, IRegisterPercolatorRequest
		where T : class
	{
		internal RegisterPercolatorDescriptor() { }

		public RegisterPercolatorDescriptor(Name name) 
			: base(r=>r.Required("index", (IndexName)typeof(T)).Required("type", (TypeName)".percolator").Required("id", name)) { }

		public RegisterPercolatorDescriptor<T> Index(IndexName index) => Assign(a => a.RouteValues.Required("index", index));

		QueryContainer IRegisterPercolatorRequest.Query { get; set; }

		IDictionary<string, object> IRegisterPercolatorRequest.Metadata { get; set; }

		/// <summary>
		/// Add metadata associated with this percolator query document that can later be used to query or filter specific percolated queries
		/// </summary>
		public RegisterPercolatorDescriptor<T> Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Metadata = selector?.Invoke(new FluentDictionary<string, object>()));

		/// <summary>
		/// The query to perform the percolation
		/// </summary>
		public RegisterPercolatorDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
