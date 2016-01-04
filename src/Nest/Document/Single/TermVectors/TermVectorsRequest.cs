using System;
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
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }
	}

	public partial class TermVectorsRequest<TDocument>
		where TDocument : class
	{
		HttpMethod IRequest.HttpMethod => this.Document == null ? HttpMethod.GET : HttpMethod.POST;

		public TDocument Document { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		partial void DocumentFromPath(TDocument document)
		{
			Self.Document = document;
			if (Self.Document != null)
				Self.RouteValues.Remove("id");
		}
	}

	[DescriptorFor("Termvectors")]
	public partial class TermVectorsDescriptor<TDocument> where TDocument : class
	{
		HttpMethod IRequest.HttpMethod => Self.Document == null ? HttpMethod.GET : HttpMethod.POST;

		TDocument ITermVectorsRequest<TDocument>.Document { get; set; }

		IPerFieldAnalyzer ITermVectorsRequest<TDocument>.PerFieldAnalyzer { get; set; }

		public TermVectorsDescriptor<TDocument> Document(TDocument document) => Assign(a => a.Document = document);

		public TermVectorsDescriptor<TDocument> PerFieldAnalyzer(Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);
	}
}
