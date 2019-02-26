using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class ContractlessStandardResolverTest
    {
        public class Address
        {
            public string Street { get; set; }
        }

        public class Person
        {
            public string Name { get; set; }
            public object[] /*Address*/ Addresses { get; set; }
        }

        public class V1
        {
            public int ABCDEFG1 { get; set; }
            public int ABCDEFG3 { get; set; }
        }



        public class V2
        {
            public int ABCDEFG1 { get; set; }
            public int ABCDEFG2 { get; set; }
            public int ABCDEFG3 { get; set; }
        }

        public class Dup
        {
            public int ABCDEFGH { get; set; }
            public int ABCDEFGHIJKL { get; set; }
        }

        public class BinSearchSmall
        {
            public int MyP1 { get; set; }
            public int MyP2 { get; set; }
            public int MyP3 { get; set; }
            public int MyP4 { get; set; }
            public int MyP5 { get; set; }
            public int MyP6 { get; set; }
            public int MyP7 { get; set; }
            public int MyP8 { get; set; }
            public int MyP9 { get; set; }
        }

        public class BinSearchWithBranch
        {
            public int MyProperty1 { get; set; }
            public int MyProperty2 { get; set; }
            public int MyProperty3 { get; set; }
            public int MyProperty4 { get; set; }
            public int MyProperty5 { get; set; }
            public int MyProperty6 { get; set; }
            public int MyProperty7 { get; set; }
            public int MyProperty8 { get; set; }
            public int MyProperty9 { get; set; }
        }

        public class LongestString
        {
            public int MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1 { get; set; }
            public int MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty2 { get; set; }
            public int MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty2MyProperty { get; set; }
            public int OAFADFZEWFSDFSDFKSLJFWEFNWOZFUSEWWEFWEWFFFFFFFFFFFFFFZFEWBFOWUEGWHOUDGSOGUDSZNOFRWEUFWGOWHOGHWOG000000000000000000000000000000000000000HOGZ { get; set; }
        }

        public class SetOnlyProperty
        {
            public int P3 { get; set; }

            public int P3_testOnlyGet
            {
                get { return P3; }
            }

            public int P3_testOnlySet
            {
                set { P3 = value; }
            }
        }


        public class RecursiveReadNextBlockOverflow
        {
            //public int[] foo { get; set; }
            public int z { get; set; }
        }

        public interface IMyInterface
        {
            int MyProperty { get; set; }
        }

        public abstract class MyAbstract
        {
            public virtual int MyProperty { get; set; }
        }

        public class MyInterfaceNonConstructor : IMyInterface
        {
            [IgnoreDataMember]
            public bool CalledConstructor { get; }

            public int MyProperty { get; set; }

            MyInterfaceNonConstructor()
            {
                this.CalledConstructor = true;
            }

            public static MyInterfaceNonConstructor Create()
            {
                return new MyInterfaceNonConstructor();
            }
        }

        public class MyAbstructNonConstructor : MyAbstract
        {
            [IgnoreDataMember]
            public bool CalledConstructor { get; }

            public override int MyProperty { get; set; }

            MyAbstructNonConstructor()
            {
                this.CalledConstructor = true;
            }

            public static MyAbstructNonConstructor Create()
            {
                return new MyAbstructNonConstructor();
            }
        }

        public class HasIndexer
        {
            public int[] arr { get; set; }

            public int this[int i]
            {
                get { return arr[i]; }
                set { arr[i] = value; }
            }
        }

        public class SomeClass
        {
            public SomeClass()
            {
                AnotherClass = new List<AnotherClass>();
            }

            public List<AnotherClass> AnotherClass { get; set; }
        }

        public class AnotherClass
        {
            public int Foo { get; set; }
        }

        public class SideEffectPattern1
        {
            public bool B = true;
        }
        public class NonEmptyBase
        {
            public int MyProperty { get; set; }

            public NonEmptyBase()
            {
                MyProperty = 9;
            }
        }

        public class SideEffectPattern2 : NonEmptyBase
        {
            public int X { get; set; }
        }

        public struct SideEffectStructPattern
        {
            public int x;
            public int y;

            public SideEffectStructPattern(int x)
            {
                this.x = x;
                this.y = 99;
            }
        }


        public struct EmptyConstructorStruct
        {
            public int X;
        }


        [Fact]
        public void SimpleTest()
        {
            var p = new Person
            {
                Name = "John",
                Addresses = new[]
                {
                        new Address { Street = "St." },
                        new Address { Street = "Ave." }
                    }
            };

            var result = JsonSerializer.Serialize(p);

            JsonSerializer.ToJsonString(p).Is(@"{""Name"":""John"",""Addresses"":[{""Street"":""St.""},{""Street"":""Ave.""}]}");

            var p2 = JsonSerializer.Deserialize<Person>(result);
            p2.Name.Is("John");
            var addresses = p2.Addresses as IList;
            var d1 = addresses[0] as IDictionary;
            var d2 = addresses[1] as IDictionary;
            (d1["Street"] as string).Is("St.");
            (d2["Street"] as string).Is("Ave.");
        }

        [Fact]
        public void Versioning()
        {
            var v1 = JsonSerializer.Serialize(new V1 { ABCDEFG1 = 10, ABCDEFG3 = 99 });
            var v2 = JsonSerializer.Serialize(new V2 { ABCDEFG1 = 350, ABCDEFG2 = 34, ABCDEFG3 = 500 });

            var v1_1 = JsonSerializer.Deserialize<V1>(v1);
            var v1_2 = JsonSerializer.Deserialize<V1>(v2);
            var v2_1 = JsonSerializer.Deserialize<V2>(v1);
            var v2_2 = JsonSerializer.Deserialize<V2>(v2);

            v1_1.ABCDEFG1.Is(10); v1_1.ABCDEFG3.Is(99);
            v1_2.ABCDEFG1.Is(350); v1_2.ABCDEFG3.Is(500);
            v2_1.ABCDEFG1.Is(10); v2_1.ABCDEFG2.Is(0); v2_1.ABCDEFG3.Is(99);
            v2_2.ABCDEFG1.Is(350); v2_2.ABCDEFG2.Is(34); v2_2.ABCDEFG3.Is(500);
        }

        [Fact]
        public void VersioningReadNextOverflow()
        {
            var str = "[" + string.Join(",", Enumerable.Range(1, 1000000).Select(x => x.ToString())) + "]";

            var a = @"{""foo"":" + str + @",""z"":99}";
            var s = Utf8Json.JsonSerializer.Deserialize<RecursiveReadNextBlockOverflow>(a);
            s.z.Is(99);
        }

        [Fact]
        public void NestedArrayVersioning()
        {
            {
                var a = @"[  [[[[1,2,3]],9999]], 10]";
                var reader = new JsonReader(Encoding.UTF8.GetBytes(a));
                reader.ReadIsBeginArray();
                reader.ReadNextBlock();
                reader.ReadIsValueSeparatorWithVerify();
                var i = reader.ReadInt32();
                i.Is(10);
            }
            {
                var a = @"{""foo"":[[[[1,2,3]],9999]],""z"":99}";
                var s = Utf8Json.JsonSerializer.Deserialize<RecursiveReadNextBlockOverflow>(a);
                s.z.Is(99);
            }
        }

        [Fact]
        public void DuplicateAutomata()
        {
            var bin = JsonSerializer.Serialize(new Dup { ABCDEFGH = 10, ABCDEFGHIJKL = 99 });
            var v = JsonSerializer.Deserialize<Dup>(bin);

            v.ABCDEFGH.Is(10);
            v.ABCDEFGHIJKL.Is(99);
        }

        [Fact]
        public void BinSearchSmallCheck()
        {
            var o = new BinSearchSmall
            {
                MyP1 = 1,
                MyP2 = 10,
                MyP3 = 1000,
                MyP4 = 100000,
                MyP5 = 32421,
                MyP6 = 52521,
                MyP7 = 46363631,
                MyP8 = 7373731,
                MyP9 = 73573731,
            };
            var bin = JsonSerializer.Serialize(o);
            var v = JsonSerializer.Deserialize<BinSearchSmall>(bin);

            v.IsStructuralEqual(o);
        }

        [Fact]
        public void BinSearchWithBranchCheck()
        {
            var o = new BinSearchWithBranch
            {
                MyProperty1 = 1,
                MyProperty2 = 10,
                MyProperty3 = 1000,
                MyProperty4 = 100000,
                MyProperty5 = 32421,
                MyProperty6 = 52521,
                MyProperty7 = 46363631,
                MyProperty8 = 7373731,
                MyProperty9 = 73573731,
            };
            var bin = JsonSerializer.Serialize(o);
            var v = JsonSerializer.Deserialize<BinSearchWithBranch>(bin);

            v.IsStructuralEqual(o);
        }

        [Fact]
        public void LongestStringCheck()
        {
            var o = new LongestString
            {
                MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1 = 431413,
                MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty2 = 352525252,
                MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty1MyProperty2MyProperty = 532525252,
                OAFADFZEWFSDFSDFKSLJFWEFNWOZFUSEWWEFWEWFFFFFFFFFFFFFFZFEWBFOWUEGWHOUDGSOGUDSZNOFRWEUFWGOWHOGHWOG000000000000000000000000000000000000000HOGZ = 3352666,
            };
            var bin = JsonSerializer.Serialize(o);
            var v = JsonSerializer.Deserialize<LongestString>(bin);

            v.IsStructuralEqual(o);
        }

        [Fact]
        public void UninitializedObjectCreation()
        {
            {
                var obj = MyAbstructNonConstructor.Create();
                obj.MyProperty = 999;
                obj.CalledConstructor.IsTrue();

                var bin = JsonSerializer.Serialize(obj, StandardResolver.AllowPrivate);

                var d = JsonSerializer.Deserialize<MyAbstructNonConstructor>(bin, StandardResolver.AllowPrivate);
                d.CalledConstructor.IsFalse();
                d.MyProperty = 999;

                Assert.Throws<InvalidOperationException>(() => JsonSerializer.Deserialize<MyAbstract>(bin, StandardResolver.AllowPrivate));
            }
            {
                var obj = MyInterfaceNonConstructor.Create();
                obj.MyProperty = 999;
                obj.CalledConstructor.IsTrue();

                var bin = JsonSerializer.Serialize(obj, StandardResolver.AllowPrivate);

                var d = JsonSerializer.Deserialize<MyInterfaceNonConstructor>(bin, StandardResolver.AllowPrivate);
                d.CalledConstructor.IsFalse();
                d.MyProperty = 999;

                Assert.Throws<InvalidOperationException>(() => JsonSerializer.Deserialize<IMyInterface>(bin, StandardResolver.AllowPrivate));
            }
        }

        [Fact]
        public void SerializeIndexer()
        {
            var indexer = new HasIndexer() { arr = new[] { 1, 10, 100 } };
            var bin = JsonSerializer.Serialize(indexer);
            var re = JsonSerializer.Deserialize<HasIndexer>(bin);
            re.arr.Is(1, 10, 100);
        }

        [Fact]
        public void DeserializeSideEffectWithVersioned()
        {
            string jsonString = "[{}]";
            var list = new List<SomeClass>() { new SomeClass() };
            list[0].AnotherClass.Count.Is(0);
            var list2 = JsonSerializer.Deserialize<List<SomeClass>>(jsonString);
            list2[0].AnotherClass.Count.Is(0);
        }

        [Fact]
        public void DeserializeSideEffectWithVersioned123()
        {
            string jsonString = "{}";

            var s1 = JsonSerializer.Deserialize<SideEffectPattern1>(jsonString);
            var s2 = JsonSerializer.Deserialize<SideEffectPattern2>(jsonString);
            var s3 = JsonSerializer.Deserialize<SideEffectStructPattern>(jsonString);

            s1.B.IsTrue();
            s2.MyProperty.Is(9);
            s3.y.Is(99);
        }

        [Fact]
        public void SetOnlyPropertyTest()
        {
            var p = JsonSerializer.Deserialize<SetOnlyProperty>("{\"P3\":99}");
            p.P3.Is(99);
        }

        [Fact]
        public void Empty()
        {
            var x = JsonSerializer.Serialize(new EmptyConstructorStruct { X = 99 }, StandardResolver.AllowPrivate);
            JsonSerializer.Deserialize<EmptyConstructorStruct>(x, StandardResolver.AllowPrivate).X.Is(99);
        }
    }
}
