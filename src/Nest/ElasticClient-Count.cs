using System;
using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Performs a count query over all indices
		/// </summary>
		public ICountResponse CountAllRaw(string query)
		{
			return this._Count("_count", query);
		}
		/// <summary>
		/// Performs a count query over all indices
		/// </summary>
		public ICountResponse CountAll(Func<QueryDescriptor, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = new QueryDescriptor();
			var bq = querySelector(descriptor);
			var query = this.Serialize(bq);
			return this._Count("_count", query);
		}
		/// <summary>
		/// Performs a count query over all indices
		/// </summary>
		public ICountResponse CountAll<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = this.Serialize(descriptor);
			return this._Count("_count", query);
		}

        /// <summary>
        /// Performs a count query over the default index set in the client settings
        /// </summary>
        public ICountResponse CountRaw(string query)
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            string path = this.PathResolver.CreateIndexPath(index, "_count");
            return _Count(path, query);
        }
		/// <summary>
		/// Performs a count query over the default index set in the client settings
		/// </summary>
		public ICountResponse Count(Func<QueryDescriptor, BaseQuery> querySelector)
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			string path = this.PathResolver.CreateIndexPath(index, "_count");
			var descriptor = new QueryDescriptor();
			var bq = querySelector(descriptor);
			var query = this.Serialize(bq);
			return _Count(path, query);
		}
		/// <summary>
		/// Performs a count query over the passed indices
		/// </summary>
		public ICountResponse Count(IEnumerable<string> indices, Func<QueryDescriptor, BaseQuery> querySelector)
		{
			indices.ThrowIfEmpty("indices");
			string path = this.PathResolver.CreateIndexPath(indices, "_count");
			var descriptor = new QueryDescriptor();
			var bq = querySelector(descriptor);
			var query = this.Serialize(bq);
			return _Count(path, query);
		}
		/// <summary>
		/// Performs a count query over the multiple types in multiple indices.
		/// </summary>
		public ICountResponse Count(IEnumerable<string> indices, IEnumerable<string> types, Func<QueryDescriptor, BaseQuery> querySelector)
		{
			indices.ThrowIfEmpty("indices");
			indices.ThrowIfEmpty("types");
			string path = this.PathResolver.CreateIndexTypePath(indices, types, "_count");
			var descriptor = new QueryDescriptor();
			var bq = querySelector(descriptor);
			var query = this.Serialize(bq);
			return _Count(path, query);
		}

        /// <summary>
        /// Perform a count query over the default index and the inferred type name for T
        /// </summary>
        public ICountResponse CountRaw<T>(string query) where T : class
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            var typeName = this.GetTypeNameFor<T>();
            string path = this.PathResolver.CreateIndexTypePath(index, typeName) + "_count";
            return _Count(path, query);
        }
		/// <summary>
		/// Perform a count query over the default index and the inferred type name for T
		/// </summary>
		public ICountResponse Count<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = this.GetTypeNameFor<T>();
			string path = this.PathResolver.CreateIndexTypePath(index, typeName) + "_count";
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = this.Serialize(descriptor);
			return _Count(path, query);
		}
		/// <summary>
		/// Performs a count query over the specified indices
		/// </summary>
		public ICountResponse Count<T>(IEnumerable<string> indices, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class
		{
			indices.ThrowIfEmpty("indices");
			string path = this.PathResolver.CreateIndexPath(indices, "_count");
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = this.Serialize(descriptor);
			return _Count(path, query);
		}
		/// <summary>
		///  Performs a count query over the multiple types in multiple indices.
		/// </summary>
		public ICountResponse Count<T>(IEnumerable<string> indices, IEnumerable<string> types, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class
		{
			indices.ThrowIfEmpty("indices");
			indices.ThrowIfEmpty("types");
			string path = this.PathResolver.CreateIndexTypePath(indices, types, "_count");
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			var query = this.Serialize(descriptor);
			return _Count(path, query);
		}

		private CountResponse _Count(string path, string query)
		{
			var status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<CountResponse>(status);
			return r;
		}

	}
}
