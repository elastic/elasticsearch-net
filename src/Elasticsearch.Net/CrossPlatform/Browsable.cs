#if DOTNETCORE
namespace System.ComponentModel
{
	internal class Browsable : Attribute
	{
		public Browsable(bool browsable) { }
	}
}
#endif
