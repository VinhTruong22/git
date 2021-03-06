create database QTTB
go
USE [QTTB]
GO

create function Auto_MaThietBi()
returns nchar(10)
as
begin
declare @ma_next varchar(10)
declare @max int
select @max=count(matb) +1 from dbo.thietbi where matb like 'TB'
set @ma_next = 'TB' + right ('0' + cast (@max as nchar(8)),8)
while (exists(select matb from dbo.thietbi where matb = @ma_next))
begin
    set @max = @max +1
    set @ma_next = 'TB' + RIGHT('0' + cast (@max as nchar (8)),8)
end
	return @ma_next
end
go

CREATE FUNCTION Auto_MaDeXuat()
RETURNS nchar(5)
AS
BEGIN
	DECLARE @MaDeXuat nchar(5)
	IF (SELECT COUNT(madexuat) FROM dbo.xetduyet) = 0
		SET @MaDeXuat = '0'
	ELSE
		SELECT @MaDeXuat = MAX(RIGHT(madexuat, 3)) FROM dbo.xetduyet
		SELECT @MaDeXuat = CASE
			WHEN @MaDeXuat >= 0 and @MaDeXuat < 9 THEN N'ĐX00' + CONVERT(CHAR, CONVERT(INT, @MaDeXuat) + 1)
			WHEN @MaDeXuat >= 9 THEN N'ĐX0' + CONVERT(CHAR, CONVERT(INT, @MaDeXuat) + 1)
		END
	RETURN @MaDeXuat
END
go
/****** Object:  Table [dbo].[loaithietbi]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loaithietbi](
	[maloaitb] [nchar](2) NOT NULL,
	[tenloaitb] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_loaithietbi] PRIMARY KEY CLUSTERED 
(
	[maloaitb] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[loaiphieu]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loaiphieu](
	[maloaiphieu] [nchar](2) NOT NULL,
	[tenloaiphieu] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_loaiphieu] PRIMARY KEY CLUSTERED 
(
	[maloaiphieu] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kho]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kho](
	[makho] [nchar](2) NOT NULL,
	[tenkho] [nvarchar](100) NOT NULL,
	[diachikho] [nvarchar](100) NULL,
 CONSTRAINT [PK_kho] PRIMARY KEY CLUSTERED 
(
	[makho] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[donvitinh]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[donvitinh](
	[madonvitinh] [nchar](2) NOT NULL,
	[tendonvitinh] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_donvitinh] PRIMARY KEY CLUSTERED 
(
	[madonvitinh] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[donvi]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[donvi](
	[madonvi] [nchar](2) NOT NULL,
	[tendonvi] [nvarchar](50) NOT NULL,
	[dienthoai] [nchar](10) NOT NULL,
	[diadiem] [nvarchar](50) NOT NULL,
	[nguoicapnhat] [nchar](10),
 CONSTRAINT [PK_donvi] PRIMARY KEY CLUSTERED 
(
	[madonvi] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quyen]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[phonghoc]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phonghoc](
	[maphonghoc] [nchar](5) NOT NULL,
	[tenphonghoc] [nvarchar](50) NOT NULL,
	[diadiem] [nvarchar](50) NULL,
 CONSTRAINT [PK_phonghoc] PRIMARY KEY CLUSTERED 
(
	[maphonghoc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nguoidung]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nguoidung](
	[tendn] [nchar](10) NOT NULL,
	[mkdn] [nchar](10) NOT NULL,
	[hoten] [nvarchar](50) NOT NULL,
	[donvi] [nchar](2) NOT NULL,
	[ngaytao] [date] NOT NULL,
	[ngaydangnhap] [datetime] ,
	[gmail][nvarchar](70) not null,     
	[trangthaihoatdong] [nvarchar](20) NOT NULL, 
	[checkhoatdong] [time],
	[nguoicapnhat] [nchar](10),
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[tendn] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[doimatkhau]    Script Date: 06/21/2021 19:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create table [dbo].[quenmatkhau](
	[stt] [int] identity not null,
	[tendn] [nchar](10) not null,
	[gmail][nvarchar](70) not null,
	[noidungyeucau] [nvarchar](100) not null,
	[ngaygioyeucau] [datetime] not null,
	[sophutdayeucau] [int] not null,
	[trangthaiyeucau] [nvarchar](15) null,      -- chưa duyệt
CONSTRAINT [PK_Doimatkhau_1] PRIMARY KEY CLUSTERED 
(
	[stt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
go

create table [dbo].[lichsudangnhap](
	[id]  [int] identity,
	[tendn] [nchar](10) not null,
	[ngaydangnhap] [datetime] not null,
	constraint [PK_lichsudangnhap_1] primary key clustered
	(
	[id] asc
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

go
/****** Object:  Table [dbo].[bangphieutam]    Script Date: 07/25/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bangphieutam](
	[maphieu] [nchar](10) NOT NULL,
	[maloaiphieu] [nchar](2) NOT NULL,
	[tendn] [nchar](10) NOT NULL,
	[ngaylapphieu] [date]  NULL,
	[mota] [ntext] NULL,
	[tennguoixuat] [nvarchar](20) NULL,
	[tennguoinhap] [nvarchar](20) NULL,
	[bengiao] [nvarchar](20) NULL,
	[bennhan] [nvarchar](20) NULL,
	[nguoigiao] [nvarchar](20) NULL,
	[nguoinhan] [nvarchar](20) NULL,
	[ngaygiao] [date]  NULL,
	[diengiai] [ntext] NULL,
	[loaiphieu] [nchar](15)  NULL,
	[matb] [nchar] (10) null,
	[tenthietbi] [nvarchar](20) NULL,
	[loaithietbi] [nvarchar](20) NULL,
	[donvitinh] [nvarchar](10) NULL,
	[soluong] [real] NULL,
	[gianhap] [real] NULL,
	[giaban] [real] NULL,
	[tongtien] [float] NULL,
	[anhbill] [image] NULL,
	[namsudung] [date] NULL,
 CONSTRAINT [PK_phieu_tam] PRIMARY KEY CLUSTERED 
(
	[maphieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bangphieuchinh]    Script Date: 07/25/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bangphieuchinh](
	[maphieu] [nchar](10) NOT NULL,
	[maloaiphieu] [nchar](2) NOT NULL,
	[tendn] [nchar](10) NOT NULL,
	[ngaylapphieu] [date]  NULL,
	[mota] [ntext] NULL,
	[tennguoixuat] [nvarchar](20) NULL,
	[tennguoinhap] [nvarchar](20) NULL,
	[bengiao] [nvarchar](20) NULL,
	[bennhan] [nvarchar](20) NULL,
	[nguoigiao] [nvarchar](20) NULL,
	[nguoinhan] [nvarchar](20) NULL,
	[ngaygiao] [date]  NULL,
	[diengiai] [ntext] NULL,
	[loaiphieu] [nchar](15)  NULL,
	[matb] [nchar] (10) null,
	[tenthietbi] [nvarchar](20) NULL,
	[loaithietbi] [nvarchar](20) NULL,
	[donvitinh] [nvarchar](10) NULL,
	[gianhap] [real] NULL,
	[giaban] [real] NULL,
	[tongtien] [float] NULL,
	[anhbill] [image] NULL,
	[namsudung] [date] NULL,
 CONSTRAINT [PK_phieu_chinh] PRIMARY KEY CLUSTERED 
(
	[maphieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nguoidung_quyen]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nguoidung_quyen](
	[tendn] [nchar](10) NOT NULL,
	[tenquyen] [nchar](2) NOT NULL,
	)
go
 

/****** Object:  Table [dbo].[thietbi]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[thietbi](
	[matb] [nchar](10) NOT NULL,
	[tentb] [nvarchar](100) NOT NULL,
	[maloai] [nchar](2) NOT NULL,
	[madonvitinh] [nchar](2) NOT NULL,
	[nhaSX] [nvarchar](100) NULL,
	[anhtb] [image] NULL,
	[gianhap] [decimal](18, 0) NULL,
	[trangthai] [nvarchar](15) NOT NULL,
	[namsx] [nchar](4) NULL,
	[thoigianbaohanh] [int] NULL,
	[thoihanhandung] [int] NULL,
	[nambdsudung] [int] NOT NULL,
	[tylehaomon] [int] NULL,
	[tailieudinhkem] [ntext] NULL,
	[mota] [ntext] NULL,
	[makho] [nchar](2) NOT NULL,
	[tendn] [nchar](10) NOT NULL,
	[madonvi] [nchar](2) NOT NULL,
	[phonghoc] [nchar](5) NOT NULL,
 CONSTRAINT [PK_thietbi_1] PRIMARY KEY CLUSTERED 
(
	[matb] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[xetduyet]    Script Date: 06/09/2021 11:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[xetduyet](
	[madexuat] [nchar](10) NOT NULL default dbo.Auto_MaDeXuat(),
	[tendexuat] [nvarchar](30) NOT NULL,
	[madonvi] [nchar](2) NOT NULL,
	[maloaitb] [nchar](2) NULL,
	[tentb] [nvarchar](100) NOT NULL,
	[ngaydexuat] [date] NOT NULL,
	[lydodexuat] [ntext] NULL,
	[trangthaiduyet] [nvarchar](20) NOT NULL,
	[lydoduyet] [nvarchar](100) NULL,
	[ngayxetduyet] [date] NULL,
	[nguoidexuat] [nchar](10) NOT NULL,
	[nguoiduyet] [nchar](10)NULL,
 CONSTRAINT [PK_xetduyet] PRIMARY KEY CLUSTERED 
(
	[madexuat] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_nguoidung_donvi]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[nguoidung]  WITH CHECK ADD  CONSTRAINT [FK_nguoidung_donvi] FOREIGN KEY([donvi])
REFERENCES [dbo].[donvi] ([madonvi])
GO
ALTER TABLE [dbo].[nguoidung] CHECK CONSTRAINT [FK_nguoidung_donvi]
GO
/****** Object:  ForeignKey [FK_phieutam_loaiphieu]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[bangphieutam]  WITH CHECK ADD  CONSTRAINT [FK_phieutam_loaiphieu] FOREIGN KEY([maloaiphieu])
REFERENCES [dbo].[loaiphieu] ([maloaiphieu])
GO
ALTER TABLE [dbo].[bangphieutam] CHECK CONSTRAINT [FK_phieutam_loaiphieu]
GO
/****** Object:  ForeignKey [FK_phieuchinh_loaiphieu]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[bangphieuchinh]  WITH CHECK ADD  CONSTRAINT [FK_phieuchinh_loaiphieu] FOREIGN KEY([maloaiphieu])
REFERENCES [dbo].[loaiphieu] ([maloaiphieu])
GO
ALTER TABLE [dbo].[bangphieuchinh] CHECK CONSTRAINT [FK_phieuchinh_loaiphieu]
GO
/****** Object:  ForeignKey [FK_phieutam_nguoidung]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[bangphieutam]  WITH CHECK ADD  CONSTRAINT [FK_phieutam_nguoidung] FOREIGN KEY([tendn])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[bangphieutam] CHECK CONSTRAINT [FK_phieutam_nguoidung]
GO
/****** Object:  ForeignKey [FK_phieuchinh_loaiphieu]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[bangphieuchinh]  WITH CHECK ADD  CONSTRAINT [FK_phieuchinh_nguoidung] FOREIGN KEY([tendn])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[bangphieuchinh] CHECK CONSTRAINT [FK_phieuchinh_nguoidung]
GO
/****** Object:  ForeignKey [FK_nguoidung_quyen_nguoidung]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[nguoidung_quyen]  WITH CHECK ADD  CONSTRAINT [FK_nguoidung_quyen_nguoidung] FOREIGN KEY([tendn])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[nguoidung_quyen] CHECK CONSTRAINT [FK_nguoidung_quyen_nguoidung]
GO

/****** Object:  ForeignKey [FK_thietbi_donvitinh]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[thietbi]  WITH CHECK ADD  CONSTRAINT [FK_thietbi_donvitinh] FOREIGN KEY([madonvitinh])
REFERENCES [dbo].[donvitinh] ([madonvitinh])
GO
ALTER TABLE [dbo].[thietbi] CHECK CONSTRAINT [FK_thietbi_donvitinh]
GO
/****** Object:  ForeignKey [FK_thietbi_kho]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[thietbi]  WITH CHECK ADD  CONSTRAINT [FK_thietbi_kho] FOREIGN KEY([makho])
REFERENCES [dbo].[kho] ([makho])
GO
ALTER TABLE [dbo].[thietbi] CHECK CONSTRAINT [FK_thietbi_kho]
GO
/****** Object:  ForeignKey [FK_thietbi_loaithietbi]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[thietbi]  WITH CHECK ADD  CONSTRAINT [FK_thietbi_loaithietbi] FOREIGN KEY([maloai])
REFERENCES [dbo].[loaithietbi] ([maloaitb])
GO
ALTER TABLE [dbo].[thietbi] CHECK CONSTRAINT [FK_thietbi_loaithietbi]
GO
/****** Object:  ForeignKey [FK_thietbi_nguoidung]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[thietbi]  WITH CHECK ADD  CONSTRAINT [FK_thietbi_nguoidung] FOREIGN KEY([tendn])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[thietbi] CHECK CONSTRAINT [FK_thietbi_nguoidung]
GO
/****** Object:  ForeignKey [FK_thietbi_phonghoc]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[thietbi]  WITH CHECK ADD  CONSTRAINT [FK_thietbi_phonghoc] FOREIGN KEY([phonghoc])
REFERENCES [dbo].[phonghoc] ([maphonghoc])
GO
ALTER TABLE [dbo].[thietbi] CHECK CONSTRAINT [FK_thietbi_phonghoc]
GO
/****** Object:  ForeignKey [FK_lichsudangnhap_tendn]    Script Date: 06/12/2021 20:30:57 ******/
ALTER TABLE [dbo].[lichsudangnhap]  WITH CHECK ADD  CONSTRAINT [FK_lichsudangnhap_nguoidung] FOREIGN KEY([tendn])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[lichsudangnhap] CHECK CONSTRAINT [FK_lichsudangnhap_nguoidung]
GO
/****** Object:  ForeignKey [FK_xetduyet_donvi]    Script Date: 06/09/2021 11:21:57 ******/
ALTER TABLE [dbo].[xetduyet]  WITH CHECK ADD  CONSTRAINT [FK_xetduyet_donvi] FOREIGN KEY([madonvi])
REFERENCES [dbo].[donvi] ([madonvi])
GO
ALTER TABLE [dbo].[xetduyet] CHECK CONSTRAINT [FK_xetduyet_donvi]
GO

/****** Object:  ForeignKey [FK_xetduyet_nguoidung_nguoidexuat]    Script Date: 06/15/2021 10:17:44 ******/
ALTER TABLE [dbo].[xetduyet]  WITH CHECK ADD  CONSTRAINT [FK_xetduyet_nguoidung_nguoidexuat] FOREIGN KEY([nguoidexuat])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[xetduyet] CHECK CONSTRAINT [FK_xetduyet_nguoidung_nguoidexuat]
GO
/****** Object:  ForeignKey [FK_xetduyet_nguoidung_nguoiduyet]    Script Date: 06/15/2021 10:17:44 ******/
ALTER TABLE [dbo].[xetduyet]  WITH CHECK ADD  CONSTRAINT [FK_xetduyet_nguoidung_nguoiduyet] FOREIGN KEY([nguoiduyet])
REFERENCES [dbo].[nguoidung] ([tendn])
GO
ALTER TABLE [dbo].[xetduyet] CHECK CONSTRAINT [FK_xetduyet_nguoidung_nguoiduyet]
GO

/* Thiết bị(madonvi) - donvi(madondi)  */
alter table [dbo].[thietbi] with check add constraint [FK_ThietBi_DonVi] foreign key([madonvi])
references [dbo].[donvi]([madonvi])
go
alter table [dbo].[thietbi] check constraint [FK_ThietBi_DonVi]
go

/* quenmatkhau(tendn) - nguoidung(tendn)  */
alter table [dbo].[quenmatkhau] with check add constraint [FK_quenmatkhau_nguoidung] foreign key([tendn])
references [dbo].[nguoidung]([tendn])
go
alter table [dbo].[quenmatkhau] check constraint [FK_quenmatkhau_nguoidung]
go

-- gọi hàm
-- procduce đăng nhập vào
create proc Proc_DangNhap
(@taiKhoan nchar(10), @matKhau nchar(10))
as
begin
select * from dbo.nguoidung where tendn = @taiKhoan and mkdn = @matKhau
end
go
create proc sophutyeucau
as
begin
declare @thoigian datetime
select @thoigian = ngaygioyeucau from quenmatkhau;
declare @sophutyeucau int;
declare @trangthai nvarchar(15)
select @trangthai = trangthaiyeucau from quenmatkhau;
set @sophutyeucau = DATEDIFF(MINUTE,@thoigian,GETDATE())
if(@sophutyeucau >= 0 and  @sophutyeucau <= 30 and @trangthai =N'Chưa duyệt')
	update quenmatkhau set sophutdayeucau = @sophutyeucau where trangthaiyeucau = N'Chưa duyệt';
if @trangthai = N'Đã duyệt'
	update quenmatkhau set sophutdayeucau = -1 where trangthaiyeucau = N'Đã duyệt';
end
go
 exec sophutyeucau
 go
 create function travesophutcodieukien(@tenDN nchar(10))
 returns int
 as
 begin
 declare @sophut int
 select @sophut = sophutdayeucau from quenmatkhau where tendn = @tenDN
 return @sophut
 end
 go
 
 insert into donvi(madonvi,tendonvi,dienthoai,diadiem) values('QL',N'Quản Lý','01215454','NT')
 go
 insert into nguoidung(tendn,mkdn,hoten,donvi,ngaytao,gmail,trangthaihoatdong) values('admin','admin','Minh Quang','QL','2021-06-25','hmquang230200@gmail.com','Không hoạt động')
 go

 --use QTTB
 --go


