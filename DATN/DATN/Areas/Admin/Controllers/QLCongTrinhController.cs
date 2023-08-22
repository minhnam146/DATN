using DATN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATN.App_Start;

namespace DATN.Areas.Admin.Controllers
{
    public class QLCongTrinhController : Controller
    {
        // GET: Admin/QLCongTrinh

        QLCongTrinhCN db = new QLCongTrinhCN();

        [CheckSession]
        public ActionResult DanhSachCongTrinh(int? idloaiCT, string keySearch, int page = 1)
        {
            QLCNEntities db = new QLCNEntities();
            List<QLCongTrinh> dsCongTrinh = db.QLCongTrinhs.Where(x=>x.TenCT.Contains(keySearch) && x.LoaiCT == idloaiCT ||
                                                                 (x.TenCT.Contains(keySearch) && idloaiCT == null) ||
                                                                 (keySearch == null && x.LoaiCT == idloaiCT) ||
                                                                 keySearch == null && idloaiCT == null).ToList();
            int record = 15;
            int trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsCongTrinh.Count) / Convert.ToDouble(record)));
            int trangskip = (page - 1) * record;
            ViewBag.idloaiCT = idloaiCT;
            ViewBag.keySearch = keySearch;
            ViewBag.Page = page;
            ViewBag.Trang = trang;
            dsCongTrinh = dsCongTrinh.Skip(trangskip).Take(record).ToList();
            return View(dsCongTrinh);
        }
        [CheckSession]
        public ActionResult ChiTietCongTrinh(int id)
        {
            //QLCNEntities db = new QLCNEntities();
            //QLCongTrinh congTrinh = db.QLCongTrinhs.Find(id);
            //return View(congTrinh);
            return View(db.layCongTrinh(id));
        }
        [CheckSession]
        public ActionResult CreateCongTrinh()
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            List<LoaiCongTrinh> dsloaiCT = db.LoaiCongTrinhs.ToList();
            ViewBag.dsloaiCT = new SelectList(dsloaiCT, "loaiCTID","tenloaiCT");
            ViewBag.nguonNuoc = new SelectList(GetNguonNuoc(), "tenNguonNuoc", "tenNguonNuoc");
            ViewBag.loaiHinh = new SelectList(GetLoaiHinh(), "tenLoaiHinh", "tenLoaiHinh");
            ViewBag.loaihinhQL = new SelectList(GetLoaiHinhQL(), "tenloaihinhQL", "tenloaihinhQL");
            ViewBag.hienTrang = new SelectList(GetHienTrang(), "tenHienTrang", "tenHienTrang");
            ViewBag.dsyesNo = new SelectList(GetYesNo(), "Value", "Text");
            return View();
        }
        [HttpPost]
        public ActionResult CreateCongTrinh(QLCongTrinh model)
        {
            if (ModelState.IsValid)
            {
                db.themCongTrinh(model);
                return RedirectToAction("DanhSachCongTrinh");
            }
            else
            {
                return RedirectToAction("CreateCongTrinh");
            }
        }
        [CheckSession]
        public ActionResult EditCongTrinh(int id, int tinhID, int huyenID)
        {
            QLCNEntities db = new QLCNEntities();
            List<QLTinh> dsTinh = db.QLTinhs.ToList();
            ViewBag.dsTinh = new SelectList(dsTinh, "ID", "TenTinh");
            List<QLHuyen> dsHuyen = db.QLHuyens.Where(x => x.IDTinh == tinhID).ToList();
            ViewBag.dsHuyen = new SelectList(dsHuyen, "ID", "TenHuyen");
            List<QLXa> dsXa = db.QLXas.Where(x => x.IDHuyen == huyenID).ToList();
            ViewBag.dsXa = new SelectList(dsXa, "ID", "TenXa");
            List<LoaiCongTrinh> dsloaiCT = db.LoaiCongTrinhs.ToList();
            ViewBag.dsloaiCT = new SelectList(dsloaiCT, "loaiCTID", "tenloaiCT");
            ViewBag.dsnguonNuoc = new SelectList(GetNguonNuoc(), "tenNguonNuoc", "tenNguonNuoc");
            ViewBag.dsloaiHinh = new SelectList(GetLoaiHinh(), "tenLoaiHinh", "tenLoaiHinh");
            ViewBag.dshienTrang = new SelectList(GetHienTrang(), "tenHienTrang", "tenHienTrang");
            ViewBag.dsloaihinhQL = new SelectList(GetLoaiHinhQL(), "tenloaihinhQL", "tenloaihinhQL");
            ViewBag.dsyesNo = new SelectList(GetYesNo(), "Value", "Text");
            return View(db.QLCongTrinhs.Find(id));
        }
        [HttpPost]
        public ActionResult EditCongTrinh(QLCongTrinh model)
        {
            db.suaCongTrinh(model);
            return RedirectToAction("DanhSachCongTrinh");
        }
        public ActionResult DeleteCongTrinh(int id)
        {
            db.xoaCongTrinh(id);
            return RedirectToAction("DanhSachCongTrinh");
        }
        public JsonResult GetHuyen(int tinhID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<QLHuyen> dsHuyen = db.QLHuyens.Where(x => x.IDTinh == tinhID).ToList();
            return Json(dsHuyen, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetXa(int huyenID)
        {
            QLCNEntities db = new QLCNEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<QLXa> dsXa = db.QLXas.Where(x => x.IDHuyen == huyenID).ToList();
            return Json(dsXa, JsonRequestBehavior.AllowGet);
        }

        private List<NguonNuoc> GetNguonNuoc()
        {
            var nguonNuoc = new List<NguonNuoc>();
            nguonNuoc.Add(new NguonNuoc() { nguonnuocID = 1, tenNguonNuoc = "Nước mặt" });
            nguonNuoc.Add(new NguonNuoc() { nguonnuocID = 2, tenNguonNuoc = "Nước ngầm" });        
            return nguonNuoc;
        }

        private List<LoaiHinh> GetLoaiHinh()
        {
            var loaiHinh = new List<LoaiHinh>();
            loaiHinh.Add(new LoaiHinh() { loaihinhID = 1, tenLoaiHinh = "Bơm dẫn" });
            loaiHinh.Add(new LoaiHinh() { loaihinhID = 2, tenLoaiHinh = "Tự chảy" });
            return loaiHinh;
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

        private List<HienTrangHD> GetHienTrang()
        {
            var hienTrang = new List<HienTrangHD>();
            hienTrang.Add(new HienTrangHD() { hientrangID = 1, tenHienTrang = "Bền vững" });
            hienTrang.Add(new HienTrangHD() { hientrangID = 2, tenHienTrang = "Trung bình" });
            hienTrang.Add(new HienTrangHD() { hientrangID = 3, tenHienTrang = "Kém hiệu quả" });
            hienTrang.Add(new HienTrangHD() { hientrangID = 4, tenHienTrang = "Không hoạt động" });
            return hienTrang;
        }

        private List<YesNo> GetYesNo()
        {
            var yesNo = new List<YesNo>();
            yesNo.Add(new YesNo() { Value = true, Text = "Có" });
            yesNo.Add(new YesNo() { Value = false, Text = "Không" });
            return yesNo;
        }

        //public ActionResult DanhSachCTloai1()
        //{
        //    QLCNEntities db = new QLCNEntities();
        //    var loai1 = (from item in db.QLCongTrinhs
        //                 where item.LoaiCT == 1
        //                 select item).ToList();
        //    return View(loai1);
        //}
        //public ActionResult DanhSachCTloai2()
        //{
        //    QLCNEntities db = new QLCNEntities();
        //    var loai2 = (from item in db.QLCongTrinhs
        //                 where item.LoaiCT == 2
        //                 select item).ToList();
        //    return View(loai2);
        //}
        //public ActionResult DanhSachCTloai3()
        //{
        //    QLCNEntities db = new QLCNEntities();
        //    var loai3 = (from item in db.QLCongTrinhs
        //                 where item.LoaiCT == 3
        //                 select item).ToList();
        //    return View(loai3);
        //}
    }
}