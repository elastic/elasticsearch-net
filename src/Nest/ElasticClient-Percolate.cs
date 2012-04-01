using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Register a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		/// <param name="querySelector">Path and query descriptor using dynamics to describe the query</param>
		public RegisterPercolateResponse RegisterPercolator(string name, Action<QueryPathDescriptor<dynamic>> querySelector)
		{
			return this.RegisterPercolator<dynamic>(name, querySelector);
		}
		/// <summary>
		/// Register a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		/// <param name="querySelector">Path and query descriptor using T to describe the query</param>
		public RegisterPercolateResponse RegisterPercolator<T>(string name, Action<QueryPathDescriptor<T>> querySelector) where T : class
		{
		  querySelector.ThrowIfNull("queryDescriptor");
			var descriptor = new QueryPathDescriptor<T>();
			querySelector(descriptor);
			var query = ElasticClient.Serialize(descriptor);
			var index = this.Settings.DefaultIndex;
			if (descriptor._Indices.HasAny())
				index = descriptor._Indices.First();
			var path = "_percolator/{0}/{1}".F(index, name);
			return this._RegisterPercolator(path, query);
		}
		[Obsolete("Passing a query by string? Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public RegisterPercolateResponse RegisterPercolator(string index, string name, string query)
		{
			var path = "_percolator/{0}/{1}".F(index, name);
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
		public UnregisterPercolateResponse UnregisterPercolator<T>(string name) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.UnregisterPercolator(index, name);
		}
		/// <summary>
		/// Unregister a percolator
		/// </summary>
		/// <param name="name">Name of the percolator</param>
		public UnregisterPercolateResponse UnregisterPercolator(string index, string name)
		{
			var path = "_percolator/{0}/{1}".F(index, name);
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
		public PercolateResponse Percolate<T>(T @object) where T : class
		{
			var index = this.Settings.DefaultIndex;
			var type = this.InferTypeName<T>();
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);

			return this.Percolate(index, type,"{{doc:{0}}}".F(doc));
		}
		/// <summary>
		/// Manually percolate an object using its inferred typename and the specified index
		/// </summary>
		public PercolateResponse Percolate<T>(string index, T @object) where T : class
		{
			var type = this.InferTypeName<T>();
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);

			return this.Percolate(index, type, "{{doc:{0}}}".F(doc));
		}
		/// <summary>
		/// Manually percolate an object using the specified typename and the default index
		/// </summary>
		public PercolateResponse Percolate<T>(string index, string type, T @object) where T : class
		{
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
			return this.Percolate(index, type, "{{doc:{0}}}".F(doc));
		}
		private PercolateResponse Percolate(string index, string type, string doc)
		{
			var path = this.CreatePath(index, type) + "_percolate";
			var status = this.Connection.PostSync(path, doc);
			var r = this.ToParsedResponse<PercolateResponse>(status);
			return r;
		}
	}
}
