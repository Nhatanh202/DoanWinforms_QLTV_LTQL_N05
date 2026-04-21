using System;
using System.Collections.Generic;
using System.Linq;

namespace Doan_QLTV.Models
{
 // Minimal in-memory stub to satisfy compilation and allow the app to run for UI/testing.
 internal class QLTVDbContext
 {
 public DbSetNhanVien NhanVien { get; }

 public QLTVDbContext()
 {
 NhanVien = new DbSetNhanVien();
 // seed sample data with correct Vietnamese strings
 NhanVien.Add(new NhanVien { ID =1, HoVaTen = "Admin", DienThoai = "0123456789", DiaChi = "HN", TenDangNhap = "admin", MatKhau = "", QuyenHan = true, Anh = null, ChucVu = "Quản tr viên" });
 NhanVien.Add(new NhanVien { ID =2, HoVaTen = "ThuThu", DienThoai = "0987654321", DiaChi = "HCM", TenDangNhap = "thuthu", MatKhau = "", QuyenHan = false, Anh = null, ChucVu = "Thủ thư" });
 NhanVien.Add(new NhanVien { ID =3, HoVaTen = "DocGia", DienThoai = "0912345678", DiaChi = "DN", TenDangNhap = "docgia", MatKhau = "", QuyenHan = false, Anh = null, ChucVu = "Tác giả" });
 }

 public void SaveChanges()
 {
 // no-op for stub
 }

 public EntryStub<T> Entry<T>(T entity) where T : class
 {
 return new EntryStub<T>(entity);
 }
 }

 internal class DbSetNhanVien
 {
 private readonly List<NhanVien> _items = new List<NhanVien>();

 public List<NhanVien> ToList()
 {
 return new List<NhanVien>(_items);
 }

 public void Add(NhanVien nv)
 {
 // set ID if missing
 if (nv.ID ==0)
 nv.ID = (_items.Count >0) ? _items.Max(x => x.ID) +1 :1;
 _items.Add(nv);
 }

 public void Update(NhanVien nv)
 {
 var existing = Find(nv.ID);
 if (existing != null)
 {
 existing.HoVaTen = nv.HoVaTen;
 existing.DienThoai = nv.DienThoai;
 existing.DiaChi = nv.DiaChi;
 existing.TenDangNhap = nv.TenDangNhap;
 existing.MatKhau = nv.MatKhau;
 existing.QuyenHan = nv.QuyenHan;
 existing.Anh = nv.Anh;
 existing.ChucVu = nv.ChucVu;
 }
 }

 public void Remove(NhanVien nv)
 {
 _items.Remove(nv);
 }

 public NhanVien Find(int id)
 {
 return _items.FirstOrDefault(x => x.ID == id);
 }
 }

 internal class EntryStub<T> where T : class
 {
 private readonly T _entity;
 public EntryStub(T entity) { _entity = entity; }
 public PropertyEntry Property(Func<T, object> propertyExpression)
 {
 return new PropertyEntry();
 }
 }

 internal class PropertyEntry
 {
 public bool IsModified { get; set; }
 }

 internal class NhanVien
 {
 public int ID { get; set; }
 public string HoVaTen { get; set; }
 public string DienThoai { get; set; }
 public string DiaChi { get; set; }
 public string TenDangNhap { get; set; }
 public string MatKhau { get; set; }
 public bool QuyenHan { get; set; }
 public byte[] Anh { get; set; }
 public string ChucVu { get; set; }
 }
}
