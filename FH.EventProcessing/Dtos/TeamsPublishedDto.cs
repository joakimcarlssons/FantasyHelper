using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing.Dtos
{
    public class TeamsPublishedDto
    {
        public string Event { get; set; }

        public IEnumerable<TeamsPublishedTeamDto> Teams { get; set; }
    }

    /// <summary>
    /// The DTO for each of the teams in the list of teams provided in <see cref="TeamsPublishedDto"/> upon a Teams_Published event
    /// </summary>
    public class TeamsPublishedTeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
    }
}
