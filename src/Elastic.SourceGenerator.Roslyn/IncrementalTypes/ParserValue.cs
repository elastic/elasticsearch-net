using System;
using System.Linq;

using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn.IncrementalTypes;

public sealed record ParserValue<T>
	where T : IEquatable<T>
{
	public T? Value { get; init; }
	public ImmutableEquatableArray<EquatableDiagnostic> Diagnostics { get; init; } = [];

	public bool HasValue => (Value is not null);
	public bool HasErrors => Diagnostics.Any(x => x.Descriptor.DefaultSeverity is DiagnosticSeverity.Error);

	public static implicit operator ParserValue<T>(T value) => FromValue(value);
	public static implicit operator ParserValue<T>(EquatableDiagnostic diagnostic) => FromDiagnostic(diagnostic);
	public static implicit operator ParserValue<T>(ImmutableEquatableArray<EquatableDiagnostic> diagnostics) => FromDiagnostics(diagnostics);

	public static ParserValue<T> Create(T value, ImmutableEquatableArray<EquatableDiagnostic>? diagnostics = null)
	{
		if (diagnostics?.Length is > 0)
		{
			return new() { Value = value, Diagnostics = diagnostics };
		}

		return new() { Value = value };
	}

	public static ParserValue<T> FromValue(T value)
	{
		return new() { Value = value };
	}

	private static ParserValue<T> FromDiagnostic(EquatableDiagnostic diagnostic)
	{
		return new() { Diagnostics = [diagnostic] };
	}

	private static ParserValue<T> FromDiagnostics(ImmutableEquatableArray<EquatableDiagnostic> diagnostics)
	{
		return new() { Diagnostics = diagnostics };
	}
}
