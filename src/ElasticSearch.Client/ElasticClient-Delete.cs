using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {

		//Passing Object
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
		public ConnectionStatus Delete<T>(T @object, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		//Passing object async
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
		public void DeleteAsync<T>(T @object, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendToDeletePath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendToDeletePath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendToDeletePath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}

		//Passing id only
		public ConnectionStatus DeleteById<T>(int id) where T : class
        {
			return this.DeleteById<T>(id.ToString());
        }

		public ConnectionStatus DeleteById<T>(string id) where T : class
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			return this._deleteToPath(path);
        }
		public ConnectionStatus DeleteById<T>(string index, string type, string id) where T : class
        {
			var path = this.CreatePath(index, type, id);
			return this._deleteToPath(path);
        }

		public ConnectionStatus DeleteById<T>(string index, string type, int id) where T : class
        {
			var path = this.CreatePath(index, type, id.ToString());
			return this._deleteToPath(path);
        }

		public ConnectionStatus DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class
		{
			return this.DeleteById<T>(id.ToString(), deleteParameters);
		}

		public ConnectionStatus DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus DeleteById<T>(string index, string type, string id, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		public ConnectionStatus DeleteById<T>(string index, string type, int id, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendToDeletePath(path, deleteParameters);
			return this._deleteToPath(path);
		}


		public void DeleteByIdAsync<T>(int id, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByIdAsync<T>(id.ToString(), callback);
		}

		public void DeleteByIdAsync<T>(string id, Action<ConnectionStatus> callback) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteByIdAsync<T>(string index, string type, string id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id);
			this._deleteToPathAsync(path, callback);
		}

		public void DeleteByIdAsync<T>(string index, string type, int id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			this._deleteToPathAsync(path, callback);
		}


		public void DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByIdAsync<T>(id.ToString(), deleteParameters, callback);
		}

		public void DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendToDeletePath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteByIdAsync<T>(string index, string type, string id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendToDeletePath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}

		public void DeleteByIdAsync<T>(string index, string type, int id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendToDeletePath(path, deleteParameters);
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
