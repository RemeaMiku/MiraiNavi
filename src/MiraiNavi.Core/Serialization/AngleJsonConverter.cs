using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiraiNavi.Serialization;

public class AngleJsonConverter : JsonConverter<Angle>
{
    #region Public Methods

    public override Angle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Angle.FromDegrees(reader.GetDouble());
    }

    public override void Write(Utf8JsonWriter writer, Angle value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.TotalDegrees);
    }

    #endregion Public Methods
}
