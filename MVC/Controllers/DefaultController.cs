using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
namespace MVC.Controllers
{
    public class DefaultController : baseController
    {
        List<DefaultModel.Menu> ListMenu = null;//建立一個List使用
        // GET: Default
        public ActionResult Index()
        {


            ListMenu = new List<DefaultModel.Menu>(); //建立物件
            //下列為列表
            MenuToList("首頁", "Default", "Index");
            MenuToList("練習Get和Post", "GetAndPost", "Index"); 
            //給前端使用
            ViewBag.ListMenu = ListMenu;
            return View();
        } 
        protected List<DefaultModel.Menu> MenuToList(string Name, string Crtl, string Act)
        { 
            ListMenu.Add(Menu(Name, Crtl, Act));
            return ListMenu;
        }
        protected DefaultModel.Menu Menu(string Name, string Crtl, string Act)
        {
            DefaultModel.Menu Menu = new DefaultModel.Menu();
            Menu.MenuName = Name;
            Menu.MenuController = Crtl;
            Menu.MenuAction = Act; 
            return Menu;
        }
        protected void GetInfo()
        {
            baseController Ba = new baseController();
          //  Ba.Url = System.Web.HttpContext.Current.Request.Url.PathAndQuery;

        } 
    } 
}
/*
 * 來做一個列表清單顯示在頁面上
 * 
 * 專案起始
 * 建立DefaultController
 * 建立DefaultModel
 * 建立Default/Index.cshtml
 * 需要一個名字跟網址
 * 網址將會連接到該目標
 * 使用到MVC.Models
 * 建立兩個Func 
 * MenuToListc 和 Menu
 * 頁面建立迴圈 及 Css(外面抄來的)
 * 
 * 額外加入NLog
*/