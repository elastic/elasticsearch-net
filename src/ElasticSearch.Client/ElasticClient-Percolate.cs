using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ElasticSearch.Client
{
	public partial class ElasticClient
	{
		public RegisterPercolateResponse RegisterPercolate<T>(string name, string query) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.RegisterPercolate(index, name, query);
		}
		public RegisterPercolateResponse RegisterPercolate(string index, string name, string query)
		{
			var path = "_percolator/{0}/{1}".F(index, name);
			return this._RegisterPercolate(path, query);
		}
		private RegisterPercolateResponse _RegisterPercolate(string path, string query)
		{
			var status = this.Connection.PutSync(path, query);
			var r = this.ToParsedResponse<RegisterPercolateResponse>(status);
			return r;
		}
		public UnregisterPercolateResponse UnregisterPercolate<T>(string name) where T : class
		{
			var index = this.Settings.DefaultIndex;
			return this.UnregisterPercolate(index, name);
		}
		public UnregisterPercolateResponse UnregisterPercolate(string index, string name)
		{
			var path = "_percolator/{0}/{1}".F(index, name);
			return this._UnregisterPercolate(path);
		}
		private UnregisterPercolateResponse _UnregisterPercolate(string path)
		{
			var status = this.Connection.DeleteSync(path);
			var r = this.ToParsedResponse<UnregisterPercolateResponse>(status, allow404: true);
			return r;
		}

	}
}
