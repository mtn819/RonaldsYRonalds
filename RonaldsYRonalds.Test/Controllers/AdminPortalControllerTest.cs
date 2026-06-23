using System;
using System.Collections.Generic;
using System.Text;
using RonaldsYRonalds.Controllers;
using RonaldsYRonalds.Test.Data;
using RonaldsYRonalds.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace RonaldsYRonalds.Test.Controllers
{
    public class AdminPortalControllerTest
    {
        [Fact]
        public void AdminPortalController_Authorized()
        {
            var attribute = typeof(AdminPortalController)
                .GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>()
                .FirstOrDefault();

            Assert.NotNull(attribute);
            Assert.Equal("Admin", attribute.Roles);
        }

        [Fact]
        public async Task Index_Unfiltered()
        {
            using var context = ApplicationDbContextInMemory.Create();

            context.Tickets.Add(new TicketModel {
                Vin = "VIN1",
                IncidentDescription = "Claim",
                UserName = "name@example.com"
            });
            context.Tickets.Add(new TicketModel {
                Vin = "VIN2",
                IncidentDescription = "Claim",
                UserName = "name@example.com"
            });

            context.SaveChanges();

            var controller = new AdminPortalController(context);
            var result = await controller.Index(null!);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TicketModel>>(viewResult.Model);

            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Index_Filtered()
        {
            using var context = ApplicationDbContextInMemory.Create();

            context.Tickets.Add(new TicketModel
            {
                Vin = "VIN1",
                IncidentDescription = "Claim",
                UserName = "name@example.com"
            });
            context.Tickets.Add(new TicketModel
            {
                Vin = "VIN2",
                IncidentDescription = "Claim",
                UserName = "name@example.com"
            });

            context.SaveChanges();

            var controller = new AdminPortalController(context);
            var result = await controller.Index("VIN1");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TicketModel>>(viewResult.Model);

            Assert.Single(model);
        }
    }
}
