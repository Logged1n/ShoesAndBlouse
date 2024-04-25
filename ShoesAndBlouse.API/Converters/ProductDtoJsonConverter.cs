using System.Text.Json;
using System.Text.Json.Serialization;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.API.Converters;

public class ProductDtoJsonConverter : JsonConverter<ProductDto>
{
    public override ProductDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        ProductDto productDto = new ProductDto();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return productDto;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "Id":
                    productDto.Id = reader.GetInt32();
                    break;
                case "Name":
                    productDto.Name = reader.GetString();
                    break;
                case "Description":
                    productDto.Description = reader.GetString();
                    break;
                case "Price":
                    productDto.Price = JsonSerializer.Deserialize<Money>(ref reader, options);
                    break;
                case "Categories":
                    productDto.Categories = JsonSerializer.Deserialize<Dictionary<int, string>>(ref reader, options);
                    break;
                case "PhotoPath":
                    productDto.PhotoPath = reader.GetString();
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, ProductDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("Id", value.Id);
        writer.WriteString("Name", value.Name);
        writer.WriteString("Description", value.Description);

        writer.WriteStartObject("Price");
        writer.WriteString("Currency", value.Price.Currency);
        writer.WriteNumber("Amount", value.Price.Amount);
        writer.WriteEndObject();

        writer.WriteStartObject("Categories");
        foreach (var category in value.Categories)
        {
            writer.WriteString(category.Key.ToString(), category.Value);
        }
        writer.WriteEndObject();

        writer.WriteString("PhotoPath", value.PhotoPath);

        writer.WriteEndObject();
    }
}