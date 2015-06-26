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
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Template = this;
		}

		public string Query { get; set; }

		public IDictionary<string, object> Params { get; set;}

		public string Name { get; set; }

		public bool IsConditionless
		{
			get { return this.Query.IsNullOrEmpty(); }
		}
	}

	public class TemplateQueryDescriptor : ITemplateQuery
	{
		ITemplateQuery Self { get { return this; } }

		string ITemplateQuery.Query { get; set; }

		IDictionary<string, object> ITemplateQuery.Params { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless { get { return this.Self.Query.IsNullOrEmpty(); } }

		public TemplateQueryDescriptor Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public TemplateQueryDescriptor Query(string query)
		{
			this.Self.Query = query;
			return this;
		}

		public TemplateQueryDescriptor Params(IDictionary<string, object> paramsDictionary)
		{
			paramsDictionary.ThrowIfNull("paramsDictionary");
			this.Self.Params = paramsDictionary;
			return this;
		}

		public TemplateQueryDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary)
		{
			paramsDictionary.ThrowIfNull("paramsDictionary");
			this.Self.Params = paramsDictionary(new FluentDictionary<string,object>());
			return this;
		}
	}
}
