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
	public class PutWarmerDescriptor
	{
		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<string> _Types { get; set; }
		internal string _WarmerName { get; set; }

		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }

		internal SearchDescriptorBase _SearchDescriptor { get; set; }

		private readonly TypeNameResolver typeNameResolver;
		private readonly IndexNameResolver indexNameResolver;

		public PutWarmerDescriptor(IConnectionSettings connectionSettings)
		{
			this.typeNameResolver = new TypeNameResolver();
			this.indexNameResolver = new IndexNameResolver(connectionSettings);
		}

		/// <summary>
		/// The name of the warmer
		/// </summary>
		public PutWarmerDescriptor WarmerName(string warmerName)
		{
			_WarmerName = warmerName;
			return this;
		}

		/// <summary>
		/// Set the index.
		/// </summary>
		public PutWarmerDescriptor Index(string index)
		{
			index.ThrowIfNullOrEmpty("index");
			this._Indices = new[] { index };
			return this;
		}

		/// <summary>
		/// Set multiple indices
		/// </summary>
		public PutWarmerDescriptor Indices(params string[] indices)
		{
			return this.Indices((IEnumerable<string>)indices);
		}

		/// <summary>
		/// Set multiple indices
		/// </summary>
		public PutWarmerDescriptor Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("indices");
			indices.ThrowIfEmpty("indices");
			return this.Index(string.Join(",", indices));
		}

		/// <summary>
		/// Sets the index which is derived from the type T
		/// </summary>
		public PutWarmerDescriptor IndexFromType<T>()
			where T : class
		{
			return this.IndexFromType(typeof(T));
		}

		/// <summary>
		/// Sets the index which is derived from the type
		/// </summary>
		public PutWarmerDescriptor IndexFromType(Type type)
		{
			type.ThrowIfNull("type");
			return this.Index(indexNameResolver.GetIndexForType(type));
		}

		public PutWarmerDescriptor Type(string type)
		{
			return this.Types(new[] {type});
		}

		public PutWarmerDescriptor Types(IEnumerable<string> types)
		{
			this._Types = types;
			return this;
		}

		public PutWarmerDescriptor Type(Type type)
		{
			return this.Type(typeNameResolver.GetTypeNameFor(type))
							.IndexFromType(type);
		}

		public PutWarmerDescriptor Types(IEnumerable<Type> types)
		{
			return this.Types(typeNameResolver.GetTypeNamesFor(types));
		}

		public PutWarmerDescriptor Type<T>()
		{
			return this.Type(typeof (T));
		}

		public PutWarmerDescriptor Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> selector)
		{
			this._SearchDescriptor = selector(new SearchDescriptor<dynamic>());
			return this;
		}

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			this._SearchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}
	}
}
