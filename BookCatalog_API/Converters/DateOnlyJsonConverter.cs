﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookCatalog_API.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string format = "yyyy-MM-dd";
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, format);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}
