using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn;

/// <summary>
/// Provides a caching layer for common known symbols wrapping a <see cref="Compilation"/> instance.
/// </summary>
/// <param name="compilation">The compilation from which information is being queried.</param>
public class KnownSymbols(Compilation compilation)
{
	/// <summary>
	/// The compilation from which information is being queried.
	/// </summary>
	public Compilation Compilation { get; } = compilation;

	/// <summary>
	/// The assembly symbol for the core library.
	/// </summary>
	public IAssemblySymbol CoreLibAssembly => _coreLibAssembly ??= Compilation.GetSpecialType(SpecialType.System_Int32).ContainingAssembly;

	private IAssemblySymbol? _coreLibAssembly;

	/// <summary>
	/// The type symbol for <see cref="System.Reflection.MemberInfo"/>.
	/// </summary>
	public INamedTypeSymbol? MemberInfoType => GetOrResolveType("System.Reflection.MemberInfo", ref _memberInfoType);

	private Option<INamedTypeSymbol?> _memberInfoType;

	/// <summary>
	/// The type symbol for <see cref="System.Exception"/>.
	/// </summary>
	public INamedTypeSymbol? ExceptionType => GetOrResolveType("System.Exception", ref _exceptionType);

	private Option<INamedTypeSymbol?> _exceptionType;

	/// <summary>
	/// The type symbol for <see cref="System.Threading.Tasks.Task"/>.
	/// </summary>
	public INamedTypeSymbol? TaskType => GetOrResolveType("System.Threading.Tasks.Task", ref _taskType);

	private Option<INamedTypeSymbol?> _taskType;

	/// <summary>
	/// The type symbol for <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? IReadOnlyDictionaryOfTKeyTValue => GetOrResolveType("System.Collections.Generic.IReadOnlyDictionary`2", ref _iReadOnlyDictionaryOfTKeyTValue);

	private Option<INamedTypeSymbol?> _iReadOnlyDictionaryOfTKeyTValue;

	/// <summary>
	/// The type symbol for <see cref="IDictionary{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? IDictionaryOfTKeyTValue => GetOrResolveType("System.Collections.Generic.IDictionary`2", ref _iDictionaryOfTKeyTValue);

	private Option<INamedTypeSymbol?> _iDictionaryOfTKeyTValue;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.IDictionary"/>.
	/// </summary>
	public INamedTypeSymbol? IDictionary => GetOrResolveType("System.Collections.IDictionary", ref _iDictionary);

	private Option<INamedTypeSymbol?> _iDictionary;

	/// <summary>
	/// The type symbol for <see cref="IEnumerable{T}"/>.
	/// </summary>
	public INamedTypeSymbol IEnumerableOfT => _iEnumerableOfT ??= Compilation.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T);

	private INamedTypeSymbol? _iEnumerableOfT;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.IEnumerable"/>.
	/// </summary>
	public INamedTypeSymbol IEnumerable => _iEnumerable ??= Compilation.GetSpecialType(SpecialType.System_Collections_IEnumerable);

	private INamedTypeSymbol? _iEnumerable;

	/// <summary>
	/// The type symbol for IAsyncEnumerable{T}.
	/// </summary>
	public INamedTypeSymbol? IAsyncEnumerableOfT => GetOrResolveType("System.Collections.Generic.IAsyncEnumerable`1", ref _iAsyncEnumerableOfT);

	private Option<INamedTypeSymbol?> _iAsyncEnumerableOfT;

	/// <summary>
	/// The type symbol for <see cref="IEqualityComparer{T}"/>.
	/// </summary>
	public INamedTypeSymbol? IEqualityComparerOfT => GetOrResolveType("System.Collections.Generic.IEqualityComparer`1", ref _iEqualityComparerOfT);

	private Option<INamedTypeSymbol?> _iEqualityComparerOfT;

	/// <summary>
	/// The type symbol for <see cref="IComparer{T}"/>.
	/// </summary>
	public INamedTypeSymbol? IComparerOfT => GetOrResolveType("System.Collections.Generic.IComparer`1", ref _iComparerOfT);

	private Option<INamedTypeSymbol?> _iComparerOfT;

	/// <summary>
	/// The type symbol for <see cref="Span{T}"/>.
	/// </summary>
	public INamedTypeSymbol? SpanOfT => GetOrResolveType("System.Span`1", ref _spanOfT);

	private Option<INamedTypeSymbol?> _spanOfT;

	/// <summary>
	/// The type symbol for <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ReadOnlySpanOfT => GetOrResolveType("System.ReadOnlySpan`1", ref _readOnlySpanOfT);

	private Option<INamedTypeSymbol?> _readOnlySpanOfT;

	/// <summary>
	/// The type symbol for <see cref="Memory{T}"/>.
	/// </summary>
	public INamedTypeSymbol? MemoryOfT => GetOrResolveType("System.Memory`1", ref _memoryOfT);

	private Option<INamedTypeSymbol?> _memoryOfT;

	/// <summary>
	/// The type symbol for <see cref="ReadOnlyMemory{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ReadOnlyMemoryOfT => GetOrResolveType("System.ReadOnlyMemory`1", ref _readOnlyMemoryOfT);

	private Option<INamedTypeSymbol?> _readOnlyMemoryOfT;

	/// <summary>
	/// The type symbol for <see cref="List{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ListOfT => GetOrResolveType("System.Collections.Generic.List`1", ref _listOfT);

	private Option<INamedTypeSymbol?> _listOfT;

	/// <summary>
	/// The type symbol for <see cref="HashSet{T}"/>.
	/// </summary>
	public INamedTypeSymbol? HashSetOfT => GetOrResolveType("System.Collections.Generic.HashSet`1", ref _hashSetOfT);

	private Option<INamedTypeSymbol?> _hashSetOfT;

	/// <summary>
	/// The type symbol for <see cref="KeyValuePair{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? KeyValuePairOfKV => GetOrResolveType("System.Collections.Generic.KeyValuePair`2", ref _keyValuePairOfKv);

	private Option<INamedTypeSymbol?> _keyValuePairOfKv;

	/// <summary>
	/// The type symbol for <see cref="Dictionary{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? DictionaryOfTKeyTValue => GetOrResolveType("System.Collections.Generic.Dictionary`2", ref _dictionaryOfTKeyTValue);

	private Option<INamedTypeSymbol?> _dictionaryOfTKeyTValue;

	/// <summary>
	/// The type symbol for <see cref="ICollection{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ICollectionOfT => GetOrResolveType("System.Collections.Generic.ICollection`1", ref _iCollectionOfT);

	private Option<INamedTypeSymbol?> _iCollectionOfT;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.IList"/>.
	/// </summary>
	public INamedTypeSymbol? IList => GetOrResolveType("System.Collections.IList", ref _iList);

	private Option<INamedTypeSymbol?> _iList;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableArray{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableArray => GetOrResolveType("System.Collections.Immutable.ImmutableArray`1", ref _immutableArray);

	private Option<INamedTypeSymbol?> _immutableArray;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableList{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableList => GetOrResolveType("System.Collections.Immutable.ImmutableList`1", ref _immutableList);

	private Option<INamedTypeSymbol?> _immutableList;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableQueue{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableQueue => GetOrResolveType("System.Collections.Immutable.ImmutableQueue`1", ref _immutableQueue);

	private Option<INamedTypeSymbol?> _immutableQueue;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableStack{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableStack => GetOrResolveType("System.Collections.Immutable.ImmutableStack`1", ref _immutableStack);

	private Option<INamedTypeSymbol?> _immutableStack;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableHashSet{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableHashSet => GetOrResolveType("System.Collections.Immutable.ImmutableHashSet`1", ref _immutableHashSet);

	private Option<INamedTypeSymbol?> _immutableHashSet;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableSortedSet{T}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableSortedSet => GetOrResolveType("System.Collections.Immutable.ImmutableSortedSet`1", ref _immutableSortedSet);

	private Option<INamedTypeSymbol?> _immutableSortedSet;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableDictionary{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableDictionary => GetOrResolveType("System.Collections.Immutable.ImmutableDictionary`2", ref _immutableDictionary);

	private Option<INamedTypeSymbol?> _immutableDictionary;

	/// <summary>
	/// The type symbol for <see cref="System.Collections.Immutable.ImmutableSortedDictionary{TKey, TValue}"/>.
	/// </summary>
	public INamedTypeSymbol? ImmutableSortedDictionary => GetOrResolveType("System.Collections.Immutable.ImmutableSortedDictionary`2", ref _immutableSortedDictionary);

	private Option<INamedTypeSymbol?> _immutableSortedDictionary;

	/// <summary>
	/// The type symbol for FrozenDictionary.
	/// </summary>
	public INamedTypeSymbol? FrozenDictionary => GetOrResolveType("System.Collections.Frozen.FrozenDictionary`2", ref _frozenDictionary);

	private Option<INamedTypeSymbol?> _frozenDictionary;

	/// <summary>
	/// The type symbol for FrozenSet.
	/// </summary>
	public INamedTypeSymbol? FrozenSet => GetOrResolveType("System.Collections.Frozen.FrozenSet`1", ref _frozenSet);

	private Option<INamedTypeSymbol?> _frozenSet;

	/// <summary>
	/// The type symbol for the F# list type.
	/// </summary>
	public INamedTypeSymbol? FSharpList => GetOrResolveType("Microsoft.FSharp.Collections.FSharpList`1", ref _fSharpList);

	private Option<INamedTypeSymbol?> _fSharpList;

	/// <summary>
	/// The type symbol for the F# map type.
	/// </summary>
	public INamedTypeSymbol? FSharpMap => GetOrResolveType("Microsoft.FSharp.Collections.FSharpMap`2", ref _fSharpMap);

	private Option<INamedTypeSymbol?> _fSharpMap;

	/// <summary>
	/// A "simple type" in this context defines a type that is either
	/// a primitive, string or represents an irreducible value such as <see cref="Guid"/> or <see cref="DateTime"/>.
	/// </summary>
	public bool IsSimpleType(ITypeSymbol type)
	{
		switch (type.SpecialType)
		{
			// Primitive types
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			// CoreLib non-primitives that represent a single value.
			case SpecialType.System_String:
			case SpecialType.System_Decimal:
			case SpecialType.System_DateTime:
				return true;
		}

		return (_simpleTypes ??= CreateSimpleTypes(Compilation)).Contains(type);

		static HashSet<ITypeSymbol> CreateSimpleTypes(Compilation compilation)
		{
			ReadOnlySpan<string> simpleTypeNames =
			[
				"System.Half",
				"System.Int128",
				"System.UInt128",
				"System.Guid",
				"System.DateTimeOffset",
				"System.DateOnly",
				"System.TimeSpan",
				"System.TimeOnly",
				"System.Version",
				"System.Uri",
				"System.Text.Rune",
				"System.Numerics.BigInteger",
			];

			var simpleTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
			foreach (var simpleTypeName in simpleTypeNames)
			{
				var simpleType = compilation.GetTypeByMetadataName(simpleTypeName);
				if (simpleType is not null)
				{
					simpleTypes.Add(simpleType);
				}
			}

			return simpleTypes;
		}
	}

	private HashSet<ITypeSymbol>? _simpleTypes;

	/// <summary>
	/// Get or resolve a type by its fully qualified name.
	/// </summary>
	/// <param name="fullyQualifiedName">The fully qualified name of the type to resolve.</param>
	/// <param name="field">A field in which to cache the result for future use.</param>
	/// <returns>The type symbol result or null if not found.</returns>
	protected INamedTypeSymbol? GetOrResolveType(string fullyQualifiedName, ref Option<INamedTypeSymbol?> field)
	{
		if (field.HasValue)
		{
			return field.Value;
		}

		if (Compilation.Assembly.GetTypeByMetadataName(fullyQualifiedName) is { } ownSymbol)
		{
			// This assembly defines it.
			field = new(ownSymbol);
		}
		else
		{
			foreach (var reference in Compilation.References)
			{
				if (!reference.Properties.Aliases.IsEmpty)
				{
					// We don't (yet) generate code to leverage aliases, so we skip any symbols defined in aliased references.
					continue;
				}

				if (Compilation.GetAssemblyOrModuleSymbol(reference) is not IAssemblySymbol referencedAssembly)
				{
					continue;
				}

				if (referencedAssembly.GetTypeByMetadataName(fullyQualifiedName) is not { } externalSymbol)
				{
					continue;
				}

				if (!Compilation.IsSymbolAccessibleWithin(externalSymbol, Compilation.Assembly))
				{
					continue;
				}

				// A referenced assembly declares this symbol, and it is accessible to our own.
				field = new(externalSymbol);
				break;
			}

			// Ensure to record that we tried, whether we found it or not.
			field = new(field.Value);
		}

		return field.Value;
	}

	/// <summary>
	/// Defines a true optional type that supports Some(null) representations.
	/// </summary>
	/// <typeparam name="T">The optional value contained.</typeparam>
	protected readonly struct Option<T>(T value)
	{
		/// <summary>
		/// Indicates whether the option has a value, or <see langword="default" /> otherwise.
		/// </summary>
		public bool HasValue { get; } = true;

		/// <summary>
		/// The value of the option.
		/// </summary>
		public T Value { get; } = value;
	}
}
