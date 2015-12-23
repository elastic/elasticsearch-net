using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PhraseSuggestCollate>))]
	public interface IPhraseSuggestCollate
	{
		[JsonProperty(PropertyName = "query")]
		IScript Query { get; set; }

		[JsonProperty(PropertyName = "prune")]
		bool? Prune { get; set; }
	}

	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		public IScript Query { get; set; }


		public bool? Prune { get; set; }
	}

	public class PhraseSuggestCollateDescriptor<T> : DescriptorBase<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate>, IPhraseSuggestCollate
		where T : class
	{
		IScript IPhraseSuggestCollate.Query { get; set; }

		bool? IPhraseSuggestCollate.Prune { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(string script) => Assign(a => a.Query = (InlineScript)script);

		public PhraseSuggestCollateDescriptor<T> Query(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Query = scriptSelector?.Invoke(new ScriptDescriptor()));

		public PhraseSuggestCollateDescriptor<T> Prune(bool? prune = true) => Assign(a => a.Prune = prune);
	}
}
