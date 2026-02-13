CREATE TABLE nguoi_dung (
    nguoi_dung_id INT IDENTITY(1,1) PRIMARY KEY,
    ten_nguoi_dung NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) NOT NULL UNIQUE,
    mat_khau NVARCHAR(255) NOT NULL,
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
