using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	public partial interface ITermVectorsRequest<TDocument>
		where TDocument : class
	{
		/// <summary>
		/// An optional document to get termvectors for instead of using an already indexed document
		/// </summary>
		[JsonProperty("doc")]
		TDocument Document { get; set; }

		[JsonProperty("per_field_analyzer")]
		IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }
	}

	public partial class TermVectorsRequest<TDocument>
		where TDocument : class
	{
		HttpMethod IRequest.HttpMethod => this.Document == null ? HttpMethod.GET : HttpMethod.POST;

		public TDocument Document { get; set; }

		public IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }

		partial void DocumentFromPath(TDocument document)
		{
			Self.Document = document;
			if (Self.Document != null)
				Self.RouteValues.Remove("id");
		}
	}

	[DescriptorFor("Termvectors")]
	public partial class TermVectorsDescriptor<TDocument>
		where TDocument : class
	{
		HttpMethod IRequest.HttpMethod => ((ITermVectorsRequest<TDocument>)this).Document == null ? HttpMethod.GET : HttpMethod.POST;

		TDocument ITermVectorsRequest<TDocument>.Document { get; set; }

		IDictionary<FieldName, string> ITermVectorsRequest<TDocument>.PerFieldAnalyzer { get; set; }

		public TermVectorsDescriptor<TDocument> Document(TDocument document) => Assign(a => a.Document = document);

		public TermVectorsDescriptor<TDocument> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<TDocument, object>>, string>, FluentDictionary<Expression<Func<TDocument, object>>, string>> analyzerSelector) =>
			Assign(a=>a.PerFieldAnalyzer = analyzerSelector?
				.Invoke(new FluentDictionary<Expression<Func<TDocument, object>>, string>())
				?.ToDictionary(x => FieldName.Create(x.Key), x => x.Value)
			);

		public TermVectorsDescriptor<TDocument> PerFieldAnalyzer(
			Func<FluentDictionary<FieldName, string>, FluentDictionary<FieldName, string>> analyzerSelector) =>
				Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new FluentDictionary<FieldName, string>()));
	}
}
