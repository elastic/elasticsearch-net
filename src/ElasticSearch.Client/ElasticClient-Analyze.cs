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
	/*
		public IndecesOperationResponse Analyze(string text)
		{
			var index = this.Settings.DefaultIndex;
			var q = _createCommand("add", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndecesOperationResponse Analyze(string analyzer, string text)
		{
			var index = this.Settings.DefaultIndex;
			var q = _createCommand("add", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		public IndecesOperationResponse Analyze(string index, string analyzer, string text)
		{
			var index = this.Settings.DefaultIndex;
			var q = _createCommand("add", new AliasParams { Index = index, Alias = alias });
			return this._Alias(q);
		}
		private IndecesOperationResponse _Analyze(string query)
		{
			var path = "/_aliases";
			query = _aliasBody.F(query);
			var status = this.Connection.PostSync(path, query);
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
		}*/
	}
}
