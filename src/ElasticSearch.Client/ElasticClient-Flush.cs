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
		public IndicesOperationResponse Flush<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Flush(index);
		}
		public IndicesOperationResponse Flush<T>(bool refresh) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Flush(index, refresh);
		}
		public IndicesOperationResponse Flush(string index)
		{
			index.ThrowIfNull("index");
			return this.Flush(new[] { index });
		}
		public IndicesOperationResponse Flush()
		{
			return this.Flush("_all");
		}
		public IndicesOperationResponse Flush(bool refresh)
		{
			return this.Flush("_all", refresh);
		}
		public IndicesOperationResponse Flush(string index, bool refresh)
		{
			index.ThrowIfNull("index");
			return this.Flush(new[] { index }, refresh);
		}
		public IndicesOperationResponse Flush(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_flush";
			return this._Flush(path);
		}
		public IndicesOperationResponse Flush(IEnumerable<string> indices, bool refresh)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_flush?refresh=" + refresh.ToString().ToLower();
			return this._Flush(path);
		}
		
		private IndicesOperationResponse _Flush(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}

	}
}
