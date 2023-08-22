using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN.Models
{
    public class QLCongTrinhCN
    {
        QLCNEntities db = new QLCNEntities();
        public IEnumerable<QLCongTrinh> laydsCongTrinh()
        {
            return db.QLCongTrinhs.ToList();
        }
        public QLCongTrinh layCongTrinh(int id)
        {
            return db.QLCongTrinhs.Find(id);
        }
        public void themCongTrinh(QLCongTrinh n)
        {
            db.QLCongTrinhs.Add(n);
            db.SaveChanges();
        }
        public void suaCongTrinh(QLCongTrinh n)
        {
            QLCongTrinh x = layCongTrinh(n.IDCT);
            x.TenCT = n.TenCT;
            x.LoaiCT = n.LoaiCT;
            x.IDTinh = n.IDTinh;
            x.IDHuyen = n.IDHuyen;
            x.IDXa = n.IDXa;
            x.LoaiHinh = n.LoaiHinh;
            x.CSHDTK = n.CSHDTK;
            x.CSHDTT = n.CSHDTT;
            x.LoaihinhQL = n.LoaihinhQL;
            x.HoNgheo = n.HoNgheo;
            x.NamXayDung = n.NamXayDung;
            x.NamDuaVaoSD = n.NamDuaVaoSD;
            db.SaveChanges();
        }
        public void xoaCongTrinh(int id)
        {
            QLCongTrinh n = layCongTrinh(id);
            db.QLCongTrinhs.Remove(n);
            db.SaveChanges();
        }
    }
}