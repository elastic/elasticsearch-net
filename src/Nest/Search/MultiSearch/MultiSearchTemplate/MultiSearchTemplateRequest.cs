using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(MultiSearchTemplateJsonConverter))]
	public partial interface IMultiSearchTemplateRequest
	{
		IDictionary<string, ISearchTemplateRequest> Operations { get; set; }
	}

	public partial class MultiSearchTemplateRequest
	{
		public IDictionary<string, ISearchTemplateRequest> Operations { get; set; }
	}

	[DescriptorFor("MsearchTemplate")]
	public partial class MultiSearchTemplateDescriptor
	{
		internal IDictionary<string, ISearchTemplateRequest> _operations = new Dictionary<string, ISearchTemplateRequest>();

		IDictionary<string, ISearchTemplateRequest> IMultiSearchTemplateRequest.Operations
		{
			get { return _operations; }
			set { _operations = value; }
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
