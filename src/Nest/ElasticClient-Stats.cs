using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

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
		/// <summary>
		/// Gets all the stats for all the indices
		/// </summary>
		public GlobalStatsResponse Stats()
		{
			return this.Stats(new StatsParams());
		}
		/// <summary>
		/// Gets only the specified stats for all the indices
		/// </summary>
		public GlobalStatsResponse Stats(StatsParams parameters)
		{
			var status = this.Connection.GetSync(this._BuildStatsUrl(parameters));
			var r = this.ToParsedResponse<GlobalStatsResponse>(status);
			return r;
		}

		/// <summary>
		/// Gets all the stats for the specified indices
		/// </summary>
		public StatsResponse Stats(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty("indices");
			return this.Stats(indices, new StatsParams());
		}
		/// <summary>
		/// Gets all the stats for the specified index
		/// </summary>
		public StatsResponse Stats(string index)
		{
			index.ThrowIfNullOrEmpty("index");
			return this.Stats(new []{index}, new StatsParams());
		}
		/// <summary>
		/// Gets the specified stats for the specified indices
		/// </summary>
		public StatsResponse Stats(IEnumerable<string> indices, StatsParams parameters)
		{
			indices.ThrowIfEmpty("indices");
			var path = this.CreatePath(string.Join(",",indices)) + this._BuildStatsUrl(parameters);
			var status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<StatsResponse>(status);
			return r;
		}
		
	}
}
