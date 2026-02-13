CREATE TABLE nguoi_dung (
    nguoi_dung_id INT IDENTITY(1,1) PRIMARY KEY,
    ten_nguoi_dung NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) NOT NULL UNIQUE,
    mat_khau NVARCHAR(255) NOT NULL,
	quyen NVARCHAR(50) NOT NULL DEFAULT N'Khach hang',
    link_avatar NVARCHAR(255)
);
CREATE TABLE hoa (
    hoa_id INT IDENTITY(1,1) PRIMARY KEY,
    ten_hoa NVARCHAR(150) NOT NULL,
    mo_ta NVARCHAR(MAX),
    link_hinh_anh NVARCHAR(255),
    gia_ban DECIMAL(18,2) NOT NULL CHECK (gia_ban >= 0)
);
CREATE TABLE don_hang (
    don_hang_id INT IDENTITY(1,1) PRIMARY KEY,
    ten_don_hang NVARCHAR(150) NOT NULL,
    nguoi_dung_id INT NOT NULL,
    hoa_id INT NOT NULL,
    so_luong INT NOT NULL CHECK (so_luong > 0),
    gia_don_hang DECIMAL(18,2) NOT NULL CHECK (gia_don_hang >= 0),
    thoi_gian_dat DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_donhang_nguoidung 
        FOREIGN KEY (nguoi_dung_id) 
        REFERENCES nguoi_dung(nguoi_dung_id)
        ON DELETE CASCADE,

    CONSTRAINT FK_donhang_hoa 
        FOREIGN KEY (hoa_id) 
        REFERENCES hoa(hoa_id)
        ON DELETE CASCADE
);
CREATE TABLE thanh_toan (
    thanh_toan_id INT IDENTITY(1,1) PRIMARY KEY,
    don_hang_id INT NOT NULL,
    tien_da_thanh_toan DECIMAL(18,2) NOT NULL CHECK (tien_da_thanh_toan >= 0),
    gia_don_hang DECIMAL(18,2) NOT NULL CHECK (gia_don_hang >= 0),
    trang_thai NVARCHAR(50) NOT NULL,
    thoi_gian_thanh_toan DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_thanhtoan_donhang 
        FOREIGN KEY (don_hang_id) 
        REFERENCES don_hang(don_hang_id)
        ON DELETE CASCADE
);




INSERT INTO nguoi_dung (ten_nguoi_dung, email, mat_khau, quyen, link_avatar)
VALUES 
(N'Nguyễn Văn A', 'vana@gmail.com', '123456', 'Admin', 'avatar1.jpg'),
(N'Trần Thị B', 'thib@gmail.com', '123456', 'Khach hang','avatar2.jpg');
INSERT INTO hoa (ten_hoa, mo_ta, link_hinh_anh, gia_ban)
VALUES
(N'Hoa Hồng Đỏ', N'Hoa hồng đỏ tượng trưng cho tình yêu', 'hoahongdo.jpg', 50000),
(N'Hoa Tulip Vàng', N'Hoa tulip vàng rực rỡ', 'tulipvang.jpg', 70000);
INSERT INTO don_hang 
(ten_don_hang, nguoi_dung_id, hoa_id, so_luong, gia_don_hang, thoi_gian_dat)
VALUES
(N'Đơn hàng 1', 1, 1, 2, 100000, GETDATE()),
(N'Đơn hàng 2', 2, 2, 1, 70000, GETDATE());
INSERT INTO thanh_toan 
(don_hang_id, tien_da_thanh_toan, gia_don_hang, trang_thai, thoi_gian_thanh_toan)
VALUES
(1, 100000, 100000, N'Đã thanh toán', GETDATE()),
(2, 70000, 70000, N'Đã thanh toán', GETDATE());
