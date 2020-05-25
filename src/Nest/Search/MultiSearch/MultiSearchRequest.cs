// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
