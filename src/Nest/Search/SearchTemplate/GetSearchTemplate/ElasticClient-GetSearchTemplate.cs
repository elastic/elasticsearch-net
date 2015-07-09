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
		public IGetSearchTemplateResponse GetSearchTemplate(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<GetSearchTemplateDescriptor, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);
		}

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request)
		{
			return this.Dispatcher.Dispatch<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);
		}

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<GetSearchTemplateDescriptor, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
		}

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)
		{
			return this.Dispatcher.DispatchAsync<IGetSearchTemplateRequest, GetTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p)
			);
		}

	}
}
