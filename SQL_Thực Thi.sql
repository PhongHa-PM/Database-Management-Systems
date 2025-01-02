use master
create database QL_BIDA1
go

use QL_BIDA1
go


-- Bảng Thực Đơn (Menu)
CREATE TABLE ThucDon (
    MaMon INT PRIMARY KEY IDENTITY(1,1),
    TenMon NVARCHAR(100) NOT NULL,
    LoaiMon NVARCHAR(50) CHECK (LoaiMon IN (N'Đồ Ăn', N'Đồ Uống', N'Thuốc Lá', N'Rượu Bia')),
    NhomThucDon NVARCHAR(50),
    DonViTinh NVARCHAR(20),
    Gia DECIMAL(10, 2) NOT NULL CHECK (Gia >= 0),
    HinhAnh NVARCHAR(255),
    CONSTRAINT CK_NhomThucDon CHECK (
        (LoaiMon = N'Đồ Ăn' AND NhomThucDon IN (N'Cơm', N'Mì', N'Bánh')) OR
        (LoaiMon = N'Đồ Uống' AND NhomThucDon IN (N'Trà', N'Cà Phê', N'Nước Ngọt', N'Sinh Tố')) OR
        (LoaiMon = N'Thuốc Lá' AND NhomThucDon = N'Thuốc Lá') OR
        (LoaiMon = N'Rượu Bia' AND NhomThucDon IN (N'Rượu', N'Bia'))
    ),
    CONSTRAINT CK_DonViTinh CHECK (
        (LoaiMon = N'Đồ Ăn' AND DonViTinh IN (N'Hộp', N'Ly', N'Gói')) OR
        (LoaiMon = N'Đồ Uống' AND DonViTinh IN (N'Ly', N'Chai', N'Cốc', N'Lon')) OR
        (LoaiMon = N'Thuốc Lá' AND DonViTinh = N'Bao') OR
        (LoaiMon = N'Rượu Bia' AND DonViTinh IN (N'Chai', N'Ly', N'Lon'))
    )
);


-- Bảng Bàn Billiards (Table)
CREATE TABLE BanBilliards (
    MaBan INT PRIMARY KEY IDENTITY(1,1),
    LoaiBan NVARCHAR(20) CHECK(LoaiBan IN (N'Phăng', N'Lỗ')),
    TrangThai NVARCHAR(20) CHECK(TrangThai IN (N'Trống', N'Đang Sử Dụng', N'Bảo Trì'))
);


CREATE TABLE CaLamViec (
    MaCa INT PRIMARY KEY IDENTITY(1,1),  
    TenCa NVARCHAR(50) NOT NULL,         
    ThoiGianBatDau TIME NOT NULL,        
    ThoiGianKetThuc TIME NOT NULL,
    CONSTRAINT CK_ThoiGianCa CHECK (ThoiGianKetThuc > ThoiGianBatDau)  
);


-- Bảng Nhân Viên (Employee)
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),  
    TenNV NVARCHAR(100) NOT NULL,
	VaiTro NVARCHAR(100) NOT NULL, 
    NgaySinh DATE NOT NULL,              
    GioiTinh NVARCHAR(10) CHECK(GioiTinh IN (N'Nam', N'Nữ')) NOT NULL,  
    MaCa INT NOT NULL,                   
    HinhAnh NVARCHAR(255), 
    CONSTRAINT FK_NhanVien_CaLamViec FOREIGN KEY (MaCa) 
        REFERENCES CaLamViec(MaCa) 
);


-- Bảng Người Dùng (User) cho đăng nhập (admin, thu ngân)
CREATE TABLE NguoiDung (
    MaNguoiDung INT PRIMARY KEY IDENTITY(1,1),
    MaNV INT NOT NULL, 
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    LoaiNguoiDung NVARCHAR(20) CHECK(LoaiNguoiDung IN (N'Admin', N'Thu Ngân')),
    CONSTRAINT FK_NguoiDung_NhanVien FOREIGN KEY (MaNV)
        REFERENCES NhanVien(MaNV)
);


-- Bảng Khuyến Mãi (Promotion)
CREATE TABLE KhuyenMai (
    MaKM INT PRIMARY KEY IDENTITY(1,1),
    TenKM NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
	ThoiGianApDungStart DATE NOT NULL,
	ThoiGianApDungEnd DATE NOT NULL,
	GiaTriKM DECIMAL(5, 2) CHECK (GiaTriKM >= 0),
	CONSTRAINT CK_ThoiGianApDung CHECK (ThoiGianApDungEnd > ThoiGianApDungStart)
);


-- Bảng Kho Hàng (Inventory)
CREATE TABLE KhoHang (
    MaSP INT PRIMARY KEY, 
    SoLuong INT NOT NULL CHECK (SoLuong >= 0),
    NgayNhapGanNhat DATE NOT NULL CHECK (NgayNhapGanNhat <= GETDATE()),
    CONSTRAINT FK_KhoHang_ThucDon FOREIGN KEY (MaSP) REFERENCES ThucDon(MaMon) 
);


-- Bảng Khách Hàng (Customer)
CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    SDT NVARCHAR(15) UNIQUE CHECK (SDT LIKE '[0-9]%'), 
    NgaySinh DATE CHECK (NgaySinh <= GETDATE()), 
    Email NVARCHAR(100) UNIQUE, 
    DiemTichLuy INT NOT NULL DEFAULT 0 CHECK (DiemTichLuy >= 0)  
);


-- Bảng Hóa Đơn (HoaDon)
CREATE TABLE HoaDon (
    SoHoaDon INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT,  
    MaBan INT NOT NULL,  
    MaNV INT NOT NULL,  
    SoGioChoi DECIMAL(18, 2) NOT NULL,
    ThanhTien DECIMAL(18, 2) CHECK (ThanhTien >= 0), 
    NgayLapHoaDon DATETIME DEFAULT GETDATE(),
    HinhThucThanhToan NVARCHAR(50),
    MaKM INT,  
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaBan) REFERENCES BanBilliards(MaBan),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);


-- Bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    SoHoaDon INT NOT NULL,
    MaMon INT NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0), 
    Gia DECIMAL(10, 2) NOT NULL CHECK (Gia >= 0),  
    PRIMARY KEY (SoHoaDon, MaMon),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (SoHoaDon)
        REFERENCES HoaDon(SoHoaDon) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHoaDon_ThucDon FOREIGN KEY (MaMon)
        REFERENCES ThucDon(MaMon)
);


-- Bảng KhachHang_KhuyenMai 
CREATE TABLE KhachHang_KhuyenMai (
    MaKH INT NOT NULL,
    MaKM INT NOT NULL,
    NgayNhan DATE NOT NULL CHECK (NgayNhan <= GETDATE()),  
    PRIMARY KEY (MaKH, MaKM, NgayNhan), 
    CONSTRAINT FK_KhachHang_KhuyenMai_KhachHang FOREIGN KEY (MaKH)
        REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_KhachHang_KhuyenMai_KhuyenMai FOREIGN KEY (MaKM)
        REFERENCES KhuyenMai(MaKM)
);


INSERT INTO ThucDon (TenMon, LoaiMon, NhomThucDon, DonViTinh, Gia, HinhAnh) VALUES
(N'Cơm Tự Sôi Hadilao', N'Đồ Ăn', N'Cơm', N'Hộp', 55000.00, N'com_tu_soi.jpg'),
(N'Mì Omachi Sườn', N'Đồ Ăn', N'Mì', N'Ly', 20000.00, N'mi_omachi.jpg'),
(N'Mì Ly Hảo Hảo', N'Đồ Ăn', N'Mì', N'Ly', 25000.00, N'hao_hao.jpg'),
(N'Trà Chanh C2', N'Đồ Uống', N'Nước Ngọt', N'Chai', 20000.00, N'c2.jpg'),
(N'Thuốc Lá Richmond', N'Thuốc Lá', N'Thuốc Lá', N'Bao', 38000.00, N'richmond.jpg'),
(N'Mì Trộn Omachi', N'Đồ Ăn', N'Mì', N'Hộp', 17000.00, N'mi_tron_omachi.jpg'),
(N'Mì Modern Thịt Bò', N'Đồ Ăn', N'Mì', N'Ly', 17000.00, N'modern_bo.jpg'),
(N'Trà Ô Long Tea+', N'Đồ Uống', N'Nước Ngọt', N'Chai', 15000.00, N'o_long.jpg'),
(N'Nước Suối Lavie', N'Đồ Uống', N'Nước Ngọt', N'Chai', 10000.00, N'lavie.jpg'),
(N'Coca-Cola Zero', N'Đồ Uống', N'Nước Ngọt', N'Lon', 14000.00, N'coca_cola_zero.jpg'),
(N'Trà Đào Tea365', N'Đồ Uống', N'Trà', N'Chai', 15000.00, N'tra_dao.jpg'),
(N'Trà Sữa Trân Châu', N'Đồ Uống', N'Trà', N'Chai', 18000.00, N'tran_chau.jpg'),
(N'Cà Phê Highlands', N'Đồ Uống', N'Cà Phê', N'Lon', 25000.00, N'highlands_lon.jpg'),
(N'Red Bull', N'Đồ Uống', N'Nước Ngọt', N'Lon', 15000.00, N'red_bull.jpg'),
(N'Sinh Tố Bơ', N'Đồ Uống', N'Sinh Tố', N'Cốc', 20000.00, N'sinh_to_bo.jpg'),
(N'Cà Phê Sữa Đá', N'Đồ Uống', N'Cà Phê', N'Ly', 15000.00, N'ca_phe_sua.jpg'),
(N'Bia Tiger 330ml', N'Rượu Bia', N'Bia', N'Lon', 20000.00, N'tiger.jpg'),
(N'Bia Sapporo 330ml', N'Rượu Bia', N'Bia', N'Lon', 25000.00, N'sapporo.jpg'),
(N'Rượu Soju', N'Rượu Bia', N'Rượu', N'Chai', 400000.00, N'soju.jpg'),
(N'Rượu Vodka', N'Rượu Bia', N'Rượu', N'Chai', 300000.00, N'vodka.jpg'),
(N'Bia Heineken', N'Rượu Bia', N'Bia', N'Lon', 20000.00, N'bia_heineken.jpg'),
(N'Thuốc 3 Số 555', N'Thuốc Lá', N'Thuốc Lá', N'Bao', 30000.00, N'thuoc_555.jpg'),
(N'Thuốc Lá Marlboro', N'Thuốc Lá', N'Thuốc Lá', N'Bao', 35000.00, N'marlboro.jpg'),
(N'Cơm Nắm Cá Ngừ', N'Đồ Ăn', N'Cơm', N'Gói', 27000.00, N'com_nam_ca_ngu.jpg'),
(N'Sting Đỏ', N'Đồ Uống', N'Nước Ngọt', N'Lon', 13000.00, N'sting_dau.jpg'),
(N'Sting Vàng', N'Đồ Uống', N'Nước Ngọt', N'Chai', 15000.00, N'sting_vang.jpg'),
(N'Sinh Tố Dâu', N'Đồ Uống', N'Sinh Tố', N'Cốc', 23000.00, N'sinh_to_dau.png'),
(N'Indomie (Trộn Sẵn)', N'Đồ Ăn', N'Mì', N'Gói', 16000.00, N'indomie.jpg');


INSERT INTO BanBilliards (LoaiBan, TrangThai) VALUES
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống'),
(N'Lỗ', N'Trống'),
(N'Phăng', N'Trống');


INSERT INTO CaLamViec (TenCa, ThoiGianBatDau, ThoiGianKetThuc) VALUES
(N'Sáng', '08:00', '12:00'),
(N'Trưa', '12:00', '17:00'),
(N'Tối', '17:00', '22:00');


INSERT INTO NhanVien (TenNV, VaiTro, NgaySinh, GioiTinh, MaCa, HinhAnh) VALUES
(N'Nguyễn Văn An', N'Admin', '1990-01-15', N'Nam', 1, N'nguyen_van_an.jpg'),
(N'Trần Thị Bình', N'Thu Ngân', '1992-03-22', N'Nữ', 2, N'tran_thi_binh.jpg'),
(N'Nguyễn Văn Cường', N'Phục Vụ', '1988-05-30', N'Nam', 3, N'nguyen_van_cuong.jpg'),
(N'Lê Thị Duyên', N'Lao Công', '1995-07-19', N'Nữ', 1, N'le_thi_duyen.jpg'),
(N'Phạm Văn Hùng', N'Bảo Vệ', '1991-02-11', N'Nam', 2, N'pham_van_hung.jpg'),
(N'Trần Văn Kiên', N'Thu Ngân', '1985-09-05', N'Nam', 3, N'tran_van_kien.jpg'),
(N'Nguyễn Thị Lan', N'Phục Vụ', '1993-11-29', N'Nữ', 1, N'nguyen_thi_lan.jpg'),
(N'Vũ Văn Minh', N'Lao Công', '1994-06-17', N'Nam', 2, N'vu_van_minh.jpg'),
(N'Trần Thị Nga', N'Thu Ngân', '1996-10-12', N'Nữ', 3, N'tran_thi_nga.jpg'),
(N'Nguyễn Văn Nam', N'Admin', '1989-04-08', N'Nam', 1, N'nguyen_van_nam.jpg'),
(N'Lê Văn Oanh', N'Bảo Vệ', '1990-12-25', N'Nam', 2, N'le_van_oanh.jpg'),
(N'Trần Văn Phát', N'Phục Vụ', '1987-08-30', N'Nam', 3, N'tran_van_phat.jpg'),
(N'Nguyễn Thị Quỳnh', N'Thu Ngân', '1992-02-14', N'Nữ', 1, N'nguyen_thi_quynh.jpg'),
(N'Vũ Thị Sương', N'Lao Công', '1991-07-21', N'Nữ', 2, N'vu_thi_suong.jpg'),
(N'Phạm Văn Tùng', N'Phục Vụ', '1994-09-10', N'Nam', 3, N'pham_van_tung.jpg'),
(N'Lê Văn Thành', N'Admin', '1990-01-15', N'Nam', 1, N'admin_1.jpg'),
(N'Phạm Thu Thảo', N'Thu Ngân', '1992-03-22', N'Nữ', 2, N'thungan_1.jpg');


INSERT INTO NguoiDung (MaNV, TenDangNhap, MatKhau, LoaiNguoiDung) VALUES
(1, N'an', N'123', N'Admin'),          
(2, N'binh', N'123', N'Thu Ngân'),      
(6, N'kien987', N'pass4321', N'Thu Ngân'),       
(9, N'nga963', N'pass1470', N'Thu Ngân'),        
(10, N'nam741', N'pass2589', N'Admin'),          
(13, N'quynh852', N'pass2581', N'Thu Ngân'),
(16, N'admin', N'1', N'Admin'),          
(17, N'thungan', N'2', N'Thu Ngân');    


INSERT INTO KhuyenMai (TenKM, MoTa, ThoiGianApDungStart, ThoiGianApDungEnd, GiaTriKM) VALUES
(N'Giảm giá giờ vàng', N'Giảm 20% cho mỗi giờ chơi từ 14h đến 16h', '2024-01-01', '2024-01-31', 20.00),
(N'Giảm giá nhóm', N'Giảm 25% cho nhóm từ 5 người trở lên', '2024-05-01', '2024-05-31', 25.00),
(N'Giảm 10% cho sinh viên', N'Giảm 10% cho sinh viên có thẻ', '2024-08-01', '2024-08-31', 10.00),
(N'Mùa hè sôi động', N'Giảm giá 15% cho tất cả khách hàng trong tháng 6', '2024-06-01', '2024-06-30', 15.00),
(N'Giảm giá cho học sinh', N'Giảm 15% cho học sinh có thẻ học sinh', '2024-01-01', '2024-01-31', 15.00)


-- Thêm dữ liệu mới phù hợp với ThucDon
INSERT INTO KhoHang (MaSP, SoLuong, NgayNhapGanNhat) VALUES
-- Đồ ăn đóng hộp
(1, 120, '2024-07-11'),  -- Cơm Gà Tự Sôi Vinafood
(2, 100, '2024-04-17'),  -- Cơm Sườn Tự Sôi Vinafood
(3, 150, '2024-01-18'),  -- Mì Ý Ly Hảo Hạng
(4, 200, '2024-02-13'),  -- Bánh Mì Sandwich Wonder
(5, 180, '2024-11-13'),  -- Gỏi Cuốn Tươi ReadyMeal
(6, 500, '2024-05-18'),  -- Mì Gói Hảo Hảo Tôm Chua Cay
(7, 300, '2024-10-03'),  -- Mì Cốc Modern Thịt Bò
(8, 80, '2024-09-24'),   -- Cơm Chiên Hải Sản Đông Lạnh
(9, 600, '2024-03-23'),  -- Nước Suối Lavie 500ml
(10, 550, '2024-04-11'), -- Nước Ngọt Coca-Cola Lon 330ml
(11, 400, '2024-02-24'), -- Trà Đào Đóng Chai
(12, 350, '2024-07-27'), -- Trà Sữa Trân Châu Lon
(13, 200, '2024-07-22'), -- Cà Phê Đen Đóng Lon Highlands
(14, 300, '2024-09-02'), -- Nước Tăng Lực Red Bull
(15, 180, '2024-10-13'), -- Sinh Tố Bơ
(16, 250, '2024-02-09'), -- Cà Phê Sữa Đá
(17, 400, '2024-04-19'), -- Bia Tiger Lon 330ml
(18, 300, '2024-06-20'), -- Bia Sapporo Lon 330ml
(19, 100, '2024-08-16'), -- Rượu Vang Chile Đỏ
(20, 80, '2024-02-18'),  -- Rượu Vodka
(21, 350, '2024-05-20'), -- Bia Heineken 330ml
(22, 250, '2024-04-16'), -- Thuốc 3 số 555
(23, 300, '2024-11-01'), -- Thuốc Lá Marlboro
(24, 500, '2024-11-08'), -- Cơm Nắm Cá Ngừ
(25, 345, '2024-04-15'), -- Sting Đỏ
(26, 200, '2024-06-30'), -- Sting Vàng
(27, 230, '2024-08-31'), -- Sinh Tố Dâu
(28, 95, '2024-05-23');  -- Indomie (Trộn Sẵn)


INSERT INTO KhachHang (HoTen, SDT, NgaySinh, Email, DiemTichLuy) VALUES
(N'Nguyễn Văn An', '0912345678', '1990-01-15', 'nguyenvanan@gmail.com', 1200),
(N'Trần Thị Bích', '0987654321', '1992-03-22', 'tranthibich@gmail.com', 2500),
(N'Lê Văn Cường', '0123456789', '1988-05-30', 'levancuong@gmail.com', 1500),
(N'Phạm Thị Duyên', '0934567890', '1995-07-12', 'phamthiduyen@gmail.com', 800),
(N'Ngô Minh Đức', '0945678901', '1985-11-20', 'gominhduc@gmail.com', 3000),
(N'Tô Thị Hương', '0912345670', '1993-06-18', 'tothihuong@gmail.com', 1500),
(N'Hồ Văn Nam', '0956789012', '1989-02-25', 'hovannam@gmail.com', 900),
(N'Vũ Thị Ngọc', '0967890123', '1991-04-10', 'vuthingoc@gmail.com', 1800),
(N'Đinh Văn Khải', '0978901234', '1994-08-05', 'dinhvankhai@gmail.com', 1100),
(N'Trần Văn Sơn', '0989012345', '1996-12-30', 'tranvanson@gmail.com', 1700),
(N'Nguyễn Thị Hà', '0910123456', '1987-09-15', 'nguyenthih@gmail.com', 2200),
(N'Lê Văn Minh', '0921234567', '1992-10-20', 'levanminh@gmail.com', 3000),
(N'Trương Thị Lan', '0932345678', '1984-11-25', 'truongthilan@gmail.com', 1300),
(N'Phan Văn Hòa', '0943456789', '1990-03-14', 'phanvanhoa@gmail.com', 750),
(N'Tô Văn Vinh', '0954567890', '1995-01-08', 'tovanvinh@gmail.com', 1900);


INSERT INTO HoaDon (MaKH, MaBan, MaNV, SoGioChoi, ThanhTien, HinhThucThanhToan, MaKM) VALUES
(1, 1, 1, 2.50, 300000.00, N'Tiền mặt', NULL),          -- Khách hàng 1
(2, 2, 2, 1.75, 200000.00, N'Tiền mặt', 1),              -- Khách hàng 2
(3, 3, 3, 3.00, 450000.00, N'Chuyển khoản', 2),          -- Khách hàng 3
(4, 1, 1, 2.00, 250000.00, N'Tiền mặt', NULL),           -- Khách hàng 4
(5, 2, 2, 4.00, 600000.00, N'Chuyển khoản', NULL),       -- Khách hàng 5
(6, 3, 3, 1.50, 180000.00, N'Tiền mặt', 3),              -- Khách hàng 6
(7, 1, 1, 5.00, 700000.00, N'Tiền mặt', NULL),           -- Khách hàng 7
(8, 2, 2, 3.50, 450000.00, N'Chuyển khoản', 1),          -- Khách hàng 8
(9, 3, 3, 2.25, 300000.00, N'Tiền mặt', NULL),           -- Khách hàng 9
(10, 1, 1, 4.50, 550000.00, N'Chuyển khoản', 2),         -- Khách hàng 10
(11, 2, 2, 1.00, 150000.00, N'Tiền mặt', 3),             -- Khách hàng 11
(12, 3, 3, 2.75, 320000.00, N'Tiền mặt', NULL),          -- Khách hàng 12
(13, 1, 1, 3.25, 400000.00, N'Chuyển khoản', NULL),      -- Khách hàng 13
(14, 2, 2, 1.50, 200000.00, N'Tiền mặt', 1),             -- Khách hàng 14
(15, 3, 3, 4.00, 500000.00, N'Tiền mặt', 2);             -- Khách hàng 15


-- Thêm dữ liệu mới vào ChiTietHoaDon
INSERT INTO ChiTietHoaDon (SoHoaDon, MaMon, SoLuong, Gia) VALUES
(1, 1, 2, 110000.00),
(1, 2, 1, 20000.00),
(2, 3, 1, 25000.00),
(2, 4, 3, 60000.00),
-- Hóa đơn 3
(3, 5, 1, 38000.00),
(3, 16, 2, 30000.00),
-- Hóa đơn 4
(4, 1, 1, 55000.00),
(4, 15, 2, 40000.00),
-- Hóa đơn 5
(5, 21, 5, 100000.00),
(5, 22, 1, 30000.00),
-- Hóa đơn 6
(6, 17, 3, 60000.00),
-- Hóa đơn 7
(7, 16, 2, 30000.00),
(7, 20, 1, 300000.00),
-- Hóa đơn 8
(8, 11, 4, 60000.00),
(8, 14, 2, 30000.00),
-- Hóa đơn 9
(9, 25, 1, 13000.00),
(9, 28, 2, 32000.00);


INSERT INTO KhachHang_KhuyenMai (MaKH, MaKM, NgayNhan) VALUES
(1, 1, '2024-10-01'),              -- Khách hàng 1 nhận khuyến mãi 1
(1, 2, '2024-10-01'),              -- Khách hàng 1 nhận khuyến mãi 2
(1, 1, '2024-10-10'),              -- Khách hàng 1 nhận lại khuyến mãi 1
(2, 2, '2024-10-02'),              -- Khách hàng 2 nhận khuyến mãi 2
(2, 3, '2024-10-02'),              -- Khách hàng 2 nhận khuyến mãi 3
(3, 1, '2024-10-03'),              -- Khách hàng 3 nhận khuyến mãi 1
(4, 3, '2024-10-04'),              -- Khách hàng 4 nhận khuyến mãi 3
(5, 1, '2024-10-05'),              -- Khách hàng 5 nhận khuyến mãi 1
(6, 2, '2024-10-06'),              -- Khách hàng 6 nhận khuyến mãi 2
(7, 3, '2024-10-07'),              -- Khách hàng 7 nhận khuyến mãi 3
(8, 1, '2024-10-08'),              -- Khách hàng 8 nhận khuyến mãi 1
(9, 2, '2024-10-09'),              -- Khách hàng 9 nhận khuyến mãi 2
(10, 3, '2024-10-10'),             -- Khách hàng 10 nhận khuyến mãi 3
(11, 1, '2024-10-11'),             -- Khách hàng 11 nhận khuyến mãi 1
(12, 2, '2024-10-12'),             -- Khách hàng 12 nhận khuyến mãi 2
(13, 3, '2024-10-13'),             -- Khách hàng 13 nhận khuyến mãi 3
(14, 1, '2024-10-14'),             -- Khách hàng 14 nhận khuyến mãi 1
(15, 2, '2024-10-15');             -- Khách hàng 15 nhận khuyến mãi 2

--====================================Store Procedure==============================
go
CREATE PROCEDURE sp_GetAllThucDon
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo ảnh hưởng của các câu lệnh
    SELECT * FROM ThucDon;
END

go
CREATE PROCEDURE sp_DeleteThucDon
    @MaMon INT
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số hàng bị ảnh hưởng
    DELETE FROM ThucDon WHERE MaMon = @MaMon;
END


go
CREATE PROCEDURE sp_GetAllBanBilliards
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số dòng bị ảnh hưởng
    SELECT * FROM BanBilliards;
END

go
CREATE PROCEDURE sp_ThemBan
    @LoaiBan NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số dòng bị ảnh hưởng
    INSERT INTO BanBilliards (LoaiBan, TrangThai) 
    VALUES (@LoaiBan, 'Trống');
END
go


CREATE PROCEDURE sp_GetNhanVienList
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số dòng bị ảnh hưởng

    SELECT 
        NhanVien.MaNV, 
        NhanVien.TenNV, 
        NhanVien.VaiTro,               
        NhanVien.NgaySinh, 
        NhanVien.GioiTinh, 
        NhanVien.MaCa, 
        NhanVien.HinhAnh, 
        NguoiDung.TenDangNhap, 
        NguoiDung.MatKhau               
    FROM 
        NhanVien 
    LEFT JOIN 
        NguoiDung ON NhanVien.MaNV = NguoiDung.MaNV;
END

go
CREATE PROCEDURE sp_GetKhuyenMaiList
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số hàng bị ảnh hưởng

    SELECT 
		*  
    FROM 
        KhuyenMai;
END

go
CREATE PROCEDURE sp_GetKhachHangList
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số hàng bị ảnh hưởng

    SELECT 
        MaKH, 
        HoTen, 
        SDT, 
        NgaySinh, 
        Email, 
        DiemTichLuy 
    FROM 
        KhachHang;
END

go
CREATE PROCEDURE sp_GetHoaDonList
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số hàng bị ảnh hưởng

    SELECT 
        SoHoaDon, 
        MaKH, 
        MaBan, 
        MaNV, 
        SoGioChoi, 
        ThanhTien, 
        NgayLapHoaDon, 
        HinhThucThanhToan, 
        MaKM
    FROM HoaDon;
END
go
CREATE PROCEDURE sp_GetKhoList
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        KhoHang.MaSP,
        ThucDon.MaMon,
        ThucDon.TenMon,
        KhoHang.SoLuong,
        ThucDon.DonViTinh,
        ThucDon.Gia,
        KhoHang.NgayNhapGanNhat
    FROM 
        KhoHang 
    JOIN 
        ThucDon ON KhoHang.MaSP = ThucDon.MaMon;
END
go
CREATE PROCEDURE sp_GetNhomThucDon
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo về số hàng bị ảnh hưởng

    -- Lấy danh sách các nhóm thực đơn khác nhau
    SELECT DISTINCT NhomThucDon FROM ThucDon;
END


go
CREATE PROCEDURE sp_UpdateThucDon
    @MaMon INT,
    @TenMon NVARCHAR(100),
    @LoaiMon NVARCHAR(50),
    @NhomThucDon NVARCHAR(50),
    @DonViTinh NVARCHAR(20),
    @Gia DECIMAL(10, 2),
    @HinhAnh NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ThucDon
    SET TenMon = @TenMon,
        LoaiMon = @LoaiMon,
        NhomThucDon = @NhomThucDon,
        DonViTinh = @DonViTinh,
        Gia = @Gia,
        HinhAnh = @HinhAnh
    WHERE MaMon = @MaMon;
END

go
CREATE PROCEDURE sp_UpdateKhoHang
    @MaMon INT,
    @SoLuong INT,
    @NgayNhapGanNhat DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE KhoHang
    SET SoLuong = @SoLuong,
        NgayNhapGanNhat = @NgayNhapGanNhat
    WHERE MaSP = @MaMon;
END

go
CREATE PROCEDURE sp_DeleteKhoHangByMaMon
    @MaMon INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM KhoHang WHERE MaSP = @MaMon;
END
go
CREATE PROCEDURE sp_GetMaNVByTenDangNhap
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MaNV
    FROM NguoiDung
    WHERE TenDangNhap = @TenDangNhap;
END
go
CREATE PROCEDURE sp_GetNhanVienByTenDangNhap
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM NguoiDung
    WHERE TenDangNhap = @TenDangNhap;
END

go
CREATE PROCEDURE sp_XoaBan
    @MaBan INT
AS
BEGIN
    DELETE FROM BanBilliards WHERE MaBan = @MaBan;
END

--====================================Function==============================
go
CREATE FUNCTION dbo.fn_LayMaNV (@TenDangNhap NVARCHAR(100))
RETURNS INT
AS
BEGIN
    DECLARE @MaNV INT;

    -- Lấy MaNV từ bảng NguoiDung dựa trên TenDangNhap
    SELECT @MaNV = MaNV
    FROM NguoiDung
    WHERE TenDangNhap = @TenDangNhap;

    -- Trả về kết quả (Nếu không tìm thấy, trả về NULL)
    RETURN ISNULL(@MaNV, 0); 
END;

go
CREATE FUNCTION dbo.fn_GetMenuItems()
RETURNS @MenuTable TABLE
(
    MaMon INT,
    TenMon NVARCHAR(100),
    LoaiMon NVARCHAR(50),
    NhomThucDon NVARCHAR(50),
    Gia DECIMAL(10, 2),
    HinhAnh NVARCHAR(255)
)
AS
BEGIN
    -- Thêm dữ liệu vào bảng tạm @MenuTable từ bảng ThucDon
    INSERT INTO @MenuTable
    SELECT 
        MaMon, 
        TenMon, 
        LoaiMon, 
        NhomThucDon, 
        Gia, 
        HinhAnh
    FROM ThucDon;

    RETURN;
END;


go
CREATE FUNCTION dbo.fn_GetKhachHangList()
RETURNS @KhachHangTable TABLE
(
    MaKH INT,
    HoTen NVARCHAR(100),
    SDT NVARCHAR(20),
    NgaySinh DATE,
    Email NVARCHAR(100),
    DiemTichLuy INT
)
AS
BEGIN
    -- Thêm dữ liệu vào bảng tạm @KhachHangTable từ bảng KhachHang
    INSERT INTO @KhachHangTable
    SELECT 
        MaKH, 
        HoTen, 
        SDT, 
        NgaySinh, 
        Email, 
        DiemTichLuy
    FROM KhachHang;

    RETURN;
END;


go
CREATE FUNCTION dbo.fn_GetHoaDonList()
RETURNS @HoaDonTable TABLE
(
    SoHoaDon INT,
    MaKH INT NULL,
    MaBan INT,
    MaNV INT,
    SoGioChoi DECIMAL(10, 2),
    ThanhTien DECIMAL(18, 2) NULL,
    NgayLapHoaDon DATETIME,
    HinhThucThanhToan NVARCHAR(50),
    MaKM INT NULL
)
AS
BEGIN
    -- Thêm dữ liệu vào bảng tạm @HoaDonTable từ bảng HoaDon
    INSERT INTO @HoaDonTable
    SELECT 
        SoHoaDon, 
        MaKH, 
        MaBan, 
        MaNV, 
        SoGioChoi, 
        ThanhTien, 
        NgayLapHoaDon, 
        HinhThucThanhToan, 
        MaKM
    FROM HoaDon;

    RETURN;
END;


--====================================Cursor==============================
go
--Cập nhật trạng thái thành trống khi khác hàng thanh toán xong
DECLARE @MaBan INT, @TrangThai NVARCHAR(20);
DECLARE BanBilliardsCursor CURSOR FOR
SELECT MaBan, TrangThai FROM BanBilliards WHERE TrangThai = N'Đang Sử Dụng';
OPEN BanBilliardsCursor;
FETCH NEXT FROM BanBilliardsCursor INTO @MaBan, @TrangThai;
WHILE @@FETCH_STATUS = 0
BEGIN
   
    UPDATE BanBilliards
    SET TrangThai = N'Trống'
    WHERE MaBan = @MaBan;
    FETCH NEXT FROM BanBilliardsCursor INTO @MaBan, @TrangThai;
END;
CLOSE BanBilliardsCursor;
DEALLOCATE BanBilliardsCursor;  

go
--Duyệt qua tất cả các Khách Hàng và In Thông Tin
DECLARE @HoTen NVARCHAR(100), @SDT NVARCHAR(15);
DECLARE KhachHangCursor CURSOR FOR
SELECT HoTen, SDT FROM KhachHang;
OPEN KhachHangCursor;
FETCH NEXT FROM KhachHangCursor INTO @HoTen, @SDT;
WHILE @@FETCH_STATUS = 0
BEGIN
   
    PRINT 'Khách hàng: ' + @HoTen + N', Số điện thoại: ' + @SDT;
    FETCH NEXT FROM KhachHangCursor INTO @HoTen, @SDT;
END;

CLOSE KhachHangCursor;
DEALLOCATE KhachHangCursor;


go
--Tính Tổng Tiền của Tất Cả Hóa Đơn
DECLARE @SoHoaDon INT, @ThanhTien DECIMAL(18, 2), @TongTien DECIMAL(18, 2);
SET @TongTien = 0;
DECLARE HoaDonCursor CURSOR FOR
SELECT SoHoaDon, ThanhTien FROM HoaDon;
OPEN HoaDonCursor;
FETCH NEXT FROM HoaDonCursor INTO @SoHoaDon, @ThanhTien;
WHILE @@FETCH_STATUS = 0
BEGIN
   
    SET @TongTien = @TongTien + ISNULL(@ThanhTien, 0);
    FETCH NEXT FROM HoaDonCursor INTO @SoHoaDon, @ThanhTien;
END;
PRINT N'Tổng tiền tất cả hóa đơn: ' + CAST(@TongTien AS NVARCHAR(18));

CLOSE HoaDonCursor;
DEALLOCATE HoaDonCursor;


go
-- Hiển thị các nhân viên theo ca
DECLARE @MaCa INT, @MaNV INT, @TenNV NVARCHAR(100);
DECLARE CaLamViecCursor CURSOR FOR
SELECT DISTINCT MaCa
FROM NhanVien;
OPEN CaLamViecCursor;
FETCH NEXT FROM CaLamViecCursor INTO @MaCa;
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT N'Ca làm việc: ' + CAST(@MaCa AS NVARCHAR(10));
    DECLARE NhanVienInCaCursor CURSOR FOR
    SELECT MaNV, TenNV
    FROM NhanVien
    WHERE MaCa = @MaCa;
    OPEN NhanVienInCaCursor;
    FETCH NEXT FROM NhanVienInCaCursor INTO @MaNV, @TenNV;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT N'  Nhân viên: ' + @TenNV;
        FETCH NEXT FROM NhanVienInCaCursor INTO @MaNV, @TenNV;
    END;
    CLOSE NhanVienInCaCursor;
    DEALLOCATE NhanVienInCaCursor;
    FETCH NEXT FROM CaLamViecCursor INTO @MaCa;
END;


CLOSE CaLamViecCursor;
DEALLOCATE CaLamViecCursor;

go
--Kiểm tra các sản phẩm số lượng sắp hết 

DECLARE @MaSP INT, @SoLuong INT, @TenMon NVARCHAR(100);
DECLARE KhoHangCursor CURSOR FOR
SELECT KH.MaSP, KH.SoLuong, TD.TenMon
FROM KhoHang KH
JOIN ThucDon TD ON KH.MaSP = TD.MaMon
WHERE KH.SoLuong < 10;
OPEN KhoHangCursor;
FETCH NEXT FROM KhoHangCursor INTO @MaSP, @SoLuong, @TenMon;
WHILE @@FETCH_STATUS = 0
BEGIN
 
    PRINT N'Sản phẩm: ' + @TenMon + N' còn lại: ' + CAST(@SoLuong AS NVARCHAR(10)) + N' sản phẩm';
    FETCH NEXT FROM KhoHangCursor INTO @MaSP, @SoLuong, @TenMon;
END;

CLOSE KhoHangCursor;
DEALLOCATE KhoHangCursor;

--====================================Trigger==============================

--Trigger cập nhật giá trị DiemTichLuy của khách hàng khi thanh toán
go
CREATE TRIGGER trg_UpdateDiemTichLuy
ON ChiTietHoaDon
AFTER INSERT
AS
BEGIN
    DECLARE @MaKH INT, @ThanhTien DECIMAL(18, 2);
    SELECT @MaKH = H.MaKH
    FROM HoaDon H
    JOIN INSERTED I ON H.SoHoaDon = I.SoHoaDon;
    SELECT @ThanhTien = SUM(I.Gia * I.SoLuong)
    FROM INSERTED I
    WHERE I.SoHoaDon = (SELECT SoHoaDon FROM INSERTED);
    UPDATE KhachHang
    SET DiemTichLuy = DiemTichLuy + FLOOR(@ThanhTien / 1000)
    WHERE MaKH = @MaKH;
END;


--Kiểm tra số lượng trong kho trước khi bán

GO
CREATE TRIGGER trg_CheckKhoHang
ON ChiTietHoaDon
AFTER INSERT
AS
BEGIN
    DECLARE @MaMon INT, @SoLuong INT, @SoLuongKho INT;
    SELECT @MaMon = MaMon, @SoLuong = SoLuong
    FROM INSERTED;
    SELECT @SoLuongKho = SoLuong
    FROM KhoHang
    WHERE MaSP = @MaMon;
    IF @SoLuong > @SoLuongKho
    BEGIN
        PRINT 'Lỗi: Không đủ số lượng sản phẩm trong kho!';
        ROLLBACK TRANSACTION; 
    END
END;



-- Tự động cập nhật lại thời gian gần nhất của trong kho hàng
GO
CREATE TRIGGER trg_UpdateNgayNhapKho
ON KhoHang
AFTER INSERT
AS
BEGIN
    DECLARE @MaSP INT;
    SELECT @MaSP = MaSP FROM INSERTED;
    UPDATE KhoHang
    SET NgayNhapGanNhat = GETDATE()
    WHERE MaSP = @MaSP;
END;

-- Trigger kiểm tra khuyến mãi phải lớn hơn ngày bắt đầu
GO
CREATE TRIGGER trg_CheckKhuyenMai
ON KhuyenMai
AFTER INSERT
AS
BEGIN
    DECLARE @ThoiGianApDungStart DATE, @ThoiGianApDungEnd DATE;
    SELECT @ThoiGianApDungStart = ThoiGianApDungStart, @ThoiGianApDungEnd = ThoiGianApDungEnd
    FROM INSERTED;
    IF @ThoiGianApDungEnd <= @ThoiGianApDungStart
    BEGIN
        PRINT 'Lỗi: Ngày kết thúc khuyến mãi phải lớn hơn ngày bắt đầu!';
        ROLLBACK TRANSACTION; 
    END;
END;

-- Trigger tự động cập nhật ngày xuất hoá đơn là ngày hiện tại
GO
CREATE TRIGGER trg_UpdateNgayLapHoaDon
ON HoaDon
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @SoHoaDon INT, @NgayLapHoaDon DATETIME;
    
    SELECT @SoHoaDon = SoHoaDon FROM INSERTED;

    -- Nếu chưa có giá trị NgayLapHoaDon thì cập nhật
    SELECT @NgayLapHoaDon = NgayLapHoaDon FROM HoaDon WHERE SoHoaDon = @SoHoaDon;
    
    IF @NgayLapHoaDon IS NULL
    BEGIN
        UPDATE HoaDon
        SET NgayLapHoaDon = GETDATE()
        WHERE SoHoaDon = @SoHoaDon;
    END;
END;


---------------------View--------------------
-- Tạo View để hiển thị thông tin nhân viên
GO
CREATE VIEW V_NhanVien AS
SELECT 
    NV.MaNV,
    NV.TenNV,
    NV.VaiTro,
    ND.TenDangNhap,
    ND.MatKhau,
    NV.NgaySinh,
    NV.GioiTinh,
    NV.MaCa,
    NV.HinhAnh
FROM 
    NhanVien NV
JOIN 
    NguoiDung ND ON NV.MaNV = ND.MaNV;
GO


-- Tạo View để hiển thị thông tin khuyến mãi
CREATE VIEW V_KhuyenMai AS
SELECT 
    KM.MaKM,
    KM.TenKM AS TenLM,
    KM.MoTa,
    KM.ThoiGianApDungStart,
    KM.ThoiGianApDungEnd,
    KM.GiaTriKM,
    CONCAT(KM.TenKM, ' - ', KM.GiaTriKM, '%') AS TenKMWithValue
FROM 
    KhuyenMai KM;
GO


-- Tạo View để hiển thị thông tin kho
CREATE VIEW V_Kho AS
SELECT 
    K.MaSP,
    T.MaMon,
    T.TenMon,
    K.SoLuong,
    T.DonViTinh AS DonVi,
    T.Gia AS GiaBan,
    K.NgayNhapGanNhat
FROM 
    KhoHang K
JOIN 
    ThucDon T ON K.MaSP = T.MaMon;
GO


-- Tạo View để hiển thị thông tin lịch sử hóa đơn
CREATE VIEW V_LichSu AS
SELECT 
    SoHoaDon,
    MaKH,
    MaBan,
    MaNV,
    SoGioChoi,
    ThanhTien,
    NgayLapHoaDon,
    HinhThucThanhToan,
    MaKM
FROM 
    HoaDon;
GO


-- Tạo View để hiển thị thông tin khách hàng
CREATE VIEW V_KhachHang AS
SELECT 
    MaKH,
    HoTen,
    SDT,
    NgaySinh,
    Email,
    DiemTichLuy,
    -- Tạo trường TenHienThi bằng cách kết hợp HoTen và SDT
    CONCAT(HoTen, ' - ', SDT) AS TenHienThi
FROM 
    KhachHang;
GO


-- Tạo View để hiển thị thông tin các món trong thực đơn
CREATE VIEW V_ThucDon AS
SELECT 
    MaMon,
    TenMon,
    LoaiMon,
    NhomThucDon,
    DonViTinh,
    Gia,
    HinhAnh
FROM 
    ThucDon;
GO
