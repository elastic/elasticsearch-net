using System;
using System.Collections.Generic;
using Utf8Json;

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
		public IDictionary<string, ISearchTemplateRequest> Operations { get; set; }

		protected sealed override void Initialize() => TypedKeys = true;
	}

	public partial class MultiSearchTemplateDescriptor
	{
		internal IDictionary<string, ISearchTemplateRequest> _operations = new Dictionary<string, ISearchTemplateRequest>();

		IDictionary<string, ISearchTemplateRequest> IMultiSearchTemplateRequest.Operations
		{
			get => _operations;
			set => _operations = value;
		}

		protected sealed override void Initialize() => TypedKeys();

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
