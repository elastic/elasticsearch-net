using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TemplateQuery>))]
	public interface ITemplateQuery : IQuery
	{
		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty("inline")]
		string Inline { get; set; }

		[JsonProperty("id")]
		Id Id { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; } 
	}

	public class TemplateQuery : QueryBase, ITemplateQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public string File { get; set; }
		public string Inline { get; set; }
		public Id Id { get; set; }
		public IDictionary<string, object> Params { get; set;}

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Template = this;
		internal static bool IsConditionless(ITemplateQuery q) => q.File.IsNullOrEmpty() && q.Id == null && q.Inline.IsNullOrEmpty();
	}

	public class TemplateQueryDescriptor<T> 
		: QueryDescriptorBase<TemplateQueryDescriptor<T>, ITemplateQuery>
		, ITemplateQuery where T : class
	{
		protected override bool Conditionless => TemplateQuery.IsConditionless(this);
		string ITemplateQuery.File { get; set; }
		string ITemplateQuery.Inline { get; set; }
		Id ITemplateQuery.Id { get; set; }
		IDictionary<string, object> ITemplateQuery.Params { get; set; }

		public TemplateQueryDescriptor<T> Inline(string script) => Assign(a => a.Inline = script);

		public TemplateQueryDescriptor<T> File(string file) => Assign(a => a.File = file);

		public TemplateQueryDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public TemplateQueryDescriptor<T> Params(IDictionary<string, object> paramsDictionary) => Assign(a => a.Params = paramsDictionary);

		public TemplateQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary(new FluentDictionary<string, object>()));
	}
}
