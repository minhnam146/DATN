using DATN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATN.App_Start;

namespace DATN.Areas.Admin.Controllers
{
    public class QLDiaBanCNController : Controller
    {

        QLDiaBanCapNuoc db = new QLDiaBanCapNuoc();

        //Quản lý Tỉnh
        [CheckSession]
        public ActionResult DanhSachTinh(string keySearch, int page = 1)
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.Where(x =>x.TenTinh.Contains(keySearch) || keySearch == null).ToList();
            int record = 10;
            int trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsTinh.Count)/Convert.ToDouble(record)));
            int trangskip = (page - 1) * record;
            ViewBag.keySearch = keySearch;
            ViewBag.Page = page;
            ViewBag.Trang = trang;
            dsTinh = dsTinh.Skip(trangskip).Take(record).ToList();
            return View(dsTinh);
        }

        [CheckSession]
        public ActionResult CreateTinh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTinh(QLTinh model)
        {
            if (ModelState.IsValid)
            {
                db.themTinh(model);
                return RedirectToAction("DanhSachTinh");
            }
            else
            {
                return RedirectToAction("CreateTinh");
            }
        }

        [CheckSession]
        public ActionResult EditTinh(int id)
        {
            return View(db.layTinh(id));
        }
        [HttpPost]
        public ActionResult EditTinh(QLTinh model)
        {
            db.suaTinh(model);
            return RedirectToAction("DanhSachTinh");
        }
        public ActionResult DeleteTinh(int id)
        {
            db.xoaTinh(id);
            return RedirectToAction("DanhSachTinh");
        }

        //Quản lý Huyện
        [CheckSession]
        public ActionResult DanhSachHuyen(string keySearch, int page = 1)
        {
            QLCNEntities db = new QLCNEntities();
            List<QLHuyen> dsHuyen = db.QLHuyens.Where(x => x.TenHuyen.Contains(keySearch) || 
                                                      x.QLTinh.TenTinh.Contains(keySearch) || 
                                                      keySearch == null).ToList();
            int record = 10;
            int trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsHuyen.Count) / Convert.ToDouble(record)));
            int trangskip = (page - 1) * record;
            ViewBag.keySearch = keySearch;
            ViewBag.Page = page;
            ViewBag.Trang = trang;
            dsHuyen = dsHuyen.Skip(trangskip).Take(record).ToList();
            return View(dsHuyen);
            //return View(db.laydsHuyen().Where(x=>x.TenHuyen.ToLower().Contains(s)));
        }

        [CheckSession]
        public ActionResult CreateHuyen()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult CreateHuyen(QLHuyen model)
        {
            if (ModelState.IsValid)
            {
                db.themHuyen(model);
                return RedirectToAction("DanhSachHuyen");
            }
            else
            {
                return RedirectToAction("CreateHuyen");
            }
        }

        [CheckSession]
        public ActionResult EditHuyen(int id)
        {
            return View(db.layHuyen(id));
        }

        [HttpPost]
        public ActionResult EditHuyen(QLHuyen model)
        {
            db.suaHuyen(model);
            return RedirectToAction("DanhSachHuyen");
        }
        public ActionResult DeleteHuyen(int id)
        {
            db.xoaHuyen(id);
            return RedirectToAction("DanhSachHuyen");
        }

        //Quản lý Xã
        [CheckSession]
        public ActionResult DanhSachXa(string keySearch, int page = 1)
        {       
            QLCNEntities db = new QLCNEntities();
            List<QLXa> dsXa = db.QLXas.Where(x => x.TenXa.Contains(keySearch) || 
                                             x.QLHuyen.TenHuyen.Contains(keySearch)|| 
                                             x.QLTinh.TenTinh.Contains(keySearch)||
                                             keySearch == null).ToList();
            int record = 10;
            int trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsXa.Count) / Convert.ToDouble(record)));
            int trangskip = (page - 1) * record;
            ViewBag.keySearch = keySearch;
            ViewBag.Page = page;
            ViewBag.Trang = trang;
            dsXa = dsXa.Skip(trangskip).Take(record).ToList();
            return View(dsXa);

            //var dsXa =  from t in db.QLTinhs
            //            join h in db.QLHuyens
            //            on t.ID equals h.IDTinh
            //            join x in db.QLXas
            //            on h.ID equals x.IDHuyen
            //            select new QLXa1
            //            {
            //                tinhdetails = t,
            //                huyendetails = h,
            //                xadetails = x
            //            };
            //return View(dsXa);
        }

        [CheckSession]
        public ActionResult CreateXa()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult CreateXa(QLXa model)
        {
            if (ModelState.IsValid)
            {
                db.themXa(model);
                return RedirectToAction("DanhSachXa");
            }
            else
            {
                return RedirectToAction("CreateXa");
            }
        }
        [CheckSession]
        public ActionResult EditXa(int id,int tinhId)
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            List<QLHuyen> dsHuyen = db.QLHuyens.Where(x => x.IDTinh == tinhId).ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            ViewBag.dsHuyen = new SelectList(dsHuyen, "ID", "TenHuyen");
            return View(db.QLXas.Find(id));
        }
        [HttpPost]
        public ActionResult EditXa(QLXa model)
        {
            db.suaXa(model);
            return RedirectToAction("DanhSachXa");
        }
        public ActionResult DeleteXa(int id)
        {
            db.xoaXa(id);
            return RedirectToAction("DanhSachXa");
        }
        
        public ActionResult GetXaTheoTinh()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            return View();
        }
        public JsonResult GetXaTheoHuyen(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<QLHuyen> dsHuyen = db.QLHuyens.Where(x => x.IDTinh == tinhID).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }
    }
}