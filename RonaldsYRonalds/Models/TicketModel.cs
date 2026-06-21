using System.ComponentModel.DataAnnotations;

namespace RonaldsYRonalds.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Submitted;
        [StringLength(17, MinimumLength = 17)]
        public string Vin { get; set; }
        public string IncidentDescription { get; set; }
    }
}
