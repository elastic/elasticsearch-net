using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFileScript : IScript
	{
		[JsonProperty("file")]
		string File { get; set; }
	}

	public class FileScript : ScriptBase, IFileScript
	{
		public FileScript(string file)
		{
			this.File = file;
		}

		public string File { get; set; }
	}

	public class FileScriptDescriptor
		: ScriptDescriptorBase<FileScriptDescriptor, IFileScript>, IFileScript
	{
		string IFileScript.File { get; set; }

		public FileScriptDescriptor File(string file) => Assign(a => a.File = file);
	}
}
