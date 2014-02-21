using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor :
		NamePathDescriptor<DeleteTemplateDescriptor, DeleteTemplateQueryString>
		, IPathInfo<DeleteTemplateQueryString>
	{
		ElasticsearchPathInfo<DeleteTemplateQueryString> IPathInfo<DeleteTemplateQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<DeleteTemplateQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			
			return pathInfo;
		}

	}
}
