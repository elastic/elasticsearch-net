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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Internal.Emit;

namespace Elasticsearch.Net.Utf8Json
{
    // NonGeneric API
    internal static partial class JsonSerializer
    {
        public static class NonGeneric
        {
            private static readonly Func<Type, CompiledMethods> CreateCompiledMethods;
            private static readonly ThreadsafeTypeKeyHashTable<CompiledMethods> serializes = new ThreadsafeTypeKeyHashTable<CompiledMethods>(capacity: 64);

			private delegate void SerializeJsonWriter(ref JsonWriter writer, object value, IJsonFormatterResolver resolver);

			private delegate object DeserializeJsonReader(ref JsonReader reader, IJsonFormatterResolver resolver);

            static NonGeneric() => CreateCompiledMethods = t => new CompiledMethods(t);

			private static CompiledMethods GetOrAdd(Type type) => serializes.GetOrAdd(type, CreateCompiledMethods);

			/// <summary>
            /// Serialize to binary with default resolver.
            /// </summary>
            public static byte[] Serialize(object value) =>
				value == null
					? Serialize<object>(value)
					: Serialize(value.GetType(), value, _defaultResolver);

			/// <summary>
            /// Serialize to binary with default resolver.
            /// </summary>
            public static byte[] Serialize(Type type, object value) => Serialize(type, value, _defaultResolver);

			/// <summary>
            /// Serialize to binary with specified resolver.
            /// </summary>
            public static byte[] Serialize(object value, IJsonFormatterResolver resolver) =>
				value == null
					? Serialize<object>(value, resolver)
					: Serialize(value.GetType(), value, resolver);

			/// <summary>
            /// Serialize to binary with specified resolver.
            /// </summary>
            public static byte[] Serialize(Type type, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).serialize1.Invoke(value, resolver);

			/// <summary>
            /// Serialize to stream.
            /// </summary>
            public static void Serialize(Stream stream, object value)
            {
                if (value == null) { Serialize<object>(stream, value); return; }
                Serialize(value.GetType(), stream, value, _defaultResolver);
            }

            /// <summary>
            /// Serialize to stream.
            /// </summary>
            public static void Serialize(Type type, Stream stream, object value) => Serialize(type, stream, value, _defaultResolver);

			/// <summary>
            /// Serialize to stream with specified resolver.
            /// </summary>
            public static void Serialize(Stream stream, object value, IJsonFormatterResolver resolver)
            {
                if (value == null) { Serialize<object>(stream, value, resolver); return; }
                Serialize(value.GetType(), stream, value, resolver);
            }

            /// <summary>
            /// Serialize to stream with specified resolver.
            /// </summary>
            public static void Serialize(Type type, Stream stream, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).serialize2.Invoke(stream, value, resolver);


			/// <summary>
            /// Serialize to stream.
            /// </summary>
            public static Task SerializeAsync(Stream stream, object value) =>
				value == null
					? SerializeAsync<object>(stream, value)
					: SerializeAsync(value.GetType(), stream, value, _defaultResolver);

			/// <summary>
            /// Serialize to stream.
            /// </summary>
            public static Task SerializeAsync(Type type, Stream stream, object value) => SerializeAsync(type, stream, value, _defaultResolver);

			/// <summary>
            /// Serialize to stream with specified resolver.
            /// </summary>
            public static Task SerializeAsync(Stream stream, object value, IJsonFormatterResolver resolver)
            {
                if (value == null) { return SerializeAsync<object>(stream, value, resolver); }
                return SerializeAsync(value.GetType(), stream, value, resolver);
            }

            /// <summary>
            /// Serialize to stream with specified resolver.
            /// </summary>
            public static Task SerializeAsync(Type type, Stream stream, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).serializeAsync.Invoke(stream, value, resolver);


			public static void Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver resolver)
			{
				if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

				Serialize(value.GetType(), ref writer, value, resolver);
			}

            public static void Serialize(Type type, ref JsonWriter writer, object value) => Serialize(type, ref writer, value, _defaultResolver);

			public static void Serialize(Type type, ref JsonWriter writer, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).serialize3.Invoke(ref writer, value, resolver);

			/// <summary>
            /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
            /// </summary>
            public static ArraySegment<byte> SerializeUnsafe(object value)
            {
                if (value == null) return SerializeUnsafe<object>(value);
                return SerializeUnsafe(value.GetType(), value);
            }

            /// <summary>
            /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
            /// </summary>
            public static ArraySegment<byte> SerializeUnsafe(Type type, object value) => SerializeUnsafe(type, value, _defaultResolver);

			/// <summary>
            /// Serialize to binary with specified resolver. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
            /// </summary>
            public static ArraySegment<byte> SerializeUnsafe(object value, IJsonFormatterResolver resolver)
            {
                if (value == null) return SerializeUnsafe<object>(value);
                return SerializeUnsafe(value.GetType(), value, resolver);
            }

            /// <summary>
            /// Serialize to binary with specified resolver. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
            /// </summary>
            public static ArraySegment<byte> SerializeUnsafe(Type type, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).serializeUnsafe.Invoke(value, resolver);

			/// <summary>
            /// Serialize to JsonString.
            /// </summary>
            public static string ToJsonString(object value)
            {
                if (value == null) return "null";
                return ToJsonString(value.GetType(), value);
            }

            /// <summary>
            /// Serialize to JsonString.
            /// </summary>
            public static string ToJsonString(Type type, object value) => ToJsonString(type, value, _defaultResolver);

			/// <summary>
            /// Serialize to JsonString with specified resolver.
            /// </summary>
            public static string ToJsonString(object value, IJsonFormatterResolver resolver)
            {
                if (value == null) return "null";
                return ToJsonString(value.GetType(), value, resolver);
            }

            /// <summary>
            /// Serialize to JsonString with specified resolver.
            /// </summary>
            public static string ToJsonString(Type type, object value, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).toJsonString.Invoke(value, resolver);

			public static object Deserialize(Type type, string json) => Deserialize(type, json, _defaultResolver);

			public static object Deserialize(Type type, string json, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).deserialize1.Invoke(json, resolver);

			public static object Deserialize(Type type, byte[] bytes) => Deserialize(type, bytes, _defaultResolver);

			public static object Deserialize(Type type, byte[] bytes, IJsonFormatterResolver resolver) =>
				Deserialize(type, bytes, 0, _defaultResolver);

			public static object Deserialize(Type type, byte[] bytes, int offset) => Deserialize(type, bytes, offset, _defaultResolver);

			public static object Deserialize(Type type, byte[] bytes, int offset, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).deserialize2.Invoke(bytes, offset, resolver);

			public static object Deserialize(Type type, Stream stream) => Deserialize(type, stream, _defaultResolver);

			public static object Deserialize(Type type, Stream stream, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).deserialize3.Invoke(stream, resolver);

			public static object Deserialize(Type type, ref JsonReader reader) => Deserialize(type, ref reader, _defaultResolver);

			public static object Deserialize(Type type, ref JsonReader reader, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).deserialize4.Invoke(ref reader, resolver);

			public static Task<object> DeserializeAsync(Type type, Stream stream) => DeserializeAsync(type, stream, _defaultResolver);

			public static Task<object> DeserializeAsync(Type type, Stream stream, IJsonFormatterResolver resolver) =>
				GetOrAdd(type).deserializeAsync.Invoke(stream, resolver);

			private class CompiledMethods
            {
                public readonly Func<object, IJsonFormatterResolver, byte[]> serialize1;
                public readonly Action<Stream, object, IJsonFormatterResolver> serialize2;
                public readonly SerializeJsonWriter serialize3;
                public readonly Func<object, IJsonFormatterResolver, ArraySegment<byte>> serializeUnsafe;
                public readonly Func<object, IJsonFormatterResolver, string> toJsonString;
                public readonly Func<string, IJsonFormatterResolver, object> deserialize1;
                public readonly Func<byte[], int, IJsonFormatterResolver, object> deserialize2;
                public readonly Func<Stream, IJsonFormatterResolver, object> deserialize3;
                public readonly DeserializeJsonReader deserialize4;

                public readonly Func<Stream, object, IJsonFormatterResolver, Task> serializeAsync;
                public readonly Func<Stream, IJsonFormatterResolver, Task<object>> deserializeAsync;

                public CompiledMethods(Type type)
                {
                    {
                        var dm = new DynamicMethod(nameof(serialize1), typeof(byte[]), new[] { typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // obj
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(Serialize), new[] { null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        serialize1 = CreateDelegate<Func<object, IJsonFormatterResolver, byte[]>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(serialize2), null, new[] { typeof(Stream), typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // stream
                        il.EmitLdarg(1);
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(2);
                        il.EmitCall(GetMethod(type, nameof(Serialize), new[] { typeof(Stream), null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        serialize2 = CreateDelegate<Action<Stream, object, IJsonFormatterResolver>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(serialize3), null, new[] { typeof(JsonWriter).MakeByRefType(), typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // ref writer
                        il.EmitLdarg(1);
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(2);
                        il.EmitCall(GetMethod(type, nameof(Serialize), new[] { typeof(JsonWriter).MakeByRefType(), null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        serialize3 = CreateDelegate<SerializeJsonWriter>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(serializeUnsafe), typeof(ArraySegment<byte>), new[] { typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // obj
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(SerializeUnsafe), new[] { null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        serializeUnsafe = CreateDelegate<Func<object, IJsonFormatterResolver, ArraySegment<byte>>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(toJsonString), typeof(string), new[] { typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // obj
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(ToJsonString), new[] { null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        toJsonString = CreateDelegate<Func<object, IJsonFormatterResolver, string>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(Deserialize), typeof(object), new[] { typeof(string), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(Deserialize), new[] { typeof(string), typeof(IJsonFormatterResolver) }));
                        il.EmitBoxOrDoNothing(type);
                        il.Emit(OpCodes.Ret);

                        deserialize1 = CreateDelegate<Func<string, IJsonFormatterResolver, object>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(Deserialize), typeof(object), new[] { typeof(byte[]), typeof(int), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0);
                        il.EmitLdarg(1);
                        il.EmitLdarg(2);
                        il.EmitCall(GetMethod(type, nameof(Deserialize), new[] { typeof(byte[]), typeof(int), typeof(IJsonFormatterResolver) }));
                        il.EmitBoxOrDoNothing(type);
                        il.Emit(OpCodes.Ret);

                        deserialize2 = CreateDelegate<Func<byte[], int, IJsonFormatterResolver, object>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(Deserialize), typeof(object), new[] { typeof(Stream), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(Deserialize), new[] { typeof(Stream), typeof(IJsonFormatterResolver) }));
                        il.EmitBoxOrDoNothing(type);
                        il.Emit(OpCodes.Ret);

                        deserialize3 = CreateDelegate<Func<Stream, IJsonFormatterResolver, object>>(dm);
                    }
                    {
                        var dm = new DynamicMethod(nameof(Deserialize), typeof(object), new[] { typeof(JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // ref reader
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(Deserialize), new[] { typeof(JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) }));
                        il.EmitBoxOrDoNothing(type);
                        il.Emit(OpCodes.Ret);

                        deserialize4 = CreateDelegate<DeserializeJsonReader>(dm);
                    }

                    {
                        var dm = new DynamicMethod(nameof(SerializeAsync), typeof(Task), new[] { typeof(Stream), typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0); // stream
                        il.EmitLdarg(1);
                        il.EmitUnboxOrCast(type);
                        il.EmitLdarg(2);
                        il.EmitCall(GetMethod(type, nameof(SerializeAsync), new[] { typeof(Stream), null, typeof(IJsonFormatterResolver) }));
                        il.Emit(OpCodes.Ret);

                        serializeAsync = CreateDelegate<Func<Stream, object, IJsonFormatterResolver, Task>>(dm);
                    }

                    {
                        var dm = new DynamicMethod(nameof(DeserializeAsync), typeof(Task<object>), new[] { typeof(Stream), typeof(IJsonFormatterResolver) }, type.Module, true);
                        var il = dm.GetILGenerator();

                        il.EmitLdarg(0);
                        il.EmitLdarg(1);
                        il.EmitCall(GetMethod(type, nameof(DeserializeAsync), new[] { typeof(Stream), typeof(IJsonFormatterResolver) }));
                        il.EmitCall(typeof(CompiledMethods).GetMethod(nameof(TaskCast), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(type));
                        il.Emit(OpCodes.Ret);

                        deserializeAsync = CreateDelegate<Func<Stream, IJsonFormatterResolver, Task<object>>>(dm);
                    }
                }

				private static async Task<object> TaskCast<T>(Task<T> task)
                {
                    var t = await task.ConfigureAwait(false);
                    return t;
                }

				private static T CreateDelegate<T>(DynamicMethod dm) => (T)(object)dm.CreateDelegate(typeof(T));

				private static MethodInfo GetMethod(Type type, string name, Type[] arguments) =>
					typeof(JsonSerializer).GetMethods(BindingFlags.Static | BindingFlags.Public)
						.Where(x => x.Name == name)
						.Single(x =>
						{
							var ps = x.GetParameters();
							if (ps.Length != arguments.Length) return false;
							for (var i = 0; i < ps.Length; i++)
							{
								// null for <T>.
								if (arguments[i] == null && ps[i].ParameterType.IsGenericParameter) continue;
								if (ps[i].ParameterType != arguments[i]) return false;
							}
							return true;
						})
						.MakeGenericMethod(type);
			}
        }
    }
}
