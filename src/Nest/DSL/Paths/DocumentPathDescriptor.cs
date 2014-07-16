using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class DocumentPathBase<TParameters> : DocumentOptionalPathBase<TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		protected DocumentPathBase(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id)
		{
		}
	}


	public abstract class DocumentPathBase<TParameters, T> : DocumentOptionalPathBase<TParameters, T>
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		protected DocumentPathBase(string id) : base(id) { }

		protected DocumentPathBase(long id) : base(id) { }

		protected DocumentPathBase(T document) : base(document) { }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			base.SetRouteParameters(settings, pathInfo);
			pathInfo.Index.ThrowIfNullOrEmpty("index");
			pathInfo.Type.ThrowIfNullOrEmpty("type");
			pathInfo.Id.ThrowIfNullOrEmpty("id");
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public abstract class DocumentPathDescriptor<TDescriptor, TParameters, T> : DocumentOptionalPathDescriptor<TDescriptor, TParameters, T>
		where TDescriptor : DocumentPathDescriptor<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
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
