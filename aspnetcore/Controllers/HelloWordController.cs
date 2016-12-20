using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using System.Linq;

namespace WebApplication.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly ILogger _logger;
        public HelloWorldController(ILoggerFactory loggerFactory)
        {
            _logger=loggerFactory.CreateLogger<HelloWorldController>();
        }

        public string Hello()
        {
            return "This is my Hello action...";
        }

        public string Welcome(string name,int numTimes=1)
        {
            // return "This is the Welcome action method...";
            return HtmlEncoder.Default.Encode($"Hello {name},NumTimes is:{numTimes}");
        }

        public string modeltest(Person p)
        {
            if(ModelState.IsValid==false)
            {
                return ParseModelErrors(ModelState);
            }
            return p.ToString();
        }

        private string ParseModelErrors(ModelStateDictionary stateDict)
        {
            if (stateDict.IsValid)
            {
                return string.Empty;
            }
            var errorStates = stateDict.Where(s => s.Value.Errors.Count > 0).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var state in errorStates)
            {
                sb.AppendFormat("{0}:", state.Key);
                foreach (var error in state.Value.Errors)
                {
                    sb.AppendFormat("{0},", error.ErrorMessage);
                }
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString().TrimEnd(',');
        }
    }

    public class Person
    {
        [Required]
        public string name{get;set;}
        public int age{get;set;}
        public string sex{get;set;}

        public Person()
        {}

        public Person(string _name,int _age,string _sex)
        {
            name=_name;
            age=_age;
            sex=_sex;
        }

       public override string ToString()
        {
            return string.Format("name:{0},age:{1},sex:{2}",name,age,sex);
        }
    }
}