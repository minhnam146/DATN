using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN.Models
{
    public class QLDiaBanCapNuoc
    {
        QLCNEntities db = new QLCNEntities();

        //Quản lý Tỉnh
        public IEnumerable<QLTinh> laydsTinh()
        {
            return db.QLTinhs.ToList();
        }
        public QLTinh layTinh(int id)
        {
            return db.QLTinhs.Find(id);
        }
        public void themTinh(QLTinh n)
        {
            db.QLTinhs.Add(n);
            db.SaveChanges();
        }
        public void suaTinh(QLTinh n)
        {
            QLTinh x = layTinh(n.ID);
            x.TenTinh = n.TenTinh;
            db.SaveChanges();
        }
        public void xoaTinh(int id)
        {
            QLTinh n = layTinh(id);
            db.QLTinhs.Remove(n);
            db.SaveChanges();
        }

        //Quản lý Huyện
        public IEnumerable<QLHuyen> laydsHuyen()
        {
            return db.QLHuyens.ToList();
        }
        public QLHuyen layHuyen(int id)
        {
            return db.QLHuyens.Find(id);
        }
        public void themHuyen(QLHuyen n)
        {
            db.QLHuyens.Add(n);
            db.SaveChanges();
        }
        public void suaHuyen(QLHuyen n)
        {
            QLHuyen x = layHuyen(n.ID);
            x.IDTinh = n.IDTinh;
            x.TenHuyen = n.TenHuyen;
            db.SaveChanges();
        }
        public void xoaHuyen(int id)
        {
            QLHuyen n = layHuyen(id);
            db.QLHuyens.Remove(n);
            db.SaveChanges();
        }

        //Quản lý Xã
        public IEnumerable<QLXa> laydsXa()
        {
            return db.QLXas.ToList();
        }
        public QLXa layXa(int id)
        {
            return db.QLXas.Find(id);
        }
        public void themXa(QLXa n)
        {
            db.QLXas.Add(n);
            db.SaveChanges();
        }
        public void suaXa(QLXa n)
        {
            QLXa x = layXa(n.ID);
            x.TenXa = n.TenXa;
            db.SaveChanges();
        }
        public void xoaXa(int id)
        {
            QLXa n = layXa(id);
            db.QLXas.Remove(n);
            db.SaveChanges();
        }
    }
}