using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN.Models.QLDuLieu
{
    public class QLDuLieu
    {
        QLCNEntities db = new QLCNEntities();
         public IEnumerable<DataHuyen> laydsDuLieu()
        {
            return db.DataHuyens.ToList();
        }
        public void themDuLieu(DataHuyen n)
        {
            db.DataHuyens.Add(n);
            db.SaveChanges();
        }
        public DataHuyen layDuLieu(int id)
        {
            return db.DataHuyens.Find(id);
        }
        public void suaDuLieu(DataHuyen n)
        {
            DataHuyen x = layDuLieu(n.dthID);
            x.NamDanhGia = n.NamDanhGia;
            x.SoHoDan = n.SoHoDan;
            x.SoHoNgheo = n.SoHoNgheo;
            db.SaveChanges();
        }
        public void xoaDuLieu(int id)
        {
            DataHuyen n = layDuLieu(id);
            db.DataHuyens.Remove(n);
            db.SaveChanges();
        }
        public IEnumerable<DanhGia> laydsDanhGia()
        {
            return db.DanhGias.ToList();
        }
        public void themDanhGia(DanhGia n)
        {
            db.DanhGias.Add(n);
            db.SaveChanges();
        }

        public DanhGia layDanhGia(int id)
        {
            return db.DanhGias.Find(id);
        }
        public void suaDanhGia(DanhGia n)
        {
            DanhGia x = layDanhGia(n.danhgiaID);
            x.HoDan = n.HoDan;
            x.HoNgheo = n.HoNgheo;
            x.LoaihinhQL = n.LoaihinhQL;
            x.ThuphiDV = n.ThuphiDV;
            x.DatQCVN = n.DatQCVN;
            x.KhaNangCapNuoc = n.KhaNangCapNuoc;
            x.TyLeDauNoi = n.TyLeDauNoi;
            x.CanboQL = n.CanboQL;
            db.SaveChanges();
        }
        public void xoaDanhGia(int id)
        {
            DanhGia n = layDanhGia(id);
            db.DanhGias.Remove(n);
            db.SaveChanges();
        }
    }
}