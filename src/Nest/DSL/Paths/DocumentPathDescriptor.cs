using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public abstract class DocumentPathDescriptorBase<TDescriptor, T, TParameters> : DocumentOptionalPathDescriptorBase<TDescriptor, T, TParameters>
		where TDescriptor : DocumentPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			base.SetRouteParameters(settings, pathInfo);
			pathInfo.Index.ThrowIfNullOrEmpty("index");
			pathInfo.Type.ThrowIfNullOrEmpty("type");
			pathInfo.Id.ThrowIfNullOrEmpty("id");
		}

	}
}
