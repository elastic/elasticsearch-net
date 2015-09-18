using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexRequest : IRequest<IndexRequestParameters>
	{
		object UntypedDocument { get; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(IndexRequestJsonConverter))]
	public interface IIndexRequest<TDocument> : IIndexRequest
		where TDocument : class
	{
		TDocument Document { get; set; }
	}

	internal static class IndexPathInfo
	{
		public static void Update<T>(RouteValues pathInfo, IIndexRequest<T> request) 
			where T : class
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = pathInfo.Id;
			pathInfo.HttpMethod = id == null || id.IsNullOrEmpty() ? HttpMethod.POST : HttpMethod.PUT;
		}
	}
	
	public partial class IndexRequest<TDocument> : RequestBase<IndexRequestParameters>, IIndexRequest<TDocument>
		where TDocument : class
	{
		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RouteValues pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class IndexDescriptor<T> : RequestDescriptorBase<IndexDescriptor<T>, IndexRequestParameters>, IIndexRequest<T>
		where T : class
	{
		public IndexDescriptor<T> Assign(Action<IIndexRequest<T>> assigner) => Fluent.Assign(this, assigner);

		object IIndexRequest.UntypedDocument => ((IIndexRequest<T>)this).Document;

		T IIndexRequest<T>.Document { get; set; }

		public IndexDescriptor<T> Document(T document) => Assign(a => {
			a.Document = document;
        });

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RouteValues pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}

	}
}
