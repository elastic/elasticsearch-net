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
		internal MoreLikeThisQueryDescriptor<T> _Options { get; set; }

		/// <summary>
		/// Specify on which fields the _mlt should act and how it should behave
		/// </summary>
		public MoreLikeThisDescriptor<T> Options(Func<MoreLikeThisQueryDescriptor<T>, MoreLikeThisQueryDescriptor<T>> optionsSelector)
		{
			optionsSelector.ThrowIfNull("optionsSelector");
			var d = optionsSelector(new MoreLikeThisQueryDescriptor<T>());
			this._Options = d;
			return this;
		}

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

		ElasticSearchPathInfo<MoreLikeThisQueryString> IPathInfo<MoreLikeThisQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<MoreLikeThisQueryString>(settings);
			pathInfo.HttpMethod = this._Search == null ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
