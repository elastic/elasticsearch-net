namespace Elastic.Clients.Elasticsearch
{
	//[DebuggerDisplay("{DebugDisplay,nq}")]
	//[JsonConverter(typeof(StringAliasConverter<Name>))]
	//public class Name : IEquatable<Name>, IUrlParameter
	//{
	//	public Name(string name) => Value = name?.Trim();

	//	internal string Value { get; }

	//	private string DebugDisplay => Value;

	//	private static int TypeHashCode { get; } = typeof(Name).GetHashCode();

	//	public bool Equals(Name other) => EqualsString(other?.Value);

	//	string IUrlParameter.GetString(ITransportConfiguration settings) => Value;

	//	public override string ToString() => DebugDisplay;

	//	public static implicit operator Name(string name) => name.IsNullOrEmpty() ? null : new Name(name);

	//	public static bool operator ==(Name left, Name right) => Equals(left, right);

	//	public static bool operator !=(Name left, Name right) => !Equals(left, right);

	//	public override bool Equals(object obj) =>
	//		obj is string s ? EqualsString(s) : obj is Name i && EqualsString(i.Value);

	//	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Trim() == Value;

	//	public override int GetHashCode()
	//	{
	//		unchecked
	//		{
	//			var result = TypeHashCode;
	//			result = (result * 397) ^ (Value?.GetHashCode() ?? 0);
	//			return result;
	//		}
	//	}
	//}
}
