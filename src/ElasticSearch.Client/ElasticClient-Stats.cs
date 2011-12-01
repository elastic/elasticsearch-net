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
		private string _BuildStatsUrl(StatsParams parameters)
		{
			var path = "_stats";
			if (parameters == null)
				return path;

			var info = parameters.InfoOn;
			if (info != StatsInfo.None)
			{
				path += "?clear=true";
				var isAll = (info & StatsInfo.All) == StatsInfo.All;
				var options = new List<string>();
				if (isAll || (info & StatsInfo.Docs) == StatsInfo.Docs)
				{
					options.Add("docs=true");
				}
				if (isAll || (info & StatsInfo.Store) == StatsInfo.Store)
				{
					options.Add("store=true");
				}
				if (isAll || (info & StatsInfo.Indexing) == StatsInfo.Indexing)
				{
					options.Add("indexing=true");
				}
				if (isAll || (info & StatsInfo.Get) == StatsInfo.Get)
				{
					options.Add("get=true");
				}
				if (isAll || (info & StatsInfo.Search) == StatsInfo.Search)
				{
					options.Add("search=true");
				}
				if (isAll || (info & StatsInfo.Merge) == StatsInfo.Merge)
				{
					options.Add("merge=true");
				}
				if (isAll || (info & StatsInfo.Flush) == StatsInfo.Flush)
				{
					options.Add("flush=true");
				}
				path += "&" + string.Join("&", options);
			}
			if (parameters.Refresh)
				path += "&refresh=true" ;
			if (parameters.Types != null && parameters.Types.Any())
				path += "&types=" + string.Join(",", parameters.Types);
			if (parameters.Groups != null && parameters.Groups.Any())
				path += "&groups=" + string.Join(",", parameters.Groups);
			return path;
		}

		public GlobalStatsResponse Stats()
		{
			return this.Stats(new StatsParams());
		}

		public GlobalStatsResponse Stats(StatsParams parameters)
		{
			var status = this.Connection.GetSync(this._BuildStatsUrl(parameters));
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
		
		public StatsResponse Stats(string index)
		{
			@index.ThrowIfNull("index");
			return this.Stats(index,new StatsParams());
		}

		public StatsResponse Stats(string index, StatsParams parameters)
		{
			index.ThrowIfNull("index");
			var path = this.CreatePath(index) + this._BuildStatsUrl(parameters);
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
