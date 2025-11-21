#if NEWTONSOFT_JSON
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MewtonGames.JSON.Converters
{
    public class NewtonSoftJSONConverter : IJSONConverter
    {
        private readonly List<JsonConverter> _jsonConverters;

        public NewtonSoftJSONConverter(List<JsonConverter> jsonConverters)
        {
            _jsonConverters = jsonConverters;
        }

        public NewtonSoftJSONConverter(JsonConverter jsonConverter)
        {
            _jsonConverters = new List<JsonConverter> { jsonConverter };
        }

        public NewtonSoftJSONConverter()
        {
            _jsonConverters = new List<JsonConverter>();
        }

        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, _jsonConverters.ToArray());
        }

        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _jsonConverters.ToArray());
        }
    }
}
#endif