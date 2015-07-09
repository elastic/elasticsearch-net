using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IDocumentOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexName Index { get; set; }
		TypeName Type { get; set; }
		string Id { get; set; }
	}

	public interface IDocumentOptionalPath<TParameters, T> : IDocumentOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		T IdFrom { get; set; }
	}

	internal static class DocumentOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IDocumentOptionalPath<TParameters> path,
			IConnectionSettingsValues settings,
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{
			var inferrer = settings.Inferrer;

			pathInfo.Index = inferrer.IndexName(path.Index);
			pathInfo.Type = inferrer.TypeName(path.Type);
			pathInfo.Id = path.Id;
		}

		public static void SetRouteParameters<TParameters, T>(
			IDocumentOptionalPath<TParameters, T> path,
			IConnectionSettingsValues settings,
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{
			var inferrer = settings.Inferrer;

			var index = path.Index != null ? inferrer.IndexName(path.Index) : inferrer.IndexName<T>();
			var type = path.Type != null ? inferrer.TypeName(path.Type) : inferrer.TypeName<T>();
			var id = path.Id ?? inferrer.Id(path.IdFrom);

			pathInfo.Index = index;
			pathInfo.Type = type;
			pathInfo.Id = id;
		}

	}

	public abstract class DocumentOptionalPathBase<TParameters> : BasePathRequest<TParameters>, IDocumentOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public string Id { get; set; }
		
		public DocumentOptionalPathBase(IndexName indexName, TypeName typeName, string id)
		{
			this.Index = indexName;
			this.Type = typeName;
			this.Id = id;
		}
		protected override void SetRouteParameters(
			IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			DocumentOptionalPathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}
	}

	public abstract class DocumentOptionalPathBase<TParameters, T> : BasePathRequest<TParameters>, IDocumentOptionalPath<TParameters, T>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public string Id { get; set; }
		public T IdFrom { get; set; }

		public DocumentOptionalPathBase(string id) { this.Id = id; }
		public DocumentOptionalPathBase(long id) : this(id.ToString(CultureInfo.InvariantCulture)) {}
		public DocumentOptionalPathBase(T idFrom) { this.IdFrom = idFrom; }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			DocumentOptionalPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type 
	/// this version won't throw if any of the parts are inferred to be empty<para>T</para>
	/// </summary>
	public abstract class DocumentOptionalPathDescriptor<TDescriptor, TParameters, T>
		: BasePathDescriptor<TDescriptor, TParameters>, IDocumentOptionalPath<TParameters, T>
		where TDescriptor : DocumentOptionalPathDescriptor<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{

		private IDocumentOptionalPath<TParameters, T> Self => this;
		IndexName IDocumentOptionalPath<TParameters>.Index { get; set; }
		TypeName IDocumentOptionalPath<TParameters>.Type { get; set; }
		string IDocumentOptionalPath<TParameters>.Id { get; set; }
		T IDocumentOptionalPath<TParameters, T>.IdFrom { get; set; }

		public TDescriptor Index(string index)
		{
			Self.Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type index)
		{
			Self.Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			Self.Index = typeof(TAlternative);
			return (TDescriptor)this;
		}

		public TDescriptor Type(string type)
		{
			Self.Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor Type(Type type)
		{
			Self.Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor Type<TAlternative>() where TAlternative : class
		{
			Self.Type = typeof(TAlternative);
			return (TDescriptor)this;
		}
		public TDescriptor Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}
		public TDescriptor Id(string id)
		{
			Self.Id = id;
			return (TDescriptor)this;
		}
		public TDescriptor IdFrom(T @object)
		{
			Self.IdFrom = @object;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(
			IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			DocumentOptionalPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}


	}
}
