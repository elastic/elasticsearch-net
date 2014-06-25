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
	public abstract class NamePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			if (this._Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			pathInfo.Name = this._Name;
		}

	}
}
