using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermvectorRequest : IDocumentOptionalPath<TermvectorRequestParameters>
	{
		/// <summary>
		/// An optional document to get termvectors for instead of using an already indexed document
		/// </summary>
		[JsonProperty("doc")]
		object Document { get; set; }
	}

	public interface ITermvectorRequest<T> : ITermvectorRequest where T : class { }

	internal static class TermvectorPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo, ITermvectorRequest request)
		{
			pathInfo.HttpMethod = request.Document == null ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;
		}
	}

	public partial class TermvectorRequest : DocumentOptionalPathBase<TermvectorRequestParameters>, ITermvectorRequest
	{
		public TermvectorRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
			TermvectorPathInfo.Update(settings, pathInfo, this);
		}

		public object Document { get; set; }
	}

	public partial class TermvectorRequest<T> : DocumentOptionalPathBase<TermvectorRequestParameters, T>, ITermvectorRequest<T>
		where T : class
	{
		object ITermvectorRequest.Document { get; set; }

		public TermvectorRequest(string id) : base(id) { }

		public TermvectorRequest(long id) : base(id) { }

		public TermvectorRequest(T document) : base(document) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
			TermvectorPathInfo.Update(settings, pathInfo, this);
		}
	}

	public partial class TermvectorDescriptor<T> : DocumentOptionalPathDescriptor<TermvectorDescriptor<T>, TermvectorRequestParameters, T>
		, ITermvectorRequest
		where T : class
	{
		object ITermvectorRequest.Document { get; set; }

		public TermvectorDescriptor<T> Document<TDocument>(TDocument document) where TDocument : class
		{
			((ITermvectorRequest) this).Document = document;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
			TermvectorPathInfo.Update(settings, pathInfo, this);
		}

	}
}
