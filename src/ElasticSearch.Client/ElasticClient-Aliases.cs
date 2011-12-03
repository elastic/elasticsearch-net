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
		private readonly string _aliasBody = @"{{""actions"" : [{0}] }}";
		private string _createCommand(string command, AliasParams aliasParam)
		{
			var cmd  = @"{{ ""{0}"" : {{
				index: ""{1}"",
				alias: ""{2}""".F(command, aliasParam.Index, aliasParam.Alias);

			if (!aliasParam.Filter.IsNullOrEmpty())
				cmd += @", ""filter"": {0} ".F(aliasParam.Filter);

			if (!aliasParam.Routing.IsNullOrEmpty())
				cmd += @", ""routing"": ""{0}"" ".F(aliasParam.Routing);
			else
			{
				if (!aliasParam.IndexRouting.IsNullOrEmpty())
					cmd += @", ""index_routing"": ""{0}"" ".F(aliasParam.IndexRouting);
				if (!aliasParam.SearchRouting.IsNullOrEmpty())
					cmd += @", ""search_routing"": ""{0}"" ".F(aliasParam.SearchRouting);
			}
			cmd += "} }";

			return cmd;
		}
		public IndicesOperationResponse Alias(string alias)
		{
			var index = this.Settings.DefaultIndex;
			var q = _createCommand("add", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndicesOperationResponse Alias(string index, string alias)
		{
			var q = _createCommand("add", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndicesOperationResponse Alias(string index, IEnumerable<string> aliases)
		{
			aliases.Select(a=> _createCommand("add", new AliasParams { Index = index, Alias = a }));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		public IndicesOperationResponse Alias(IEnumerable<string> aliases)
		{
			var index = this.Settings.DefaultIndex;
			aliases.Select(a=> _createCommand("add", new AliasParams { Index = index, Alias = a }));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		public IndicesOperationResponse RemoveAlias(string alias)
		{
			var index = this.Settings.DefaultIndex;
			var q = _createCommand("remove", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndicesOperationResponse RemoveAlias(string index, string alias)
		{
			var q = _createCommand("remove", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndicesOperationResponse RemoveAlias(IEnumerable<string> aliases)
		{
			var index = this.Settings.DefaultIndex;
			aliases.Select(a => _createCommand("remove", new AliasParams { Index = index, Alias = a }));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		public IndicesOperationResponse RemoveAlias(string index, IEnumerable<string> aliases)
		{
			aliases.Select(a => _createCommand("remove", new AliasParams { Index = index, Alias = a }));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		public IndicesOperationResponse Alias(IEnumerable<string> indices, string alias)
		{
			indices.Select(i => _createCommand("add", new AliasParams { Index = i, Alias = alias }));
			var q = string.Join(",", indices);
			return this._Alias(q);
		}
		public IndicesOperationResponse Rename(string index, string oldAlias, string newAlias)
		{
			var r = _createCommand("remove", new AliasParams { Index = index, Alias = oldAlias });
			var a = _createCommand("add", new AliasParams { Index = index, Alias = newAlias });
			return this._Alias(r + ", " + a);
		}
		public IndicesOperationResponse Alias(AliasParams aliasParams)
		{
			return this._Alias(_createCommand("add", aliasParams));
		}
		public IndicesOperationResponse Alias(IEnumerable<AliasParams> aliases)
		{
			var cmds = aliases.Select(a=>_aliasBody.F(_createCommand("add", a)));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		public IndicesOperationResponse RemoveAlias(AliasParams aliasParams)
		{
			return this._Alias(_createCommand("remove", aliasParams));
		}
		public IndicesOperationResponse RemoveAliases(IEnumerable<AliasParams> aliases)
		{
			var cmds = aliases.Select(a => _aliasBody.F(_createCommand("remove", a)));
			var q = string.Join(",", aliases);
			return this._Alias(q);
		}
		private IndicesOperationResponse _Alias(string query)
		{
			var path = "/_aliases";
			query = _aliasBody.F(query);
			var status = this.Connection.PostSync(path, query);			
			
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}
	}
}
