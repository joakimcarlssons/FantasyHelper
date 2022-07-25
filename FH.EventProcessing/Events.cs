using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing
{
    public enum EventType
    {
        TeamsPublished,
        PlayersPublished,
        Undetermined
    }

    public enum EventSource
    {
        FPL,
        FantasyAllsvenskan
    }
}
