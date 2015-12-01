using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IScriptSort : ISort
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; set; }

		[JsonProperty(PropertyName = "inline")]
		string Inline { get; set; }

		[JsonProperty(PropertyName = "id")]
		Id Id { get; set; }

		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Language { get; set; }

	}

	public class ScriptSort : SortBase, IScriptSort
	{
		protected override Field SortKey => "_script";

		public string Type { get; set; }
		public string Inline { get; set; }
		public Id Id { get; set; }
		public string File { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public string Language { get; set; }
	}

	public class SortScriptDescriptor<T> : SortDescriptorBase<SortScriptDescriptor<T>, IScriptSort, T>, IScriptSort 
		where T : class
	{
		protected override Field SortKey => "_script";

		string IScriptSort.Type { get; set; }

		string IScriptSort.Inline { get; set; }
		Id IScriptSort.Id { get; set; }
		string IScriptSort.File { get; set; }
		string IScriptSort.Language { get; set; }
		Dictionary<string, object> IScriptSort.Params { get; set; }

		public SortScriptDescriptor<T> Inline(string script) => Assign(a => a.Inline = script);
		public SortScriptDescriptor<T> Id(Id id) => Assign(a => a.Id = id);
		public SortScriptDescriptor<T> File(string file) => Assign(a => a.File = file);

		public SortScriptDescriptor<T> Params(Dictionary<string, object> scriptParams) => Assign(a => a.Params = scriptParams);

		public SortScriptDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));

		public virtual SortScriptDescriptor<T> MissingLast() => Assign(a => a.Missing = "_last");

		public virtual SortScriptDescriptor<T> MissingFirst() => Assign(a => a.Missing = "_first");

		public virtual SortScriptDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		/// <summary>
		/// Value to sort on when the orginal value for the field is missing
		/// </summary>
		public virtual SortScriptDescriptor<T> MissingValue(string value) => Assign(a => a.Missing = value);

		public SortScriptDescriptor<T> Language(string language) => Assign(a => a.Language = language);

	}
}
