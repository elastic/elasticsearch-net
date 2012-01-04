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
using Nest.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
		public RegisterPercolateResponse RegisterPercolator<T>(string name, string query) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.RegisterPercolator(index, name, query);
		}
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
		public UnregisterPercolateResponse UnregisterPercolator<T>(string name) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.UnregisterPercolator(index, name);
		}
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

		public PercolateResponse Percolate<T>(T @object) where T : class
		{
			var index = this.Settings.DefaultIndex;
			var type = this.InferTypeName<T>();
			var doc = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);

			return this.Percolate(index, type,"{{doc:{0}}}".F(doc));
		}

		public PercolateResponse Percolate(string index, string type, string doc)
		{
			var path = this.CreatePath(index, type) + "_percolate";
			var status = this.Connection.PostSync(path, doc);
			var r = this.ToParsedResponse<PercolateResponse>(status);
			return r;
		}
		public PercolateResponse Percolate(string index, string type, string doc, string query)
		{
			var path = this.CreatePath(index, type) + "_percolate";
			var status = this.Connection.PostSync(path, doc);
			var r = this.ToParsedResponse<PercolateResponse>(status);
			return r;
		}
	}
}
