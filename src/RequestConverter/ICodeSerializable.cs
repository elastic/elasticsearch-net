using System.Text;

namespace RequestConverter;

internal interface ICodeSerializable
{
	public void SerializeToCode(StringBuilder build, int indent);
}

internal interface ICustomCodeSerializable
{
	public void SerializeToCode(StringBuilder build, int indent);
}
