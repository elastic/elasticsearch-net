using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetTemplateConverter = Func<IApiCallDetails, Stream, TemplateResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets an index template
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="name">The name of the template to get</param>
		/// <param name="getTemplateSelector">An optional selector specifying additional parameters for the get template operation</param>
		ITemplateResponse GetTemplate(Func<GetTemplateDescriptor, IGetTemplateRequest> getTemplateSelector = null);

		/// <inheritdoc/>
		ITemplateResponse GetTemplate(IGetTemplateRequest getTemplateRequest);

		/// <inheritdoc/>
		Task<ITemplateResponse> GetTemplateAsync(Func<GetTemplateDescriptor, IGetTemplateRequest> getTemplateSelector = null);

		/// <inheritdoc/>
		Task<ITemplateResponse> GetTemplateAsync(IGetTemplateRequest getTemplateRequest);

	}

	//TODO discuss with @gmarz changing this and other methods that can actually return multiple to plural form e.g GetTemplates/GetIndexTemplates
	
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ITemplateResponse GetTemplate(Func<GetTemplateDescriptor, IGetTemplateRequest> getTemplateSelector = null) =>
			this.GetTemplate(getTemplateSelector.InvokeOrDefault(new GetTemplateDescriptor()));
		
		/// <inheritdoc/>
		public ITemplateResponse GetTemplate(IGetTemplateRequest getTemplateRequest)
		{
			return this.Dispatcher.Dispatch<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse>(
				getTemplateRequest,
				new GetTemplateConverter(DeserializeTemplateResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatch<TemplateResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<ITemplateResponse> GetTemplateAsync(Func<GetTemplateDescriptor, IGetTemplateRequest> getTemplateSelector = null) =>
			this.GetTemplateAsync(getTemplateSelector.InvokeOrDefault(new GetTemplateDescriptor()));

		/// <inheritdoc/>
		public Task<ITemplateResponse> GetTemplateAsync(IGetTemplateRequest getTemplateRequest) => 
			this.Dispatcher.DispatchAsync<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse, ITemplateResponse>(
				getTemplateRequest,
				new GetTemplateConverter(DeserializeTemplateResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(p)
			);

		//TODO DictionaryResponse!
		private TemplateResponse DeserializeTemplateResponse(IApiCallDetails response, Stream stream)
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