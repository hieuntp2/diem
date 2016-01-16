using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scrapping.Models;
using HtmlAgilityPack;

namespace scrapping.Controllers
{
    //http://diemthi.tuyensinh247.com/diem-chuan.html
    public class HomeController : Controller
    {
        diem247Entities db = new diem247Entities();

        Random rd = new Random();
        int _currentSchool = 0;

        // Diem 2015, http://diemthi.tuyensinh247.com/diem-chuan.html
        public ActionResult Index()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load("http://diemthi.tuyensinh247.com/diem-chuan.html");
            HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes("//*[@id='benchmarking']/li");

            // Step 1: Tim truong
            foreach (HtmlNode node in nodelist)
            {          
                // Ten truong + href
                HtmlNode nodeA = node.ChildNodes[0];

                // Ma Truong
                HtmlNode nodeStrong = nodeA.ChildNodes[0];
                NodeTruong item = new NodeTruong();
                item.MaTruong = nodeStrong.InnerText;
                item.hreft = "http://diemthi.tuyensinh247.com" + nodeA.Attributes["href"].Value;
                item.ten = nodeA.Attributes["title"].Value;

                TempTruong temptruong = db.TempTruongs.SingleOrDefault(t => t.Ma == item.MaTruong);
                if (temptruong == null)
                {
                    temptruong = new TempTruong();
                    temptruong.Ma = item.MaTruong;
                    temptruong.Ten = item.ten;
                    db.TempTruongs.Add(temptruong);
                    db.SaveChanges();
                }

                timNganh(ref item);

                addTruongNganhbyID(item);
                System.Threading.Thread.Sleep(rd.Next(500, 1000));
                _currentSchool += 1;

            }
            Logs("FINISH at " + DateTime.Now);
            return View();
        }

        // step 2: Tim Nganh
        private void timNganh(ref NodeTruong truong)
        {
           
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = htmlWeb.Load(truong.hreft);
                HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes(".//*[@class='bg_white']");

                for (int i = 0; i < nodelist.Count; i++)
                {
                    try
                    {
                        scrapping.Models.truongnganhmonthi tn = new truongnganhmonthi();
                        //3 Ma nganh
                        tn.manganh = nodelist[i].ChildNodes[3].ChildNodes[0].InnerText;
                        //5 Ten Nganh
                        tn.tennganh = nodelist[i].ChildNodes[5].ChildNodes[0].InnerText;
                        //7 To Hop Mon
                        tn.ToHopMon = nodelist[i].ChildNodes[7].ChildNodes[0].InnerText;
                        //9 Diem Chuan
                        tn.diem = float.Parse(nodelist[i].ChildNodes[9].ChildNodes[0].InnerText);

                        tn.matruong = truong.MaTruong;
                        tn.tentruong = truong.ten;

                        truongnganhmonthi temptruongnganh = db.truongnganhmonthis.SingleOrDefault(t => t.matruong == tn.matruong
                                                                                                && t.manganh == tn.manganh
                                                                                                && t.ToHopMon == tn.ToHopMon);

                        if (temptruongnganh != null)
                        {
                            Logs("Step2: Trung TN: " + tn.matruong + "_" + tn.manganh);
                        }
                        else
                        {
                            db.truongnganhmonthis.Add(tn);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Logs("Step2_ Tr-Ng: " + truong.MaTruong + " -  " + i + ": " + e.ToString());
                    }
                    finally
                    {

                    }
                }
          
        }

        private void addTruongNganhbyID(NodeTruong truong)
        {
            //Truongs item = db.Truongs.SingleOrDefault(t => t.MaTruong.ToUpper() == truong.MaTruong.ToUpper());
            //if (item == null)
            //{
            //    Logs("LỖI khi thêm trường: " + truong.MaTruong + ", " + truong.ten + " không tồn tại");
            //    item = new Truongs();
            //    return;
            //}

            //for (int i = 0; i < truong.nganhs.Count; i++)
            //{
            //    string manganh = truong.nganhs[i].MaNganh;
            //    string khoithi = truong.nganhs[i].Khoi.Trim();
            //    Nganhs nganh = db.Nganhs.SingleOrDefault(t => t.MaNganh == manganh);
            //    if (nganh == null)
            //    {
            //        Logs("LỖI khi thêm ngành: " + truong.nganhs[i].MaNganh + ", " + truong.nganhs[i].Ten + " không tồn tại, trường:" + truong.MaTruong + ": " + truong.ten);
            //        continue;
            //    }

            //    TruongNganhs tn = db.TruongNganhs.SingleOrDefault(t => t.Truongs.MaTruong == truong.MaTruong
            //                                                    && t.MaNganh == manganh);
            //    if (tn == null)
            //    {
            //        tn = new TruongNganhs();
            //        tn.Diem1 = truong.nganhs[i].DiemChuan;
            //        tn.Diem2 = 0;
            //        tn.Diem3 = 0;

            //        tn.MaNganh = truong.nganhs[i].MaNganh;
            //        tn.MaTruong = truong.MaTruong;
            //        tn.NgayCapNhat = DateTime.Now;
            //        db.TruongNganhs.Add(tn);
            //        Logs("Thêm ngành: " + truong.nganhs[i].MaNganh + ", Khoi:" + khoithi + " vào trường " + truong.MaTruong + ": " + truong.ten);
            //    }
            //    else
            //    {
            //        tn.Diem1 = truong.nganhs[i].DiemChuan;
            //        tn.Diem2 = 0;
            //        tn.Diem3 = 0;

            //        tn.NgayCapNhat = DateTime.Now;
            //        db.Entry(tn).State = System.Data.Entity.EntityState.Modified;
            //        db.SaveChanges();
            //        Logs("Sửa ngành: " + truong.nganhs[i].MaNganh + " vào trường " + truong.MaTruong + ": " + truong.ten);
            //    }
            //}
        }

        private void Logs(string Message)
        {
            tempLog log = db.tempLogs.Create();
            log.mMessage = Message;
            db.tempLogs.Add(log);
            db.SaveChanges();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}