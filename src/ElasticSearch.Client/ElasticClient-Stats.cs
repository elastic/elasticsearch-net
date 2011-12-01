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
		/// <summary>
		///  refreshes all
		/// </summary>
		/// <returns></returns>
		public GlobalStatsResponse Stats()
		{
			var status = this.Connection.GetSync("_stats");
			if (status.Error != null)
			{
				return new GlobalStatsResponse()
				{
					IsValid = false,
					ConnectionError = status.Error
				};
			}

			var response = JsonConvert.DeserializeObject<GlobalStatsResponse>(status.Result, this.SerializationSettings);
			return response;
		}
		/// <summary>
		/// Refresh an index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public StatsResponse Stats(string index)
		{
			index.ThrowIfNull("index");
			var path = this.CreatePath(index) + "_stats";
			var status = this.Connection.GetSync(path);
			if (status.Error != null)
			{
				return new StatsResponse()
				{
					IsValid = false,
					ConnectionError = status.Error
				};
			}

			var response = JsonConvert.DeserializeObject<StatsResponse>(status.Result, this.SerializationSettings);
			return response;
		}
		
	}
}
