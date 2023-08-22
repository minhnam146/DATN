using DATN.Models;
using DATN.Models.QLDuLieu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATN.App_Start;

namespace DATN.Areas.Admin.Controllers
{
    public class QLDuLieuController : Controller
    {
        // GET: Admin/QLDuLieu

        QLDuLieu db = new QLDuLieu();
        public ActionResult DanhSachDuLieu()
        {
            return View(db.laydsDuLieu());
        }
        public ActionResult DanhSachDanhGia()
        {
            return View(db.laydsDanhGia());
        }

        [CheckSession]
        public ActionResult Test()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            List<DataHuyen> dsnamDanhGia = db.DataHuyens.Distinct().ToList();
            ViewBag.dsnamDanhGia = new SelectList(dsnamDanhGia, "NamDanhGia", "NamDanhGia");
            var dsnamDanhGia1 = (from item in db.DataHuyens
                                 select new
                                 {
                                     item.NamDanhGia
                                 }).Distinct().ToList();
            ViewBag.dsnamDanhGia1 = new SelectList(dsnamDanhGia1, "NamDanhGia", "NamDanhGia");
            return View();
        }

        [CheckSession]
        public ActionResult CreateDuLieu()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult CreateDuLieu(DataHuyen model)
        {
            if (ModelState.IsValid)
            {
                db.themDuLieu(model);
                return RedirectToAction("Test");
            }
            else
            {
                return RedirectToAction("CreateDuLieu");
            }
        }

        [CheckSession]
        public ActionResult EditDuLieu(int id)
        {
            return View(db.layDuLieu(id));
        }
        [HttpPost]
        public ActionResult EditDuLieu(DataHuyen model)
        {
            db.suaDuLieu(model);
            return RedirectToAction("Test");
        }
        public ActionResult DeleteDuLieu(int id)
        {
            db.xoaDuLieu(id);
            return RedirectToAction("Test");
        }
        public JsonResult GetDLHuyenTheoNam(int huyenID, int year)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            //List<DataHuyen> dsdlHuyen = db.DataHuyens.Where(x => x.IDHuyen == huyenID).ToList();
            var dsdataHuyen = (from tinh in db.QLTinhs
                        join huyen in db.QLHuyens
                        on tinh.ID equals huyen.IDTinh
                        join data in db.DataHuyens
                        on huyen.ID equals data.IDHuyen
                        where data.IDHuyen == huyenID && data.NamDanhGia == year
                        orderby data.NamDanhGia
                        select new
                        {
                            data.dthID,
                            tinh.TenTinh,
                            huyen.TenHuyen,
                            data.NamDanhGia,
                            data.SoHoDan,
                            data.SoHoNgheo
                        }).ToList();
            return Json(dsdataHuyen, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYear(int huyenID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsYear = (from item in db.DataHuyens
                          where item.IDHuyen == huyenID
                          select new
                          {
                              item.NamDanhGia
                          }).ToList();
            return Json(dsYear, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDLHuyenTheoTinh(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsdataHuyen = (from tinh in db.QLTinhs
                               join huyen in db.QLHuyens
                               on tinh.ID equals huyen.IDTinh
                               join data in db.DataHuyens
                               on huyen.ID equals data.IDHuyen
                               where data.IDTinh == tinhID
                               orderby data.NamDanhGia
                               select new
                               {
                                   data.dthID,
                                   tinh.TenTinh,
                                   huyen.TenHuyen,
                                   data.NamDanhGia,
                                   data.SoHoDan,
                                   data.SoHoNgheo
                               }).ToList();
            return Json(dsdataHuyen, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDLHuyen(int huyenID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsdataHuyen = (from tinh in db.QLTinhs
                               join huyen in db.QLHuyens
                               on tinh.ID equals huyen.IDTinh
                               join data in db.DataHuyens
                               on huyen.ID equals data.IDHuyen
                               where data.IDHuyen == huyenID
                               orderby data.NamDanhGia
                               select new
                               {
                                   data.dthID,
                                   tinh.TenTinh,
                                   huyen.TenHuyen,
                                   data.NamDanhGia,
                                   data.SoHoDan,
                                   data.SoHoNgheo
                               }).ToList();
            return Json(dsdataHuyen, JsonRequestBehavior.AllowGet);
        }

        [CheckSession]
        public ActionResult Test1(int dataID, int page = 1)
        {
            QLCNEntities db = new QLCNEntities();
            var dsCongTrinh = (from dt in db.DataHuyens
                               join dg in db.DanhGias
                               on dt.dthID equals dg.dthID
                               join ct in db.QLCongTrinhs
                               on dg.IDCT equals ct.IDCT
                               where dg.dthID == dataID
                               select new DanhSachDanhGia
                               {
                                   ct1 = ct,
                                   dt1 = dt,
                                   dg1= dg
                               }).ToList();
            ViewBag.id = dataID;
            var huyenID = db.DataHuyens.FirstOrDefault(x => x.dthID == dataID).IDHuyen;
            var tenHuyen = db.QLHuyens.FirstOrDefault(x => x.ID == huyenID).TenHuyen;
            var year = db.DataHuyens.FirstOrDefault(x => x.dthID == dataID).NamDanhGia;
            var hoDan = db.DataHuyens.FirstOrDefault(x => x.dthID == dataID).SoHoDan;
            var hoNgheo = db.DataHuyens.FirstOrDefault(x => x.dthID == dataID).SoHoNgheo;
            ViewBag.year = year;
            ViewBag.tenHuyen = tenHuyen;
            ViewBag.hoDan = hoDan;
            ViewBag.HoNgheo = hoNgheo;
            int record = 10;
            int trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsCongTrinh.Count) / Convert.ToDouble(record)));
            int trangskip = (page - 1) * record;
            ViewBag.Page = page;
            ViewBag.Trang = trang;
            dsCongTrinh = dsCongTrinh.Skip(trangskip).Take(record).ToList();
            return View(dsCongTrinh);
        }
        public JsonResult GetHoDan(int congtrinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<QLCongTrinh> hoDan = db.QLCongTrinhs.Where(x => x.IDCT == congtrinhID).ToList();
            return Json(hoDan, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetHoNgheo(int congtrinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<QLCongTrinh> hoNgheo = db.QLCongTrinhs.Where(x => x.IDCT == congtrinhID).ToList();
            return Json(hoNgheo, JsonRequestBehavior.AllowGet);
        }

        [CheckSession]
        public ActionResult CreateDanhGia(int id)
        {
            QLCNEntities db = new QLCNEntities();
            var huyenID = db.DataHuyens.FirstOrDefault(x => x.dthID == id).IDHuyen;
            var tenHuyen = db.QLHuyens.FirstOrDefault(x => x.ID == huyenID).TenHuyen;
            var year = db.DataHuyens.FirstOrDefault(x => x.dthID == id).NamDanhGia;
            ViewBag.year = year;
            ViewBag.tenHuyen = tenHuyen;
            ViewBag.id = id;
            List<QLCongTrinh> dsCongTrinh = db.QLCongTrinhs.Where(x => x.IDHuyen == huyenID).ToList();
            ViewBag.dsCongTrinh = new SelectList(dsCongTrinh, "IDCT", "TenCT");
            ViewBag.dsyesNo = new SelectList(GetYesNo(), "Value", "Text");
            ViewBag.loaihinhQL = new SelectList(GetLoaiHinhQL(), "tenloaihinhQL", "tenloaihinhQL");
            return View();
        }
        [HttpPost]
        public ActionResult CreateDanhGia(DanhGia model)
        {
            var dth = Request["dthID"];
            int dthid = Convert.ToInt32(dth);
            model.dthID = dthid;
            if (ModelState.IsValid)
            {
                db.themDanhGia(model);
                return RedirectToAction("Test1", new { dataID = dthid });
            }
            else
            {
                return RedirectToAction("CreateDanhGia", new { id = dthid });
            }
        }

        [CheckSession]
        public ActionResult EditDanhGia(int id)
        {
            QLCNEntities db = new QLCNEntities();
            var dataID = db.DanhGias.FirstOrDefault(x => x.danhgiaID == id).dthID;
            ViewBag.dataID = dataID;
            ViewBag.loaihinhQL = new SelectList(GetLoaiHinhQL(), "tenloaihinhQL", "tenloaihinhQL");
            ViewBag.dsyesNo = new SelectList(GetYesNo(), "Value", "Text");
            return View(db.DanhGias.Find(id));
        }
        [HttpPost]
        public ActionResult EditDanhGia(DanhGia model)
        {
            var dth = Request["dthID"];
            int dthid = Convert.ToInt32(dth);
            model.dthID = dthid;
            db.suaDanhGia(model);
            return RedirectToAction("Test1", new { dataID = dthid });
        }
        public ActionResult DeleteDanhGia(int id)
        {
            QLCNEntities db = new QLCNEntities();
            DanhGia n = db.DanhGias.Find(id);
            var dthID = db.DanhGias.FirstOrDefault(x => x.danhgiaID == id).dthID;
            db.DanhGias.Remove(n);
            db.SaveChanges();
            return RedirectToAction("Test1", new { dataID=dthID});
        }
        private List<LoaiHinhQL> GetLoaiHinhQL()
        {
            var loaihinhQL = new List<LoaiHinhQL>();
            loaihinhQL.Add(new LoaiHinhQL() { loaihinhqlID = 1, tenloaihinhQL = "Cộng đồng" });
            loaihinhQL.Add(new LoaiHinhQL() { loaihinhqlID = 2, tenloaihinhQL = "HTX" });
            loaihinhQL.Add(new LoaiHinhQL() { loaihinhqlID = 3, tenloaihinhQL = "Đơn vị sự nghiệp có thu" });
            loaihinhQL.Add(new LoaiHinhQL() { loaihinhqlID = 4, tenloaihinhQL = "Doanh nghiệp" });
            loaihinhQL.Add(new LoaiHinhQL() { loaihinhqlID = 5, tenloaihinhQL = "Khác" });
            return loaihinhQL;
        }
        private List<YesNo> GetYesNo()
        {
            var yesNo = new List<YesNo>();
            yesNo.Add(new YesNo() { Value = true, Text = "Có" });
            yesNo.Add(new YesNo() { Value = false, Text = "Không" });
            return yesNo;
        }
        //public JsonResult GetDanhSachCT(int dataID)
        //{
        //    QLCNEntities db = new QLCNEntities();
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var dsCongTrinh = (from dt in db.DataHuyens
        //                       join dg in db.DanhGias
        //                       on dt.dthID equals dg.dthID
        //                       join ct in db.QLCongTrinhs
        //                       on dg.IDCT equals ct.IDCT
        //                       where dg.dthID == dataID 
        //                       select new
        //                       {
        //                           ct.TenCT,
        //                           ct.QLXa.TenXa,
        //                           dg.HoDan,
        //                           dg.HoNgheo,
        //                           dg.LoaihinhQL,
        //                           dg.ThuphiDV,
        //                           dg.DatQCVN,
        //                           dg.KhaNangCapNuoc,
        //                           dg.TyLeDauNoi,
        //                           dg.CanboQL
        //                       }).ToList();
        //    return Json(dsCongTrinh, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult Test3(int huyenID)
        //{
        //    QLCNEntities db = new QLCNEntities();
        //    var CtrByHuyen = db.QLCongTrinhs.Where(x => x.IDHuyen == huyenID).ToList();
        //    List<DanhGia> lstDanhGia = new List<DanhGia>();
        //    foreach (var ctr in CtrByHuyen)
        //    {
        //        DanhGia dataDanhGia = new DanhGia();
        //        //Code Tao Entity dataDanhGia tu ban ghi cong trinh

        //        dataDanhGia.QLCongTrinh.TenCT = ctr.TenCT;          
        //        lstDanhGia.Add(dataDanhGia);
        //    }
        //    return View(lstDanhGia);
        //}
    }
}