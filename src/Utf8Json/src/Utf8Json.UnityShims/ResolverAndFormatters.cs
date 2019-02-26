#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace Utf8Json.Unity
{
    using System;

    public class UnityResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new UnityResolver();

        UnityResolver()
        {

        }

        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var f = UnityResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class UnityResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static UnityResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(7)
            {
                {typeof(global::UnityEngine.Vector2), 0 },
                {typeof(global::UnityEngine.Vector3), 1 },
                {typeof(global::UnityEngine.Vector4), 2 },
                {typeof(global::UnityEngine.Quaternion), 3 },
                {typeof(global::UnityEngine.Color), 4 },
                {typeof(global::UnityEngine.Bounds), 5 },
                {typeof(global::UnityEngine.Rect), 6 },
                {typeof(global::UnityEngine.Vector2[]), 7 },
                {typeof(global::UnityEngine.Vector3[]), 8 },
                {typeof(global::UnityEngine.Vector4[]), 9 },
                {typeof(global::UnityEngine.Quaternion[]), 10 },
                {typeof(global::UnityEngine.Color[]), 11 },
                {typeof(global::UnityEngine.Bounds[]), 12 },
                {typeof(global::UnityEngine.Rect[]), 13 },
                {typeof(global::UnityEngine.Vector2?), 14 },
                {typeof(global::UnityEngine.Vector3?), 15 },
                {typeof(global::UnityEngine.Vector4?), 16 },
                {typeof(global::UnityEngine.Quaternion?), 17 },
                {typeof(global::UnityEngine.Color?), 18 },
                {typeof(global::UnityEngine.Bounds?), 19 },
                {typeof(global::UnityEngine.Rect?), 20 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new Utf8Json.Unity.Vector2Formatter();
                case 1: return new Utf8Json.Unity.Vector3Formatter();
                case 2: return new Utf8Json.Unity.Vector4Formatter();
                case 3: return new Utf8Json.Unity.QuaternionFormatter();
                case 4: return new Utf8Json.Unity.ColorFormatter();
                case 5: return new Utf8Json.Unity.BoundsFormatter();
                case 6: return new Utf8Json.Unity.RectFormatter();
                case 7: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Vector2>();
                case 8: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Vector3>();
                case 9: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Vector4>();
                case 10: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Quaternion>();
                case 11: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Color>();
                case 12: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Bounds>();
                case 13: return new Utf8Json.Formatters.ArrayFormatter<UnityEngine.Rect>();
                case 14: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Vector2>(new Utf8Json.Unity.Vector2Formatter());
                case 15: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Vector3>(new Utf8Json.Unity.Vector3Formatter());
                case 16: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Vector4>(new Utf8Json.Unity.Vector4Formatter());
                case 17: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Quaternion>(new Utf8Json.Unity.QuaternionFormatter());
                case 18: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Color>(new Utf8Json.Unity.ColorFormatter());
                case 19: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Bounds>(new Utf8Json.Unity.BoundsFormatter());
                case 20: return new Utf8Json.Formatters.StaticNullableFormatter<UnityEngine.Rect>(new Utf8Json.Unity.RectFormatter());
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace Utf8Json.Unity
{
    using System;
    using Utf8Json;


    public sealed class Vector2Formatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Vector2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public Vector2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("x"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("y"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("x"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("y"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Vector2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.x);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.y);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Vector2 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __x__ = default(float);
            var __y__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __x__ = reader.ReadSingle();
                        break;
                    case 1:
                        __y__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Vector2(__x__, __y__);
            ____result.x = __x__;
            ____result.y = __y__;

            return ____result;
        }
    }


    public sealed class Vector3Formatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Vector3>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public Vector3Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("x"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("y"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("z"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("x"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("y"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("z"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Vector3 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.x);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.y);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.z);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Vector3 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __x__ = default(float);
            var __y__ = default(float);
            var __z__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __x__ = reader.ReadSingle();
                        break;
                    case 1:
                        __y__ = reader.ReadSingle();
                        break;
                    case 2:
                        __z__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Vector3(__x__, __y__, __z__);
            ____result.x = __x__;
            ____result.y = __y__;
            ____result.z = __z__;

            return ____result;
        }
    }


    public sealed class Vector4Formatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Vector4>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public Vector4Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("x"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("y"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("z"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("w"), 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("x"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("y"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("z"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("w"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Vector4 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.x);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.y);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.z);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.w);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Vector4 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __x__ = default(float);
            var __y__ = default(float);
            var __z__ = default(float);
            var __w__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __x__ = reader.ReadSingle();
                        break;
                    case 1:
                        __y__ = reader.ReadSingle();
                        break;
                    case 2:
                        __z__ = reader.ReadSingle();
                        break;
                    case 3:
                        __w__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Vector4(__x__, __y__, __z__, __w__);
            ____result.x = __x__;
            ____result.y = __y__;
            ____result.z = __z__;
            ____result.w = __w__;

            return ____result;
        }
    }


    public sealed class QuaternionFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Quaternion>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public QuaternionFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("x"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("y"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("z"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("w"), 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("x"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("y"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("z"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("w"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Quaternion value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.x);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.y);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.z);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.w);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Quaternion Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __x__ = default(float);
            var __y__ = default(float);
            var __z__ = default(float);
            var __w__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __x__ = reader.ReadSingle();
                        break;
                    case 1:
                        __y__ = reader.ReadSingle();
                        break;
                    case 2:
                        __z__ = reader.ReadSingle();
                        break;
                    case 3:
                        __w__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Quaternion(__x__, __y__, __z__, __w__);
            ____result.x = __x__;
            ____result.y = __y__;
            ____result.z = __z__;
            ____result.w = __w__;

            return ____result;
        }
    }


    public sealed class ColorFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Color>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ColorFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("r"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("g"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("b"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("a"), 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("r"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("g"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("b"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("a"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Color value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.r);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.g);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.b);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.a);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Color Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __r__ = default(float);
            var __g__ = default(float);
            var __b__ = default(float);
            var __a__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __r__ = reader.ReadSingle();
                        break;
                    case 1:
                        __g__ = reader.ReadSingle();
                        break;
                    case 2:
                        __b__ = reader.ReadSingle();
                        break;
                    case 3:
                        __a__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Color(__r__, __g__, __b__, __a__);
            ____result.r = __r__;
            ____result.g = __g__;
            ____result.b = __b__;
            ____result.a = __a__;

            return ____result;
        }
    }


    public sealed class BoundsFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Bounds>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public BoundsFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("center"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("size"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("center"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("size"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Bounds value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref writer, value.center, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref writer, value.size, formatterResolver);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Bounds Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __center__ = default(global::UnityEngine.Vector3);
            var __size__ = default(global::UnityEngine.Vector3);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __center__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        __size__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Bounds(__center__, __size__);
            ____result.center = __center__;
            ____result.size = __size__;

            return ____result;
        }
    }


    public sealed class RectFormatter : global::Utf8Json.IJsonFormatter<global::UnityEngine.Rect>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public RectFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("x"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("y"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("width"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("height"), 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("x"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("y"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("width"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("height"),

            };
        }

        public void Serialize(ref JsonWriter writer, global::UnityEngine.Rect value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {


            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSingle(value.x);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteSingle(value.y);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.width);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteSingle(value.height);

            writer.WriteEndObject();
        }

        public global::UnityEngine.Rect Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }


            var __x__ = default(float);
            var __y__ = default(float);
            var __width__ = default(float);
            var __height__ = default(float);

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __x__ = reader.ReadSingle();
                        break;
                    case 1:
                        __y__ = reader.ReadSingle();
                        break;
                    case 2:
                        __width__ = reader.ReadSingle();
                        break;
                    case 3:
                        __height__ = reader.ReadSingle();
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::UnityEngine.Rect(__x__, __y__, __width__, __height__);
            ____result.x = __x__;
            ____result.y = __y__;
            ____result.width = __width__;
            ____result.height = __height__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
