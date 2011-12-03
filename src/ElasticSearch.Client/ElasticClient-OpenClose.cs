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
		public IndicesOperationResponse OpenIndex(string index)
		{
			string path = this.CreatePath(index) + "_open";
			return this._OpenClose(path);
		}
		public IndicesOperationResponse CloseIndex(string index)
		{
			string path = this.CreatePath(index) + "_close";
			return this._OpenClose(path);
		}
		public IndicesOperationResponse OpenIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return OpenIndex(index);
		}
		public IndicesOperationResponse CloseIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return CloseIndex(index);
		}
		private IndicesOperationResponse _OpenClose(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}

	}
}
