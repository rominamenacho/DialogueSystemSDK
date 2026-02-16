namespace DialogSystem.Interfaces
{
    public interface ILocalizationProvider
    {
        string GetText(string lineId);
        bool Contains(string lineId);
    }
}
