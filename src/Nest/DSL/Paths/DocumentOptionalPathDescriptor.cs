using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type 
	/// this version won't throw if any of the parts are inferred to be empty<para>T</para>
	/// </summary>
	public interface IDocumentOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		IndexNameMarker Index { get; set; }
		TypeNameMarker Type { get; set; }
		string Id { get; set; }
	}

	public abstract class DocumentOptionalPathBase<TParameters> : BaseRequest<TParameters>, IDocumentOptionalPath<TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		public IndexNameMarker Index { get; set; }
		public TypeNameMarker Type { get; set; }
		public string Id { get; set; }
	}


	public abstract class DocumentOptionalPathDescriptorBase<TDescriptor, T, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IDocumentOptionalPath<TParameters>
		where TDescriptor : DocumentOptionalPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{

		private IDocumentOptionalPath<TParameters> Self { get { return this;  } }

		IndexNameMarker IDocumentOptionalPath<TParameters>.Index { get; set; }
		TypeNameMarker IDocumentOptionalPath<TParameters>.Type { get; set; }
		string IDocumentOptionalPath<TParameters>.Id { get; set; }
		private T ObjectToInferOn { get; set; }

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
			return this.Id(id.ToString());
		}
		public TDescriptor Id(string id)
		{
			Self.Id = id;
			return (TDescriptor)this;
		}
		public TDescriptor Object(T @object)
		{
			this.ObjectToInferOn = @object;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(
			IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			var inferrer = new ElasticInferrer(settings);
			var index = Self.Index != null ? inferrer.IndexName(Self.Index) : inferrer.IndexName<T>();
			var type = Self.Type != null ? inferrer.TypeName(Self.Type) : inferrer.TypeName<T>();
			var id = Self.Id ?? inferrer.Id(this.ObjectToInferOn);
		
			pathInfo.Index = index;
			pathInfo.Type = type;
			pathInfo.Id = id;
		}
	}
}
