using DATN.App_Start;
using DATN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN.Areas.Admin.Controllers
{
    public class ShowChartController : Controller
    {
        // GET: Admin/ShowChart

        [CheckSession]
        public ActionResult ThongKe()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            return View();
        }
        public JsonResult GetHuyenThongKe(int tinhID, int year)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsHuyen = (from item in db.DataHuyens
                          where item.IDTinh == tinhID && item.NamDanhGia == year
                          select new 
                          {
                              item.QLHuyen.TenHuyen,
                              item.SoHoDan
                          }).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetHuyenThongKe2(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsHuyen = (from item in db.DataHuyens
                           where item.IDTinh == tinhID 
                           select new
                           {
                               item.QLHuyen.TenHuyen,
                               item.SoHoDan
                           }).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHuyenThucTe(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsHuyen = (from item in db.QLCongTrinhs
                           join huyen in db.QLHuyens
                           on item.IDHuyen equals huyen.ID
                           where item.IDTinh == tinhID
                           group item by huyen.TenHuyen into ds
                           select new
                           {
                              huyen = ds.Key,
                              Sum = ds.Sum(i=>i.CSHDTT)
                           }).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHuyenThietKe(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsHuyen = (from item in db.QLCongTrinhs
                           join huyen in db.QLHuyens
                           on item.IDHuyen equals huyen.ID
                           where item.IDTinh == tinhID
                           group item by huyen.TenHuyen into ds
                           select new
                           {
                               huyen = ds.Key,
                               Sum = ds.Sum(i => i.CSHDTK)
                           }).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }
        [CheckSession]
        public ActionResult ThongKe2()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            var dsnamDanhGia1 = (from item in db.DataHuyens
                                 select new
                                 {
                                     item.NamDanhGia
                                 }).Distinct().ToList();
            ViewBag.dsnamDanhGia1 = new SelectList(dsnamDanhGia1, "NamDanhGia", "NamDanhGia");
            return View();
        }

        public JsonResult GetYear(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsYear = (from item in db.DataHuyens
                          where item.IDTinh == tinhID
                          select new
                          {
                              item.NamDanhGia
                          }).Distinct().ToList();
            return Json(dsYear, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetHuyenThucTe1(int tinhID, int year)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var dsHuyen = (from dg in db.DanhGias
                           join dt in db.DataHuyens
                           on dg.dthID equals dt.dthID
                           join h in db.QLHuyens
                           on dt.IDHuyen equals h.ID
                           where dt.IDTinh == tinhID && dt.NamDanhGia == year
                           group dg by h.TenHuyen into ds
                           select new
                           {
                               huyen = ds.Key,
                               Sum = ds.Sum(i => i.HoDan)
                           }).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }
    }
}