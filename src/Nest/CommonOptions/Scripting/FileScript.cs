using System;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Scheduled to be removed in 6.0")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFileScript : IScript
	{
		[JsonProperty("file")]
		string File { get; set; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FileScript : ScriptBase, IFileScript
	{
		public FileScript(string file)
		{
			this.File = file;
		}

		public string File { get; set; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FileScriptDescriptor
		: ScriptDescriptorBase<FileScriptDescriptor, IFileScript>, IFileScript
	{
		public FileScriptDescriptor() {}

		public FileScriptDescriptor(string file)
		{
			Self.File = file;
		}

		string IFileScript.File { get; set; }

		public FileScriptDescriptor File(string file) => Assign(a => a.File = file);
	}
}
