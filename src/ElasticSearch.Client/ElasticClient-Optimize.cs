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
using ElasticSearch.Client.Domain;

namespace ElasticSearch.Client
{
	public partial class ElasticClient
	{
		public IndecesOperationResponse Optimize<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index);
		}
		public IndecesOperationResponse Optimize<T>(OptimizeParams optimizeParameters) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index, optimizeParameters);
		}
		public IndecesOperationResponse Optimize(string index)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index });
		}
		public IndecesOperationResponse Optimize(string index, OptimizeParams optimizeParameters)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index }, optimizeParameters);
		}
		public IndecesOperationResponse Optimize()
		{
			return this.Optimize("_all");
		}
		public IndecesOperationResponse Optimize(OptimizeParams optimizeParameters)
		{
			return this.Optimize("_all", optimizeParameters);
		}
		public IndecesOperationResponse Optimize(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_optimize";
			return this._Optimize(path, null);
		}
		public IndecesOperationResponse Optimize(IEnumerable<string> indices, OptimizeParams optimizeParameters)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_optimize";
			return this._Optimize(path, optimizeParameters);
		}

		private IndecesOperationResponse _Optimize(string path, OptimizeParams optimizeParameters)
		{
			if (optimizeParameters != null)
			{
				path += "?max_num_segments=" + optimizeParameters.MaximumSegments;
				path += "&only_expunge_deletes=" + optimizeParameters.OnlyExpungeDeletes.ToString().ToLower();
				path += "&refresh=" + optimizeParameters.Refresh.ToString().ToLower();
				path += "&flush=" + optimizeParameters.Flush.ToString().ToLower();
				path += "&wait_for_merge=" + optimizeParameters.WaitForMerge.ToString().ToLower();
			}
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
