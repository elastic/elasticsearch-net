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
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("msearch.json")]
	[JsonFormatter(typeof(MultiSearchFormatter))]
	public partial interface IMultiSearchRequest
	{
		IDictionary<string, ISearchRequest> Operations { get; set; }
	}

	public partial class MultiSearchRequest
	{
		protected sealed override void RequestDefaults(MultiSearchRequestParameters parameters)
		{
			TypedKeys = true;
			parameters.CustomResponseBuilder = new MultiSearchResponseBuilder(this);
		}

		public IDictionary<string, ISearchRequest> Operations { get; set; }
	}

	public partial class MultiSearchDescriptor
	{
		protected sealed override void RequestDefaults(MultiSearchRequestParameters parameters)
		{
			TypedKeys();
			parameters.CustomResponseBuilder = new MultiSearchResponseBuilder(this);
		}

		private IDictionary<string, ISearchRequest> _operations = new Dictionary<string, ISearchRequest>();

		IDictionary<string, ISearchRequest> IMultiSearchRequest.Operations
		{
			get => _operations;
			set => _operations = value;
		}

		public MultiSearchDescriptor Search<T>(string name, Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class
		{
			name.ThrowIfNull(nameof(name));
			searchSelector.ThrowIfNull(nameof(searchSelector));
			var descriptor = searchSelector(new SearchDescriptor<T>());
			if (descriptor == null) return this;

			_operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchDescriptor Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class =>
			Search(Guid.NewGuid().ToString(), searchSelector);
	}
}
