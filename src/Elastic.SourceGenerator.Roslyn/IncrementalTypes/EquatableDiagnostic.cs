using System;
using System.Linq;

using Elastic.SourceGenerator.Roslyn.Helpers;

using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn.IncrementalTypes;

/// <summary>
/// Descriptor for diagnostic instances using structural equality comparison.
/// Provides a work-around for https://github.com/dotnet/roslyn/issues/68291.
/// </summary>
public readonly struct EquatableDiagnostic(
	DiagnosticDescriptor descriptor,
	Location? location,
	object?[] messageArgs) : IEquatable<EquatableDiagnostic>
{
	/// <summary>
	/// The <see cref="DiagnosticDescriptor"/> for the diagnostic.
	/// </summary>
	public DiagnosticDescriptor Descriptor { get; } = descriptor;

	/// <summary>
	/// The message arguments for the diagnostic.
	/// </summary>
	public object?[] MessageArgs { get; } = messageArgs;

	/// <summary>
	/// The location of the diagnostic.
	/// </summary>
	public Location? Location { get; } = location?.GetLocationTrimmed();

	/// <summary>
	/// Creates a new <see cref="Diagnostic"/> instance from the current instance.
	/// </summary>
	public Diagnostic CreateDiagnostic()
	{
		return Diagnostic.Create(Descriptor, Location, MessageArgs);
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is EquatableDiagnostic info && Equals(info);
	}

	/// <inheritdoc/>
	public bool Equals(EquatableDiagnostic other)
	{
		return Descriptor.Equals(other.Descriptor) &&
			MessageArgs.SequenceEqual(other.MessageArgs) &&
			Location == other.Location;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		var hashCode = Descriptor.GetHashCode();
		foreach (var messageArg in MessageArgs)
		{
			hashCode = CommonHelpers.CombineHashCodes(hashCode, messageArg?.GetHashCode() ?? 0);
		}

		hashCode = CommonHelpers.CombineHashCodes(hashCode, Location?.GetHashCode() ?? 0);

		return hashCode;
	}

	public static bool operator ==(EquatableDiagnostic left, EquatableDiagnostic right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(EquatableDiagnostic left, EquatableDiagnostic right)
	{
		return !left.Equals(right);
	}
}
