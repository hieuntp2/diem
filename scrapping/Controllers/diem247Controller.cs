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

                try
                {
                    TempTruong temptruong = new TempTruong();
                    temptruong.Ma = item.MaTruong;
                    temptruong.Ten = item.ten;
                    db.TempTruong.Add(temptruong);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Logs("Step_1: " + item.MaTruong + " - " + item.ten + ": " + e.ToString());
                }
                finally
                {

                }

                timNganh(ref item);
                addTruongNganhbyID(item);
                System.Threading.Thread.Sleep(rd.Next(500, 1000));
            }

            return View();
        }

        // step 2: Tim Nganh
        private void timNganh(ref NodeTruong truong)
        {
            scrapping.Models.truongnganhmonthi tn = new truongnganhmonthi();
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = htmlWeb.Load(truong.hreft);//tr[contains(@class, 'bg_white')]
                HtmlNodeCollection nodelist = htmlDocument.DocumentNode.SelectNodes(".//*[@class='bg_white']");

                for (int i = 0; i < nodelist.Count; i++)
                {
                    try
                    {
                        //3 Ma nganh
                        tn.manganh = nodelist[i].ChildNodes[3].ChildNodes[0].InnerText;
                        //5 Ten Nganh
                        tn.tennganh = nodelist[i].ChildNodes[5].ChildNodes[0].InnerText;
                        //7 To Hop Mon
                        tn.ToHopMon = nodelist[i].ChildNodes[7].ChildNodes[0].InnerText;
                        //9 Diem Chuan
                        tn.diem = float.Parse(nodelist[i].ChildNodes[9].ChildNodes[0].InnerText);

                        db.truongnganhmonthi.Add(tn);
                        db.SaveChanges();
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
            catch (Exception ex)
            {
                Logs("Step2_ Tr: " + truong.MaTruong + ": " + ex.ToString());
            }
            finally
            {

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