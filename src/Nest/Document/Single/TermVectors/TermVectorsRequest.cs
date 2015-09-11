using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	public interface ITermVectorsRequest : IDocumentOptionalPath<TermVectorsRequestParameters>
	{
		/// <summary>
		/// An optional document to get termvectors for instead of using an already indexed document
		/// </summary>
		[JsonProperty("doc")]
		object Document { get; set; }

		[JsonProperty("per_field_analyzer")]
		IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }
	}

	public interface ITermVectorsRequest<T> : ITermVectorsRequest where T : class { }

	internal static class TermVectorsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, RequestPath<TermVectorsRequestParameters> pathInfo, ITermVectorsRequest request)
		{
			pathInfo.HttpMethod = request.Document == null ? HttpMethod.GET : HttpMethod.POST;
		}
	}

	public partial class TermVectorsRequest : DocumentOptionalPathBase<TermVectorsRequestParameters>, ITermVectorsRequest
	{
		public TermVectorsRequest(IndexName indexName, TypeName typeName, string id) : base(indexName, typeName, id) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<TermVectorsRequestParameters> pathInfo)
		{
			TermVectorsPathInfo.Update(settings, pathInfo, this);
		}

		public object Document { get; set; }

		public IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }
	}

	public partial class TermVectorsRequest<T> : DocumentOptionalPathBase<TermVectorsRequestParameters, T>, ITermVectorsRequest<T>
		where T : class
	{
		object ITermVectorsRequest.Document { get; set; }

		IDictionary<FieldName, string> ITermVectorsRequest.PerFieldAnalyzer { get; set; }

		public TermVectorsRequest(string id) : base(id) { }

		public TermVectorsRequest(long id) : base(id) { }

		public TermVectorsRequest(T document) : base(document) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<TermVectorsRequestParameters> pathInfo)
		{
			TermVectorsPathInfo.Update(settings, pathInfo, this);
		}
	}

	[DescriptorFor("Termvectors")]
	public partial class TermVectorsDescriptor<T> : DocumentOptionalPathDescriptor<TermVectorsDescriptor<T>, TermVectorsRequestParameters, T>
		, ITermVectorsRequest
		where T : class
	{
		private ITermVectorsRequest Self => this;
		
		object ITermVectorsRequest.Document { get; set; }

		IDictionary<FieldName, string> ITermVectorsRequest.PerFieldAnalyzer { get; set; }

		public TermVectorsDescriptor<T> Document<TDocument>(TDocument document) where TDocument : class
		{
			Self.Document = document;	
			return this;
		}
		public TermVectorsDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<T, object>>, string>, FluentDictionary<Expression<Func<T, object>>, string>> analyzerSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, string>();
			analyzerSelector(d);
			Self.PerFieldAnalyzer = d.ToDictionary(x => FieldName.Create(x.Key), x => x.Value);
			return this;
		}

		public TermVectorsDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<FieldName, string>, FluentDictionary<FieldName, string>> analyzerSelector)
		{
			Self.PerFieldAnalyzer = analyzerSelector(new FluentDictionary<FieldName, string>());
			return this;
		}
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<TermVectorsRequestParameters> pathInfo)
		{
			TermVectorsPathInfo.Update(settings, pathInfo, this);
		}

	}
}
