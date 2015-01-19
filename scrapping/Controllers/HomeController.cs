using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scrapping.Models;
namespace scrapping.Controllers
{
    public class HomeController : Controller
    {
        ProjectHEntities db = new ProjectHEntities();
        // Diem 2013
        public ActionResult Index()
        {
            List<NodeTruong> truongs = new List<NodeTruong>();
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load("http://trangvangdaotao.edu.vn/diem-chuan-2013.html");
            HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@class='list-title']");

            foreach (HtmlNode node in nodelist)
            {
                NodeTruong item = new NodeTruong();
                HtmlNode tabA = node.ChildNodes[1];
                item.hreft = "http://trangvangdaotao.edu.vn" + tabA.Attributes["href"].Value;

                item.ten = tabA.InnerText.Trim().Remove(0, 10).Trim();

                timNganh(ref item);
                truongs.Add(item);
            }

            return View();
        }

        private void timNganh(ref NodeTruong truong)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(truong.hreft);
            HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@id='rt-mainbody']/div/div/div/div/table/tbody/tr");

            for (int i = 1; i < nodelist.Count; i++ )
            {
                HtmlNode node = nodelist[i];
                NodeNganh nganh = new NodeNganh();
                nganh.Ten = node.ChildNodes[1].ChildNodes[1].InnerHtml.Trim();
                nganh.MaNganh = node.ChildNodes[3].ChildNodes[1].InnerHtml.Trim();
                nganh.Khoi = node.ChildNodes[5].ChildNodes[1].InnerHtml.Trim();
                nganh.DiemChuan = float.Parse(node.ChildNodes[7].ChildNodes[1].InnerText.Trim().Replace('.', ','));
                truong.nganhs.Add(nganh);
            }    


        }

        private void addTruongNganh(NodeTruong truong) 
        {
            Truong item = db.Truongs.SingleOrDefault(t => t.Ten.ToUpper() == truong.ten.ToUpper());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}