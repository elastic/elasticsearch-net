using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexRequest
	{
		object UntypedDocument { get; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(IndexRequestJsonConverter))]
	public interface IIndexRequest<TDocument> : IIndexRequest, IDocumentOptionalPath<IndexRequestParameters, TDocument> 
		where TDocument : class
	{
		TDocument Document { get; set; }
	}

	internal static class IndexPathInfo
	{
		public static void Update<T>(RequestPath<IndexRequestParameters> pathInfo, IIndexRequest<T> request) 
			where T : class
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = pathInfo.Id;
			pathInfo.HttpMethod = id == null || id.IsNullOrEmpty() ? HttpMethod.POST : HttpMethod.PUT;
		}
	}
	
	public partial class IndexRequest<TDocument> : DocumentPathBase<IndexRequestParameters, TDocument>, IIndexRequest<TDocument>
		where TDocument : class
	{
		object IIndexRequest.UntypedDocument => this.Document;

		public TDocument Document { get; set; }

		public IndexRequest(TDocument document) : base(document)
		{
			this.Document = document;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptor<IndexDescriptor<T>, IndexRequestParameters, T>, IIndexRequest<T>
		where T : class
	{
		public IndexDescriptor<T> Assign(Action<IIndexRequest<T>> assigner) => Fluent.Assign(this, assigner);

		object IIndexRequest.UntypedDocument => ((IIndexRequest<T>)this).Document;

		T IIndexRequest<T>.Document { get; set; }

		public IndexDescriptor<T> Document(T document) => Assign(a => {
			a.Document = document;
			a.IdFrom = document;
        });

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}

	}
}
