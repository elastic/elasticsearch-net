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
		public IndecesOperationResponse OpenIndex(string index)
		{
			string path = this.CreatePath(index) + "_open";
			return this._OpenClose(path);
		}
		public IndecesOperationResponse CloseIndex(string index)
		{
			string path = this.CreatePath(index) + "_close";
			return this._OpenClose(path);
		}
		public IndecesOperationResponse OpenIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return OpenIndex(index);
		}
		public IndecesOperationResponse CloseIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return CloseIndex(index);
		}
		private IndecesOperationResponse _OpenClose(string path)
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
