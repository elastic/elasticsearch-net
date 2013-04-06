using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	public class GetWarmerDescriptor
	{
		internal IEnumerable<string> _Indices { get; set; }
		internal string _WarmerName { get; set; }

		internal bool _AllIndices { get; set; }

		private readonly IndexNameResolver indexNameResolver;

		public GetWarmerDescriptor(IConnectionSettings connectionSettings)
		{
			this.indexNameResolver = new IndexNameResolver(connectionSettings);
		}

		public GetWarmerDescriptor AllWarmers()
		{
			_WarmerName = null;
			return this;
		}

		/// <summary>
		/// A wildcard like: "warm*"
		/// </summary>
		public GetWarmerDescriptor WamerWildcard(string wildcard)
		{
			_WarmerName = wildcard;
			return this;
		}

		/// <summary>
		/// The name of the warmer
		/// </summary>
		public GetWarmerDescriptor WarmerName(string warmerName)
		{
			_WarmerName = warmerName;
			return this;
		}

		/// <summary>
		/// Gets the warmer from all the indices, this is the default.
		/// </summary>
		/// <returns></returns>
		public GetWarmerDescriptor AllIndices()
		{
			this._AllIndices = true;
			return this;
		}

		/// <summary>
		/// Set the index.
		/// </summary>
		public GetWarmerDescriptor Index(string index)
		{
			index.ThrowIfNullOrEmpty("index");
			this._Indices = new[] { index };
			return this;
		}


		/// <summary>
		/// Set multiple indices
		/// </summary>
		public GetWarmerDescriptor Indices(params string[] indices)
		{
			return this.Indices((IEnumerable<string>)indices);
		}

		/// <summary>
		/// Set multiple indices
		/// </summary>
		public GetWarmerDescriptor Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("indices");
			indices.ThrowIfEmpty("indices");
			this._Indices = indices;
			return this;
		}

		/// <summary>
		/// Sets the index which is derived from the type T
		/// </summary>
		public GetWarmerDescriptor IndexFromType<T>()
			where T : class
		{
			return IndexFromType(typeof(T));
		}

		/// <summary>
		/// Sets the index which is derived from the type
		/// </summary>
		public GetWarmerDescriptor IndexFromType(Type type)
		{
			type.ThrowIfNull("type");
			return this.Index(indexNameResolver.GetIndexForType(type));
		}
	}
}
