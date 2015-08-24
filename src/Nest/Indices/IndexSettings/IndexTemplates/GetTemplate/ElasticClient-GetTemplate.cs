using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetTemplateConverter = Func<IApiCallDetails, Stream, TemplateResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ITemplateResponse GetTemplate(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.Dispatcher.Dispatch<GetTemplateDescriptor, GetTemplateRequestParameters, TemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => LowLevelDispatch.IndicesGetTemplateDispatch<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc/>
		public ITemplateResponse GetTemplate(IGetTemplateRequest getTemplateRequest)
		{
			return this.Dispatcher.Dispatch<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse>(
				getTemplateRequest,
				(p, d) => LowLevelDispatch.IndicesGetTemplateDispatch<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc/>
		public Task<ITemplateResponse> GetTemplateAsync(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<GetTemplateDescriptor, GetTemplateRequestParameters, TemplateResponse, ITemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc/>
		public Task<ITemplateResponse> GetTemplateAsync(IGetTemplateRequest getTemplateRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse, ITemplateResponse>(
				getTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		private TemplateResponse DeserializeTemplateResponse(
			IApiCallDetails response,
			Stream stream)
		{
			if (!response.Success) return new TemplateResponse();

			var dict = this.Serializer.Deserialize<Dictionary<string, TemplateMapping>>(stream);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping");

			return new TemplateResponse
			{
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}
	}
}