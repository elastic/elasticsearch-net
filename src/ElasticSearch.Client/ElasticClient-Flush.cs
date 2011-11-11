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
		public IndecesOperationResponse Flush<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Flush(index);
		}
		public IndecesOperationResponse Flush<T>(bool refresh) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Flush(index, refresh);
		}
		public IndecesOperationResponse Flush(string index)
		{
			index.ThrowIfNull("index");
			return this.Flush(new[] { index });
		}
		public IndecesOperationResponse Flush()
		{
			return this.Flush("_all");
		}
		public IndecesOperationResponse Flush(bool refresh)
		{
			return this.Flush("_all", refresh);
		}
		public IndecesOperationResponse Flush(string index, bool refresh)
		{
			index.ThrowIfNull("index");
			return this.Flush(new[] { index }, refresh);
		}
		public IndecesOperationResponse Flush(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_flush";
			return this._Flush(path);
		}
		public IndecesOperationResponse Flush(IEnumerable<string> indices, bool refresh)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_flush?refresh=" + refresh.ToString().ToLower();
			return this._Flush(path);
		}
		
		private IndecesOperationResponse _Flush(string path)
		{
			var status = this.Connection.PostSync(path, "");
			if (status.Error != null)
			{
				return new IndecesOperationResponse()
				{
					IsValid = false,
					ConnectionError = status.Error
				};
			}

			var response = JsonConvert.DeserializeObject<IndecesOperationResponse>(status.Result, this.SerializationSettings);
			return response;
		}

	}
}
