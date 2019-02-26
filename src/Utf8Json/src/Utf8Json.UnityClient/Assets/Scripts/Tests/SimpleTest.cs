using UnityEngine;
using RuntimeUnitTestToolkit;
using System.Collections;

using System.Collections.Generic;
using System;
using Utf8Json;
using System.Text;
using System.Linq;

namespace Utf8Json.UnityClient.Tests
{
    //public class TestObject
    //{
    //    public class PrimitiveObject
    //    {
    //        public int v_int;

    //        public string v_str;

    //        public float v_float;

    //        public bool v_bool;
    //        public PrimitiveObject(int vi, string vs, float vf, bool vb)
    //        {
    //            v_int = vi; v_str = vs; v_float = vf; v_bool = vb;
    //        }
    //    }

    //    [Key(0)]
    //    public PrimitiveObject[] objectArray;

    //    [Key(1)]
    //    public List<PrimitiveObject> objectList;

    //    [Key(2)]
    //    public Dictionary<string, PrimitiveObject> objectMap;

    //    public void CreateArray(int num)
    //    {
    //        objectArray = new PrimitiveObject[num];
    //        for (int i = 0; i < num; i++)
    //        {
    //            objectArray[i] = new PrimitiveObject(i, i.ToString(), (float)i, i % 2 == 0 ? true : false);
    //        }
    //    }

    //    public void CreateList(int num)
    //    {
    //        objectList = new List<PrimitiveObject>(num);
    //        for (int i = 0; i < num; i++)
    //        {
    //            objectList.Add(new PrimitiveObject(i, i.ToString(), (float)i, i % 2 == 0 ? true : false));
    //        }
    //    }

    //    public void CreateMap(int num)
    //    {
    //        objectMap = new Dictionary<string, PrimitiveObject>(num);
    //        for (int i = 0; i < num; i++)
    //        {
    //            objectMap.Add(i.ToString(), new PrimitiveObject(i, i.ToString(), (float)i, i % 2 == 0 ? true : false));
    //        }
    //    }
    //    // I only tested with array
    //    public static TestObject TestBuild()
    //    {
    //        TestObject to = new TestObject();
    //        //to.CreateArray(1000000);
    //        to.CreateArray(1);

    //        return to;
    //    }
    //}

    //public class NonSerializableObject
    //{
    //    public int v_int;

    //    public string v_str;

    //    public float v_float;

    //    public bool v_bool;
    //    public NonSerializableObject(int vi, string vs, float vf, bool vb)
    //    {
    //        v_int = vi; v_str = vs; v_float = vf; v_bool = vb;
    //    }
    //}

    [Serializable]
    public class Person
    {
        public int Age;
        public string Name;
    }

    [Serializable]
    public class ArrayWrapper
    {
        public Person[] wrap;
    }

    public class SimpleTest
    {
        ArrayWrapper p = new ArrayWrapper { wrap = Enumerable.Range(1, 100).Select(x => new Person { Age = x, Name = "foobar" }).ToArray() };
        byte[] js = JsonSerializer.Serialize(new ArrayWrapper { wrap = Enumerable.Range(1, 100).Select(x => new Person { Age = x, Name = "foobar" }).ToArray() });

        public void Utf8Json()
        {
            JsonSerializer.Serialize(p);
        }
        public void JsonUtilityToJson()
        {
            JsonUtility.ToJson(p);
        }
        public void JsonUtilityToJsonEncode()
        {
            Encoding.UTF8.GetBytes(JsonUtility.ToJson(p));
        }

        public void Utf8JsonDeserialize()
        {
            JsonSerializer.Deserialize<ArrayWrapper>(js);
        }
        public void JsonUtilityFromJsonDeode()
        {
            JsonUtility.FromJson<ArrayWrapper>(Encoding.UTF8.GetString(js));
        }
    }
}