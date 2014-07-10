using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IFixedIndexTypePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexNameMarker Index { get; set; }
		TypeNameMarker Type { get; set; }
	}

	internal static class FixedIndexTypePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IFixedIndexTypePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = settings.Inferrer;
			var index = inferrer.IndexName(path.Index);
			var type = inferrer.TypeName(path.Type);
		
			pathInfo.Index = index;
			pathInfo.Type = type;
		}
	
	}

	public abstract class FixedIndexTypePathBase<TParameters> : BasePathRequest<TParameters>, IFixedIndexTypePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexNameMarker Index { get; set; }
		public TypeNameMarker Type { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			FixedIndexTypePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}


	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}
	/// </pre>
	/// {index} is optional and so is {type} and will NOT fallback to the defaults of <para>T</para>
	/// type can only be specified in conjuction with index.
	/// </summary>
	public abstract class FixedIndexTypePathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IFixedIndexTypePath<TParameters> 
		where TDescriptor : FixedIndexTypePathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IFixedIndexTypePath<TParameters> Self { get { return this; } }

		IndexNameMarker IFixedIndexTypePath<TParameters>.Index { get; set; }
		TypeNameMarker IFixedIndexTypePath<TParameters>.Type { get; set; }

		public TDescriptor FixedPath(string index, string type = null)
		{
			Self.Index = index;
			Self.Type = type;
			return (TDescriptor)this;
		}

		public TDescriptor FixedPath(Type index, Type type = null)
		{
			Self.Index = index;
			Self.Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor FixedPath<TAlternative>(bool fixateType = false)
		{
			Self.Index = typeof (TAlternative);
			if (fixateType)
				Self.Type = typeof(TAlternative);
			return (TDescriptor) this;
		}
		public TDescriptor FixedPath<TIndex,TType>()
		{
			Self.Index = typeof (TIndex);
			Self.Type = typeof(TType);
			return (TDescriptor) this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			FixedIndexTypePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
