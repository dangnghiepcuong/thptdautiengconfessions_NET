create database thptdtcfs_voucher_event;

use thptdtcfs_voucher_event;

create table ACCOUNT(Id smallint IDENTITY(1,1), Username nvarchar(100), Password nvarchar(128), Level tinyint);
alter table ACCOUNT add constraint PK_ACCOUNT primary key (Id);

create table ACCOUNT_INFO(AccountId smallint not null, Tel nvarchar(10), Email nvarchar(100));
alter table ACCOUNT_INFO add constraint PK_ACCOUNT_INFO primary key (AccountId);
--alter table ACCOUNT_INFO add constraint FK_ACCOUNT_INFO_ACCOUNT foreign key (AccountId) references ACCOUNT(AccountId);

create table USER_PROFILE(AccountId smallint, FirstName nvarchar(20), LastName nvarchar(60), Birthday date, Gender nvarchar(30));
create table USER_DETAIL(FieldId tinyint IDENTITY(1,1), AccountId smallint, FieldName nvarchar(200), FieldDetail nvarchar(2000));
--create table SCHOOL_PROFILE(AccountId smallint, );

create table Voucher_REDEMPTION(AccountId smallint, VoucherCode nvarchar(15), Redemption smallint);

create table SHOP(Id tinyint IDENTITY(1,1), Name nvarchar(100), Address nvarchar(500), Image image, AccountId smallint);
alter table SHOP add constraint PK_SHOP primary key (Id);

create table VOUCHER(Code nvarchar(15) not null, QRCode Image, ShopId tinyint, ShopName nvarchar(100), Description nvarchar(1000), StartDate date, EndDate date, Redemption smallint, Limitation smallint, Activation bit);
alter table VOUCHER add constraint PK_VOUCHER primary key (Code);
--alter table VOUCHER add constraint FK_VOUCHER_SHOP foreign key (ShopId) references SHOP(ShopId);
SET DATEFORMAT dmy;
INSERT INTO ACCOUNT(Username, Password, Level) VALUES('thptdtcfs', 'Cuong61H114670', 1);
INSERT INTO ACCOUNT_INFO(AccountId, Tel, Email) VALUES(1, '0339770526', 'thptdautiengconfessions@gmail.com');


INSERT INTO SHOP(Name,Address,Image,AccountId) VALUES(N'Laxy Coffee & Tea', N'123456abc ', NULL, NULL  );
INSERT INTO SHOP(Name,Address,Image,AccountId) VALUES(N'My House Coffee & Tea', N'@147869iii ', NULL, NULL  );
INSERT INTO SHOP(Name,Address,Image,AccountId) VALUES(N'TocoToco', 'qwertyuiop ', NULL, NULL  );


INSERT INTO VOUCHER(Code,ShopId,Description,StartDate,EndDate,Redemption,Limitation,Activation) VALUES('ASD1234', 1, N'Giảm 50% cho thức uống trà sữa (tối đa 15k)', '22-02-2023', '22-12-2023', 0, 1, 1  );
INSERT INTO VOUCHER(Code,ShopId,Description,StartDate,EndDate,Redemption,Limitation,Activation) VALUES('AWS1234', 1, N'Giảm 30k cho hóa đơn từ 80k', '22-02-2023', '22-12-2023', 0, 1, 1  );

