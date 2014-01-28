using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> : DocumentPathDescriptorBase<DeleteDescriptor<T>, T, DeleteQueryString>
		, IPathInfo<DeleteQueryString>
		where T : class
	{
		ElasticSearchPathInfo<DeleteQueryString> IPathInfo<DeleteQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<DeleteQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			return pathInfo;
		}
	}
}
