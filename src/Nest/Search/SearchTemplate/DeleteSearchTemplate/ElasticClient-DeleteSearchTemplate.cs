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
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAcknowledgedResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		public IAcknowledgedResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) =>
			this.DeleteSearchTemplate(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));


		public IAcknowledgedResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<AcknowledgedResponse>(p)
			);

		public Task<IAcknowledgedResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) => 
			this.DeleteSearchTemplateAsync(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));

		public Task<IAcknowledgedResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}
