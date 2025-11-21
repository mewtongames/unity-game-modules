using UnityEngine;

namespace MewtonGames.JSON.Converters
{
    public class UnityJSONConverter : IJSONConverter
    {
        public string SerializeObject(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public T DeserializeObject<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}