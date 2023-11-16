using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public APIController(IWebHostEnvironment host, DemoContext dbDemo)
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
        public IActionResult register(Members member, IFormFile formFile)
        {
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);

            using (var filestream = new FileStream(strPath, FileMode.Create))
            {
                formFile.CopyTo(filestream);
            }
            member.FileName = formFile.FileName;
            //將上傳的圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileData = imgByte;

            //將資料寫進資料庫中
            _dbDemo.Members.Add(member);
            _dbDemo.SaveChanges();

            return Content("新增成功");
        }

        public IActionResult CheckAccount(MemberViewModel member)
        { if (member.name.IsNullOrEmpty())
                return Content("請輸入名字");
            var data = _dbDemo.Members.Where(p => p.Name.Contains(member.name));
            if (data == null)
            {
                return Content("該名稱可使用");
            }
            else return Content("該名稱已被註冊");
        }

        public IActionResult Fetch()
        {
            return View();
        }


        public IActionResult Cities()
        {
            var cities = _dbDemo.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }

        //根據城市名稱讀取不會重複的鄉鎮區名稱
        public IActionResult Districts(string city)
        {
            var districts = _dbDemo.Address.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區讀取路的名稱
        public IActionResult Roads(string siteId)
        {
            var roads = _dbDemo.Address.Where(a => a.SiteId == siteId).Select(a => a.Road);
            return Json(roads);
        }

        public IActionResult GetImageByte(int? id =1) 
        {
            Members members = _dbDemo.Members.Find(id);
            byte[] img = members.FileData;

            if (img != null)
                return File(img, "image/jpeg");
            return NotFound();
        }

        //public IActionResult register(MemberViewModel member)
        //{
        //    return Content($"Hello {member.name} , your email is {member.email} , you are {member.age} years old");
        //}
    }
}
