using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
		public ConnectionStatus Delete<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._deleteToPath(path);
		}
		public void DeleteAsync<T>(T @object, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, string type, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			this._deleteToPathAsync(path, callback);
		}


		public ConnectionStatus Delete<T>(int id) where T : class
        {
            return this.Delete<T>(id.ToString());
        }

		public ConnectionStatus Delete<T>(string id) where T : class
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            var typeName = this.InferTypeName<T>();
			var path = this.createPath(index, typeName, id);
			return this._deleteToPath(path);
        }
		public ConnectionStatus Delete<T>(string index, string type, string id) where T : class
        {
			var path = this.createPath(index, type, id);
			return this._deleteToPath(path);
        }

		public ConnectionStatus Delete<T>(string index, string type, int id) where T : class
        {
			var path = this.createPath(index, type, id.ToString());
			return this._deleteToPath(path);
        }

		public void DeleteAsync<T>(int id, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteAsync<T>(id.ToString(), callback);
		}

		public void DeleteAsync<T>(string id, Action<ConnectionStatus> callback) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.createPath(index, typeName, id);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(string index, string type, string id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.createPath(index, type, id);
			this._deleteToPathAsync(path, callback);
		}

		public void DeleteAsync<T>(string index, string type, int id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.createPath(index, type, id.ToString());
			this._deleteToPathAsync(path, callback);
		}


		private ConnectionStatus _deleteToPath(string path)
		{
			path.ThrowIfNull("path");
			return this.Connection.DeleteSync(path);
		}
		private void _deleteToPathAsync(string path, Action<ConnectionStatus> callback)
		{
			path.ThrowIfNull("path");
			this.Connection.Delete(path, callback);
		}
    }
}
