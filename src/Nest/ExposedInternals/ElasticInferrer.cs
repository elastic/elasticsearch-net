using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;

namespace Nest
{
	public class ElasticInferrer
	{
		private readonly IConnectionSettings _connectionSettings;

		private TypeNameResolver TypeNameResolver { get; set; }
		private IdResolver IdResolver { get; set; }
		private IndexNameResolver IndexNameResolver { get; set; }

		public string DefaultIndex
		{
			get
			{
				return (this._connectionSettings == null) ? string.Empty : this._connectionSettings.DefaultIndex;
			}
		}
		public ElasticInferrer(IConnectionSettings connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this.TypeNameResolver = new TypeNameResolver();
			this.IdResolver = new IdResolver();
			this.IndexNameResolver = new IndexNameResolver(this._connectionSettings);
		}

		public string IndexName<T>() where T : class
		{
			return this.IndexName(typeof(T));
		}

		public string IndexName(Type type)
		{
			return this.IndexNameResolver.GetIndexForType(type);
		}

		public string IndexName(IndexNameMarker index)
		{
			return index.Resolve(this._connectionSettings);
		}

		public string Id<T>(T obj) where T : class
		{
			return this.IdResolver.GetIdFor(obj);
		}

		public string TypeName<T>() where T : class
		{
			return this.TypeName(typeof(T));
		}
		public string TypeName(Type t)
		{
			return TypeNameMarker.Create(t).Resolve(this._connectionSettings);
		}
	}
}
