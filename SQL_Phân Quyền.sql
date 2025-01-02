-- 1. TẠO TK LOGIN, USER
CREATE LOGIN thanh WITH PASSWORD = 'thanh123';
CREATE LOGIN tuan WITH PASSWORD = 'tuan123';
CREATE LOGIN phong WITH PASSWORD = 'phong123';
CREATE LOGIN quan WITH PASSWORD = 'quan123';


CREATE USER thanh FOR LOGIN thanh;
CREATE USER tuan FOR LOGIN tuan;
CREATE USER phong FOR LOGIN phong;
CREATE USER quan FOR LOGIN quan;


-- 2. TẠO NHÓM QUYỀN ROLE
CREATE ROLE role_admin;
CREATE ROLE role_thungan;
CREATE ROLE role_kythuat;

-- 3. CẤP QUYỀN CHO ADMIN, THU NGÂN, KĨ THUẬT
EXEC sp_addrolemember 'db_owner', 'role_admin';
EXEC sp_addrolemember 'role_admin', 'thanh';


GRANT SELECT, UPDATE ON BanBilliards To role_thungan;
GRANT SELECT ON ThucDon TO role_thungan;
GRANT SELECT, INSERT, DELETE, UPDATE ON KhachHang TO role_thungan;
GRANT SELECT, UPDATE ON HoaDon TO role_thungan;
EXEC sp_addrolemember 'role_thungan', 'tuan';
EXEC sp_addrolemember 'role_thungan', 'phong';


GRANT SELECT, UPDATE, DELETE ON BanBilliards TO role_kythuat;
GRANT SELECT, UPDATE, DELETE ON KhoHang TO role_kythuat;
GRANT SELECT, UPDATE ON ThucDon TO role_kythuat;
EXEC sp_addrolemember 'role_kythuat', 'quan';


-- 4. HỦY QUYỀN INSERT TỪ NHÓM QUYỀN role_thungan
REVOKE INSERT ON BanBillards FROM role_thungan;


-- 5. XÓA NGƯỜI DÙNG KHỎI NHÓM QUYỀN
EXEC sp_droprolemember 'role_thungan', 'phong';
