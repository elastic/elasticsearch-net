using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IPutSearchTemplateResponse PutSearchTemplate(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<PutSearchTemplateDescriptor, PutTemplateRequestParameters, PutSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.PutTemplateDispatch<PutSearchTemplateResponse>(p, d)
			);
		}

		public IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request)
		{
			return this.Dispatcher.Dispatch<IPutSearchTemplateRequest, PutTemplateRequestParameters, PutSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.PutTemplateDispatch<PutSearchTemplateResponse>(p, d)
			);
		}

		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<PutSearchTemplateDescriptor, PutTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.RawDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>(p, d)
			);
		}

		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)
		{
			return this.Dispatcher.DispatchAsync<IPutSearchTemplateRequest, PutTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				request,
				(p, d) => this.RawDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>(p, d)
			);
		}
	}
}
