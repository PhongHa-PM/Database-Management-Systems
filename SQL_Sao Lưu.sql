USE msdb;
GO

-- Xóa job nếu đã tồn tại
IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = N'FullBU')
    EXEC sp_delete_job @job_name = N'FullBU';
GO

-- Tạo job Full Backup
EXEC sp_add_job
    @job_name = N'FullBU';
GO

-- Thêm bước cho job Full Backup
EXEC sp_add_jobstep
    @job_name = N'FullBU',
    @step_name = N'Bước Full Backup',
    @subsystem = N'TSQL',
    @command = N'BACKUP DATABASE QL_BIDA1 TO DISK = ''D:\HUIT\Hệ Quản Trị CSDL\Backup\QL_BIDA1_Full.bak'' WITH INIT, NAME = ''Sao lưu toàn bộ hàng ngày cho QL_BIDA1'';',
    @on_success_action = 1,  -- Kết thúc job nếu thành công
    @on_fail_action = 2;     -- Dừng job nếu thất bại
GO

-- Lên lịch chạy job Full Backup lúc 23:00 hàng ngày
EXEC sp_add_jobschedule
    @job_name = N'FullBU',
    @name = N'Lịch sao lưu FULL mỗi ngày',
    @freq_type = 4,          -- Hàng ngày
    @freq_interval = 1,      -- Mỗi ngày
    @active_start_time = 230000;  -- 23:00
GO

-- Gắn job vào SQL Server Agent
EXEC sp_add_jobserver
    @job_name = N'FullBU';
GO


-- Xóa job nếu đã tồn tại
IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = N'DiffBU')
    EXEC sp_delete_job @job_name = N'DiffBU';
GO

-- Tạo job Differential Backup
EXEC sp_add_job
    @job_name = N'DiffBU';
GO

-- Thêm bước cho job Differential Backup
EXEC sp_add_jobstep
    @job_name = N'DiffBU',
    @step_name = N'Bước Differential Backup',
    @subsystem = N'TSQL',
    @command = N'BACKUP DATABASE QL_BIDA1 TO DISK = ''D:\HUIT\Hệ Quản Trị CSDL\Backup\QL_BIDA1_Diff.bak'' WITH DIFFERENTIAL, NAME = ''Sao lưu vi sai cho QL_BIDA1'';',
    @on_success_action = 1,
    @on_fail_action = 2;
GO

-- Lên lịch chạy job Differential Backup mỗi 6 giờ
EXEC sp_add_jobschedule
    @job_name = N'DiffBU',
    @name = N'Lịch sao lưu Diff 6 giờ/lần',
    @freq_type = 4,                 -- Hàng ngày
    @freq_interval = 1,             -- Mỗi ngày
    @freq_subday_type = 8,          -- Theo giờ
    @freq_subday_interval = 6,      -- Mỗi 6 giờ
    @active_start_time = 0;         -- Bắt đầu từ 00:00
GO

-- Gắn job vào SQL Server Agent
EXEC sp_add_jobserver
    @job_name = N'DiffBU';
GO

-- Xóa job nếu đã tồn tại
IF EXISTS (SELECT 1 FROM msdb.dbo.sysjobs WHERE name = N'TransBU')
    EXEC sp_delete_job @job_name = N'TransBU';
GO

-- Tạo job Transaction Log Backup
EXEC sp_add_job
    @job_name = N'TransBU';
GO

-- Thêm bước cho job Transaction Log Backup
EXEC sp_add_jobstep
    @job_name = N'TransBU',
    @step_name = N'Bước Transaction Log Backup',
    @subsystem = N'TSQL',
    @command = N'BACKUP LOG QL_BIDA1 TO DISK = ''D:\HUIT\Hệ Quản Trị CSDL\Backup\QL_BIDA1_Log.bak'' WITH INIT, NAME = ''Sao lưu log giao dịch cho QL_BIDA1'';',
    @on_success_action = 1,
    @on_fail_action = 2;
GO

-- Lên lịch chạy job Transaction Log Backup mỗi 15 phút
EXEC sp_add_jobschedule
    @job_name = N'TransBU',
    @name = N'Lịch sao lưu Log 15 phút/lần',
    @freq_type = 4,                  -- Hàng ngày
    @freq_interval = 1,              -- Mỗi ngày
    @freq_subday_type = 4,           -- Theo phút
    @freq_subday_interval = 15,      -- Mỗi 15 phút
    @active_start_time = 0;          -- Bắt đầu từ 00:00
GO

-- Gắn job vào SQL Server Agent
EXEC sp_add_jobserver
    @job_name = N'TransBU';
GO
