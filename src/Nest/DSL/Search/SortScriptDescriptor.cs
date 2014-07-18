using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.DSL.Descriptors
{
	public interface IScriptSort : ISort
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; set; }

		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}

	public class ScriptSort : IScriptSort
	{
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public string Type { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class SortScriptDescriptor<T> : IScriptSort
	{
		public IScriptSort Self { get { return this; } }

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

		SortMode? ISort.Mode { get; set; }

		string IScriptSort.Type { get; set; }

		string IScriptSort.Script { get; set; }

		Dictionary<string, object> IScriptSort.Params { get; set; }

		public SortScriptDescriptor<T> Script(string script)
		{
			script.ThrowIfNull("script");
			Self.Script = script;
			return this;
		}

		public SortScriptDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public virtual SortScriptDescriptor<T> MissingLast()
		{
			Self.Missing = "_last";
			return this;
		}
		public virtual SortScriptDescriptor<T> MissingFirst()
		{
			Self.Missing = "_first";
			return this;
		}
		public virtual SortScriptDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Value to sort on when the orginal value for the field is missing
		/// </summary>
		public virtual SortScriptDescriptor<T> MissingValue(string value)
		{
			Self.Missing = value;
			return this;
		}

		public virtual SortScriptDescriptor<T> Ascending()
		{
			Self.Order = SortOrder.Ascending;
			return this;
		}

		public virtual SortScriptDescriptor<T> Descending()
		{
			Self.Order = SortOrder.Descending;
			return this;
		}

		public SortScriptDescriptor<T> Order(SortOrder order)
		{
			Self.Order = order;
			return this;
		}

		public SortScriptDescriptor<T> Mode(SortMode mode)
		{
			Self.Mode = mode;
			return this;
		}
	}
}
