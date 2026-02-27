using System.Text;

namespace RequestConverter;

public interface ICodeFormattable
{
	internal void FormatCode(StringBuilder builder);
}
