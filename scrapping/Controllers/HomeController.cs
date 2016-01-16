using HtmlAgilityPack;
using System;
using System.Linq;
using System.Web.Mvc;
using scrapping.Models;
namespace scrapping.Controllers
{
    public class HomeController1 : Controller
    {
        ProjectHEntities db = new ProjectHEntities();
        Random rd = new Random();

        // Diem 2014, http://diemthi.24h.com.vn/diem-chuan/
        public ActionResult Index()
        {           
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load("http://diemthi.24h.com.vn/diem-chuan/");
            HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@id='div_tra_diem_chuan_dhcd_chon_nhanh']/a");

            // Step 1: Tim truong
            foreach (HtmlNode node in nodelist)
            {
                NodeTruong item = new NodeTruong();

                item.hreft = "http://diemthi.24h.com.vn" + node.Attributes["href"].Value;

                item.ten = node.Attributes["title"].Value;
                convertTitle(ref item);

                // Voi moi truong thi lay thong tin nganh
                timNganh(ref item);

                addTruongNganhbyID(item);
                System.Threading.Thread.Sleep(rd.Next(100, 200));
            }

            return View();
        }

        private void convertTitle(ref NodeTruong truong)
        {
            string[] words = truong.ten.Split('-');
            truong.ten = words[1].Trim();
            truong.MaTruong = words[0].Trim();
        }

        private void timNganh(ref NodeTruong truong)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(truong.hreft);
            HtmlNodeCollection table = htmlDocument.DocumentNode.SelectNodes("//*[@id='div_kq_diem_chuan_dhcd']/table");
            HtmlNode tablebody = table[0];
            HtmlNodeCollection nodelist = tablebody.ChildNodes;


            for (int i = 5; i < nodelist.Count - 2; i++)
            {
                if (i % 2 == 0)
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
                catch (Exception e)
                {
                    Logs(e.ToString());
                }
                finally { }
            }


        }

        private void addTruongNganhbyID(NodeTruong truong)
        {
            Truongs item = db.Truongs.SingleOrDefault(t => t.MaTruong.ToUpper() == truong.MaTruong.ToUpper());
            if (item == null)
            {
                Logs("LỖI khi thêm trường: " + truong.MaTruong + ", " + truong.ten + " không tồn tại");
                item = new Truongs();
                return;
            }

            for (int i = 0; i < truong.nganhs.Count; i++)
            {
                string manganh = truong.nganhs[i].MaNganh;
                string khoithi = truong.nganhs[i].Khoi.Trim();
                Nganhs nganh = db.Nganhs.SingleOrDefault(t => t.MaNganh == manganh);
                if (nganh == null)
                {
                    Logs("LỖI khi thêm ngành: " + truong.nganhs[i].MaNganh + ", " + truong.nganhs[i].Ten + " không tồn tại, trường:" + truong.MaTruong + ": " + truong.ten);
                    continue;
                }

                TruongNganhs tn = db.TruongNganhs.SingleOrDefault(t => t.Truongs.MaTruong == truong.MaTruong
                                                                && t.MaNganh == manganh);
                if (tn == null)
                {
                    tn = new TruongNganhs();
                    tn.Diem1 = truong.nganhs[i].DiemChuan;
                    tn.Diem2 = 0;
                    tn.Diem3 = 0;
                    
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
            SystemLogs log = new SystemLogs();
            log.DateCreated = DateTime.Now;
            log.LogMessage = Message;
            db.SystemLogs.Add(log);
            db.SaveChanges();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}