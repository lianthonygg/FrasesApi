using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrasesApi.Shared.Application.Common.ResultsHandler;

public class ResultJsonConverter<T> : JsonConverter<Result<T>>
{
    public override Result<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Reading Result<T> from JSON is not supported.");
    }

    public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
    {
        // Si es fallo, solo escribir el error
        if (value.IsFailure)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("error");
            JsonSerializer.Serialize(writer, value.Error, options);
            writer.WriteEndObject();
            return;
        }
        
        // Si es éxito, escribir solo el valor
        if (value.Value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Error, options);
        }
    }
}

public class ResultJsonConverter : JsonConverter<Result>
{
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Reading Result from JSON is not supported.");
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        // Si es fallo, solo escribir el error
        if (value.IsFailure)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("error");
            JsonSerializer.Serialize(writer, value.Error, options);
            writer.WriteEndObject();
        }
        else
        {
            // Si es éxito, escribir un objeto vacío o null
            writer.WriteNullValue();
        }
    }
}

// Converter genérico que funciona para cualquier Result<T> usando reflexión
public class GenericResultJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        // Verificar si es un Result<T>
        if (!typeToConvert.IsGenericType) return false;
        
        var genericType = typeToConvert.GetGenericTypeDefinition();
        return genericType == typeof(Result<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        // Crear un converter específico para este tipo genérico
        var valueType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(ResultJsonConverter<>).MakeGenericType(valueType);
        
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}