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
using Nest.Domain;

namespace Nest
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
			var r = this.ToParsedResponse<GlobalStatsResponse>(status);
			return r;
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
			var r = this.ToParsedResponse<StatsResponse>(status);
			return r;
		}
		
	}
}
