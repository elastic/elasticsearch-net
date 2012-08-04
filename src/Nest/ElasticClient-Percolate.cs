using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Register a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		/// <param name="querySelector">Path and query descriptor using dynamics to describe the query</param>
		public IRegisterPercolateResponse RegisterPercolator(string name, Action<QueryPathDescriptor<dynamic>> querySelector)
		{
			return this.RegisterPercolator<dynamic>(name, querySelector);
		}
		/// <summary>
		/// Register a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		/// <param name="querySelector">Path and query descriptor using T to describe the query</param>
		public IRegisterPercolateResponse RegisterPercolator<T>(string name, Action<QueryPathDescriptor<T>> querySelector) where T : class
		{
		  querySelector.ThrowIfNull("queryDescriptor");
			var descriptor = new QueryPathDescriptor<T>();
			querySelector(descriptor);
			var query = this.Serialize(new { query = descriptor });
			var index = this.IndexNameResolver.GetIndexForType<T>();
			if (descriptor._Indices.HasAny())
				index = descriptor._Indices.First();
			var path = "_percolator/{0}/{1}".F(Uri.EscapeDataString(index), Uri.EscapeDataString(name));
			return this._RegisterPercolator(path, query);
		}
		[Obsolete("Deprecated but will never be removed. Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public IRegisterPercolateResponse RegisterPercolator(string index, string name, string query)
		{
			var path = "_percolator/{0}/{1}".F(Uri.EscapeDataString(index), Uri.EscapeDataString(name));
			return this._RegisterPercolator(path, query);
		}
		private RegisterPercolateResponse _RegisterPercolator(string path, string query)
		{
			var status = this.Connection.PutSync(path, query);
			var r = this.ToParsedResponse<RegisterPercolateResponse>(status);
			return r;
		}
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
			var r = this.ToParsedResponse<UnregisterPercolateResponse>(status, allow404: true);
			return r;
		}
		
		/// <summary>
		/// Manually percolate an object using its inferred typename and the default index
		/// </summary>
		public IPercolateResponse Percolate<T>(T @object) where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			var type = this.TypeNameResolver.GetTypeNameFor<T>();
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, IndexSerializationSettings);

			return this.Percolate(index, type,"{{doc:{0}}}".F(doc));
		}
		/// <summary>
		/// Manually percolate an object using its inferred typename and the specified index
		/// </summary>
		public IPercolateResponse Percolate<T>(string index, T @object) where T : class
		{
			var type = this.TypeNameResolver.GetTypeNameFor<T>();
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);

			return this.Percolate(index, type, "{{doc:{0}}}".F(doc));
		}
		/// <summary>
		/// Manually percolate an object using the specified typename and the default index
		/// </summary>
		public IPercolateResponse Percolate<T>(string index, string type, T @object) where T : class
		{
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
			return this.Percolate(index, type, "{{doc:{0}}}".F(doc));
		}
		private PercolateResponse Percolate(string index, string type, string doc)
		{
			var path = this.PathResolver.CreateIndexTypePath(index, type, "_percolate");
			var status = this.Connection.PostSync(path, doc);
			var r = this.ToParsedResponse<PercolateResponse>(status);
			return r;
		}
	}
}
