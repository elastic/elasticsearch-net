using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;
using Utf8Json.Resolvers.Internal;

namespace DynamicCodeDumper
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //DynamicObjectResolver.Default.GetFormatter<TestShouldSerialize>();
                //DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<TestShouldSerialize>();

                //var seq = Enumerable.Range(1, 10).Select(x => x);
                //DynamicObjectResolver.Default.GetFormatterDynamic(seq.GetType());


                //var yahhoi = CompositeResolver.Create(new IJsonFormatter[0], new[] { StandardResolver.Default });

                DynamicObjectResolver.ExcludeNull.GetFormatter<System.Exception>();

                //DynamicObjectResolver.Default.GetFormatter<System.Collections.ICollection>();
                //DynamicObjectResolver.Default.GetFormatter<Test2>();
                //DynamicObjectResolver.Default.GetFormatter<TargetClassContractless>();
                //var test = DynamicObjectResolver.Default.GetFormatter<Person>();
                //DynamicObjectResolver.Default.GetFormatter<IInterface>();
                //DynamicObjectResolver.Default.GetFormatter<NoSideEffectFreePattern1>();
                //DynamicObjectResolver.Default.GetFormatter<NoSideEffectFreePattern2>();
                //DynamicObjectResolver.Default.GetFormatter<NoSideEffectStructPattern>();



                //DynamicObjectResolver.ExcludeNull.GetFormatter<Nullaer>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

#if NET45
                var a1 = (DynamicObjectResolver.Default as ISave).Save();
                var a2 = (DynamicObjectResolver.ExcludeNull as ISave).Save();
                //var a2 = DynamicUnionResolver.Instance.Save();
                //var a3 = DynamicEnumResolver.Instance.Save();
                //var a4 = DynamicContractlessObjectResolver.Instance.Save();
                var a3 = DynamicCompositeResolver.Save();

                Verify(a2);
#endif
            }
        }

        static void Verify(params AssemblyBuilder[] builders)
        {
            var path = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\x64\PEVerify.exe";

            foreach (var targetDll in builders)
            {
                var psi = new ProcessStartInfo(path, targetDll.GetName().Name + ".dll")
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                var p = Process.Start(psi);
                var data = p.StandardOutput.ReadToEnd();
                Console.WriteLine(data);
            }
        }
    }

    public class HogeHogeHoge : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class ConceptFormatter : IJsonFormatter<HogeHogeHoge>
    {
        public HogeHogeHoge Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, HogeHogeHoge value, IJsonFormatterResolver formatterResolver)
        {
            formatterResolver.GetFormatterWithVerify<IEnumerable<int>>().Serialize(ref writer, value, formatterResolver);
        }
    }

    public class Contractless
    {
        public Contractless()
        {

        }

        //public Contractless(string b, int p3)
        //{
        //    P3 = p3;
        //}

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

    public class InvalidCtor
    {
        public InvalidCtor(int x)
        {

        }
    }

    public class Nullaer
    {
        public int? MyProperty1 { get; set; }
        public char? MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
    }


    public class NoSideEffectFreePattern1
    {
        public bool B = true;
    }
    public class EmptyBase
    {

    }

    public class NoSideEffectFreePattern2 : EmptyBase
    {
        public int X { get; set; }
    }

    public struct NoSideEffectStructPattern
    {
        int x;
        public int y;

        public NoSideEffectStructPattern(int x)
        {
            this.x = x;
            this.y = 99;
        }
    }


    public class TestShouldSerialize
    {
        public int MyProperty { get; set; }
        public List<string> Data { get; set; } = new List<string>();
        public string MyProperty2 { get; set; }
        public long MyProperty3 { get; set; }

        public bool ShouldSerializeData()
        {
            return Data?.Count > 0;
        }

        public bool ShouldSerializeMyProperty3()
        {
            return MyProperty3 > 0;
        }
    }

    public interface IInterface
    {
        string Huga { get; }
    }

    public class MyClassInter : IInterface
    {
        public string Huga { get; set; }
    }


    internal static class CheckCheck
    {
        internal static void SerializeMethod(object dynamicFormatter, ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
        {
            ((IJsonFormatter<Address>)dynamicFormatter).Serialize(ref writer, (Address)value, formatterResolver);
        }
    }

    public class Address
    {
        public string Street { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public object[] /*Address*/ Addresses { get; set; }
    }

    public class PersonSample
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }


    public class Test
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public Test2 MyProperty3 { get; set; }
    }

    public class Test2
    {
        public string MyProperty { get; set; }
    }

    public class TargetClassContractless
    {
        public sbyte Number1 { get; set; }
        public short Number2 { get; set; }
        public int Number3 { get; set; }
        public long Number4 { get; set; }
        public byte Number5 { get; set; }
        public ushort Number6 { get; set; }
        public uint Number7 { get; set; }
        public ulong Number8 { get; set; }
        //public float Number9 { get; set; }
        //public double Number10 { get; set; }
        public string Str { get; set; }
        public int[] Array { get; set; }
    }

    public class SimplePerson
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        [JsonFormatter(typeof(FooFormatter))]
        public string LastName { get; set; }
    }

    public class FooFormatter : IJsonFormatter<int>
    {
        public int Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }
}