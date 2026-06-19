namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Request-converter formatting helpers for <see cref="Union{T1,T2}"/> values. Lives in the
/// request-converter compilation only (it depends on <see cref="RequestConverter.CodeWriter"/>), so it
/// never ships in the client package.
/// </summary>
public static class UnionExtensions
{
	/// <summary>
	/// Writes the C# representation of a <see cref="Union{T1,T2}"/> value. The active member is emitted
	/// directly; the union's implicit conversion operators reconstruct it at the assignment site.
	/// </summary>
	public static void FormatCode<T1, T2>(Union<T1, T2> union, RequestConverter.CodeWriter writer)
	{
		if (union is null)
		{
			writer.Write("null");
			return;
		}

		switch (union.Tag)
		{
			case UnionTag.T1:
				writer.WriteValue(union.Value1);
				break;
			case UnionTag.T2:
				writer.WriteValue(union.Value2);
				break;
			default:
				writer.Write("null");
				break;
		}
	}
}
