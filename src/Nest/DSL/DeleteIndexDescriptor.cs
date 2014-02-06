using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : 
		IndicesOptionalPathDescriptor<DeleteIndexDescriptor, DeleteIndexQueryString>
		, IPathInfo<DeleteIndexQueryString>
	{
		ElasticSearchPathInfo<DeleteIndexQueryString> IPathInfo<DeleteIndexQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<DeleteIndexQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
