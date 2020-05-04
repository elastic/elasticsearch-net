// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[ReadAs(typeof(SearchTemplateRequest))]
	public partial interface ISearchTemplateRequest : ITypedSearchRequest
	{
		[DataMember(Name ="id")]
		string Id { get; set; }

		[DataMember(Name ="params")]
		IDictionary<string, object> Params { get; set; }

		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public partial class SearchTemplateRequest
	{
		public string Id { get; set; }

		public IDictionary<string, object> Params { get; set; }

		public string Source { get; set; }
		protected Type ClrType { get; set; }
		Type ITypedSearchRequest.ClrType => ClrType;

		protected sealed override void RequestDefaults(SearchTemplateRequestParameters parameters) => TypedKeys = true;
	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest() : base(typeof(T)) => ClrType = typeof(T);

		public SearchTemplateRequest(Indices indices) : base(indices) => ClrType = typeof(T);
	}

	public partial class SearchTemplateDescriptor<TDocument> where TDocument : class
	{
		Type ITypedSearchRequest.ClrType => typeof(TDocument);

		string ISearchTemplateRequest.Id { get; set; }

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }

		string ISearchTemplateRequest.Source { get; set; }

		protected sealed override void RequestDefaults(SearchTemplateRequestParameters parameters) => TypedKeys();

		public SearchTemplateDescriptor<TDocument> Source(string template) => Assign(template, (a, v) => a.Source = v);

		public SearchTemplateDescriptor<TDocument> Id(string id) => Assign(id, (a, v) => a.Id = v);

		public SearchTemplateDescriptor<TDocument> Params(Dictionary<string, object> paramDictionary) => Assign(paramDictionary, (a, v) => a.Params = v);

		public SearchTemplateDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(paramDictionary, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
