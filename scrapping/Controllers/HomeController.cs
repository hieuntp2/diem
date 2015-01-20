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

        // Diem 2014, http://diemthi.24h.com.vn/diem-chuan/
        public ActionResult Index()
        {
            Random rd = new Random();
            //List<NodeTruong> truongs = new List<NodeTruong>();
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load("http://diemthi.24h.com.vn/diem-chuan/");
            HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@id='div_tra_diem_chuan_dhcd_chon_nhanh']/a");

            foreach (HtmlNode node in nodelist)
            {
                NodeTruong item = new NodeTruong();

                item.hreft = "http://diemthi.24h.com.vn" + node.Attributes["href"].Value;

                item.ten = node.Attributes["title"].Value;
                convertTitle(ref item);
                timNganh2(ref item);
               // truongs.Add(item);

                addTruongNganhbyID(item);
                System.Threading.Thread.Sleep(rd.Next(100,200));
            }

            return View();
        }

            private void convertTitle(ref NodeTruong truong)
        {
            string[] words = truong.ten.Split('-');
            truong.ten = words[1].Trim();
            truong.MaTruong = words[0].Trim();
        }

            private void timNganh2(ref NodeTruong truong)
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = htmlWeb.Load(truong.hreft);
                HtmlNodeCollection table = htmlDocument.DocumentNode.SelectNodes("//*[@id='div_kq_diem_chuan_dhcd']/table");
                HtmlNode tablebody = table[0];
                HtmlNodeCollection nodelist = tablebody.ChildNodes;


                for (int i = 5; i < nodelist.Count - 2; i++)
                {
                    if(i % 2 == 0)
                    {
                        continue;
                    }

                    try
                    {
                        HtmlNode node = nodelist[i];
                        NodeNganh nganh = new NodeNganh();
                        nganh.MaNganh = node.ChildNodes[3].InnerHtml.Trim();
                        nganh.Ten = node.ChildNodes[5].InnerText.Trim();

                        nganh.Khoi = node.ChildNodes[7].InnerHtml.Trim();
                        nganh.DiemChuan = float.Parse(node.ChildNodes[9].InnerText.Trim().Replace('.', ','));
                        truong.nganhs.Add(nganh);
                    }
                   catch(Exception e)
                    {
                        Logs(e.ToString());
                    }
                    finally{}
                }


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

            private void doiTen(ref NodeTruong truong)
            {
                
            }

            private void addTruongNganhbyID(NodeTruong truong) 
            {
                Truong item = db.Truongs.SingleOrDefault(t => t.MaTruong.ToUpper() == truong.MaTruong.ToUpper());
                    if(item == null)
                    {
                        Logs("LỖI khi thêm trường: " + truong.MaTruong + ", " + truong.ten + " không tồn tại");
                        item = new Truong();
                        return;
                    }
                
                for(int i = 0; i < truong.nganhs.Count; i++)
                {
                    string manganh = truong.nganhs[i].MaNganh;
                    string khoithi = truong.nganhs[i].Khoi.Trim();
                    Nganh nganh = db.Nganhs.SingleOrDefault(t => t.MaNganh == manganh);
                    if(nganh == null)
                    {
                        Logs("LỖI khi thêm ngành: " + truong.nganhs[i].MaNganh + ", " + truong.nganhs[i].Ten + " không tồn tại, trường:" + truong.MaTruong + ": " + truong.ten);
                        continue;
                    }

                    TruongNganh tn = db.TruongNganhs.SingleOrDefault(t => t.Truong.MaTruong == truong.MaTruong
                                                                    && t.MaNganh == manganh
                                                                    && t.KhoiThi == khoithi);
                    if(tn == null)
                    {                        
                        tn = new TruongNganh();
                        tn.Diem1 = truong.nganhs[i].DiemChuan;
                        tn.Diem2 = 0;
                        tn.Diem3 = 0;

                        tn.KhoiThi = truong.nganhs[i].Khoi;
                        tn.MaNganh = truong.nganhs[i].MaNganh;
                        tn.MaTruong = truong.MaTruong;
                        tn.NgayCapNhat = DateTime.Now;
                        db.TruongNganhs.Add(tn);
                        Logs("Thêm ngành: " + truong.nganhs[i].MaNganh + ", Khoi:" + khoithi + " vào trường " + truong.MaTruong + ": " + truong.ten);
                    }
                    else
                    {                        
                        tn.Diem1 = truong.nganhs[i].DiemChuan;
                        tn.Diem2 = 0;
                        tn.Diem3 = 0;

                        tn.NgayCapNhat = DateTime.Now;
                        db.Entry(tn).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        Logs("Sửa ngành: " + truong.nganhs[i].MaNganh + " vào trường " + truong.MaTruong + ": " + truong.ten);
                    }
                }
            }

        private void Logs(string Message)
            {
                SystemLog log = new SystemLog();
                log.DateCreated = DateTime.Now;
                log.LogMessage = Message;
                db.SystemLogs.Add(log);
                db.SaveChanges();
            }
        
            // Diem 2013, http://trangvangdaotao.edu.vn/diem-chuan-2013.html
        public ActionResult About()
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}