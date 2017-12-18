using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(MultiSearchTemplateJsonConverter))]
	public partial interface IMultiSearchTemplateRequest
	{
		IDictionary<string, ISearchTemplateRequest> Operations { get; set; }
	}

	public partial class MultiSearchTemplateRequest
	{
		protected sealed override void Initialize() => this.TypedKeys = true;

		public IDictionary<string, ISearchTemplateRequest> Operations { get; set; }
	}

	[DescriptorFor("MsearchTemplate")]
	public partial class MultiSearchTemplateDescriptor
	{
		protected sealed override void Initialize() => this.TypedKeys();

		internal IDictionary<string, ISearchTemplateRequest> _operations = new Dictionary<string, ISearchTemplateRequest>();

		IDictionary<string, ISearchTemplateRequest> IMultiSearchTemplateRequest.Operations
		{
			get => _operations;
			set => _operations = value;
		}

		public MultiSearchTemplateDescriptor Template<T>(string name, Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class
		{
			name.ThrowIfNull(nameof(name));
			selector.ThrowIfNull(nameof(selector));
			var descriptor = selector(new SearchTemplateDescriptor<T>());
			if (descriptor == null)
				return this;
			this._operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchTemplateDescriptor Template<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector) where T : class
		{
			return this.Template(Guid.NewGuid().ToString(), selector);
		}
	}
}
