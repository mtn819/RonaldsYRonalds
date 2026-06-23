using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RonaldsYRonalds.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RonaldsYRonalds.Test.Controllers;
public class ControllerClaimsRoller
{
    public static ControllerContext RollIdentity(List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        return controllerContext;
    }
}
