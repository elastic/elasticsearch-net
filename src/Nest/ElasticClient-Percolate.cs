using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Unregister a percolator, on the default index.
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		public IUnregisterPercolateResponse UnregisterPercolator<T>(string name) where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			return this.UnregisterPercolator(index, name);
		}
		/// <summary>
		/// Unregister a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		public IUnregisterPercolateResponse UnregisterPercolator(string index, string name)
		{
			var path = "_percolator/{0}/{1}".F(Uri.EscapeDataString(index), Uri.EscapeDataString(name));
			return this._UnregisterPercolator(path);
		}
		private UnregisterPercolateResponse _UnregisterPercolator(string path)
		{
			var status = this.Connection.DeleteSync(path);
			var r = this.Deserialize<UnregisterPercolateResponse>(status, allow404: true);
			return r;
		}

		public IRegisterPercolateResponse RegisterPercolator<T>(
			Func<PercolatorDescriptor<T>, PercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			var pathAndData = this._registerPercolator(percolatorSelector);
			var status = this.Connection.PutSync(pathAndData.Path, pathAndData.Data);
			var r = this.Deserialize<RegisterPercolateResponse>(status);
			return r;
		}

	
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(
			Func<PercolatorDescriptor<T>, PercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			var pathAndData = this._registerPercolator(percolatorSelector);
			return this.Connection.Put(pathAndData.Path, pathAndData.Data).ContinueWith(c =>
			{
				var r = this.Deserialize<RegisterPercolateResponse>(c.Result);
				return (IRegisterPercolateResponse)r;
			});
			
		}

		private PathAndData _registerPercolator<T>(Func<PercolatorDescriptor<T>, PercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");

			var descriptor = percolatorSelector(new PercolatorDescriptor<T>());
			if (string.IsNullOrEmpty(descriptor._Name))
				throw new Exception("A percolator needs a name");
			var query = this.Serialize(descriptor);
			var index = descriptor._Index ?? this.IndexNameResolver.GetIndexForType<T>();

			var path = "_percolator/{0}/{1}".F(Uri.EscapeDataString(index), Uri.EscapeDataString(descriptor._Name));
			return new PathAndData() { Path = path, Data = query };
		}

		public IPercolateResponse Percolate<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class
		{
			var pathAndData = this._percolate(percolateSelector);
			var status = this.Connection.PostSync(pathAndData.Path, pathAndData.Data);
			var r = this.Deserialize<PercolateResponse>(status);
			return r;
		}

		

		public Task<IPercolateResponse> PercolateAsync<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class
		{
			var pathAndData = this._percolate(percolateSelector);
			var status = this.Connection.Post(pathAndData.Path, pathAndData.Data).ContinueWith(c =>
			{
				var r = this.Deserialize<PercolateResponse>(c.Result);
				return (IPercolateResponse)r;
			});
			return status;
		}

		private PathAndData _percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class
		{
			var descriptor = percolateSelector(new PercolateDescriptor<T>());
			var index = descriptor._Index ?? this.IndexNameResolver.GetIndexForType<T>();
			var type = descriptor._Type ?? this.GetTypeNameFor<T>();
			var percolateJson = this.Serialize(descriptor);

			var path = this.PathResolver.CreateIndexTypePath(index, type, "_percolate");
			return new PathAndData() { Path = path, Data = percolateJson };
		}
	}
}
