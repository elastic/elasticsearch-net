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
	[DescriptorFor("Index")]
	public partial class IndexDescriptor<T> : DocumentPathDescriptorBase<IndexDescriptor<T>, T, IndexQueryString>
		, IPathInfo<IndexQueryString>
		where T : class
	{
		ElasticsearchPathInfo<IndexQueryString> IPathInfo<IndexQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<IndexQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = this._Id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
			return pathInfo;
		}
	}
}
