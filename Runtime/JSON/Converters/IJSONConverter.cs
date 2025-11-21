namespace MewtonGames.JSON.Converters
{
    public interface IJSONConverter
    {
        public string SerializeObject(object obj);

        public T DeserializeObject<T>(string json);
    }
}