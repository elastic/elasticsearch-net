using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.ServiceModel.Configuration;

namespace Nest
{
	public partial interface ITermVectorsRequest 
	{
		/// <summary>
		/// An optional document to get termvectors for instead of using an already indexed document
		/// </summary>
		[JsonProperty("doc")]
		object Document { get; set; }

		[JsonProperty("per_field_analyzer")]
		IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }
	}

	public partial class TermVectorsRequest 
	{
		HttpMethod IRequest.HttpMethod => this.Document == null ? HttpMethod.GET : HttpMethod.POST;

		public object Document { get; set; }

		public IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }
	}

	//TODO Removed typed variant is this ok? probably not cause Document

	[DescriptorFor("Termvectors")]
	public partial class TermVectorsDescriptor<T> where T : class
	{
		HttpMethod IRequest.HttpMethod => ((ITermVectorsRequest)this).Document == null ? HttpMethod.GET : HttpMethod.POST;

		object ITermVectorsRequest.Document { get; set; }

		IDictionary<FieldName, string> ITermVectorsRequest.PerFieldAnalyzer { get; set; }

		public TermVectorsDescriptor<T> Document<TDocument>(TDocument document) where TDocument : class =>
			Assign(a => a.Document = document);

		public TermVectorsDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<T, object>>, string>, FluentDictionary<Expression<Func<T, object>>, string>> analyzerSelector) =>
			Assign(a=>a.PerFieldAnalyzer = analyzerSelector?
				.Invoke(new FluentDictionary<Expression<Func<T, object>>, string>())
				?.ToDictionary(x => FieldName.Create(x.Key), x => x.Value)
			);

		public TermVectorsDescriptor<T> PerFieldAnalyzer(
			Func<FluentDictionary<FieldName, string>, FluentDictionary<FieldName, string>> analyzerSelector) =>
				Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new FluentDictionary<FieldName, string>()));
	}
}
