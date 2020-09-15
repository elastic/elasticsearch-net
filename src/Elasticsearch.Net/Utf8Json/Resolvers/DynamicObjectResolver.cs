#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Internal.Emit;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	/// <summary>
	/// ObjectResolver by dynamic code generation.
	/// </summary>
	internal static class DynamicObjectResolver
	{
		/// <summary>AllowPrivate:False, ExcludeNull:False, NameMutate:Original</summary>
		public static readonly IJsonFormatterResolver Default = DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal.Instance;
		/// <summary>AllowPrivate:False, ExcludeNull:True,  NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver ExcludeNullCamelCase = DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase.Instance;
		/// <summary>AllowPrivate:True,  ExcludeNull:True,  NameMutate:CamelCase</summary>
		public static readonly IJsonFormatterResolver AllowPrivateExcludeNullCamelCase = DynamicObjectResolverAllowPrivateTrueExcludeNullTrueNameMutateCamelCase.Instance;

		public static IJsonFormatterResolver Create(Func<MemberInfo, JsonProperty> propertyMapper, Lazy<Func<string, string>> mutator, bool excludeNull) =>
			new CustomDynamicObjectResolver(propertyMapper, mutator, excludeNull);
	}

	internal sealed class CustomDynamicObjectResolver : IJsonFormatterResolver
	{
		private readonly Func<MemberInfo, JsonProperty> _propertyMapper;
		private readonly Lazy<Func<string, string>> _mutator;
		private readonly bool _excludeNull;
		private readonly ThreadsafeTypeKeyHashTable<object> _formatters = new ThreadsafeTypeKeyHashTable<object>();

		public CustomDynamicObjectResolver(Func<MemberInfo, JsonProperty> propertyMapper, Lazy<Func<string, string>> mutator, bool excludeNull)
		{
			_propertyMapper = propertyMapper;
			_mutator = mutator;
			_excludeNull = excludeNull;
		}

		public IJsonFormatter<T> GetFormatter<T>() =>
			(IJsonFormatter<T>)_formatters.GetOrAdd(typeof(T), type =>
				DynamicObjectTypeBuilder.BuildFormatterToDynamicMethod<T>(this, _mutator.Value, _propertyMapper, _excludeNull, false));
	}

	#region DynamicAssembly

	internal sealed class DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal : IJsonFormatterResolver
	{
		// configuration
		public static readonly IJsonFormatterResolver Instance = new DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal();
		private static readonly Func<string, string> NameMutator = StringMutator.Original;
		private static readonly bool ExcludeNull = false;
		private static readonly string ModuleName = $"{ResolverConfig.Namespace}.{nameof(DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal)}";
		private static readonly DynamicAssembly Assembly;

		static DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal() => Assembly = new DynamicAssembly(ModuleName);

		private DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateOriginal()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache() =>
				formatter = (IJsonFormatter<T>)DynamicObjectTypeBuilder.BuildFormatterToAssembly<T>(Assembly, Instance, NameMutator, null, ExcludeNull);
		}
	}

	internal sealed class DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase : IJsonFormatterResolver
	{
		// configuration
		public static readonly IJsonFormatterResolver Instance = new DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase();
		private static readonly Func<string, string> NameMutator = StringMutator.ToCamelCase;
		private static readonly bool ExcludeNull = true;
		private static readonly string ModuleName = $"{ResolverConfig.Namespace}.{nameof(DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase)}";
		private static readonly DynamicAssembly Assembly;

		static DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase() => Assembly = new DynamicAssembly(ModuleName);

		private DynamicObjectResolverAllowPrivateFalseExcludeNullTrueNameMutateCamelCase()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache() =>
				formatter = (IJsonFormatter<T>)DynamicObjectTypeBuilder.BuildFormatterToAssembly<T>(Assembly, Instance, NameMutator, null, ExcludeNull);
		}
	}

	#endregion

	#region DynamicMethod

	internal sealed class DynamicObjectResolverAllowPrivateTrueExcludeNullTrueNameMutateCamelCase : IJsonFormatterResolver
	{
		// configuration
		public static readonly IJsonFormatterResolver Instance = new DynamicObjectResolverAllowPrivateTrueExcludeNullTrueNameMutateCamelCase();
		private static readonly Func<string, string> NameMutator = StringMutator.ToCamelCase;
		private static readonly bool ExcludeNull = true;

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache() =>
				formatter = (IJsonFormatter<T>)DynamicObjectTypeBuilder.BuildFormatterToDynamicMethod<T>(Instance, NameMutator, null, ExcludeNull, true);
		}
	}

	#endregion

	internal static class DynamicObjectTypeBuilder
	{
		private static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);
		private static int _nameSequence;

		private static readonly HashSet<Type> IgnoreTypes = new HashSet<Type>
		{
			typeof(object),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(bool),
			typeof(byte),
			typeof(sbyte),
			typeof(decimal),
			typeof(char),
			typeof(string),
			typeof(Guid),
			typeof(TimeSpan),
			typeof(DateTime),
			typeof(DateTimeOffset),
		};

		private static readonly HashSet<Type> JsonPrimitiveTypes = new HashSet<Type>
		{
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(bool),
			typeof(byte),
			typeof(sbyte),
			typeof(string),
		};

		public static object BuildFormatterToAssembly<T>(DynamicAssembly assembly, IJsonFormatterResolver selfResolver, Func<string, string> mutator, Func<MemberInfo, JsonProperty> propertyMapper, bool excludeNull)
		{
			var type = typeof(T);

			if (type.IsNullable())
			{
				type = type.GenericTypeArguments[0];

				var innerFormatter = selfResolver.GetFormatterDynamic(type);
				if (innerFormatter == null)
					return null;

				return (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(type), innerFormatter);
			}

			if (typeof(Exception).IsAssignableFrom(type))
				return BuildAnonymousFormatter(typeof(T), mutator, propertyMapper, excludeNull, false, true);

			if (type.IsAnonymous() || TryGetInterfaceEnumerableElementType(typeof(T), out _))
				return BuildAnonymousFormatter(typeof(T), mutator, propertyMapper, excludeNull, false, false);

			var formatterTypeInfo = BuildType(assembly, typeof(T), mutator, propertyMapper, excludeNull);
			if (formatterTypeInfo == null) return null;

			return (IJsonFormatter<T>)Activator.CreateInstance(formatterTypeInfo.AsType());
		}

		public static object BuildFormatterToDynamicMethod<T>(IJsonFormatterResolver selfResolver, Func<string,string> mutator, Func<MemberInfo, JsonProperty> propertyMapper, bool excludeNull, bool allowPrivate)
		{
			var type = typeof(T);

			if (type.IsNullable())
			{
				type = type.GenericTypeArguments[0];

				var innerFormatter = selfResolver.GetFormatterDynamic(type);
				if (innerFormatter == null)
					return null;

				return (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(type), innerFormatter);
			}
			if (typeof(Exception).IsAssignableFrom(type))
				return BuildAnonymousFormatter(typeof(T), mutator, propertyMapper, excludeNull, false, true);

			return BuildAnonymousFormatter(typeof(T), mutator, propertyMapper, excludeNull, allowPrivate, false);
		}

		private static TypeInfo BuildType(DynamicAssembly assembly, Type type, Func<string, string> mutator, Func<MemberInfo, JsonProperty> propertyMapper, bool excludeNull)
		{
			if (IgnoreTypes.Contains(type)) return null;

			var serializationInfo = new MetaType(type, mutator, propertyMapper, false);
			var hasShouldSerialize = serializationInfo.Members.Any(x => x.ShouldSerializeMethodInfo != null);

			var formatterType = typeof(IJsonFormatter<>).MakeGenericType(type);
			var typeBuilder = assembly.DefineType(ResolverConfig.Namespace + "." + SubtractFullNameRegex.Replace(type.FullName, "").Replace(".", "_") + "Formatter" + Interlocked.Increment(ref _nameSequence), TypeAttributes.NotPublic | TypeAttributes.Sealed, null, new[] { formatterType });

			FieldBuilder stringByteKeysField;
			Dictionary<MetaMember, FieldInfo> customFormatterLookup;

			// for serialize, bake cache.
			{
				var method = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
				stringByteKeysField = typeBuilder.DefineField("stringByteKeys", typeof(byte[][]), FieldAttributes.Private | FieldAttributes.InitOnly);

				var il = method.GetILGenerator();
				customFormatterLookup = BuildConstructor(typeBuilder, serializationInfo, method, stringByteKeysField, il, excludeNull, hasShouldSerialize);
			}

			{
				var method = typeBuilder.DefineMethod("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
					null,
					new[] { typeof(JsonWriter).MakeByRefType(), type, typeof(IJsonFormatterResolver) });

				var il = method.GetILGenerator();
				BuildSerialize(type, serializationInfo, il, () =>
				{
					il.EmitLoadThis();
					il.EmitLdfld(stringByteKeysField);
				}, (index, member) =>
				{
					FieldInfo fi;
					if (!customFormatterLookup.TryGetValue(member, out fi)) return false;

					il.EmitLoadThis();
					il.EmitLdfld(fi);
					return true;
				}, excludeNull, hasShouldSerialize, 1); // firstArgIndex:0 is this.
			}

			{
				var method = typeBuilder.DefineMethod("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
					type,
					new Type[] { typeof(JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) });

				var il = method.GetILGenerator();
				BuildDeserialize(type, serializationInfo, il, (index, member) =>
				{
					FieldInfo fi;
					if (!customFormatterLookup.TryGetValue(member, out fi)) return false;

					il.EmitLoadThis();
					il.EmitLdfld(fi);
					return true;
				}, false, 1); // firstArgIndex:0 is this.
			}

			return typeBuilder.CreateTypeInfo();
		}

		public static object BuildAnonymousFormatter(Type type, Func<string, string> nameMutator, Func<MemberInfo, JsonProperty> propertyMapper, bool excludeNull, bool allowPrivate, bool isException)
		{
			if (IgnoreTypes.Contains(type)) return false;

			MetaType serializationInfo;
			if (isException)
			{
				var ignoreSet = new HashSet<string>(new[]
				{
					"TargetSite", "ClassName", "InnerException"
				}.Select(x => nameMutator(x)));

				// special case for exception, modify
				serializationInfo = new MetaType(type, nameMutator, propertyMapper, false);

				serializationInfo.BestMatchConstructor = null;
				serializationInfo.ConstructorParameters = new MetaMember[0];
				serializationInfo.Members = new[] { new StringConstantValueMetaMember(nameMutator("ClassName"), type.FullName) }
					.Concat(serializationInfo.Members.Where(x => !ignoreSet.Contains(x.Name)))
					.Concat(new[] { new InnerExceptionMetaMember(nameMutator("InnerException")) })
					.ToArray();
			}
			else
			{
				serializationInfo = new MetaType(type, nameMutator, propertyMapper, allowPrivate); // can be allowPrivate:true
			}
			var hasShouldSerialize = serializationInfo.Members.Any(x => x.ShouldSerializeMethodInfo != null);

			// build instance instead of emit constructor.
			var stringByteKeysField = new List<byte[]>();
			var i = 0;
			foreach (var item in serializationInfo.Members.Where(x => x.IsReadable))
			{
				if (excludeNull || hasShouldSerialize)
				{
					stringByteKeysField.Add(JsonWriter.GetEncodedPropertyName(item.Name));
				}
				else
				{
					if (i == 0)
					{
						stringByteKeysField.Add(JsonWriter.GetEncodedPropertyNameWithBeginObject(item.Name));
					}
					else
					{
						stringByteKeysField.Add(JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator(item.Name));
					}
				}
				i++;
			}

			var serializeCustomFormatters = new List<object>();
			var deserializeCustomFormatters = new List<object>();
			foreach (var item in serializationInfo.Members.Where(x => x.IsReadable))
			{
				var attr = item.GetCustomAttribute<JsonFormatterAttribute>(true);
				if (attr != null)
				{
					var formatterType = attr.Attribute.FormatterType;

					if (attr.Attribute.FormatterType.IsGenericType &&
						!attr.Attribute.FormatterType.IsConstructedGenericType &&
						attr.Attribute.FormatterType.GetGenericArguments().Length == 1)
						formatterType = attr.Attribute.FormatterType.MakeGenericType(item.PropertyInfo.PropertyType);

					var formatter = Activator.CreateInstance(formatterType, attr.Attribute.Arguments);
					serializeCustomFormatters.Add(formatter);
				}
				else if (item.JsonFormatter != null)
					serializeCustomFormatters.Add(item.JsonFormatter);
				else
					serializeCustomFormatters.Add(null);
			}
			foreach (var item in serializationInfo.Members) // not only for writable because for use ctor.
			{
				var attr = item.GetCustomAttribute<JsonFormatterAttribute>(true);
				if (attr != null)
				{
					var formatterType = attr.Attribute.FormatterType;

					if (attr.Attribute.FormatterType.IsGenericType &&
						!attr.Attribute.FormatterType.IsConstructedGenericType &&
						attr.Attribute.FormatterType.GetGenericArguments().Length == 1)
						formatterType = attr.Attribute.FormatterType.MakeGenericType(item.PropertyInfo.PropertyType);

					var formatter = Activator.CreateInstance(formatterType, attr.Attribute.Arguments);
					deserializeCustomFormatters.Add(formatter);
				}
				else if (item.JsonFormatter != null)
					deserializeCustomFormatters.Add(item.JsonFormatter);
				else
					deserializeCustomFormatters.Add(null);
			}

			var serialize = new DynamicMethod("Serialize", null, new Type[] { typeof(byte[][]), typeof(object[]), typeof(JsonWriter).MakeByRefType(), type, typeof(IJsonFormatterResolver) }, type.Module, true);
			{
				var il = serialize.GetILGenerator();
				BuildSerialize(type, serializationInfo, il, () =>
				{
					il.EmitLdarg(0);
				}, (index, member) =>
				{
					if (serializeCustomFormatters.Count == 0) return false;
					if (serializeCustomFormatters[index] == null) return false;

					il.EmitLdarg(1); // read object[]
					il.EmitLdc_I4(index);
					il.Emit(OpCodes.Ldelem_Ref); // object
					il.Emit(OpCodes.Castclass, serializeCustomFormatters[index].GetType());
					return true;
				}, excludeNull, hasShouldSerialize, 2);
			}

			var deserialize = new DynamicMethod("Deserialize", type, new Type[] { typeof(object[]), typeof(JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) }, type.Module, true);
			{
				var il = deserialize.GetILGenerator();
				BuildDeserialize(type, serializationInfo, il, (index, member) =>
				{
					if (deserializeCustomFormatters.Count == 0) return false;
					if (deserializeCustomFormatters[index] == null) return false;

					il.EmitLdarg(0); // read object[]
					il.EmitLdc_I4(index);
					il.Emit(OpCodes.Ldelem_Ref); // object
					il.Emit(OpCodes.Castclass, deserializeCustomFormatters[index].GetType());
					return true;
				}, true, 1);
			}

			object serializeDelegate = serialize.CreateDelegate(typeof(AnonymousJsonSerializeAction<>).MakeGenericType(type));
			object deserializeDelegate = deserialize.CreateDelegate(typeof(AnonymousJsonDeserializeFunc<>).MakeGenericType(type));

			return Activator.CreateInstance(typeof(DynamicMethodAnonymousFormatter<>).MakeGenericType(type), stringByteKeysField.ToArray(), serializeCustomFormatters.ToArray(), deserializeCustomFormatters.ToArray(), serializeDelegate, deserializeDelegate);
		}

		private static Dictionary<MetaMember, FieldInfo> BuildConstructor(TypeBuilder builder, MetaType info, ConstructorInfo method, FieldBuilder stringByteKeysField, ILGenerator il, bool excludeNull, bool hasShouldSerialize)
		{
			il.EmitLdarg(0);
			il.Emit(OpCodes.Call, EmitInfo.ObjectCtor);

			var writeCount = info.Members.Count(x => x.IsReadable);
			il.EmitLdarg(0);
			il.EmitLdc_I4(writeCount);
			il.Emit(OpCodes.Newarr, typeof(byte[]));

			var i = 0;
			foreach (var item in info.Members.Where(x => x.IsReadable))
			{
				il.Emit(OpCodes.Dup);
				il.EmitLdc_I4(i);
				il.Emit(OpCodes.Ldstr, item.Name);
				if (excludeNull || hasShouldSerialize)
				{
					il.EmitCall(EmitInfo.JsonWriter.GetEncodedPropertyName);
				}
				else
				{
					if (i == 0)
						il.EmitCall(EmitInfo.JsonWriter.GetEncodedPropertyNameWithBeginObject);
					else
						il.EmitCall(EmitInfo.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator);
				}

				il.Emit(OpCodes.Stelem_Ref);
				i++;
			}

			il.Emit(OpCodes.Stfld, stringByteKeysField);

			var customFormatterField = BuildCustomFormatterField(builder, info, il);
			il.Emit(OpCodes.Ret);
			return customFormatterField;
		}

		private static Dictionary<MetaMember, FieldInfo> BuildCustomFormatterField(TypeBuilder builder, MetaType info, ILGenerator il)
		{
			var dict = new Dictionary<MetaMember, FieldInfo>();
			foreach (var item in info.Members.Where(x => x.IsReadable || x.IsWritable))
			{
				var attr = item.GetCustomAttribute<JsonFormatterAttribute>(true);
				if (attr != null)
				{
					Type formatterType;
					if (attr.Attribute.FormatterType.IsGenericType
						&& !attr.Attribute.FormatterType.IsConstructedGenericType)
					{
						// generic types need to be deconstructed
						var types = item.Type.IsGenericType
							? item.Type.GenericTypeArguments
							: new[] { item.Type };

						formatterType = attr.Attribute.FormatterType.MakeGenericType(types);
					}
					else
						formatterType = attr.Attribute.FormatterType;

					var f = builder.DefineField(item.Name + "_formatter", formatterType, FieldAttributes.Private | FieldAttributes.InitOnly);

					var bindingFlags = (int)(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

					var attrVar = il.DeclareLocal(typeof(JsonFormatterAttribute));

					// get the JsonFormatter from the declaring type
					il.Emit(OpCodes.Ldtoken, attr.DeclaringType);
					il.EmitCall(EmitInfo.GetTypeFromHandle);
					il.Emit(OpCodes.Ldstr, item.MemberName);
					il.EmitLdc_I4(bindingFlags);
					if (item.IsProperty)
						il.EmitCall(EmitInfo.TypeGetProperty);
					else
						il.EmitCall(EmitInfo.TypeGetField);

					il.EmitTrue();
					il.EmitCall(EmitInfo.GetCustomAttributeJsonFormatterAttribute);
					il.EmitStloc(attrVar);

					var formatterVar = il.DeclareLocal(typeof(Type));

					il.EmitLdloc(attrVar);
					il.EmitCall(EmitInfo.JsonFormatterAttr.FormatterType);
					il.EmitStloc(formatterVar);

					// see if formatter is open generic type
					if (attr.Attribute.FormatterType.IsGenericType && !attr.Attribute.FormatterType.IsConstructedGenericType)
					{
						var typesVar = il.DeclareLocal(typeof(Type[]));

						if (item.Type.IsGenericType)
						{
							// construct a generic type from the open formatter type and the generic type arguments from the member type
							il.Emit(OpCodes.Ldtoken, item.Type);
							il.EmitCall(EmitInfo.GetTypeFromHandle);
							il.EmitCall(EmitInfo.TypeGetGenericArguments);
							il.EmitStloc(typesVar);
						}
						else
						{
							// create a type array of length 1 with the member type in index 0
							// and construct a generic type from the open formatter type using this
							il.EmitLdc_I4(1);
							il.Emit(OpCodes.Newarr, typeof(Type));
							il.Emit(OpCodes.Dup);
							il.EmitLdc_I4(0);
							il.Emit(OpCodes.Ldtoken, item.Type);
							il.EmitCall(EmitInfo.GetTypeFromHandle);
							il.Emit(OpCodes.Stelem_Ref);
							il.EmitStloc(typesVar);
						}

						il.EmitLdloc(formatterVar);
						il.EmitLdloc(typesVar);
						il.EmitCall(EmitInfo.MakeGenericType);
						il.EmitStloc(formatterVar);
					}

					il.EmitLoadThis();

					il.EmitLdloc(formatterVar);
					il.EmitLdloc(attrVar);
					il.EmitCall(EmitInfo.JsonFormatterAttr.Arguments);
					il.EmitCall(EmitInfo.ActivatorCreateInstance);

					il.Emit(OpCodes.Castclass, formatterType);
					il.Emit(OpCodes.Stfld, f);

					dict.Add(item, f);
				}
			}

			return dict;
		}

		private static void BuildSerialize(Type type, MetaType info, ILGenerator il, Action emitStringByteKeys, Func<int, MetaMember, bool> tryEmitLoadCustomFormatter, bool excludeNull, bool hasShouldSerialize, int firstArgIndex)
		{
			var argWriter = new ArgumentField(il, firstArgIndex);
			var argValue = new ArgumentField(il, firstArgIndex + 1, type);
			var argResolver = new ArgumentField(il, firstArgIndex + 2);

			// special case for serialize exception...
			var innerExceptionMetaMember = info.Members.OfType<InnerExceptionMetaMember>().FirstOrDefault();
			if (innerExceptionMetaMember != null)
			{
				innerExceptionMetaMember.ArgWriter = argWriter;
				innerExceptionMetaMember.ArgValue = argValue;
				innerExceptionMetaMember.ArgResolver = argResolver;
			}

			// Special case for serialize IEnumerable<>.
			if (info.IsClass && info.BestMatchConstructor == null)
			{
				if (TryGetInterfaceEnumerableElementType(type, out var elementType))
				{
					var t = typeof(IEnumerable<>).MakeGenericType(elementType);

					argResolver.EmitLoad();
					il.EmitCall(EmitInfo.GetFormatterWithVerify.MakeGenericMethod(t));

					argWriter.EmitLoad();
					argValue.EmitLoad();
					argResolver.EmitLoad();
					il.EmitCall(EmitInfo.Serialize(t));
					il.Emit(OpCodes.Ret);
					return;
				}
			}

			// if(value == null) { writer.WriteNull(); return; }
			if (info.IsClass)
			{
				var elseBody = il.DefineLabel();

				argValue.EmitLoad();
				il.Emit(OpCodes.Brtrue_S, elseBody);

				argWriter.EmitLoad();
				il.EmitCall(EmitInfo.JsonWriter.WriteNull);
				il.Emit(OpCodes.Ret); // return;

				il.MarkLabel(elseBody);
			}

			// special case for exception
			if (type == typeof(Exception))
			{
				//var exceptionType = value.GetType();
				//if (exceptionType != typeof(Exception))
				//{
				//    JsonSerializer.NonGeneric.Serialize(exceptionType, ref writer, value, formatterResolver);
				//    return;
				//}

				var elseBody = il.DefineLabel();
				var exceptionType = il.DeclareLocal(typeof(Type));
				argValue.EmitLoad();
				il.EmitCall(EmitInfo.GetTypeMethod);
				il.EmitStloc(exceptionType);
				il.EmitLdloc(exceptionType);
				il.Emit(OpCodes.Ldtoken, typeof(Exception));
				il.EmitCall(EmitInfo.GetTypeFromHandle);
				il.EmitCall(EmitInfo.TypeEquals);
				il.Emit(OpCodes.Brtrue, elseBody);

				il.EmitLdloc(exceptionType);
				argWriter.EmitLoad();
				argValue.EmitLoad();
				argResolver.EmitLoad();
				il.EmitCall(EmitInfo.NongenericSerialize);
				il.Emit(OpCodes.Ret); // return;

				il.MarkLabel(elseBody);
			}

			// for-loop WriteRaw -> WriteValue, EndObject
			LocalBuilder wrote = null;
			var endObjectLabel = il.DefineLabel();
			Label[] labels = null;
			if (excludeNull || hasShouldSerialize)
			{
				// wrote = false; writer.WriteBeginObject();
				wrote = il.DeclareLocal(typeof(bool));
				argWriter.EmitLoad();
				il.EmitCall(EmitInfo.JsonWriter.WriteBeginObject);
				labels = info.Members.Where(x => x.IsReadable).Select(_ => il.DefineLabel()).ToArray();
			}

			var index = 0;
			foreach (var item in info.Members.Where(x => x.IsReadable))
			{
				if (excludeNull || hasShouldSerialize)
				{
					il.MarkLabel(labels[index]);

					// if(value.X != null)
					if (excludeNull)
					{
						if (item.Type.IsNullable())
						{
							var local = il.DeclareLocal(item.Type);

							argValue.EmitLoad();
							item.EmitLoadValue(il);
							il.EmitStloc(local);
							il.EmitLdloca(local);
							il.EmitCall(EmitInfo.GetNullableHasValue(item.Type.GetGenericArguments()[0]));
							il.Emit(OpCodes.Brfalse_S, (index < labels.Length - 1) ? labels[index + 1] : endObjectLabel); // null, next label
						}
						else if (!item.Type.IsValueType && !(item is StringConstantValueMetaMember))
						{
							argValue.EmitLoad();
							item.EmitLoadValue(il);
							il.Emit(OpCodes.Brfalse_S, (index < labels.Length - 1) ? labels[index + 1] : endObjectLabel); // null, next label
						}
					}
					if (hasShouldSerialize && item.ShouldSerializeMethodInfo != null)
					{
						argValue.EmitLoad();
						il.EmitCall(item.ShouldSerializeMethodInfo);
						il.Emit(OpCodes.Brfalse_S, (index < labels.Length - 1) ? labels[index + 1] : endObjectLabel); // false, next label
					}

					if (item.ShouldSerializeTypeMethodInfo != null)
					{
						argValue.EmitLoad();
						item.EmitLoadValue(il);
						argResolver.EmitLoad();
						il.EmitCall(item.ShouldSerializeTypeMethodInfo);
						il.Emit(OpCodes.Brfalse_S, (index < labels.Length - 1) ? labels[index + 1] : endObjectLabel); // false, next label
					}

					// if(wrote)
					var toWrite = il.DefineLabel();
					var flagTrue = il.DefineLabel();
					il.EmitLdloc(wrote);
					il.Emit(OpCodes.Brtrue_S, flagTrue);

					il.EmitTrue();
					il.EmitStloc(wrote);
					il.Emit(OpCodes.Br, toWrite);

					il.MarkLabel(flagTrue);
					argWriter.EmitLoad();
					il.EmitCall(EmitInfo.JsonWriter.WriteValueSeparator);

					il.MarkLabel(toWrite);
				}

				// WriteRaw
				argWriter.EmitLoad();
				emitStringByteKeys();
				il.EmitLdc_I4(index);
				il.Emit(OpCodes.Ldelem_Ref);
				// same as in constructor
				byte[] rawField;
				if (excludeNull || hasShouldSerialize)
				{
					rawField = JsonWriter.GetEncodedPropertyName(item.Name);
				}
				else
				{
					rawField = (index == 0) ? JsonWriter.GetEncodedPropertyNameWithBeginObject(item.Name) : JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator(item.Name);
				}
				if (rawField.Length < 32)
				{
					if (UnsafeMemory.Is32Bit)
						il.EmitCall(typeof(UnsafeMemory32).GetRuntimeMethod("WriteRaw" + rawField.Length, new[] { typeof(JsonWriter).MakeByRefType(), typeof(byte[]) }));
					else
						il.EmitCall(typeof(UnsafeMemory64).GetRuntimeMethod("WriteRaw" + rawField.Length, new[] { typeof(JsonWriter).MakeByRefType(), typeof(byte[]) }));
				}
				else
					il.EmitCall(EmitInfo.UnsafeMemory_MemoryCopy);

				// EmitValue
				EmitSerializeValue(item, il, index, tryEmitLoadCustomFormatter, argWriter, argValue, argResolver);

				index++;
			}

			il.MarkLabel(endObjectLabel);

			// for case of empty
			if (!excludeNull && index == 0)
			{
				argWriter.EmitLoad();
				il.EmitCall(EmitInfo.JsonWriter.WriteBeginObject);
			}

			argWriter.EmitLoad();
			il.EmitCall(EmitInfo.JsonWriter.WriteEndObject);
			il.Emit(OpCodes.Ret);
		}

		private static void EmitSerializeValue(MetaMember member, ILGenerator il, int index, Func<int, MetaMember, bool> tryEmitLoadCustomFormatter, ArgumentField writer, ArgumentField argValue, ArgumentField argResolver)
		{
			var t = member.Type;
			if (member is InnerExceptionMetaMember innerExceptionMetaMember)
				innerExceptionMetaMember.EmitSerializeDirectly(il);
			else if (tryEmitLoadCustomFormatter(index, member))
			{
				writer.EmitLoad();
				argValue.EmitLoad();
				member.EmitLoadValue(il);
				argResolver.EmitLoad();
				il.EmitCall(EmitInfo.Serialize(t));
			}
			else if (JsonPrimitiveTypes.Contains(t))
			{
				writer.EmitLoad();
				argValue.EmitLoad();
				member.EmitLoadValue(il);
				il.EmitCall(typeof(JsonWriter).GetDeclaredMethods("Write" + t.Name).OrderByDescending(x => x.GetParameters().Length).First());
			}
			else
			{
				argResolver.EmitLoad();
				il.Emit(OpCodes.Call, EmitInfo.GetFormatterWithVerify.MakeGenericMethod(t));
				writer.EmitLoad();
				argValue.EmitLoad();
				member.EmitLoadValue(il);
				argResolver.EmitLoad();
				il.EmitCall(EmitInfo.Serialize(t));
			}
		}

		private static void BuildDeserialize(Type type, MetaType info, ILGenerator il, Func<int, MetaMember, bool> tryEmitLoadCustomFormatter, bool useGetUninitializedObject, int firstArgIndex)
		{
			if (info.IsClass && info.BestMatchConstructor == null && !(useGetUninitializedObject && info.IsConcreteClass))
			{
				il.Emit(OpCodes.Ldstr, "generated serializer for " + type.Name + " does not support deserialize.");
				il.Emit(OpCodes.Newobj, EmitInfo.InvalidOperationExceptionConstructor);
				il.Emit(OpCodes.Throw);
				return;
			}

			var argReader = new ArgumentField(il, firstArgIndex);
			var argResolver = new ArgumentField(il, firstArgIndex + 1);

			// if (reader.ReadIsNull()) return null;
			{
				var elseBody = il.DefineLabel();

				argReader.EmitLoad();
				il.EmitCall(EmitInfo.JsonReader.ReadIsNull);
				il.Emit(OpCodes.Brfalse_S, elseBody);

				if (info.IsClass)
				{
					il.Emit(OpCodes.Ldnull);
					il.Emit(OpCodes.Ret); // return;
				}
				else
				{
					il.Emit(OpCodes.Ldstr, "json value is null, struct is not supported");
					il.Emit(OpCodes.Newobj, EmitInfo.InvalidOperationExceptionConstructor);
					il.Emit(OpCodes.Throw);
				}

				il.MarkLabel(elseBody);
			}

			// read '{'
			argReader.EmitLoad();
			il.EmitCall(EmitInfo.JsonReader.ReadIsBeginObjectWithVerify);

			// check side-effect-free for optimize set member value(reduce is-exists-member on json check)
			var isSideEffectFreeType = true;
			if (info.BestMatchConstructor != null)
			{
				isSideEffectFreeType = IsSideEffectFreeConstructorType(info.BestMatchConstructor);
				// if set only property, it is not side-effect but same as has side-effect
				var hasSetOnlyMember = info.Members.Any(x => !x.IsReadable && x.IsWritable);
				if (hasSetOnlyMember)
					isSideEffectFreeType = false;
			}

			// make local fields
			var infoList = info.Members
				.Select(item => new DeserializeInfo
				{
					MemberInfo = item,
					LocalField = il.DeclareLocal(item.Type),
					IsDeserializedField = isSideEffectFreeType ? null : il.DeclareLocal(typeof(bool))
				})
				.ToArray();

			var countField = il.DeclareLocal(typeof(int));

			// read member loop
			{
				var automata = new AutomataDictionary();
				for (var i = 0; i < info.Members.Length; i++)
				{
					automata.Add(JsonWriter.GetEncodedPropertyNameWithoutQuotation(info.Members[i].Name), i);
				}

				var baseBytes = il.DeclareLocal(typeof(byte[]));
				var buffer = il.DeclareLocal(typeof(byte).MakeByRefType(), true);
				var keyArraySegment = il.DeclareLocal(typeof(ArraySegment<byte>));
				var longKey = il.DeclareLocal(typeof(ulong));
				var p = il.DeclareLocal(typeof(byte*));
				var rest = il.DeclareLocal(typeof(int));

				// baseBytes = reader.GetBufferUnsafe();
				// fixed (byte* buffer = &baseBytes[0]) {
				argReader.EmitLoad();
				il.EmitCall(EmitInfo.JsonReader.GetBufferUnsafe);
				il.EmitStloc(baseBytes);

				il.EmitLdloc(baseBytes);
				il.EmitLdc_I4(0);
				il.Emit(OpCodes.Ldelema, typeof(byte));
				il.EmitStloc(buffer);

				// while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count)) // "}", skip "," when count != 0
				var continueWhile = il.DefineLabel();
				var breakWhile = il.DefineLabel();
				var readNext = il.DefineLabel();

				il.MarkLabel(continueWhile);

				argReader.EmitLoad();
				il.EmitLdloca(countField); // ref count field(use ldloca)
				il.EmitCall(EmitInfo.JsonReader.ReadIsEndObjectWithSkipValueSeparator);
				il.Emit(OpCodes.Brtrue, breakWhile); // found '}', break

				argReader.EmitLoad();
				il.EmitCall(EmitInfo.JsonReader.ReadPropertyNameSegmentUnsafe);
				il.EmitStloc(keyArraySegment);

				// p = buffer + arraySegment.Offset
				il.EmitLdloc(buffer);
				il.Emit(OpCodes.Conv_I);
				il.EmitLdloca(keyArraySegment);
				il.EmitCall(typeof(ArraySegment<byte>).GetRuntimeProperty("Offset").GetMethod);
				il.Emit(OpCodes.Add);
				il.EmitStloc(p);

				// rest = arraySegment.Count
				il.EmitLdloca(keyArraySegment);
				il.EmitCall(typeof(ArraySegment<byte>).GetRuntimeProperty("Count").GetMethod);
				il.EmitStloc(rest);

				// if(rest == 0) goto End
				il.EmitLdloc(rest);
				il.Emit(OpCodes.Brfalse, readNext);

				//// gen automata name lookup
				automata.EmitMatch(il, p, rest, longKey, x =>
				{
					var i = x.Value;
					if (infoList[i].MemberInfo != null)
					{
						EmitDeserializeValue(il, infoList[i], i, tryEmitLoadCustomFormatter, argReader, argResolver);
						if (!isSideEffectFreeType)
						{
							il.EmitTrue();
							il.EmitStloc(infoList[i].IsDeserializedField);
						}
						il.Emit(OpCodes.Br, continueWhile);
					}
					else
						il.Emit(OpCodes.Br, readNext);
				}, () =>
				{
					il.Emit(OpCodes.Br, readNext);
				});

				il.MarkLabel(readNext);
				argReader.EmitLoad();
				il.EmitCall(EmitInfo.JsonReader.ReadNextBlock);

				il.Emit(OpCodes.Br, continueWhile); // loop again

				il.MarkLabel(breakWhile);

				// end fixed
				il.Emit(OpCodes.Ldc_I4_0);
				il.Emit(OpCodes.Conv_U);
				il.EmitStloc(buffer);
			}

			// create result object
			var localResult = EmitNewObject(il, type, info, infoList, isSideEffectFreeType);

			if (localResult != null)
				il.Emit(OpCodes.Ldloc, localResult);

			il.Emit(OpCodes.Ret);
		}

		private static void EmitDeserializeValue(ILGenerator il, DeserializeInfo info, int index, Func<int, MetaMember, bool> tryEmitLoadCustomFormatter, ArgumentField reader, ArgumentField argResolver)
		{
			var member = info.MemberInfo;
			var t = member.Type;
			if (tryEmitLoadCustomFormatter(index, member))
			{
				reader.EmitLoad();
				argResolver.EmitLoad();
				il.EmitCall(EmitInfo.Deserialize(t));
			}
			else if (JsonPrimitiveTypes.Contains(t))
			{
				reader.EmitLoad();
				il.EmitCall(typeof(JsonReader).GetDeclaredMethods("Read" + t.Name).OrderByDescending(x => x.GetParameters().Length).First());
			}
			else
			{
				argResolver.EmitLoad();
				il.Emit(OpCodes.Call, EmitInfo.GetFormatterWithVerify.MakeGenericMethod(t));
				reader.EmitLoad();
				argResolver.EmitLoad();
				il.EmitCall(EmitInfo.Deserialize(t));
			}

			il.EmitStloc(info.LocalField);
		}

		private static LocalBuilder EmitNewObject(ILGenerator il, Type type, MetaType info, DeserializeInfo[] members, bool isSideEffectFreeType)
		{
			if (info.IsClass)
			{
				LocalBuilder result = null;
				if (!isSideEffectFreeType)
				{
					result = il.DeclareLocal(type);
				}

				if (info.BestMatchConstructor != null)
				{
					foreach (var item in info.ConstructorParameters)
					{
						var local = members.First(x => x.MemberInfo == item);
						il.EmitLdloc(local.LocalField);
					}
					il.Emit(OpCodes.Newobj, info.BestMatchConstructor);
				}
				else
				{
					il.Emit(OpCodes.Ldtoken, type);
					il.EmitCall(EmitInfo.GetTypeFromHandle);
					il.EmitCall(EmitInfo.GetUninitializedObject);
				}
				if (!isSideEffectFreeType)
					il.EmitStloc(result);

				foreach (var item in members.Where(x => x.MemberInfo != null && x.MemberInfo.IsWritable))
				{
					if (isSideEffectFreeType)
					{
						var next = il.DefineLabel();

						// don't assign the value if it's null
						var infoType = item.MemberInfo.Type;
						if (infoType.IsClass || infoType.IsInterface || infoType.IsAbstract)
						{
							il.EmitLdloc(item.LocalField);
							il.Emit(OpCodes.Brfalse, next);
						}

						il.Emit(OpCodes.Dup);
						il.EmitLdloc(item.LocalField);
						item.MemberInfo.EmitStoreValue(il);

						il.MarkLabel(next);
					}
					else
					{
						var next = il.DefineLabel();
						il.EmitLdloc(item.IsDeserializedField);
						il.Emit(OpCodes.Brfalse, next);

						// don't assign the value if it's null
						var infoType = item.MemberInfo.Type;
						if (infoType.IsClass || infoType.IsInterface || infoType.IsAbstract)
						{
							il.EmitLdloc(item.LocalField);
							il.Emit(OpCodes.Brfalse, next);
						}

						il.EmitLdloc(result);
						il.EmitLdloc(item.LocalField);
						item.MemberInfo.EmitStoreValue(il);

						il.MarkLabel(next);
					}
				}

				return result;
			}
			else
			{
				var result = il.DeclareLocal(type);
				if (info.BestMatchConstructor == null)
				{
					il.Emit(OpCodes.Ldloca, result);
					il.Emit(OpCodes.Initobj, type);
				}
				else
				{
					foreach (var item in info.ConstructorParameters)
					{
						var local = members.First(x => x.MemberInfo == item);
						il.EmitLdloc(local.LocalField);
					}
					il.Emit(OpCodes.Newobj, info.BestMatchConstructor);
					il.Emit(OpCodes.Stloc, result);
				}

				foreach (var item in members.Where(x => x.MemberInfo != null && x.MemberInfo.IsWritable))
				{
					if (isSideEffectFreeType)
					{
						var next = il.DefineLabel();

						// don't assign the value if it's null
						var infoType = item.MemberInfo.Type;
						if (infoType.IsClass || infoType.IsInterface || infoType.IsAbstract)
						{
							il.EmitLdloc(item.LocalField);
							il.Emit(OpCodes.Brfalse, next);
						}

						il.EmitLdloca(result);
						il.EmitLdloc(item.LocalField);
						item.MemberInfo.EmitStoreValue(il);

						il.MarkLabel(next);
					}
					else
					{
						var next = il.DefineLabel();
						il.EmitLdloc(item.IsDeserializedField);
						il.Emit(OpCodes.Brfalse, next);

						// don't assign the value if it's null
						var infoType = item.MemberInfo.Type;
						if (infoType.IsClass || infoType.IsInterface || infoType.IsAbstract)
						{
							il.EmitLdloc(item.LocalField);
							il.Emit(OpCodes.Brfalse, next);
						}

						il.EmitLdloca(result);
						il.EmitLdloc(item.LocalField);
						item.MemberInfo.EmitStoreValue(il);

						il.MarkLabel(next);
					}
				}

				return result; // struct returns local result field
			}
		}

		private static bool IsSideEffectFreeConstructorType(ConstructorInfo ctorInfo)
		{
			var methodBody = ctorInfo.GetMethodBody();
			var array = methodBody?.GetILAsByteArray();
			if (array == null)
				return false;

			// (ldarg.0, call(empty ctor), ret) == side-effect free.
			// Release build is 7, Debug build has nop(or nop like code) so should use ILStreamReader
			var opCodes = new List<OpCode>();
			using (var reader = new ILStreamReader(array))
			{
				while (!reader.EndOfStream)
				{
					var code = reader.ReadOpCode();
					if (code != OpCodes.Nop
						&& code != OpCodes.Ldloc_0
						&& code != OpCodes.Ldloc_S
						&& code != OpCodes.Stloc_0
						&& code != OpCodes.Stloc_S
						&& code != OpCodes.Blt
						&& code != OpCodes.Blt_S
						&& code != OpCodes.Bgt
						&& code != OpCodes.Bgt_S)
					{
						opCodes.Add(code);
						if (opCodes.Count == 4) break;
					}
				}
			}

			if (opCodes.Count == 3
				&& opCodes[0] == OpCodes.Ldarg_0
				&& opCodes[1] == OpCodes.Call
				&& opCodes[2] == OpCodes.Ret)
			{
				if (ctorInfo.DeclaringType.BaseType == typeof(object))
					return true;

				// use empty constuctor.
				var baseCtorInfo = ctorInfo.DeclaringType.BaseType.GetConstructor(Type.EmptyTypes);
				if (baseCtorInfo == null)
					return false;

				// check parent constructor
				return IsSideEffectFreeConstructorType(baseCtorInfo);
			}

			return false;
		}

		private static bool TryGetInterfaceEnumerableElementType(Type type, out Type elementType)
		{
			foreach (var implInterface in type.GetInterfaces())
			{
				if (implInterface.IsGenericType)
				{
					var genericTypeDef = implInterface.GetGenericTypeDefinition();
					if (genericTypeDef == typeof(IEnumerable<>))
					{
						var args = implInterface.GetGenericArguments();
						elementType = args[0];
						return true;
					}
				}
			}

			elementType = null;
			return false;
		}

		private struct DeserializeInfo
		{
			public MetaMember MemberInfo;
			public LocalBuilder LocalField;
			public LocalBuilder IsDeserializedField;
		}

		internal static class EmitInfo
		{
			public static readonly ConstructorInfo ObjectCtor = typeof(object).GetDeclaredConstructors().First(x => x.GetParameters().Length == 0);

			public static readonly MethodInfo GetFormatterWithVerify = typeof(JsonFormatterResolverExtensions).GetRuntimeMethod("GetFormatterWithVerify", new[] { typeof(IJsonFormatterResolver) });
			public static readonly MethodInfo UnsafeMemory_MemoryCopy = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer, byte[] src) => UnsafeMemory.MemoryCopy(ref writer, src));
			public static readonly ConstructorInfo InvalidOperationExceptionConstructor = typeof(InvalidOperationException).GetDeclaredConstructors().First(x => { var p = x.GetParameters(); return p.Length == 1 && p[0].ParameterType == typeof(string); });
			public static readonly MethodInfo GetTypeFromHandle = ExpressionUtility.GetMethodInfo(() => Type.GetTypeFromHandle(default(RuntimeTypeHandle)));

			public static readonly MethodInfo TypeGetProperty = ExpressionUtility.GetMethodInfo((Type t) => t.GetProperty(default(string), default(BindingFlags)));
			public static readonly MethodInfo TypeGetField = ExpressionUtility.GetMethodInfo((Type t) => t.GetField(default(string), default(BindingFlags)));

			public static readonly MethodInfo GetCustomAttributeJsonFormatterAttribute = ExpressionUtility.GetMethodInfo(() => CustomAttributeExtensions.GetCustomAttribute<JsonFormatterAttribute>(default(MemberInfo), default(bool)));

			public static readonly MethodInfo ActivatorCreateInstance = ExpressionUtility.GetMethodInfo(() => Activator.CreateInstance(default(Type), default(object[])));
			public static readonly MethodInfo GetUninitializedObject = ExpressionUtility.GetMethodInfo(() => System.Runtime.Serialization.FormatterServices.GetUninitializedObject(default(Type)));

			public static readonly MethodInfo GetTypeMethod = ExpressionUtility.GetMethodInfo((object o) => o.GetType());
			public static readonly MethodInfo TypeGetGenericArguments = ExpressionUtility.GetPropertyInfo((Type t) => t.GenericTypeArguments).GetMethod;
			public static readonly MethodInfo TypeEquals = ExpressionUtility.GetMethodInfo((Type t) => t.Equals(default(Type)));

			public static readonly MethodInfo MakeGenericType = ExpressionUtility.GetMethodInfo((Type t) => t.MakeGenericType(default(Type[])));

			public static readonly MethodInfo NongenericSerialize = ExpressionUtility.GetMethodInfo<Utf8Json.JsonWriter>(writer => JsonSerializer.NonGeneric.Serialize(default(Type), ref writer, default(object), default(IJsonFormatterResolver)));

			public static MethodInfo Serialize(Type type) =>
				typeof(IJsonFormatter<>).MakeGenericType(type).GetRuntimeMethod(nameof(IJsonFormatter<object>.Serialize), new[] { typeof(Utf8Json.JsonWriter).MakeByRefType(), type, typeof(IJsonFormatterResolver) });

			public static MethodInfo Deserialize(Type type) =>
				typeof(IJsonFormatter<>).MakeGenericType(type).GetRuntimeMethod(nameof(IJsonFormatter<object>.Deserialize), new[] { typeof(Utf8Json.JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) });

			public static MethodInfo GetNullableHasValue(Type type) =>
				typeof(Nullable<>).MakeGenericType(type).GetRuntimeProperty(nameof(Nullable<int>.HasValue)).GetMethod;

			internal static class JsonWriter
			{
				public static readonly MethodInfo GetEncodedPropertyNameWithBeginObject = ExpressionUtility.GetMethodInfo(() => Utf8Json.JsonWriter.GetEncodedPropertyNameWithBeginObject(default(string)));

				public static readonly MethodInfo GetEncodedPropertyNameWithPrefixValueSeparator = ExpressionUtility.GetMethodInfo(() => Utf8Json.JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator(default(string)));

				public static readonly MethodInfo GetEncodedPropertyNameWithoutQuotation = ExpressionUtility.GetMethodInfo(() => Utf8Json.JsonWriter.GetEncodedPropertyNameWithoutQuotation(default(string)));

				public static readonly MethodInfo GetEncodedPropertyName = ExpressionUtility.GetMethodInfo(() => Utf8Json.JsonWriter.GetEncodedPropertyName(default(string)));

				public static readonly MethodInfo WriteNull = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer) => writer.WriteNull());
				public static readonly MethodInfo WriteRaw = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer) => writer.WriteRaw(default(byte[])));
				public static readonly MethodInfo WriteBeginObject = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer) => writer.WriteBeginObject());
				public static readonly MethodInfo WriteEndObject = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer) => writer.WriteEndObject());
				public static readonly MethodInfo WriteValueSeparator = ExpressionUtility.GetMethodInfo((Utf8Json.JsonWriter writer) => writer.WriteValueSeparator());

				static JsonWriter()
				{
				}
			}

			internal static class JsonReader
			{
				public static readonly MethodInfo ReadIsNull = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.ReadIsNull());
				public static readonly MethodInfo ReadIsBeginObjectWithVerify = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.ReadIsBeginObjectWithVerify());
				public static readonly MethodInfo ReadIsEndObjectWithSkipValueSeparator = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader, int count) => reader.ReadIsEndObjectWithSkipValueSeparator(ref count));
				public static readonly MethodInfo ReadPropertyNameSegmentUnsafe = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.ReadPropertyNameSegmentRaw());
				public static readonly MethodInfo ReadNextBlock = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.ReadNextBlock());
				public static readonly MethodInfo GetBufferUnsafe = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.GetBufferUnsafe());
				public static readonly MethodInfo GetCurrentOffsetUnsafe = ExpressionUtility.GetMethodInfo((Utf8Json.JsonReader reader) => reader.GetCurrentOffsetUnsafe());

				static JsonReader()
				{
				}
			}

			internal static class JsonFormatterAttr
			{
				internal static readonly MethodInfo FormatterType = ExpressionUtility.GetPropertyInfo((JsonFormatterAttribute attr) => attr.FormatterType).GetMethod;
				internal static readonly MethodInfo Arguments = ExpressionUtility.GetPropertyInfo((JsonFormatterAttribute attr) => attr.Arguments).GetMethod;
			}
		}
	}

	internal delegate void AnonymousJsonSerializeAction<T>(byte[][] stringByteKeysField, object[] customFormatters, ref JsonWriter writer, T value, IJsonFormatterResolver resolver);
	internal delegate T AnonymousJsonDeserializeFunc<T>(object[] customFormatters, ref JsonReader reader, IJsonFormatterResolver resolver);

	internal class DynamicMethodAnonymousFormatter<T> : IJsonFormatter<T>
	{
		private readonly byte[][] _stringByteKeysField;
		private readonly object[] _serializeCustomFormatters;
		private readonly object[] _deserializeCustomFormatters;
		private readonly AnonymousJsonSerializeAction<T> _serialize;
		private readonly AnonymousJsonDeserializeFunc<T> _deserialize;

		public DynamicMethodAnonymousFormatter(byte[][] stringByteKeysField, object[] serializeCustomFormatters, object[] deserializeCustomFormatters, AnonymousJsonSerializeAction<T> serialize, AnonymousJsonDeserializeFunc<T> deserialize)
		{
			_stringByteKeysField = stringByteKeysField;
			_serializeCustomFormatters = serializeCustomFormatters;
			_deserializeCustomFormatters = deserializeCustomFormatters;
			_serialize = serialize;
			_deserialize = deserialize;
		}

		public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			if (_serialize == null) throw new InvalidOperationException(GetType().Name + " does not support Serialize.");
			_serialize(_stringByteKeysField, _serializeCustomFormatters, ref writer, value, formatterResolver);
		}

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (_deserialize == null) throw new InvalidOperationException(GetType().Name + " does not support Deserialize.");
			return _deserialize(_deserializeCustomFormatters, ref reader, formatterResolver);
		}
	}
}
