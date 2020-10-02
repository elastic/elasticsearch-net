module Tests.YamlRunner.YamlMapConverter

open System
open System.Collections.Generic
open System.Text.Json
open System.Text.Json.Serialization

type YampMapConverter(options: JsonSerializerOptions ) =
    inherit JsonConverter<Dictionary<Object,Object>>()
    
    let _valueConverter = options.GetConverter(typeof<Object>) :?> JsonConverter<Object>
    
    override this.Write (writer: Utf8JsonWriter, value: Dictionary<Object, Object>, options: JsonSerializerOptions) =
        writer.WriteStartObject()
        value
            |> Seq.iter(fun kvp ->
                writer.WritePropertyName(kvp.Key.ToString());
                JsonSerializer.Serialize(writer, kvp.Value, options);
            )

        writer.WriteEndObject();

    override this.Read (reader: byref<Utf8JsonReader>, _typ: Type, options: JsonSerializerOptions) =
        if reader.TokenType <> JsonTokenType.StartObject then raise <| JsonException();

        let dictionary = Dictionary<Object, Object>();
        
        let mutable continueRead = true;
        while continueRead && reader.Read() do
            match reader.TokenType with
            | JsonTokenType.EndObject -> continueRead <- false
            | JsonTokenType.PropertyName -> 
                let propertyName = reader.GetString()
                let v = 
                    match _valueConverter with
                    | null ->
                        JsonSerializer.Deserialize<Object>(&reader, options)
                    | converter ->
                        reader.Read() |> ignore
                        converter.Read(&reader, typeof<Object>, options);
                dictionary.Add(propertyName, v)
            | _ -> raise <| JsonException()
        dictionary

type YamlMapConverterFactory() =
    inherit JsonConverterFactory()
        override this.CanConvert(t: Type) : bool =
            let genericArgs = t.GetGenericArguments()
            t.IsGenericType
                && t.GetGenericTypeDefinition() = typedefof<Dictionary<_,_>>
                && genericArgs.Length = 2
                && genericArgs.[0] = typedefof<Object> 
                && genericArgs.[1] = typedefof<Object> 

        override this.CreateConverter(_: Type, options: JsonSerializerOptions) : JsonConverter =
            YampMapConverter(options) :> JsonConverter;

