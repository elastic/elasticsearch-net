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
		public IDeleteSearchTemplateResponse DeleteSearchTemplate(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);
		}

		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request)
		{
			return this.Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);
		}

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				d => selector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
		}

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)
		{
			return this.Dispatcher.DispatchAsync<IDeleteSearchTemplateRequest, DeleteTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p)
			);
		}

	}
}
