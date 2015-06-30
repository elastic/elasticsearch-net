using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITemplateQuery : IQuery
	{
		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; } 
	}

	public class TemplateQuery : PlainQuery, ITemplateQuery
	{
		public bool Conditionless => IsConditionless(this);
		public string Query { get; set; }
		public IDictionary<string, object> Params { get; set;}

		protected override void WrapInContainer(IQueryContainer c) => c.Template = this;
		internal static bool IsConditionless(ITemplateQuery q) => q.Query.IsNullOrEmpty();
	}

	public class TemplateQueryDescriptor<T> : ITemplateQuery where T : class
	{
		ITemplateQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => TemplateQuery.IsConditionless(this);
		string ITemplateQuery.Query { get; set; }
		IDictionary<string, object> ITemplateQuery.Params { get; set; }

		public TemplateQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public TemplateQueryDescriptor<T> Query(string query)
		{
			this.Self.Query = query;
			return this;
		}

		public TemplateQueryDescriptor<T> Params(IDictionary<string, object> paramsDictionary)
		{
			paramsDictionary.ThrowIfNull("paramsDictionary");
			this.Self.Params = paramsDictionary;
			return this;
		}

		public TemplateQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary)
		{
			paramsDictionary.ThrowIfNull("paramsDictionary");
			this.Self.Params = paramsDictionary(new FluentDictionary<string,object>());
			return this;
		}
	}
}
