using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public class DocumentPathDescriptorBase<TDescriptor, T, TParameters> : DocumentOptionalPathDescriptorBase<TDescriptor, T, TParameters>
		where TDescriptor : DocumentPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{

		internal override ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var pathInfo = base.ToPathInfo(settings, queryString);
			
			pathInfo.Index.ThrowIfNullOrEmpty("index");
			pathInfo.Type.ThrowIfNullOrEmpty("type");
			pathInfo.Id.ThrowIfNullOrEmpty("id");

			return pathInfo;
		}

	}
}
