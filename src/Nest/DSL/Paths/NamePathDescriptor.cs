using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{name}
	/// </pre>
	/// name is mandatory.
	/// </summary>
	public class NamePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : NamePathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal string _Name { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public TDescriptor Name(string name)
		{
			this._Name = name;
			return (TDescriptor)this;
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			if (this._Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Name = this._Name;
			return pathInfo;
		}

	}
}
