using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexTemplateConverter = Func<IApiCallDetails, Stream, GetIndexTemplateResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets an index template
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="name">The name of the template to get</param>
		/// <param name="getTemplateSelector">An optional selector specifying additional parameters for the get template operation</param>
		IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> getTemplateSelector = null);

		/// <inheritdoc/>
		IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest getTemplateRequest);

		/// <inheritdoc/>
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> getTemplateSelector = null);

		/// <inheritdoc/>
		Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest getTemplateRequest);

	}

	//TODO discuss with @gmarz changing this and other methods that can actually return multiple to plural form e.g GetIndexTemplates/GetIndexTemplates
	
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexTemplateResponse GetIndexTemplate(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> getTemplateSelector = null) =>
			this.GetIndexTemplate(getTemplateSelector.InvokeOrDefault(new GetIndexTemplateDescriptor()));
		
		/// <inheritdoc/>
		public IGetIndexTemplateResponse GetIndexTemplate(IGetIndexTemplateRequest getTemplateRequest)
		{
			return this.Dispatcher.Dispatch<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse>(
				getTemplateRequest,
				new GetIndexTemplateConverter(DeserializeGetIndexTemplateResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatch<GetIndexTemplateResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> getTemplateSelector = null) =>
			this.GetIndexTemplateAsync(getTemplateSelector.InvokeOrDefault(new GetIndexTemplateDescriptor()));

		/// <inheritdoc/>
		public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest getTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IGetIndexTemplateRequest, GetIndexTemplateRequestParameters, GetIndexTemplateResponse, IGetIndexTemplateResponse>(
				getTemplateRequest,
				new GetIndexTemplateConverter(DeserializeGetIndexTemplateResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatchAsync<GetIndexTemplateResponse>(p)
			);

		//TODO DictionaryResponse!
		private GetIndexTemplateResponse DeserializeGetIndexTemplateResponse(IApiCallDetails response, Stream stream)
		{
			if (!response.Success) return new GetIndexTemplateResponse();

			var dict = this.Serializer.Deserialize<Dictionary<string, TemplateMapping>>(stream);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping");

			return new GetIndexTemplateResponse
			{
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}
	}
}