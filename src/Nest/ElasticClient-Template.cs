using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{

		public ITemplateResponse GetTemplate(string templateName)
		{
			templateName.ThrowIfNull("templateName");
			//TODO validate templateName for invalid url chars?

			string path = this.PathResolver.CreateTemplatePath(templateName);
			ConnectionStatus status = this.Connection.GetSync(path);

			return this.ToParsedResponse<TemplateResponse>(status);
		}

		public IIndicesOperationResponse PutTemplate(Func<TemplateMappingDescriptor, TemplateMappingDescriptor> templateMappingSelector) // TODO: use descriptor
		{
			templateMappingSelector.ThrowIfNull("templateMappingSelector");

			var templateMappingDescriptor = templateMappingSelector(new TemplateMappingDescriptor(this.Settings));
			templateMappingDescriptor.ThrowIfNull("templateMappingDescriptor");

			var templateMapping = templateMappingDescriptor._TemplateMapping;
			templateMapping.ThrowIfNull("templateMapping");

			var templateName = templateMappingDescriptor._Name;

			templateName.ThrowIfNull("templateName");
			templateMapping.ThrowIfNull("templateMapping");

			string template = JsonConvert.SerializeObject(templateMapping, Formatting.None, SerializationSettings);

			return PutTemplateRaw(templateName, template);
		}

		public IIndicesOperationResponse PutTemplateRaw(string templateName, string template)
		{
			string path = this.PathResolver.CreateTemplatePath(templateName);
			ConnectionStatus status = this.Connection.PutSync(path, template);

			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}

		public IIndicesOperationResponse DeleteTemplate(string templateName)
		{
			string path = this.PathResolver.CreateTemplatePath(templateName);
			ConnectionStatus status = this.Connection.DeleteSync(path);

			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}
	}
}