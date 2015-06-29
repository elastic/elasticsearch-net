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
		public string Name { get; set; }
		public bool Conditionless
		{
			get { return this.Query.IsNullOrEmpty(); }
		}
		public string Query { get; set; }
		public IDictionary<string, object> Params { get; set;}

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Template = this;
		}
	}

	public class TemplateQueryDescriptor<T> : ITemplateQuery where T : class
	{
		ITemplateQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless { get { return this.Self.Query.IsNullOrEmpty(); } }
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
