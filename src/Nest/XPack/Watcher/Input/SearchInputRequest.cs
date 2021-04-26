/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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
