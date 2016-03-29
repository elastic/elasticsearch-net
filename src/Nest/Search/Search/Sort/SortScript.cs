using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IScriptSort : ISort
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; set; }

		[JsonProperty(PropertyName = "script")]
		IScript Script { get; set; }

	}

	public class ScriptSort : SortBase, IScriptSort
	{
		protected override Field SortKey => "_script";

		public string Type { get; set; }
		public IScript Script { get; set; }
		public string Language { get; set; }
	}

	public class SortScriptDescriptor<T> : SortDescriptorBase<SortScriptDescriptor<T>, IScriptSort, T>, IScriptSort 
		where T : class
	{
		protected override Field SortKey => "_script";

		string IScriptSort.Type { get; set; }

		IScript IScriptSort.Script { get; set; }

		public virtual SortScriptDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		public SortScriptDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

	}
}
