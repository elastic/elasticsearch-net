#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace Utf8Json.Resolvers
{
    using System;
    using Utf8Json;

    public class GeneratedResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
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
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(12)
            {
                {typeof(global::Hoge), 0 },
                {typeof(global::MyPerson), 1 },
                {typeof(global::Hoge2), 2 },
                {typeof(global::Person), 3 },
                {typeof(global::Person2), 4 },
                {typeof(global::IInterface), 5 },
                {typeof(global::MyClassInter), 6 },
                {typeof(global::SimplePerson), 7 },
                {typeof(global::SimplePersonMsgpack), 8 },
                {typeof(global::Utf8Json.Formatters.TargetClassContractless), 9 },
                {typeof(global::Utf8Json.Formatters.LongUnion), 10 },
                {typeof(global::Utf8Json.Formatters.TargetClass), 11 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new Utf8Json.Formatters.HogeFormatter();
                case 1: return new Utf8Json.Formatters.MyPersonFormatter();
                case 2: return new Utf8Json.Formatters.Hoge2Formatter();
                case 3: return new Utf8Json.Formatters.PersonFormatter();
                case 4: return new Utf8Json.Formatters.Person2Formatter();
                case 5: return new Utf8Json.Formatters.IInterfaceFormatter();
                case 6: return new Utf8Json.Formatters.MyClassInterFormatter();
                case 7: return new Utf8Json.Formatters.SimplePersonFormatter();
                case 8: return new Utf8Json.Formatters.SimplePersonMsgpackFormatter();
                case 9: return new Utf8Json.Formatters.Utf8Json.Formatters.TargetClassContractlessFormatter();
                case 10: return new Utf8Json.Formatters.Utf8Json.Formatters.LongUnionFormatter();
                case 11: return new Utf8Json.Formatters.Utf8Json.Formatters.TargetClassFormatter();
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
#pragma warning disable 219
#pragma warning disable 168

namespace Utf8Json.Formatters
{
    using System;
    using Utf8Json;


    public sealed class HogeFormatter : global::Utf8Json.IJsonFormatter<global::Hoge>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public HogeFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("name"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Hoge value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.name);
            
            writer.WriteEndObject();
        }

        public global::Hoge Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Name__ = default(string);
            var __Name__b__ = false;
            var __name__ = default(string);
            var __name__b__ = false;

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
                        __Name__ = reader.ReadString();
                        __Name__b__ = true;
                        break;
                    case 1:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Hoge();
            if(__name__b__) ____result.name = __name__;

            return ____result;
        }
    }


    public sealed class MyPersonFormatter : global::Utf8Json.IJsonFormatter<global::MyPerson>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public MyPersonFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Addresses"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Addresses"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::MyPerson value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref writer, value.Addresses, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::MyPerson Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Name__ = default(string);
            var __Name__b__ = false;
            var __Addresses__ = default(string[]);
            var __Addresses__b__ = false;

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
                        __Name__ = reader.ReadString();
                        __Name__b__ = true;
                        break;
                    case 1:
                        __Addresses__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(ref reader, formatterResolver);
                        __Addresses__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::MyPerson();
            if(__Name__b__) ____result.Name = __Name__;
            if(__Addresses__b__) ____result.Addresses = __Addresses__;

            return ____result;
        }
    }


    public sealed class Hoge2Formatter : global::Utf8Json.IJsonFormatter<global::Hoge2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public Hoge2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("_Name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("_Name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("name"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Hoge2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value._Name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.name);
            
            writer.WriteEndObject();
        }

        public global::Hoge2 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var ___Name__ = default(string);
            var ___Name__b__ = false;
            var __name__ = default(string);
            var __name__b__ = false;

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
                        ___Name__ = reader.ReadString();
                        ___Name__b__ = true;
                        break;
                    case 1:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Hoge2(__name__);
            if(__name__b__) ____result.name = __name__;

            return ____result;
        }
    }


    public sealed class PersonFormatter : global::Utf8Json.IJsonFormatter<global::Person>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public PersonFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Age"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Age"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Name"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Person value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.Age);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.Name);
            
            writer.WriteEndObject();
        }

        public global::Person Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Age__ = default(int);
            var __Age__b__ = false;
            var __Name__ = default(string);
            var __Name__b__ = false;

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
                        __Age__ = reader.ReadInt32();
                        __Age__b__ = true;
                        break;
                    case 1:
                        __Name__ = reader.ReadString();
                        __Name__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Person();
            if(__Age__b__) ____result.Age = __Age__;
            if(__Name__b__) ____result.Name = __Name__;

            return ____result;
        }
    }


    public sealed class Person2Formatter : global::Utf8Json.IJsonFormatter<global::Person2>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public Person2Formatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Age"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Name"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Age"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Name"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Person2 value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<int?>().Serialize(ref writer, value.Age, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.Name);
            
            writer.WriteEndObject();
        }

        public global::Person2 Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Age__ = default(int?);
            var __Age__b__ = false;
            var __Name__ = default(string);
            var __Name__b__ = false;

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
                        __Age__ = formatterResolver.GetFormatterWithVerify<int?>().Deserialize(ref reader, formatterResolver);
                        __Age__b__ = true;
                        break;
                    case 1:
                        __Name__ = reader.ReadString();
                        __Name__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Person2();
            if(__Age__b__) ____result.Age = __Age__;
            if(__Name__b__) ____result.Name = __Name__;

            return ____result;
        }
    }


    public sealed class IInterfaceFormatter : global::Utf8Json.IJsonFormatter<global::IInterface>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public IInterfaceFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Huga"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Huga"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::IInterface value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Huga);
            
            writer.WriteEndObject();
        }

        public global::IInterface Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            
            
	        throw new InvalidOperationException("generated serializer for IInterface does not support deserialize.");
        }
    }


    public sealed class MyClassInterFormatter : global::Utf8Json.IJsonFormatter<global::MyClassInter>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public MyClassInterFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Huga"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Huga"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::MyClassInter value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.Huga);
            
            writer.WriteEndObject();
        }

        public global::MyClassInter Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Huga__ = default(string);
            var __Huga__b__ = false;

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
                        __Huga__ = reader.ReadString();
                        __Huga__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::MyClassInter();
            if(__Huga__b__) ____result.Huga = __Huga__;

            return ____result;
        }
    }


    public sealed class SimplePersonFormatter : global::Utf8Json.IJsonFormatter<global::SimplePerson>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SimplePersonFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("i_do	nt_know"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("FirstName"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("FavoriteFruit"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("i_do	nt_know"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FirstName"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FavoriteFruit"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::SimplePerson value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.Age);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.FirstName);
            writer.WriteRaw(this.____stringByteKeys[2]);
            formatterResolver.GetFormatterWithVerify<global::MyEnum>().Serialize(ref writer, value.FavoriteFruit, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::SimplePerson Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Age__ = default(int);
            var __Age__b__ = false;
            var __FirstName__ = default(string);
            var __FirstName__b__ = false;
            var __FavoriteFruit__ = default(global::MyEnum);
            var __FavoriteFruit__b__ = false;

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
                        __Age__ = reader.ReadInt32();
                        __Age__b__ = true;
                        break;
                    case 1:
                        __FirstName__ = reader.ReadString();
                        __FirstName__b__ = true;
                        break;
                    case 2:
                        __FavoriteFruit__ = formatterResolver.GetFormatterWithVerify<global::MyEnum>().Deserialize(ref reader, formatterResolver);
                        __FavoriteFruit__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::SimplePerson();
            if(__Age__b__) ____result.Age = __Age__;
            if(__FirstName__b__) ____result.FirstName = __FirstName__;
            if(__FavoriteFruit__b__) ____result.FavoriteFruit = __FavoriteFruit__;

            return ____result;
        }
    }


    public sealed class SimplePersonMsgpackFormatter : global::Utf8Json.IJsonFormatter<global::SimplePersonMsgpack>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SimplePersonMsgpackFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Age"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("FirstName"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("LastName"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Age"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("FirstName"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("LastName"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::SimplePersonMsgpack value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.Age);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.FirstName);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.LastName);
            
            writer.WriteEndObject();
        }

        public global::SimplePersonMsgpack Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Age__ = default(int);
            var __Age__b__ = false;
            var __FirstName__ = default(string);
            var __FirstName__b__ = false;
            var __LastName__ = default(string);
            var __LastName__b__ = false;

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
                        __Age__ = reader.ReadInt32();
                        __Age__b__ = true;
                        break;
                    case 1:
                        __FirstName__ = reader.ReadString();
                        __FirstName__b__ = true;
                        break;
                    case 2:
                        __LastName__ = reader.ReadString();
                        __LastName__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::SimplePersonMsgpack();
            if(__Age__b__) ____result.Age = __Age__;
            if(__FirstName__b__) ____result.FirstName = __FirstName__;
            if(__LastName__b__) ____result.LastName = __LastName__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Utf8Json.Formatters.Utf8Json.Formatters
{
    using System;
    using Utf8Json;


    public sealed class TargetClassContractlessFormatter : global::Utf8Json.IJsonFormatter<global::Utf8Json.Formatters.TargetClassContractless>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TargetClassContractlessFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number1"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number2"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number3"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number4"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number5"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number6"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number7"), 6},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number8"), 7},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Str"), 8},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Array"), 9},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Number1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number6"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number7"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number8"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Str"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Array"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Utf8Json.Formatters.TargetClassContractless value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSByte(value.Number1);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt16(value.Number2);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteInt32(value.Number3);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt64(value.Number4);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteByte(value.Number5);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteUInt16(value.Number6);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteUInt32(value.Number7);
            writer.WriteRaw(this.____stringByteKeys[7]);
            writer.WriteUInt64(value.Number8);
            writer.WriteRaw(this.____stringByteKeys[8]);
            writer.WriteString(value.Str);
            writer.WriteRaw(this.____stringByteKeys[9]);
            formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.Array, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::Utf8Json.Formatters.TargetClassContractless Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Number1__ = default(sbyte);
            var __Number1__b__ = false;
            var __Number2__ = default(short);
            var __Number2__b__ = false;
            var __Number3__ = default(int);
            var __Number3__b__ = false;
            var __Number4__ = default(long);
            var __Number4__b__ = false;
            var __Number5__ = default(byte);
            var __Number5__b__ = false;
            var __Number6__ = default(ushort);
            var __Number6__b__ = false;
            var __Number7__ = default(uint);
            var __Number7__b__ = false;
            var __Number8__ = default(ulong);
            var __Number8__b__ = false;
            var __Str__ = default(string);
            var __Str__b__ = false;
            var __Array__ = default(int[]);
            var __Array__b__ = false;

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
                        __Number1__ = reader.ReadSByte();
                        __Number1__b__ = true;
                        break;
                    case 1:
                        __Number2__ = reader.ReadInt16();
                        __Number2__b__ = true;
                        break;
                    case 2:
                        __Number3__ = reader.ReadInt32();
                        __Number3__b__ = true;
                        break;
                    case 3:
                        __Number4__ = reader.ReadInt64();
                        __Number4__b__ = true;
                        break;
                    case 4:
                        __Number5__ = reader.ReadByte();
                        __Number5__b__ = true;
                        break;
                    case 5:
                        __Number6__ = reader.ReadUInt16();
                        __Number6__b__ = true;
                        break;
                    case 6:
                        __Number7__ = reader.ReadUInt32();
                        __Number7__b__ = true;
                        break;
                    case 7:
                        __Number8__ = reader.ReadUInt64();
                        __Number8__b__ = true;
                        break;
                    case 8:
                        __Str__ = reader.ReadString();
                        __Str__b__ = true;
                        break;
                    case 9:
                        __Array__ = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, formatterResolver);
                        __Array__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Utf8Json.Formatters.TargetClassContractless();
            if(__Number1__b__) ____result.Number1 = __Number1__;
            if(__Number2__b__) ____result.Number2 = __Number2__;
            if(__Number3__b__) ____result.Number3 = __Number3__;
            if(__Number4__b__) ____result.Number4 = __Number4__;
            if(__Number5__b__) ____result.Number5 = __Number5__;
            if(__Number6__b__) ____result.Number6 = __Number6__;
            if(__Number7__b__) ____result.Number7 = __Number7__;
            if(__Number8__b__) ____result.Number8 = __Number8__;
            if(__Str__b__) ____result.Str = __Str__;
            if(__Array__b__) ____result.Array = __Array__;

            return ____result;
        }
    }


    public sealed class LongUnionFormatter : global::Utf8Json.IJsonFormatter<global::Utf8Json.Formatters.LongUnion>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public LongUnionFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Int1"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Int2"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Float"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Double"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Long"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Int1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Int2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Float"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Double"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Long"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Utf8Json.Formatters.LongUnion value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.Int1);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt32(value.Int2);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteSingle(value.Float);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteDouble(value.Double);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteUInt64(value.Long);
            
            writer.WriteEndObject();
        }

        public global::Utf8Json.Formatters.LongUnion Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __Int1__ = default(int);
            var __Int1__b__ = false;
            var __Int2__ = default(int);
            var __Int2__b__ = false;
            var __Float__ = default(float);
            var __Float__b__ = false;
            var __Double__ = default(double);
            var __Double__b__ = false;
            var __Long__ = default(ulong);
            var __Long__b__ = false;

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
                        __Int1__ = reader.ReadInt32();
                        __Int1__b__ = true;
                        break;
                    case 1:
                        __Int2__ = reader.ReadInt32();
                        __Int2__b__ = true;
                        break;
                    case 2:
                        __Float__ = reader.ReadSingle();
                        __Float__b__ = true;
                        break;
                    case 3:
                        __Double__ = reader.ReadDouble();
                        __Double__b__ = true;
                        break;
                    case 4:
                        __Long__ = reader.ReadUInt64();
                        __Long__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Utf8Json.Formatters.LongUnion();
            if(__Int1__b__) ____result.Int1 = __Int1__;
            if(__Int2__b__) ____result.Int2 = __Int2__;
            if(__Float__b__) ____result.Float = __Float__;
            if(__Double__b__) ____result.Double = __Double__;
            if(__Long__b__) ____result.Long = __Long__;

            return ____result;
        }
    }


    public sealed class TargetClassFormatter : global::Utf8Json.IJsonFormatter<global::Utf8Json.Formatters.TargetClass>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TargetClassFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number1"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number2"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number3"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number4"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number5"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number6"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number7"), 6},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Number8"), 7},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Str"), 8},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("Array"), 9},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Number1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number6"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number7"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Number8"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Str"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Array"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::Utf8Json.Formatters.TargetClass value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteSByte(value.Number1);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt16(value.Number2);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteInt32(value.Number3);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt64(value.Number4);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteByte(value.Number5);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteUInt16(value.Number6);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteUInt32(value.Number7);
            writer.WriteRaw(this.____stringByteKeys[7]);
            writer.WriteUInt64(value.Number8);
            writer.WriteRaw(this.____stringByteKeys[8]);
            writer.WriteString(value.Str);
            writer.WriteRaw(this.____stringByteKeys[9]);
            formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.Array, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::Utf8Json.Formatters.TargetClass Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __Number1__ = default(sbyte);
            var __Number1__b__ = false;
            var __Number2__ = default(short);
            var __Number2__b__ = false;
            var __Number3__ = default(int);
            var __Number3__b__ = false;
            var __Number4__ = default(long);
            var __Number4__b__ = false;
            var __Number5__ = default(byte);
            var __Number5__b__ = false;
            var __Number6__ = default(ushort);
            var __Number6__b__ = false;
            var __Number7__ = default(uint);
            var __Number7__b__ = false;
            var __Number8__ = default(ulong);
            var __Number8__b__ = false;
            var __Str__ = default(string);
            var __Str__b__ = false;
            var __Array__ = default(int[]);
            var __Array__b__ = false;

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
                        __Number1__ = reader.ReadSByte();
                        __Number1__b__ = true;
                        break;
                    case 1:
                        __Number2__ = reader.ReadInt16();
                        __Number2__b__ = true;
                        break;
                    case 2:
                        __Number3__ = reader.ReadInt32();
                        __Number3__b__ = true;
                        break;
                    case 3:
                        __Number4__ = reader.ReadInt64();
                        __Number4__b__ = true;
                        break;
                    case 4:
                        __Number5__ = reader.ReadByte();
                        __Number5__b__ = true;
                        break;
                    case 5:
                        __Number6__ = reader.ReadUInt16();
                        __Number6__b__ = true;
                        break;
                    case 6:
                        __Number7__ = reader.ReadUInt32();
                        __Number7__b__ = true;
                        break;
                    case 7:
                        __Number8__ = reader.ReadUInt64();
                        __Number8__b__ = true;
                        break;
                    case 8:
                        __Str__ = reader.ReadString();
                        __Str__b__ = true;
                        break;
                    case 9:
                        __Array__ = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, formatterResolver);
                        __Array__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::Utf8Json.Formatters.TargetClass();
            if(__Number1__b__) ____result.Number1 = __Number1__;
            if(__Number2__b__) ____result.Number2 = __Number2__;
            if(__Number3__b__) ____result.Number3 = __Number3__;
            if(__Number4__b__) ____result.Number4 = __Number4__;
            if(__Number5__b__) ____result.Number5 = __Number5__;
            if(__Number6__b__) ____result.Number6 = __Number6__;
            if(__Number7__b__) ____result.Number7 = __Number7__;
            if(__Number8__b__) ____result.Number8 = __Number8__;
            if(__Str__b__) ____result.Str = __Str__;
            if(__Array__b__) ____result.Array = __Array__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
