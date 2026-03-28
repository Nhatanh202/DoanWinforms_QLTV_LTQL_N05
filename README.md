# 📚 Đồ án LTQL: Hệ thống Quản lý Thư viện (Library Management System)

Chào mừng đến với kho lưu trữ mã nguồn (Repository) Đồ án môn Lập trình Quản lý của nhóm chúng mình. Ứng dụng được xây dựng trên nền tảng **C# WinForms** và hệ quản trị cơ sở dữ liệu **SQL Server**.

---

## 📖 Giới thiệu chung
Phần mềm Quản lý Thư viện giúp số hóa quy trình mượn/trả sách, quản lý độc giả, nhân viên và theo dõi tình trạng kho sách một cách trực quan, chính xác. 
* **Thời gian hoàn thành mục tiêu:** 10/04/2026.

## 🛠 Công nghệ & Kỹ thuật nổi bật
* **Ngôn ngữ lập trình:** C# (Windows Forms).
* **Cơ sở dữ liệu:** Microsoft SQL Server.
* **Kiến trúc dữ liệu:** ADO.NET (Sử dụng `SqlConnection`, `SqlCommand`, `DataTable`).
* **Xử lý hình ảnh:** * Lưu trữ đường dẫn ảnh (String/NVARCHAR) cho Độc giả.
  * Lưu trữ trực tiếp ảnh dưới dạng mảng Byte (VARBINARY) cho Nhân viên.
* **Quản lý mã nguồn:** Git & GitHub.

---

## 👥 Thành viên nhóm & Phân công công việc

| STT | Thành viên | Nhiệm vụ Code (WinForms & SQL) | Nhiệm vụ Báo cáo (Word/PPT) |
|:---:|:---|:---|:---|
| 1 | **[Tên Trưởng Nhóm]** (Leader)| Tạo khung Project, Class Kết nối CSDL. Code Form Mượn/Trả sách. Ghép code. | Sơ đồ CSDL (ERD), Lời mở đầu, Kết luận, Tổng hợp file Word. |
| 2 | **Đạt** | Code Form Đăng nhập, Phân quyền. Form Nhân viên (Xử lý ảnh `VARBINARY`), Tài khoản. | Chụp ảnh app, viết Hướng dẫn sử dụng (User Manual). |
| 3 | **Khải** | Code Form Thể loại, Sách. Form Độc giả (Xử lý ảnh `STRING`). | Viết Sơ đồ Use-case (Đăng nhập, Thêm sách, Mượn trả...). |
| 4 | **Hào** | Thiết kế UI/UX tổng thể. Code Form Tìm kiếm sách, Thống kê. | Viết Mô tả bài toán, dán ảnh giao diện và giải thích. |
| 5 | **Huy** | Viết Trigger SQL Server. Bắt lỗi (Validation) nhập liệu trên form. Testing phần mềm. | Lên Kịch bản Test case. Thiết kế Slide PowerPoint báo cáo. |

---
