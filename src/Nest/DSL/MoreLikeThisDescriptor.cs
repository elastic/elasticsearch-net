using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("Mlt")]
	public partial class MoreLikeThisDescriptor<T> 
		: DocumentPathDescriptorBase<MoreLikeThisDescriptor<T>, T, MoreLikeThisQueryString>
		, IPathInfo<MoreLikeThisQueryString> 
		where T : class
	{
		internal SearchDescriptor<T> _Search { get; set; }
		
		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public MoreLikeThisDescriptor<T> Search(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchDescriptor)
		{
			searchDescriptor.ThrowIfNull("searchDescriptor");
			var d = searchDescriptor(new SearchDescriptor<T>());
			this._Search = d;
			return this;
		}

		ElasticsearchPathInfo<MoreLikeThisQueryString> IPathInfo<MoreLikeThisQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<MoreLikeThisQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = this._Search == null ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
