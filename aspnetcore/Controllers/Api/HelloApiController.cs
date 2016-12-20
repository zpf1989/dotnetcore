using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers.Api
{
    [Route("api/[controller]")]
    public class HelloApiController:Controller
    {
      private IEnumerable<Person> Persons=new List<Person>
            {
                new Person(_name:"zpf1",_age:12,_sex:"male"),
                new Person(_name:"zpf2",_age:23,_sex:"male"),
                new Person(_name:"zpf3",_age:34,_sex:"male"),
                new Person(_name:"张鹏飞",_age:45,_sex:"male"),
            };
        public HelloApiController()
        {

        }

        [HttpGet]
        public IEnumerable<Person> GetPerson()
        {
            return Persons;
        }

        [HttpGet("person/{name}",Name="getbyname")]
        public IActionResult GetById(string name)
        {
            var person=Persons.Where(p=>p.name==name).FirstOrDefault();
            if(person==null)
            {
                return NotFound();
            }
            return new ObjectResult(person);
        }
    }
}