public interface IParser
{
    public bool TryParse(string genDataStr, ref ParseData parseData);
}
