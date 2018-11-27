using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("termvectors.json")]
	public partial interface ITermVectorsRequest<TDocument>
		where TDocument : class
	{
		/// <summary>
		/// An optional document to get term vectors for instead of using an already indexed document
		/// </summary>
		[DataMember(Name ="doc")]
		[JsonConverter(typeof(SourceConverter))]
		TDocument Document { get; set; }

		/// <summary>
		/// Filter the terms returned based on their TF-IDF scores.
		/// This can be useful in order find out a good characteristic vector of a document.
		/// </summary>
		[DataMember(Name ="filter")]
		ITermVectorFilter Filter { get; set; }

		/// <summary>
		/// Provide a different analyzer than the one at the field.
		/// This is useful in order to generate term vectors in any fashion, especially when using artificial documents.
		/// </summary>
		[DataMember(Name ="per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }
	}

	public partial class TermVectorsRequest<TDocument>
		where TDocument : class
	{
		/// <inheritdoc cref="ITermVectorsRequest{TDocument}.Document"/>
		public TDocument Document { get; set; }

		/// <summary>
		/// Filter the terms returned based on their TF-IDF scores.
		/// This can be useful in order find out a good characteristic vector of a document.
		/// </summary>
		public ITermVectorFilter Filter { get; set; }

		/// <summary>
		/// Provide a different analyzer than the one at the field.
		/// This is useful in order to generate term vectors in any fashion, especially when using artificial documents.
		/// </summary>
		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		HttpMethod IRequest.HttpMethod => Document != null || Filter != null ? HttpMethod.POST : HttpMethod.GET;

		partial void DocumentFromPath(TDocument document)
		{
			Document = document;
			if (Document != null) Self.RouteValues.Remove("id");
		}
	}

	public partial class TermVectorsDescriptor<TDocument> where TDocument : class
	{
		TDocument ITermVectorsRequest<TDocument>.Document { get; set; }

		ITermVectorFilter ITermVectorsRequest<TDocument>.Filter { get; set; }
		HttpMethod IRequest.HttpMethod => Self.Document != null || Self.Filter != null ? HttpMethod.POST : HttpMethod.GET;

		IPerFieldAnalyzer ITermVectorsRequest<TDocument>.PerFieldAnalyzer { get; set; }

		/// <summary>
		/// An optional document to get term vectors for instead of using an already indexed document
		/// </summary>
		public TermVectorsDescriptor<TDocument> Document(TDocument document) => Assign(a => a.Document = document);

		/// <summary>
		/// Provide a different analyzer than the one at the field.
		/// This is useful in order to generate term vectors in any fashion, especially when using artificial documents.
		/// </summary>
		public TermVectorsDescriptor<TDocument> PerFieldAnalyzer(
			Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector
		) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);

		/// <summary>
		/// Filter the terms returned based on their TF-IDF scores.
		/// This can be useful in order find out a good characteristic vector of a document.
		/// </summary>
		public TermVectorsDescriptor<TDocument> Filter(Func<TermVectorFilterDescriptor, ITermVectorFilter> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new TermVectorFilterDescriptor()));
	}
}
