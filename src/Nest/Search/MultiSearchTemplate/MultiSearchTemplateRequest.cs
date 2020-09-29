// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("msearch_template.json")]
	[JsonFormatter(typeof(MultiSearchTemplateFormatter))]
	public partial interface IMultiSearchTemplateRequest
	{
		IDictionary<string, ISearchTemplateRequest> Operations { get; set; }
	}

	public partial class MultiSearchTemplateRequest
	{
		protected sealed override void RequestDefaults(MultiSearchTemplateRequestParameters parameters)
		{
			TypedKeys = true;
			parameters.CustomResponseBuilder = new MultiSearchResponseBuilder(this);
		}

		public IDictionary<string, ISearchTemplateRequest> Operations { get; set; }

	}

	public partial class MultiSearchTemplateDescriptor
	{
		protected sealed override void RequestDefaults(MultiSearchTemplateRequestParameters parameters)
		{
			TypedKeys();
			parameters.CustomResponseBuilder = new MultiSearchResponseBuilder(this);
		}

		private IDictionary<string, ISearchTemplateRequest> _operations = new Dictionary<string, ISearchTemplateRequest>();

		IDictionary<string, ISearchTemplateRequest> IMultiSearchTemplateRequest.Operations
		{
			get => _operations;
			set => _operations = value;
		}

		public MultiSearchTemplateDescriptor Template<T>(string name, Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)
			where T : class
		{
			name.ThrowIfNull(nameof(name));
			selector.ThrowIfNull(nameof(selector));
			var descriptor = selector(new SearchTemplateDescriptor<T>());
			if (descriptor == null)
				return this;

			_operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchTemplateDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class =>
			Template(Guid.NewGuid().ToString(), selector);
	}
}
