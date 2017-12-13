using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<StoredScript>))]
	public interface IStoredScript
	{
		[JsonProperty("source")]
		string Source { get; set; }
	}
	public class StoredScript : IStoredScript
	{
		[JsonProperty("source")]
		string IStoredScript.Source { get; set; }

		//used for deserialization
		internal StoredScript() { }

		protected StoredScript(string source)
		{
			((IStoredScript) this).Source = source;
		}
	}

	public class PainlessScript : StoredScript
	{
		public PainlessScript(string source) : base(source) { }
	}

	public class StoredScriptDescriptor : DescriptorBase<StoredScriptDescriptor, IStoredScript>, IStoredScript
	{
		string IStoredScript.Source { get; set; }

		public StoredScriptDescriptor Source(string source) => Assign(a => a.Source = source);
	}
}
