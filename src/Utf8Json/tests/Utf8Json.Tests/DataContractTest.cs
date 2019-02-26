using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Utf8Json.Tests
{



    public class DataContractTest
    {
        [DataContract]
        public class MyClass1
        {
            [DataMember(Name = "mp1")]
            public int MyProperty1 { get; set; }
            [DataMember(Name = "mp2")]
            public string MyProperty2;
        }

        [DataContract]
        public class MyClass2
        {
            [DataMember]
            public int MyProperty1 { get; set; }
            [DataMember]
            public string MyProperty2;           
            public string MyProperty3 { get; set; }
        }
        
        public class MyClass3
        {
            [DataMember]
            public int MyProperty1 { get; set; }          
            public string MyProperty2 { get; set; }          
            [DataMember]
            public string MyProperty3;
        }
        
        [InterfaceDataContract]
        public interface IMyClass4
        {
            [DataMember]
            int MyProperty1 { get; set; }  
            
            string MyProperty2 { get; set; }
        }

        public class MyClass4 : IMyClass4
        {
            public int MyProperty1 { get; set; }
            [DataMember]
            public string MyProperty2 { get; set; }
        }
        
        [DataContract]
        public class MyOtherClass4 : IMyClass4
        {
            public int MyProperty1 { get; set; }
            [DataMember]
            public string MyProperty2 { get; set; }
        }

        [Fact]
        public void SerializeDataMemberName()
        {
            var mc = new MyClass1 { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = JsonSerializer.Serialize(mc);
            Encoding.UTF8.GetString(bin).Is(@"{""mp1"":100,""mp2"":""foobar""}");

            var mc2 = JsonSerializer.Deserialize<MyClass1>(bin);
            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);
        }

        [Fact]
        public void SerializeOnlyDataMemberWhenDataContract()
        {
            var mc = new MyClass2 { MyProperty1 = 100, MyProperty2 = "foobar", MyProperty3 = "baz" };

            var bin = JsonSerializer.Serialize(mc);
            Encoding.UTF8.GetString(bin).Is(@"{""MyProperty1"":100,""MyProperty2"":""foobar""}");
            
            var mc2 = JsonSerializer.Deserialize<MyClass2>(bin);
            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);
            mc.MyProperty3.IsNot(mc2.MyProperty3);
            mc2.MyProperty3.Is(null);
        }
        
        [Fact]
        public void Serialize()
        {
            var mc = new MyClass3 { MyProperty1 = 100, MyProperty2 = "foobar", MyProperty3 = "baz" };

            var bin = JsonSerializer.Serialize(mc);
            Encoding.UTF8.GetString(bin).Is(@"{""MyProperty1"":100,""MyProperty2"":""foobar"",""MyProperty3"":""baz""}");

            var mc2 = JsonSerializer.Deserialize<MyClass3>(bin);
            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);
            mc.MyProperty3.Is(mc2.MyProperty3);
        }

        [Fact]
        public void SerializeInterfaceOnlyDataMemberWhenInterfaceDataContract()
        {
            IMyClass4 mc = new MyClass4 { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = JsonSerializer.Serialize(mc);
            Encoding.UTF8.GetString(bin).Is(@"{""MyProperty1"":100}");
            
            var mc2 = JsonSerializer.Deserialize<MyClass4>(bin);
            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.IsNot(mc2.MyProperty2);
            mc2.MyProperty2.Is(null);
        }
        
        [Fact]
        public void SerializeConcreteOnlyDataMemberWhenDataContract()
        {
            var mc = new MyOtherClass4 { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = JsonSerializer.Serialize(mc);
            Encoding.UTF8.GetString(bin).Is(@"{""MyProperty2"":""foobar""}");
            
            var mc2 = JsonSerializer.Deserialize<MyOtherClass4>(bin);
            mc.MyProperty1.IsNot(mc2.MyProperty1);
            mc2.MyProperty1.Is(0);
            mc.MyProperty2.Is(mc2.MyProperty2);
        }
    }

}
