using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RonaldsYRonalds.Controllers;
using Xunit;

namespace RonaldsYRonalds.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_View()
        {
            var controller = new HomeController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_View()
        {
            var controller = new HomeController();
            var result = controller.Privacy();
            Assert.IsType<ViewResult>(result);
        }
    }
}
