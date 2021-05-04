// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SearchInputRequest))]
	public interface ISearchInputRequest
	{
		[DataMember(Name = "body")]
		[ReadAs(typeof(SearchRequest))]
		ISearchRequest Body { get; set; }

		[DataMember(Name = "indices")]
		IEnumerable<IndexName> Indices { get; set; }

		[DataMember(Name = "indices_options")]
		IIndicesOptions IndicesOptions { get; set; }

		[DataMember(Name = "search_type")]
		SearchType? SearchType { get; set; }

		[DataMember(Name = "template")]
		[ReadAs(typeof(SearchTemplateRequest))]
		ISearchTemplateRequest Template { get; set; }
	}

	public class SearchInputRequest : ISearchInputRequest
	{
		public ISearchRequest Body { get; set; }
		public IEnumerable<IndexName> Indices { get; set; }

		public IIndicesOptions IndicesOptions { get; set; }

		public SearchType? SearchType { get; set; }

		public ISearchTemplateRequest Template { get; set; }
	}

	public class SearchInputRequestDescriptor : DescriptorBase<SearchInputRequestDescriptor, ISearchInputRequest>, ISearchInputRequest
	{
		ISearchRequest ISearchInputRequest.Body { get; set; }
		IEnumerable<IndexName> ISearchInputRequest.Indices { get; set; }
		IIndicesOptions ISearchInputRequest.IndicesOptions { get; set; }
		SearchType? ISearchInputRequest.SearchType { get; set; }
		ISearchTemplateRequest ISearchInputRequest.Template { get; set; }

		public SearchInputRequestDescriptor Indices(IEnumerable<IndexName> indices) =>
			Assign(indices, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor Indices(params IndexName[] indices) =>
			Assign(indices, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor Indices<T>() =>
			Assign(new[] { (IndexName)typeof(T) }, (a, v) => a.Indices = v);

		public SearchInputRequestDescriptor SearchType(SearchType? searchType) =>
			Assign(searchType, (a, v) => a.SearchType = v);

		public SearchInputRequestDescriptor IndicesOptions(Func<IndicesOptionsDescriptor, IIndicesOptions> selector) =>
			Assign(selector(new IndicesOptionsDescriptor()), (a, v) => a.IndicesOptions = v);

		public SearchInputRequestDescriptor Body<T>(Func<SearchDescriptor<T>, ISearchRequest> selector) where T : class =>
			Assign(selector, (a, v) => a.Body = v?.InvokeOrDefault(new SearchDescriptor<T>()));

		public SearchInputRequestDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			Assign(selector, (a, v) => a.Template = v?.InvokeOrDefault(new SearchTemplateDescriptor<T>()));
	}
}
