using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
      return "value";
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
