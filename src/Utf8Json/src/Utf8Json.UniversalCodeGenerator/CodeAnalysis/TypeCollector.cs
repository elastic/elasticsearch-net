using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utf8Json.UniversalCodeGenerator
{
    public class ReferenceSymbols
    {
        public readonly INamedTypeSymbol Task;
        public readonly INamedTypeSymbol TaskOfT;
        public readonly string DataMemberAttribute;
        public readonly string IgnoreDataMemberAttribute;
        public readonly string SerializationConstructorAttribute;

        public ReferenceSymbols(Compilation compilation)
        {
            TaskOfT = compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            Task = compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");

            DataMemberAttribute = "DataMember";
            IgnoreDataMemberAttribute = "IgnoreDataMember";
            SerializationConstructorAttribute = "SerializationConstructor";
            //DataMemberAttribute = compilation.GetTypeByMetadataName("System.Runtime.Serialization.DataMemberAttribute");
            //IgnoreDataMemberAttribute = compilation.GetTypeByMetadataName("System.Runtime.Serialization.IgnoreDataMemberAttribute");
            //SerializationConstructorAttribute = compilation.GetTypeByMetadataName("Utf8Json.SerializationConstructorAttribute");
        }
    }

    public class TypeCollector
    {
        const string CodegeneratorOnlyPreprocessorSymbol = "INCLUDE_ONLY_CODE_GENERATION";

        static readonly SymbolDisplayFormat binaryWriteFormat = new SymbolDisplayFormat(
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                miscellaneousOptions: SymbolDisplayMiscellaneousOptions.ExpandNullable,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly);

        static readonly SymbolDisplayFormat shortTypeNameFormat = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes);
        static readonly HashSet<string> embeddedTypes = new HashSet<string>(new string[]
        {
            "short",
            "int",
            "long",
            "ushort",
            "uint",
            "ulong",
            "float",
            "double",
            "bool",
            "byte",
            "sbyte",
            "decimal",
            "char",
            "string",
            "object",
            "System.Guid",
            "System.TimeSpan",
            "System.DateTime",
            "System.DateTimeOffset",

            // "MessagePack.Nil",

            // and arrays
            
            "short[]",
            "int[]",
            "long[]",
            "ushort[]",
            "uint[]",
            "ulong[]",
            "float[]",
            "double[]",
            "bool[]",
            "byte[]",
            "sbyte[]",
            "decimal[]",
            "char[]",
            "string[]",
            "System.DateTime[]",
            "System.ArraySegment<byte>",
            "System.ArraySegment<byte>?",

            // extensions

            "UnityEngine.Vector2",
            "UnityEngine.Vector3",
            "UnityEngine.Vector4",
            "UnityEngine.Quaternion",
            "UnityEngine.Color",
            "UnityEngine.Bounds",
            "UnityEngine.Rect",

            // "System.Reactive.Unit",
        });
        static readonly Dictionary<string, string> knownGenericTypes = new Dictionary<string, string>
        {
            {"System.Collections.Generic.List<>", "global::Utf8Json.Formatters.ListFormatter<TREPLACE>" },
            {"System.Collections.Generic.LinkedList<>", "global::Utf8Json.Formatters.LinkedListFormatter<TREPLACE>"},
            {"System.Collections.Generic.Queue<>", "global::Utf8Json.Formatters.QeueueFormatter<TREPLACE>"},
            {"System.Collections.Generic.Stack<>", "global::Utf8Json.Formatters.StackFormatter<TREPLACE>"},
            {"System.Collections.Generic.HashSet<>", "global::Utf8Json.Formatters.HashSetFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyCollection<>", "global::Utf8Json.Formatters.ReadOnlyCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.IList<>", "global::Utf8Json.Formatters.InterfaceListFormatter<TREPLACE>"},
            {"System.Collections.Generic.ICollection<>", "global::Utf8Json.Formatters.InterfaceCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.IEnumerable<>", "global::Utf8Json.Formatters.InterfaceEnumerableFormatter<TREPLACE>"},
            {"System.Collections.Generic.Dictionary<,>", "global::Utf8Json.Formatters.DictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.IDictionary<,>", "global::Utf8Json.Formatters.InterfaceDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.SortedDictionary<,>", "global::Utf8Json.Formatters.SortedDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.SortedList<,>", "global::Utf8Json.Formatters.SortedListFormatter<TREPLACE>"},
            {"System.Linq.ILookup<,>", "global::Utf8Json.Formatters.InterfaceLookupFormatter<TREPLACE>"},
            {"System.Linq.IGrouping<,>", "global::Utf8Json.Formatters.InterfaceGroupingFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ObservableCollection<>", "global::Utf8Json.Formatters.ObservableCollectionFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyObservableCollection<>", "global::Utf8Json.Formatters.ReadOnlyObservableCollectionFormatter<TREPLACE>" },
            {"System.Collections.Generic.IReadOnlyList<>", "global::Utf8Json.Formatters.InterfaceReadOnlyListFormatter<TREPLACE>"},
            {"System.Collections.Generic.IReadOnlyCollection<>", "global::Utf8Json.Formatters.InterfaceReadOnlyCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.ISet<>", "global::Utf8Json.Formatters.InterfaceSetFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentBag<>", "global::Utf8Json.Formatters.ConcurrentBagFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentQueue<>", "global::Utf8Json.Formatters.ConcurrentQueueFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentStack<>", "global::Utf8Json.Formatters.ConcurrentStackFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyDictionary<,>", "global::Utf8Json.Formatters.ReadOnlyDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.IReadOnlyDictionary<,>", "global::Utf8Json.Formatters.InterfaceReadOnlyDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentDictionary<,>", "global::Utf8Json.Formatters.ConcurrentDictionaryFormatter<TREPLACE>"},
            {"System.Lazy<>", "global::Utf8Json.Formatters.LazyFormatter<TREPLACE>"},
            {"System.Threading.Tasks<>", "global::Utf8Json.Formatters.TaskValueFormatter<TREPLACE>"},

            {"System.Tuple<>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,,,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,,,,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple<,,,,,,,>", "global::Utf8Json.Formatters.TupleFormatter<TREPLACE>"},

            {"System.ValueTuple<>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,,,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,,,,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple<,,,,,,,>", "global::Utf8Json.Formatters.ValueTupleFormatter<TREPLACE>"},

            {"System.Collections.Generic.KeyValuePair<,>", "global::Utf8Json.Formatters.KeyValuePairFormatter<TREPLACE>"},
            {"System.Threading.Tasks.ValueTask<>", "global::Utf8Json.Formatters.KeyValuePairFormatter<TREPLACE>"},
            {"System.ArraySegment<>", "global::Utf8Json.Formatters.ArraySegmentFormatter<TREPLACE>"},

            // extensions

            {"System.Collections.Immutable.ImmutableArray<>", "global::Utf8Json.ImmutableCollection.ImmutableArrayFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableList<>", "global::Utf8Json.ImmutableCollection.ImmutableListFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableDictionary<,>", "global::Utf8Json.ImmutableCollection.ImmutableDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableHashSet<>", "global::Utf8Json.ImmutableCollection.ImmutableHashSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableSortedDictionary<,>", "global::Utf8Json.ImmutableCollection.ImmutableSortedDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableSortedSet<>", "global::Utf8Json.ImmutableCollection.ImmutableSortedSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableQueue<>", "global::Utf8Json.ImmutableCollection.ImmutableQueueFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableStack<>", "global::Utf8Json.ImmutableCollection.ImmutableStackFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableList<>", "global::Utf8Json.ImmutableCollection.InterfaceImmutableListFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableDictionary<,>", "global::Utf8Json.ImmutableCollection.InterfaceImmutableDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableQueue<>", "global::Utf8Json.ImmutableCollection.InterfaceImmutableQueueFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableSet<>", "global::Utf8Json.ImmutableCollection.InterfaceImmutableSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableStack<>", "global::Utf8Json.ImmutableCollection.InterfaceImmutableStackFormatter<TREPLACE>"},
        };


        readonly ReferenceSymbols typeReferences;
        readonly INamedTypeSymbol[] targetTypes;
        readonly bool disallowInternal;

        // visitor workspace:
        HashSet<ITypeSymbol> alreadyCollected;
        List<ObjectSerializationInfo> collectedObjectInfo;
        List<GenericSerializationInfo> collectedGenericInfo;

        // --- 

        public TypeCollector(IEnumerable<string> inputFiles, IEnumerable<string> inputDirs, IEnumerable<string> conditinalSymbols, bool disallowInternal)
        {
            var compilation = RoslynExtensions.GetCompilationFromProject(inputFiles, inputDirs,
                conditinalSymbols.Concat(new[] { CodegeneratorOnlyPreprocessorSymbol }).ToArray()
            );

            this.typeReferences = new ReferenceSymbols(compilation);
            this.disallowInternal = disallowInternal;

            targetTypes = compilation.GetNamedTypeSymbols()
                .Where(x =>
                {
                    if (x.DeclaredAccessibility == Accessibility.Public) return true;
                    if (!disallowInternal)
                    {
                        return (x.DeclaredAccessibility == Accessibility.Friend);
                    }

                    return false;
                })
                .Where(x => (x.TypeKind == TypeKind.Interface) || (x.TypeKind == TypeKind.Class) || (x.TypeKind == TypeKind.Struct))
                .ToArray();
        }

        void ResetWorkspace()
        {
            alreadyCollected = new HashSet<ITypeSymbol>();
            collectedObjectInfo = new List<ObjectSerializationInfo>();
            collectedGenericInfo = new List<GenericSerializationInfo>();
        }

        // EntryPoint
        public (ObjectSerializationInfo[] objectInfo, GenericSerializationInfo[] genericInfo) Collect()
        {
            ResetWorkspace();

            foreach (var item in targetTypes)
            {
                CollectCore(item);
            }

            return (collectedObjectInfo.ToArray(), collectedGenericInfo.Distinct().ToArray());
        }

        // Gate of recursive collect
        void CollectCore(ITypeSymbol typeSymbol)
        {
            if (!alreadyCollected.Add(typeSymbol))
            {
                return;
            }

            if (embeddedTypes.Contains(typeSymbol.ToString()))
            {
                return;
            }

            if (typeSymbol.TypeKind == TypeKind.Array)
            {
                CollectArray(typeSymbol as IArrayTypeSymbol);
                return;
            }

            if (!IsAllowAccessibility(typeSymbol))
            {
                return;
            }

            var type = typeSymbol as INamedTypeSymbol;

            if (typeSymbol.TypeKind == TypeKind.Enum)
            {
                return;
            }

            if (type.IsGenericType)
            {
                CollectGeneric(type);
                return;
            }

            if (type.Locations[0].IsInMetadata)
            {
                return;
            }

            CollectObject(type);
            return;
        }

        void CollectArray(IArrayTypeSymbol array)
        {
            var elemType = array.ElementType;
            CollectCore(elemType);

            var info = new GenericSerializationInfo
            {
                FullName = array.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            };

            if (array.IsSZArray)
            {
                info.FormatterName = $"global::Utf8Json.Formatters.ArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 2)
            {
                info.FormatterName = $"global::Utf8Json.Formatters.TwoDimentionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 3)
            {
                info.FormatterName = $"global::Utf8Json.Formatters.ThreeDimentionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else if (array.Rank == 4)
            {
                info.FormatterName = $"global::Utf8Json.Formatters.FourDimentionalArrayFormatter<{elemType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>";
            }
            else
            {
                throw new InvalidOperationException("does not supports array dimention, " + info.FullName);
            }

            collectedGenericInfo.Add(info);

            return;
        }

        void CollectGeneric(INamedTypeSymbol type)
        {
            var genericType = type.ConstructUnboundGenericType();
            var genericTypeString = genericType.ToDisplayString();
            var fullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            // special case
            if (fullName == "global::System.ArraySegment<byte>" || fullName == "global::System.ArraySegment<byte>?")
            {
                return;
            }

            // nullable
            if (genericTypeString == "T?")
            {
                CollectCore(type.TypeArguments[0]);

                if (!embeddedTypes.Contains(type.TypeArguments[0].ToString()))
                {
                    var info = new GenericSerializationInfo
                    {
                        FormatterName = $"global::Utf8Json.Formatters.NullableFormatter<{type.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>",
                        FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    };

                    collectedGenericInfo.Add(info);
                }
                return;
            }

            // collection
            if (knownGenericTypes.TryGetValue(genericTypeString, out var formatter))
            {
                foreach (var item in type.TypeArguments)
                {
                    CollectCore(item);
                }

                var typeArgs = string.Join(", ", type.TypeArguments.Select(x => x.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));
                var f = formatter.Replace("TREPLACE", typeArgs);

                var info = new GenericSerializationInfo
                {
                    FormatterName = f,
                    FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                };

                collectedGenericInfo.Add(info);

                if (genericTypeString == "System.Linq.ILookup<,>")
                {
                    formatter = knownGenericTypes["System.Linq.IGrouping<,>"];
                    f = formatter.Replace("TREPLACE", typeArgs);

                    var groupingInfo = new GenericSerializationInfo
                    {
                        FormatterName = f,
                        FullName = $"global::System.Linq.IGrouping<{typeArgs}>",
                    };

                    collectedGenericInfo.Add(groupingInfo);

                    formatter = knownGenericTypes["System.Collections.Generic.IEnumerable<>"];
                    typeArgs = type.TypeArguments[1].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    f = formatter.Replace("TREPLACE", typeArgs);

                    var enumerableInfo = new GenericSerializationInfo
                    {
                        FormatterName = f,
                        FullName = $"global::System.Collections.Generic.IEnumerable<{typeArgs}>",
                    };

                    collectedGenericInfo.Add(enumerableInfo);
                }
            }
        }

        void CollectObject(INamedTypeSymbol type)
        {
            var isClass = !type.IsValueType;

            var stringMembers = new Dictionary<string, MemberSerializationInfo>();

            foreach (var item in type.GetAllMembers().OfType<IPropertySymbol>().Where(x => !x.IsOverride))
            {
                if (item.GetAttributes().FindAttributeShortName(typeReferences.IgnoreDataMemberAttribute) != null) continue;
                var dm = item.GetAttributes().FindAttributeShortName(typeReferences.DataMemberAttribute);

                var name = (dm.GetSingleNamedArgumentValueFromSyntaxTree("Name") as string) ?? item.Name;

                var member = new MemberSerializationInfo
                {
                    IsReadable = (item.GetMethod != null) && item.GetMethod.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    IsWritable = (item.SetMethod != null) && item.SetMethod.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    MemberName = item.Name,
                    IsProperty = true,
                    IsField = false,
                    Name = name,
                    Type = item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    ShortTypeName = item.Type.ToDisplayString(binaryWriteFormat)
                };
                if (!member.IsReadable && !member.IsWritable) continue;

                stringMembers.Add(name, member);

                CollectCore(item.Type); // recursive collect
            }
            foreach (var item in type.GetAllMembers().OfType<IFieldSymbol>())
            {
                if (item.GetAttributes().FindAttributeShortName(typeReferences.IgnoreDataMemberAttribute) != null) continue;
                if (item.IsImplicitlyDeclared) continue;

                var dm = item.GetAttributes().FindAttributeShortName(typeReferences.DataMemberAttribute);
                var name = (dm.GetSingleNamedArgumentValueFromSyntaxTree("Name") as string) ?? item.Name;

                var member = new MemberSerializationInfo
                {
                    IsReadable = item.DeclaredAccessibility == Accessibility.Public && !item.IsStatic,
                    IsWritable = item.DeclaredAccessibility == Accessibility.Public && !item.IsReadOnly && !item.IsStatic,
                    MemberName = item.Name,
                    IsProperty = false,
                    IsField = true,
                    Name = name,
                    Type = item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    ShortTypeName = item.Type.ToDisplayString(binaryWriteFormat)
                };
                if (!member.IsReadable && !member.IsWritable) continue;

                stringMembers.Add(name, member);
                CollectCore(item.Type); // recursive collect
            }

            // GetConstructor
            IEnumerator<IMethodSymbol> ctorEnumerator = null;
            var ctor = type.Constructors.Where(x => x.DeclaredAccessibility == Accessibility.Public)
                .SingleOrDefault(x => x.GetAttributes().FindAttributeShortName(typeReferences.SerializationConstructorAttribute) != null);
            if (ctor == null)
            {
                ctorEnumerator =
                    type.Constructors.Where(x => x.DeclaredAccessibility == Accessibility.Public && !x.IsImplicitlyDeclared).OrderBy(x => x.Parameters.Length)
                    .Concat(type.Constructors.Where(x => x.DeclaredAccessibility == Accessibility.Public).OrderByDescending(x => x.Parameters.Length).Take(1))
                    .GetEnumerator();

                if (ctorEnumerator.MoveNext())
                {
                    ctor = ctorEnumerator.Current;
                }
            }

            var constructorParameters = new List<MemberSerializationInfo>();
            if (ctor != null)
            {
                var constructorLookupDictionary = stringMembers.ToLookup(x => x.Key, x => x, StringComparer.OrdinalIgnoreCase);
                do
                {
                    constructorParameters.Clear();
                    var ctorParamIndex = 0;
                    foreach (var item in ctor.Parameters)
                    {
                        MemberSerializationInfo paramMember;

                        var hasKey = constructorLookupDictionary[item.Name];
                        var len = hasKey.Count();
                        if (len != 0)
                        {
                            if (len != 1)
                            {
                                if (ctorEnumerator != null)
                                {
                                    ctor = null;
                                    continue;
                                }
                                else
                                {
                                    throw new CodeGeneratorResolveFailedException("duplicate matched constructor parameter name:" + type.Name + " parameterName:" + item.Name + " paramterType:" + item.Type.Name);
                                }
                            }

                            paramMember = hasKey.First().Value;
                            if (item.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == paramMember.Type && paramMember.IsReadable)
                            {
                                constructorParameters.Add(paramMember);
                            }
                            else
                            {
                                if (ctorEnumerator != null)
                                {
                                    ctor = null;
                                    continue;
                                }
                                else
                                {
                                    throw new CodeGeneratorResolveFailedException("can't find matched constructor parameter, parameterType mismatch. type:" + type.Name + " parameterName:" + item.Name + " paramterType:" + item.Type.Name);
                                }
                            }
                        }
                        else
                        {
                            if (ctorEnumerator != null)
                            {
                                ctor = null;
                                continue;
                            }
                            else
                            {
                                throw new CodeGeneratorResolveFailedException("can't find matched constructor parameter, index not found. type:" + type.Name + " parameterName:" + item.Name);
                            }
                        }
                        ctorParamIndex++;
                    }
                } while (TryGetNextConstructor(ctorEnumerator, ref ctor));

                if (ctor == null)
                {
                    return; // ignore instead of exception
                    // throw new CodeGeneratorResolveFailedException("can't find matched constructor. type:" + type.Name);
                }
            }

            if (stringMembers.Count == 0) return;

            var info = new ObjectSerializationInfo
            {
                IsClass = isClass,
                ConstructorParameters = constructorParameters.ToArray(),
                Members = stringMembers.Values.ToArray(),
                Name = type.ToDisplayString(shortTypeNameFormat).Replace(".", "_"),
                FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                Namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString(),
                HasConstructor = ctor != null
            };
            collectedObjectInfo.Add(info);
        }

        static bool TryGetNextConstructor(IEnumerator<IMethodSymbol> ctorEnumerator, ref IMethodSymbol ctor)
        {
            if (ctorEnumerator == null || ctor != null)
            {
                return false;
            }

            if (ctorEnumerator.MoveNext())
            {
                ctor = ctorEnumerator.Current;
                return true;
            }
            else
            {
                ctor = null;
                return false;
            }
        }

        bool IsAllowAccessibility(ITypeSymbol symbol)
        {
            do
            {
                if (symbol.DeclaredAccessibility != Accessibility.Public)
                {
                    if (disallowInternal)
                    {
                        return false;
                    }

                    if (symbol.DeclaredAccessibility != Accessibility.Internal)
                    {
                        return true;
                    }
                }

                symbol = symbol.ContainingType;
            } while (symbol != null);

            return true;
        }
    }

    public class CodeGeneratorResolveFailedException : Exception
    {
        public CodeGeneratorResolveFailedException(string message)
            : base(message)
        {

        }
    }
}