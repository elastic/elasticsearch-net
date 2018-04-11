using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IMigrationAssistanceResponse : IResponse
	{
		[JsonProperty("indices")]
		IReadOnlyDictionary<IndexName, IndexUpgradeCheck> Indices { get; }
	}

	public class MigrationAssistanceResponse : ResponseBase, IMigrationAssistanceResponse
	{
		public IReadOnlyDictionary<IndexName, IndexUpgradeCheck> Indices { get; internal set; } =
			EmptyReadOnly<IndexName, IndexUpgradeCheck>.Dictionary;
	}

	public class IndexUpgradeCheck
	{
		[JsonProperty("action_required")]
		public UpgradeActionRequired ActionRequired { get; internal set;  }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum UpgradeActionRequired
	{
		[EnumMember(Value = "not_applicable")]
		NotApplicable,

		[EnumMember(Value = "up_to_date")]
		UpToDate,

		[EnumMember(Value = "reindex")]
		Reindex,

		[EnumMember(Value = "upgrade")]
		Upgrade
	}
}
