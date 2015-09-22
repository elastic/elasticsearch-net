using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	//this does not represent a dedicated API in elasticsearch, its syntactic sugar for indexing percolate documents

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface IRegisterPercolatorRequest : IRequest<IndexRequestParameters>, ICustomJson
	{
		IDictionary<string, object> MetaData { get; set; }
		QueryContainer Query { get; set; }
	}

	//TODO  Route parameters are complex need to be ported
	
	//internal static class RegisterPercolatorPathInfo
	//{
	//	public static void Update(RouteValues pathInfo, IRegisterPercolatorRequest request)
	//	{
	//		pathInfo.Id = pathInfo.Name;
	//		pathInfo.Type = ".percolator";
	//	}
	//}

	public class RegisterPercolatorRequest : RequestBase<IndexRequestParameters>, IRegisterPercolatorRequest
	{
		public IDictionary<string, object> MetaData { get; set; }
		public QueryContainer Query { get; set; }

		public RegisterPercolatorRequest(IndexName index, Name name) 
			: base(r=>r.Required("index", index).Required("type", (TypeName)".percolator").Required("id", name)) { }

		public object GetCustomJson()
		{
			return new FluentDictionary<string, object>(this.MetaData)
				.Add("query", this.Query);
		}

	}

	public class RegisterPercolatorDescriptor<T> 
		: RequestDescriptorBase<RegisterPercolatorDescriptor<T>, IndexRequestParameters, IRegisterPercolatorRequest>, IRegisterPercolatorRequest
		where T : class
	{
		private IRegisterPercolatorRequest Self => this;

		public RegisterPercolatorDescriptor(Name name) 
			: base(r=>r.Required("index", (IndexName)typeof(T)).Required("type", (TypeName)".percolator").Required("id", name)) { }

		public RegisterPercolatorDescriptor<T> Index(IndexName index) => Assign(a => a.RouteValues.Required("index", index));

		QueryContainer IRegisterPercolatorRequest.Query { get; set; }

		IDictionary<string, object> IRegisterPercolatorRequest.MetaData { get; set; }

		/// <summary>
		/// Add metadata associated with this percolator query document
		/// </summary>
		public RegisterPercolatorDescriptor<T> AddMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			if (selector == null)
				return this;

			Self.MetaData = selector(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The query to perform the percolation
		/// </summary>
		public RegisterPercolatorDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryContainerDescriptor<T>());
			Self.Query = d;
			return this;
		}

		public object GetCustomJson()
		{
			return new FluentDictionary<string, object>(Self.MetaData)
				.Add("query", Self.Query);
		}
	}
}
