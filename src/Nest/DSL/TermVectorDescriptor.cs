using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	public interface ITermvectorRequest : IDocumentOptionalPath<TermvectorRequestParameters>
	{
		/// <summary>
		/// An optional document to get termvectors for instead of using an already indexed document
		/// </summary>
		[JsonProperty("doc")]
		object Document { get; set; }

		[JsonProperty("per_field_analyzer")]
		IDictionary<PropertyPathMarker, string> PerFieldAnalyzer { get; set; }
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

		public IDictionary<PropertyPathMarker, string> PerFieldAnalyzer { get; set; }
	}

	public partial class TermvectorRequest<T> : DocumentOptionalPathBase<TermvectorRequestParameters, T>, ITermvectorRequest<T>
		where T : class
	{
		object ITermvectorRequest.Document { get; set; }

		IDictionary<PropertyPathMarker, string> ITermvectorRequest.PerFieldAnalyzer { get; set; }

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
		private ITermvectorRequest Self { get { return this; } }
		
		object ITermvectorRequest.Document { get; set; }

		IDictionary<PropertyPathMarker, string> ITermvectorRequest.PerFieldAnalyzer { get; set; }

		public TermvectorDescriptor<T> Document<TDocument>(TDocument document) where TDocument : class
		{
			Self.Document = document;	
			return this;
		}
		public TermvectorDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<T, object>>, string>, FluentDictionary<Expression<Func<T, object>>, string>> analyzerSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, string>();
			analyzerSelector(d);
			Self.PerFieldAnalyzer = d.ToDictionary(x => PropertyPathMarker.Create(x.Key), x => x.Value);
			return this;
		}

		public TermvectorDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<PropertyPathMarker, string>, FluentDictionary<PropertyPathMarker, string>> analyzerSelector)
		{
			Self.PerFieldAnalyzer = analyzerSelector(new FluentDictionary<PropertyPathMarker, string>());
			return this;
		}
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
			TermvectorPathInfo.Update(settings, pathInfo, this);
		}

	}
}
