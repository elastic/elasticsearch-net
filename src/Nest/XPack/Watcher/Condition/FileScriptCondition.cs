using System;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Scheduled to be removed in 6.0")]
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFileScriptCondition : IScriptCondition
	{
		[JsonProperty("file")]
		string File { get; set; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FileScriptCondition : ScriptConditionBase, IFileScriptCondition
	{
		public FileScriptCondition(string file)
		{
			this.File = file;
		}

		public string File { get; set; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FileScriptConditionDescriptor
		: ScriptConditionDescriptorBase<FileScriptConditionDescriptor, IFileScriptCondition>, IFileScriptCondition
	{
		public FileScriptConditionDescriptor(string file)
		{
			Self.File = file;
		}

		string IFileScriptCondition.File { get; set; }
	}
}
