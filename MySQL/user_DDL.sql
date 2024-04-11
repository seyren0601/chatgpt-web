-- Tạo database gpt_user và table user_info
DROP DATABASE IF EXISTS gpt_user;

CREATE DATABASE gpt_user;
USE gpt_user;

CREATE TABLE user_info(
	id int auto_increment,
    full_name nvarchar(50),
    mssv nvarchar(50),
    gender nvarchar(50),
    birthday date,
    faculty nvarchar(100),
    major nvarchar(50),
    nationality nvarchar(50),
    religion nvarchar(50),
    idcard nvarchar(50),
    dateofissue date,
    placeofissue nvarchar(50),
    myphone nvarchar(50),
    parentphone nvarchar(50),
	email nvarchar(50) unique,
    address nvarchar(50),
    aboutstudent text,
    hashed_pw nvarchar(50),
    salt nvarchar(50),
    isdeleted bool default false,
    role nvarchar(10) default "user",
    PRIMARY KEY (id)
);
CREATE TABLE MONHOC(
	IdMonhoc nvarchar(50),
    TitleMonhoc text,
    ContentMonhoc longtext,
    CONSTRAINT PK_EID PRIMARY KEY  (IdMonhoc ASC)
);

CREATE TABLE CHUONG(
	Id nvarchar(50),
    Title text,
    IdMonhoc nvarchar(50),
    ParentId nvarchar(50),
    CONSTRAINT PK_EID PRIMARY KEY  (Id ASC),
	CONSTRAINT FK1 FOREIGN KEY (ParentId) REFERENCES CHUONG(Id),
    CONSTRAINT FK2 FOREIGN KEY (IdMonhoc) REFERENCES MONHOC(IdMonhoc)
    
);
insert into MONHOC values ('CTGT01','CẤU TRÚC DỮ LIỆU VÀ GIẢI THUẬT',null);
insert into CHUONG values('CHƯƠNG 1','TỔNG QUAN','CTGT01',null),
						('1.1','Vai trò của thuật toán và cấu trúc dữ liệu','CTGT01','CHƯƠNG 1'),
                        ('1.2','Thuật toán','CTGT01','CHƯƠNG 1'),
                        ('1.3','Kiểu dữ liệu, Cấu trúc dữ liệu','CTGT01','CHƯƠNG 1'),
                        ('CHƯƠNG 2','SẮP XẾP VÀ TÌM KIẾM','CTGT01',null),
                        ('2.1','Mối quan hệ giữa nhu cầu sắp xếp và tìm kiếm dữ liệu','CTGT01','CHƯƠNG 2'),
                        ('2.2','Định nghĩa bài toán sắp xếp','CTGT01','CHƯƠNG 2'),
                        ('2.3','Các giải thuật sắp xếp nội','CTGT01','CHƯƠNG 2'),
                        ('2.3.1','Các giải thuật sắp xếp cơ bản','CTGT01','2.3'),
                        ('2.3.2','Giải thuật sắp xếp cây – Heap sort','CTGT01','2.3'),
                        ('2.3.3',' Giải thuật sắp xếp độ phức tạp giảm dần – Shell sort','CTGT01','2.3'),
                        ('2.3.4','Giải thuật sắp xếp dựa trên phân hoạch – Quick sort','CTGT01','2.3'),
                        ('2.3.5','Giải thuật sắp xếp trộn trực tiếp – Merge sort','CTGT01','2.3'),
                        ('2.4','Định nghĩa bài toán tìm kiếm','CTGT01','CHƯƠNG 2'),
						('2.5','Các giải thuật tìm kiếm nội','CTGT01','CHƯƠNG 2'),
                        ('2.5.1','Giải thuật tìm kiếm tuyến tính','CTGT01','2.5'),
						('2.5.2','Giải thuật tìm kiếm nhị phân','CTGT01','2.5');


-- Query kiểm tra
SELECT * FROM user_info;
select * from MONHOC;
WITH RECURSIVE BookHierarchy AS (
  SELECT Id, Title, IdMonhoc, ParentId, 1 AS Level
  FROM CHUONG
  WHERE ParentId IS NULL
  UNION ALL
  SELECT b.Id, b.Title, b.IdMonhoc, b.ParentId, bh.Level + 1
  FROM CHUONG b
  INNER JOIN BookHierarchy bh ON b.ParentId = bh.Id
)
SELECT Id, Title, IdMonhoc, ParentId, Level
FROM BookHierarchy
ORDER BY Id;
