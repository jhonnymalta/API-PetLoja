using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetLoja_API.models;

namespace PETLOJAAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<User>> GetUser(
        [FromServices] AppDbContext context
    )
    {

        return context.Usuarios;
    }
    [HttpGet("/{id:int}")]
    public User GetUserByID(
        [FromHeader] int id,
        [FromServices] AppDbContext context
    )
    {
        return context.Usuarios.FirstOrDefault(x => x.Id == id);

    }

    [HttpPost("")]
    public ActionResult<User> PostUser(
        [FromBody] User user,
        [FromServices] AppDbContext context
    )
    {
        context.Usuarios.Add(user);
        context.SaveChanges();
        return user;

    }
    [HttpPut("/{id:int}")]
    public User UserPut(
        [FromRoute] int id,
        [FromBody] User user,
        [FromServices] AppDbContext context
    )
    {
        var model = context.Usuarios.FirstOrDefault(x => x.Id == id);
        if (model == null)
        {
            return user;
        }
        model.Name = user.Name;
        model.Email = user.Email;

        context.Usuarios.Update(model);
        context.SaveChanges();
        return model;


    }

    [HttpDelete("/{id:int}")]
    public User Delete(
        [FromRoute] int id,
        [FromServices] AppDbContext context
    )
    {
        var model = context.Usuarios.FirstOrDefault(x => x.Id == id);
        context.Usuarios.Remove(model);
        context.SaveChanges();
        return model;
    }


}
