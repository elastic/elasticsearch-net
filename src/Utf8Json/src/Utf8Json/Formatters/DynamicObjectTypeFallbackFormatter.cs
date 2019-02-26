using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Utf8Json.Internal;
using Utf8Json.Internal.Emit;

namespace Utf8Json.Formatters
{
    public sealed class DynamicObjectTypeFallbackFormatter : IJsonFormatter<object>
    {
        delegate void SerializeMethod(object dynamicFormatter, ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver);

        readonly ThreadsafeTypeKeyHashTable<KeyValuePair<object, SerializeMethod>> serializers = new ThreadsafeTypeKeyHashTable<KeyValuePair<object, SerializeMethod>>();

        readonly IJsonFormatterResolver[] innerResolvers;

        public DynamicObjectTypeFallbackFormatter(params IJsonFormatterResolver[] innerResolvers)
        {
            this.innerResolvers = innerResolvers;
        }

        public void Serialize(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            var type = value.GetType();

            if (type == typeof(object))
            {
                // serialize to empty object
                writer.WriteBeginObject();
                writer.WriteEndObject();
                return;
            }

            KeyValuePair<object, SerializeMethod> formatterAndDelegate;
            if (!serializers.TryGetValue(type, out formatterAndDelegate))
            {
                lock (serializers)
                {
                    if (!serializers.TryGetValue(type, out formatterAndDelegate))
                    {
                        object formatter = null;
                        foreach (var innerResolver in innerResolvers)
                        {
                            formatter = innerResolver.GetFormatterDynamic(type);
                            if (formatter != null) break;
                        }
                        if (formatter == null)
                        {
                            throw new FormatterNotRegisteredException(type.FullName + " is not registered in this resolver. resolvers:" + string.Join(", ", innerResolvers.Select(x => x.GetType().Name).ToArray()));
                        }

                        var t = type;
                        {
                            var dm = new DynamicMethod("Serialize", null, new[] { typeof(object), typeof(JsonWriter).MakeByRefType(), typeof(object), typeof(IJsonFormatterResolver) }, type.Module, true);
                            var il = dm.GetILGenerator();

                            // delegate void SerializeMethod(object dynamicFormatter, ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver);

                            il.EmitLdarg(0);
                            il.Emit(OpCodes.Castclass, typeof(IJsonFormatter<>).MakeGenericType(t));
                            il.EmitLdarg(1);
                            il.EmitLdarg(2);
                            il.EmitUnboxOrCast(t);
                            il.EmitLdarg(3);

                            il.EmitCall(Resolvers.Internal.DynamicObjectTypeBuilder.EmitInfo.Serialize(t));

                            il.Emit(OpCodes.Ret);

                            formatterAndDelegate = new KeyValuePair<object, SerializeMethod>(formatter, (SerializeMethod)dm.CreateDelegate(typeof(SerializeMethod)));
                        }

                        serializers.TryAdd(t, formatterAndDelegate);
                    }
                }
            }

            formatterAndDelegate.Value(formatterAndDelegate.Key, ref writer, value, formatterResolver);
        }

        public object Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return PrimitiveObjectFormatter.Default.Deserialize(ref reader, formatterResolver);
        }
    }
}