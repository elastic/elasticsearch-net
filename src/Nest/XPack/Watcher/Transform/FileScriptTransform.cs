using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFileScriptTransform :
		IScriptTransform
	{
		[JsonProperty("file")]
		string File { get; set; }
	}

	public class FileScriptTransform : ScriptTransformBase, IFileScriptTransform
	{
		public FileScriptTransform(string file)
		{
			this.File = file;
		}

		public string File { get; set; }
	}

	public class FileScriptTransformDescriptor
		: ScriptTransformDescriptorBase<FileScriptTransformDescriptor, IFileScriptTransform>, IFileScriptTransform
	{
		public FileScriptTransformDescriptor(string file)
		{
			Self.File = file;
		}

		public FileScriptTransformDescriptor() {}

		string IFileScriptTransform.File { get; set; }
	}
}
