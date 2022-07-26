namespace FH.EventProcessing
{
    public enum EventType
    {
        DataLoadRequest,
        TeamsPublished,
        PlayersPublished,
        FixturesPublished,
        Undetermined
    }

    public enum EventSource
    {
        FPL = 1,
        FantasyAllsvenskan = 2
    }
}
