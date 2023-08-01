using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acades.Saga.Serializer
{
    internal sealed class SagaSerializer : ISagaSerializer
    {
        private readonly JsonSerializerOptions _settings;

        public SagaSerializer(JsonSerializerOptions settings = null)
        {
            _settings = settings ?? new JsonSerializerOptions();
            _settings.Converters.Add(new JsonStringEnumConverter(null,true));
            _settings.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }
        public string Serialize<T>(T value) => JsonSerializer.Serialize(value, _settings);
        public string Serialize(object value) => JsonSerializer.Serialize(value, _settings);
        public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _settings);
        public object Deserialize(string value) => JsonSerializer.Deserialize<object>(value, _settings);
    }
}
