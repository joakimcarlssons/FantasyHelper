namespace FH.EventProcessing
{
    public enum EventType
    {
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
