using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RonaldsYRonalds.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RonaldsYRonalds.Test.Data;
using RonaldsYRonalds.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RonaldsYRonalds.Test.Controllers
{
    public class CustomerPortalControllerTest
    {
        [Fact]
        public void CustomerPortalController_Authorized()
        {
            var attribute = typeof(CustomerPortalController)
                .GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>()
                .FirstOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Index_Filter()
        {
            using var context = ApplicationDbContextInMemory.Create();

            context.Tickets.Add(new TicketModel
            {
                UserName = "NoReturn1",
                Vin = "55555",
                IncidentDescription = "ShouldNotReturn1",
            });

            context.Tickets.Add(new TicketModel
            {
                UserName = "NoReturn2",
                Vin = "55555",
                IncidentDescription = "ShouldNotReturn2",
            });

            context.Tickets.Add(new TicketModel
            {
                UserName = "Return",
                Vin = "55555",
                IncidentDescription = "ShouldReturn",
            });

            context.SaveChanges();

            var controller = new CustomerPortalController(context);

            // Simulate an authenticated user with the name "Return"
            controller.ControllerContext = ControllerClaimsRoller.RollIdentity([
                new Claim(ClaimTypes.Name, "Return")]);

            var result = await controller.Index();
            var view = Assert.IsType<ViewResult>(result);
            var tickets = Assert.IsAssignableFrom<List<TicketModel>>(view.Model);

            Assert.Single(tickets); // THERE IS ONLY ONE TICKET ASSIGNED TO "Return"

            Assert.DoesNotContain(tickets, t => "NoReturn1" == t.UserName);
            Assert.DoesNotContain(tickets, t => "NoReturn2" == t.UserName);
            Assert.All(tickets, t => Assert.Equal("Return", t.UserName));
        }

        [Fact]
        public void Create_View()
        {
            using var context = ApplicationDbContextInMemory.Create();

            var controller = new CustomerPortalController(context);
            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }
    }
}
