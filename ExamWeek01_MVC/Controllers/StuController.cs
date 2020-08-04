using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamWeek01_MVC.SRC;

namespace ExamWeek01_MVC.Controllers
{
    public class StuController : Controller
    {
        WebService1SoapClient client = new WebService1SoapClient();
        // GET: Stu
        //显示
        public ActionResult Show()
        {
            var list = client.Show();
            return View(list);
        }
        //绑定下拉
        public void band()
        {
            var list = client.Show1();
            ViewBag.bandla = new SelectList(list, "Cid", "Cname");
        }
        //添加
        public ActionResult Add()
        {
            band();//绑定下拉
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(StudentModel m)
        {
            band();//绑定下拉
            m.Swei = 0;
            m.Stimes = DateTime.Now;
            var d = client.Add(m);
            if (d==1)
            {
                return Content("<script>alert('添加成功！');location.href='/Stu/Show'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！')</script>");
            }
        }
        //添加一条违纪
        [HttpGet]
        public ActionResult Upt(StudentModel m,int id)
        {
            var d = client.Find(id);
            m.Sid = d.Sid;
            if (client.Upt(m)==1)
            {
                return Content("<script>alert('添加成功！');location.href='/Stu/Show'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！')</script>");
            }
            
        }
        //详情
        public ActionResult Xiang(int id)
        {
            return View(client.Find(id));
        }
    }
}