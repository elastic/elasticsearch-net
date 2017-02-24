namespace Nest_5_2_0
{
	public static class SuffixExtensions
	{
		/// <summary>
		/// This extension method should only be used in expressions which are analysed by Nest_5_2_0.
		/// When analysed it will append <paramref name="suffix"/> to the path separating it with a dot.
		/// This is especially useful with multi fields.
		/// </summary>
		public static object Suffix(this object @object, string suffix)
		{
			return @object;
		}
	}
}
