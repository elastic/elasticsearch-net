using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{


		public IIndicesOperationResponse PutWarmer<T>(string warmerName, Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			selector.ThrowIfNull("selector");

			var descriptor = selector(new SearchDescriptor<T>());
			descriptor.ThrowIfNull("descriptor");

			var path = this.PathResolver.GetWarmerPathForTyped(descriptor, warmerName);
			var query = this.Serialize(descriptor);

			ConnectionStatus status = this.Connection.PutSync(path, query);
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;


			//var warmerMapping = new WarmerMapping();
			////var templateMapping = templateMappingDescriptor._TemplateMapping;
			////templateMapping.ThrowIfNull("templateMapping");

			////var templateName = templateMappingDescriptor._Name;

			////templateName.ThrowIfNull("templateName");
			////templateMapping.ThrowIfNull("templateMapping");

			//string warmer = JsonConvert.SerializeObject(warmerMapping, Formatting.None, SerializationSettings);

			//return PutWarmerRaw(templateName, warmer);
			//return null;
		}


		public IIndicesOperationResponse PutWarmer(string warmerName, Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> selector)
		{
			selector.ThrowIfNull("selector");

			var descriptor = selector(new SearchDescriptor<dynamic>());
			descriptor.ThrowIfNull("descriptor");

			var path = this.PathResolver.GetWarmerPathForDynamic(descriptor, warmerName);
			var query = this.Serialize(descriptor);

			ConnectionStatus status = this.Connection.PutSync(path, query);
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;


			//var warmerMapping = new WarmerMapping();
			////var templateMapping = templateMappingDescriptor._TemplateMapping;
			////templateMapping.ThrowIfNull("templateMapping");

			////var templateName = templateMappingDescriptor._Name;

			////templateName.ThrowIfNull("templateName");
			////templateMapping.ThrowIfNull("templateMapping");

			//string warmer = JsonConvert.SerializeObject(warmerMapping, Formatting.None, SerializationSettings);

			//return PutWarmerRaw(templateName, warmer);
			//return null;
		}
	}
}