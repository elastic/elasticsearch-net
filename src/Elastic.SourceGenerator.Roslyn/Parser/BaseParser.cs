using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Elastic.SourceGenerator.Roslyn.Helpers;
using Elastic.SourceGenerator.Roslyn.IncrementalTypes;

using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn.Parser;

public abstract class BaseParser
{
	/// <summary>
	/// The context symbol used to determine accessibility for processed types.
	/// </summary>
	protected ISymbol GenerationScope { get; }

	/// <summary>
	/// The default location to be used for diagnostics.
	/// </summary>
	protected virtual Location? DefaultLocation => GenerationScope.Locations.FirstOrDefault();

	/// <summary>
	/// The known symbols cache constructed from the current <see cref="Compilation" />.
	/// </summary>
	protected ElasticsearchKnownSymbols KnownSymbols { get; }

	/// <summary>
	/// The cancellation token to be used by the generator.
	/// </summary>
	protected CancellationToken CancellationToken { get; }

	/// <summary>
	/// The <see cref="SymbolEqualityComparer"/> used to identify type symbols.
	/// Defaults to <see cref="SymbolEqualityComparer.Default"/>.
	/// </summary>
	protected virtual SymbolEqualityComparer SymbolComparer => SymbolEqualityComparer.Default;

	/// <summary>
	/// Creates a new <see cref="BaseParser"/> instance.
	/// </summary>
	/// <param name="generationScope">The context symbol used to determine accessibility for processed types.</param>
	/// <param name="knownSymbols">The known symbols cache constructed from the current <see cref="Compilation"/>.</param>
	/// <param name="cancellationToken">The cancellation token to be used by the generator.</param>
	protected BaseParser(ISymbol generationScope, ElasticsearchKnownSymbols knownSymbols, CancellationToken cancellationToken)
	{
		GenerationScope = generationScope;
		KnownSymbols = knownSymbols;
		CancellationToken = cancellationToken;
	}

	protected EquatableDiagnostic CreateDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs)
	{
		if (location is not null && !KnownSymbols.Compilation.ContainsLocation(location))
		{
			// If the location is outside the current compilation, fall back to the default location for the generator.
			location = DefaultLocation;
		}

		return new EquatableDiagnostic(descriptor, location, messageArgs);
	}

	protected DiagnosticsBuilder CreateDiagnosticsBuilder()
	{
		return new DiagnosticsBuilder(this);
	}

	protected Result<T> CreateOptional<T>()
	{
		return new Result<T>(this);
	}

	protected interface IDiagnosticsBuilder
	{
		public bool HasDiagnostics { get; }

		public void AddDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs);

		public ImmutableEquatableArray<EquatableDiagnostic> ToDiagnostics();
	}

	protected class DiagnosticsBuilder :
		IDiagnosticsBuilder
	{
		private readonly BaseParser _parser;
		private List<EquatableDiagnostic>? _diagnostics;

		public bool HasDiagnostics => (_diagnostics?.Count is > 0);

		internal DiagnosticsBuilder(BaseParser parser)
		{
			_parser = parser;
		}

		public void AddDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs)
		{
			_diagnostics ??= [];
			_diagnostics.Add(_parser.CreateDiagnostic(descriptor, location, messageArgs));
		}

		public ImmutableEquatableArray<EquatableDiagnostic> ToDiagnostics()
		{
			return (_diagnostics?.Count is null or 0)
				? ImmutableEquatableArray<EquatableDiagnostic>.Empty
				: _diagnostics.ToImmutableEquatableArray();
		}
	}

	protected class Result<T> :
		IDiagnosticsBuilder
	{
		private readonly BaseParser _parser;

		public T? Value { get; set; }
		public DiagnosticsBuilder? DiagnosticsBuilder { get; private set; }

		public bool HasValue => (Value is not null);
		public bool HasDiagnostics => (DiagnosticsBuilder?.HasDiagnostics ?? false);

		internal Result(BaseParser parser)
		{
			_parser = parser;
		}

		public void AddDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[] messageArgs)
		{
			DiagnosticsBuilder ??= _parser.CreateDiagnosticsBuilder();
			DiagnosticsBuilder.AddDiagnostic(descriptor, location, messageArgs);
		}

		public ImmutableEquatableArray<EquatableDiagnostic> ToDiagnostics()
		{
			return DiagnosticsBuilder?.ToDiagnostics() ?? [];
		}
	}
}
