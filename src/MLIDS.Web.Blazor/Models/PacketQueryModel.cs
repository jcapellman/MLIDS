using System;

namespace MLIDS.Web.Blazor.Models
{
    public class PacketQueryModel
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public PacketQueryModel()
        {
            StartTime = DateTime.Now.AddDays(-7);
            EndTime = DateTime.Now;
        }
    }
}