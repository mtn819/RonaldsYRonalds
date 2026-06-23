using System;
using System.Collections.Generic;
using System.Text;
using RonaldsYRonalds.Models;
using Xunit;

namespace RonaldsYRonalds.Test.Models
{
    public class TicketModelTest
    {
        [Fact]
        public void DefaultTicket()
        {
            var before = DateTime.UtcNow;
            var ticket = new TicketModel();
            var after = DateTime.UtcNow;

            Assert.InRange(ticket.CreatedAt, before, after);
            Assert.Equal(TicketStatus.Submitted, ticket.Status);
        }

        [Fact]
        public void SetTicket()
        {
            var ticket = new TicketModel
            {
                Vin = "12345678901234567",
                IncidentDescription = "AKA The Claim",
                UserName = "UserName@Email.Com",
                Status = TicketStatus.Rejected
            };

            Assert.Equal("12345678901234567", ticket.Vin);
            Assert.Equal("AKA The Claim", ticket.IncidentDescription);
            Assert.Equal("UserName@Email.Com", ticket.UserName);
            Assert.Equal(TicketStatus.Rejected, ticket.Status);
        }
    }
}
