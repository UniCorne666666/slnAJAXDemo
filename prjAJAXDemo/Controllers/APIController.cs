using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prjAJAXDemo.Models;
using prjAJAXDemo.ViewModel;
using System.Runtime.CompilerServices;

namespace prjAJAXDemo.Controllers
{
    public class APIController : Controller
    {
        private readonly IWebHostEnvironment _host;

        private readonly DemoContext _dbDemo;


        public APIController(IWebHostEnvironment host , DemoContext dbDemo) 
        { 
           _dbDemo = dbDemo;
           _host = host;
        }
        public IActionResult Index(UserViewModel user)
        {
            System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(user.name)) 
            {
                user.name = "guest";
            }
            return Content($"Hello {user.name} , you are {user.age} years old ");
        }
        public IActionResult register(MemberViewModel member , IFormFile formFile) 
        {
            string strPath = Path.Combine(_host.WebRootPath , "uploads" , formFile.FileName);

            using (var filestream = new FileStream(strPath, FileMode.Create))
            {
                formFile.CopyTo(filestream);
            }
            return Content(strPath);
        }

        public IActionResult CheckAccount(MemberViewModel member)
        {   if (member.name.IsNullOrEmpty())
                return Content("請輸入名字");
            var data = _dbDemo.Members.Where(p => p.Name.Contains(member.name));
            if (data == null)
            {
                return Content("該名稱可使用");
            }
            else return Content("該名稱已被註冊");
        }
        //public IActionResult register(MemberViewModel member)
        //{
        //    return Content($"Hello {member.name} , your email is {member.email} , you are {member.age} years old");
        //}
    }
}
