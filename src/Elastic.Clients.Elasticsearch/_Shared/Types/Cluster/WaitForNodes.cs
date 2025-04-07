using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Cluster;

public enum WaitForNodesCondition
{
	EqualTo,
	LessThan,
	LessThanOrEqualTo,
	GreaterThan,
	GreaterThanOrEqualTo
}

public readonly struct WaitForNodes :
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<WaitForNodes>
#endif
{
	public WaitForNodesCondition Condition { get; }
	public int Nodes { get; }

	public WaitForNodes(int nodes)
	{
		Condition = WaitForNodesCondition.EqualTo;
		Nodes = nodes;
	}

	public WaitForNodes(WaitForNodesCondition condition, int nodes)
	{
		Condition = condition;
		Nodes = nodes;
	}

	public static implicit operator WaitForNodes(int nodes)
	{
		return new(nodes);
	}

	public static WaitForNodes EqualTo(int nodes)
	{
		return new(nodes);
	}

	public static WaitForNodes GreaterThan(int nodes)
	{
		return new(WaitForNodesCondition.GreaterThan, nodes);
	}

	public static WaitForNodes GreaterThanOrEqualTo(int nodes)
	{
		return new(WaitForNodesCondition.GreaterThanOrEqualTo, nodes);
	}

	public static WaitForNodes LessThan(int nodes)
	{
		return new(WaitForNodesCondition.LessThan, nodes);
	}

	public static WaitForNodes LessThanOrEqualTo(int nodes)
	{
		return new(WaitForNodesCondition.LessThanOrEqualTo, nodes);
	}

	#region IUrlParameter

	public string GetString(ITransportConfiguration settings)
	{
		var number = Nodes.ToString(CultureInfo.InvariantCulture);

		return Condition switch
		{
			WaitForNodesCondition.EqualTo => number,
			WaitForNodesCondition.LessThan => $"<{number}",
			WaitForNodesCondition.LessThanOrEqualTo => $"<={number}",
			WaitForNodesCondition.GreaterThan => $">{number}",
			WaitForNodesCondition.GreaterThanOrEqualTo => $">={number}",
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	#endregion IUrlParameter

	#region IParsable

	public static WaitForNodes Parse(string s, IFormatProvider? provider) => throw new NotImplementedException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out WaitForNodes result)
	{
		if (string.IsNullOrEmpty(s))
		{
			result = default;
			return false;
		}

		WaitForNodes? parsed = s switch
		{
			['<', '=', ..] when TryParseInt(s[2..], out var nodes) => LessThanOrEqualTo(nodes),
			['<', ..] when TryParseInt(s[1..], out var nodes) => LessThan(nodes),
			['>', '=', ..] when TryParseInt(s[2..], out var nodes) => GreaterThanOrEqualTo(nodes),
			['>', ..] when TryParseInt(s[1..], out var nodes) => GreaterThan(nodes),
			['l', 'e', '(', .., ')'] when TryParseInt(s[3..^1], out var nodes) => LessThanOrEqualTo(nodes),
			['l', 't', '(', .., ')'] when TryParseInt(s[3..^1], out var nodes) => LessThan(nodes),
			['g', 'e', '(', .., ')'] when TryParseInt(s[3..^1], out var nodes) => GreaterThanOrEqualTo(nodes),
			['g', 't', '(', .., ')'] when TryParseInt(s[3..^1], out var nodes) => GreaterThan(nodes),
			[..] when TryParseInt(s, out var nodes) => EqualTo(nodes),
			_ => null
		};

		if (parsed is null)
		{
			result = default;
			return false;
		}

		result = parsed.Value;
		return true;

		static bool TryParseInt(string s, out int result)
		{
			return int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}
	}

	#endregion IParsable
}
