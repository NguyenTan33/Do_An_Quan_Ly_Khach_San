# 🏢 Hệ Thống Quản Lý Khách Sạn (Hotel Management System)

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2%2B-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC%205-blue.svg)](https://dotnet.microsoft.com/apps/aspnet/mvc)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.0-green.svg)](https://docs.microsoft.com/en-us/ef/ef6/)
[![SQL Server](https://img.shields.io/badge/Database-MS%20SQL%20Server-red.svg)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **DoAnQuanLyKhachSan** là ứng dụng Web quản lý khách sạn toàn diện được xây dựng trên nền tảng **ASP.NET MVC 5** và **Entity Framework 6 (Database First)**. Hệ thống cung cấp giải pháp tối ưu cho bộ phận lễ tân và quản lý trong việc theo dõi sơ đồ phòng, đặt/trả phòng, chuyển phòng, tích hợp dịch vụ và quản lý doanh thu.

---

## 📌 Mục Lục
- [✨ Tính Năng Nổi Bật](#-tính-năng-nổi-bật)
- [🛠️ Công Nghệ Sử Dụng](#-công-nghệ-sử-dụng)
- [🏗️ Kiến Trúc & Thiết Kế](#-kiến-trúc--thiết-kế)
- [📁 Cấu Trúc Dự Án](#-cấu-trúc-dự-án)
- [🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án](#-hướng-dẫn-cài-đặt--chạy-dự-án)
- [👤 Tác Giả](#-tác-giả)

---

## ✨ Tính Năng Nổi Bật

### 1. 📊 Sơ Đồ Phòng & Trạng Thái Theo Thời Gian Thực (Room Status Matrix)
- **Giao diện Ma trận Phòng theo Tầng**: Hiển thị danh sách tất cả các tầng và phòng trực quan.
- **Tự động nhận diện trạng thái**: Hệ thống tự động truy vấn khoảng thời gian `CheckInDate` - `CheckOutDate` để đánh dấu phòng **Trống** hoặc **Đang có khách lưu trú**.

### 2. 🛎️ Quản Lý Đặt Phòng & Check-in / Check-out (Booking Management)
- Tạo đơn đặt phòng mới cho khách hàng với đầy đủ thông tin lưu trú.
- Hỗ trợ xử lý check-in (nhận phòng) và check-out (trả phòng) nhanh chóng.
- Tự động tính toán tổng chi phí tiền phòng dựa trên số ngày lưu trú thực tế.

### 3. 🔄 Chuyển Phòng & Ghi Vết Lịch Sử (Room Transfer & Audit Log)
- Cho phép chuyển khách đang lưu trú sang phòng trống khác theo yêu cầu.
- **Chống trùng lịch (Overlap Prevention)**: Tự động lọc và chỉ hiển thị các phòng trống khả dụng trong khoảng thời gian hiện tại.
- **Lưu vết lịch sử (`RoomTransfer`)**: Ghi lại lịch sử chuyển phòng (Phòng cũ, phòng mới, thời gian chuyển, ghi chú) giúp quản lý dễ dàng đối soát.

### 4. 🍸 Quản Lý Dịch Vụ Đi Kèm (Services Aggregation)
- Quản lý danh mục dịch vụ (Nước uống, đồ ăn, giặt ủi...).
- Đặt dịch vụ phát sinh trực tiếp vào hóa đơn của phòng tương ứng.

### 5. 👥 Quản Lý Khách Hàng & Nhân Viên (CRM & User Management)
- Lưu trữ hồ sơ thông tin khách hàng (Họ tên, SĐT, CCCD/CMND...).
- Quản lý danh sách nhân viên, tài khoản hệ thống và phân quyền truy cập.

---

## 🛠️ Công Nghệ Sử Dụng

### **Backend**
- **Ngôn ngữ**: C# (.NET Framework 4.7.2 / 4.8)
- **Framework**: ASP.NET MVC 5
- **ORM**: Entity Framework 6 (Database First Model `.edmx`)
- **Truy vấn**: LINQ (Language Integrated Query)

### **Database**
- **CSDL**: Microsoft SQL Server (SSMS)

### **Frontend**
- **View Engine**: Razor View Engine (`.cshtml`)
- **UI Framework**: HTML5, CSS3, Bootstrap 5
- **Scripting**: JavaScript, jQuery

---

## 🏗️ Kiến Trúc & Thiết Kế

- **Mô hình MVC (Model-View-Controller)**: Phân tách rõ ràng giữa Dữ liệu (Model), Giao diện (View) và Luồng xử lý nghiệp vụ (Controller).
- **Mô hình ViewModel (`RoomOverviewViewModel`, `RoomTransferViewModel`)**: Tách biệt hoàn toàn dữ liệu CSDL với dữ liệu hiển thị trên View, giúp bảo mật (chống Overposting) và tăng khả năng mở rộng.
- **Kiến trúc CSDL Quan hệ (Relational Database)**:
  - `Floors` (1) ─── (N) `Rooms`
  - `Customers` (1) ─── (N) `Bookings` ─── (1) `Rooms`
  - `Bookings` (1) ─── (N) `RoomTransfers`
  - `Bookings` (1) ─── (N) `BookingServices` ─── (1) `Services`

---

## 📁 Cấu Trúc Dự Án

```text
DoAnQuanLyKhachSan/
├── App_Start/              # Cấu hình Route, Bundle, Filter
├── Content/                # File CSS, Bootstrap, Hình ảnh tĩnh
├── Controllers/            # Bộ điều khiển xử lý logic nghiệp vụ
│   ├── AuthController.cs          # Đăng nhập & Đăng xuất
│   ├── BookingsController.cs      # Quản lý đặt phòng
│   ├── CustomersController.cs     # Quản lý khách hàng
│   ├── FloorsController.cs        # Quản lý tầng
│   ├── RoomsController.cs         # Quản lý phòng
│   ├── RoomTransfersController.cs # Quản lý chuyển phòng
│   ├── ServicesController.cs      # Quản lý dịch vụ
│   ├── StaffsController.cs        # Quản lý nhân viên
│   └── TongQuanController.cs      # Dashboard sơ đồ phòng
├── Models/                 # Entity Framework Model (.edmx) & ViewModels
│   ├── ViewModel/                 # Các ViewModel phục vụ hiển thị
│   ├── Booking.cs
│   ├── Customer.cs
│   ├── KhachSan.edmx              # Sơ đồ CSDL Entity Framework
│   ├── Room.cs
│   └── RoomTransfer.cs
├── Scripts/                # File JavaScript, jQuery, Bootstrap JS
├── Views/                  # Giao diện Razor Pages (.cshtml)
│   ├── Bookings/
│   ├── RoomTransfers/
│   ├── TongQuan/
│   └── Shared/
├── DoAnQuanLyKhachSan.sln  # Visual Studio Solution File
├── Web.config              # Cấu hình chuỗi kết nối CSDL & hệ thống
└── README.md
```

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### 1. Yêu Cầu Tiền Trạm (Prerequisites)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Tích hợp workload **ASP.NET and web development**).
- [Microsoft SQL Server](https://www.microsoft.com/sql-server) & SQL Server Management Studio (SSMS).
- .NET Framework 4.7.2 trở lên.

### 2. Các Bước Cài Đặt

1. **Clone Repository về máy local**:
```bash
git clone https://github.com/NguyenTan33/Do_An_Quan_Ly_Khach_San.git
cd Do_An_Quan_Ly_Khach_San
```

2. **Cấu hình Cơ sở dữ liệu (Database Setup)**:
   - Mở **SSMS** và import/tạo CSDL `QL_KhachSan` (khởi tạo các bảng `Floors`, `Rooms`, `Bookings`, `Customers`, `Services`, `RoomTransfers`...).

3. **Cấu hình Connection String**:
   - Mở file `Web.config` trong thư mục gốc dự án.
   - Cập nhật chuỗi kết nối `KhachSanEntities` phù hợp với SQL Server của bạn.

4. **Chạy Dự Án**:
   - Mở file `DoAnQuanLyKhachSan.sln` bằng Visual Studio 2022.
   - Nhấn **Restore NuGet Packages** (nếu cần).
   - Nhấn `F5` hoặc nút **Start** (IIS Express) để khởi chạy ứng dụng trên trình duyệt!

---

## 👤 Tác Giả

- **Nguyễn Minh Tân**
- **GitHub**: [@NguyenTan33](https://github.com/NguyenTan33)
- **Role**: Fullstack / Backend Developer

---
*Nếu thấy dự án hữu ích, đừng quên thả ⭐️ Star trên GitHub để ủng hộ mình nhé!*
