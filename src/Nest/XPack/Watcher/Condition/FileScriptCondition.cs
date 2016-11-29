using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFileScriptCondition : IScriptCondition
	{
		[JsonProperty("file")]
		string File { get; set; }
	}

	public class FileScriptCondition : ScriptConditionBase, IFileScriptCondition
	{
		public FileScriptCondition(string file)
		{
			this.File = file;
		}

		public string File { get; set; }
	}

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
