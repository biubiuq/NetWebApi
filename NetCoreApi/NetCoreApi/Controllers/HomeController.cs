using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreApi.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    public IOrderService orderService1;
    public IOrderService orderService2;
    public IOrderServiceb orderServiceb1;
    public IOrderServiceb orderServiceb2;
    public HomeController(IOrderService orderService1,IOrderService orderService2,
      IOrderServiceb orderServiceb1, IOrderServiceb orderServiceb2
      )
    {
    
      this.orderService1 = orderService1;
      this.orderService2 = orderService2;
      this.orderServiceb1 = orderServiceb1;
      this.orderServiceb2 = orderServiceb2;
    }
    // GET: api/<HomeController>
    [HttpGet]
    public string Login()
    {
      Console.WriteLine($"{this.orderService1}\r\n{this.orderService2} \r\n ------");
      Console.WriteLine(this.orderService1.Equals(orderService2));
      Console.WriteLine($"{this.orderServiceb1}\r\n{this.orderServiceb2} \r\n ------");
      Console.WriteLine(this.orderServiceb1.Equals(orderServiceb2));

      return "helloworld";
    }

    // GET api/<HomeController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
            var _tokenManagement = new
            {
                sercet = "123456789123456789",
                Issuer = "test.cn",
                audience = "test",
                accessExpiration = 30,
                refreshExpiration = 60

            };
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"12313")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.sercet));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.audience, claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.accessExpiration),
                signingCredentials: credentials);
           var  token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

    // POST api/<HomeController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<HomeController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<HomeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
