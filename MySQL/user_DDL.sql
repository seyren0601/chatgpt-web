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
CREATE TABLE BOOK(
	IdBook nvarchar(50),
    TitleBook text,
    ContentBook longtext,
    CONSTRAINT PK_EID PRIMARY KEY  (IdBook ASC)
);

CREATE TABLE CHUONG(
	Id nvarchar(50),
    Title text,
    IdBook nvarchar(50),
    ParentId nvarchar(50),
    CONSTRAINT PK_EID PRIMARY KEY  (Id ASC),
	CONSTRAINT FK1 FOREIGN KEY (ParentId) REFERENCES CHUONG(Id),
    CONSTRAINT FK2 FOREIGN KEY (IdBook) REFERENCES BOOK(IdBook)
    
);
insert into BOOK values ('CTGT01','CẤU TRÚC DỮ LIỆU VÀ GIẢI THUẬT',null);
insert into CHUONG values('CHUONG1','TỔNG QUAN','CTGT01',null),
						('CHUONG1.1','Vai trò của thuật toán và cấu trúc dữ liệu','CTGT01','CHUONG1'),
                        ('CHUONG1.2','Thuật toán','CTGT01','CHUONG1'),
                        ('CHUONG1.3','Kiểu dữ liệu, Cấu trúc dữ liệu','CTGT01','CHUONG1'),
                        ('CHUONG2','SẮP XẾP VÀ TÌM KIẾM','CTGT01',null),
                        ('CHUONG2.1','Mối quan hệ giữa nhu cầu sắp xếp và tìm kiếm dữ liệu','CTGT01','CHUONG2'),
                        ('CHUONG2.2','Định nghĩa bài toán sắp xếp','CTGT01','CHUONG2'),
                        ('CHUONG2.3','Các giải thuật sắp xếp nội','CTGT01','CHUONG2'),
                        ('CHUONG2.3.1','Các giải thuật sắp xếp cơ bản','CTGT01','CHUONG2.3'),
                        ('CHUONG2.3.2','Giải thuật sắp xếp cây – Heap sort','CTGT01','CHUONG2.3'),
                        ('CHUONG2.3.3',' Giải thuật sắp xếp độ phức tạp giảm dần – Shell sort','CTGT01','CHUONG2.3'),
                        ('CHUONG2.3.4','Giải thuật sắp xếp dựa trên phân hoạch – Quick sort','CTGT01','CHUONG2.3'),
                        ('CHUONG2.3.5','Giải thuật sắp xếp trộn trực tiếp – Merge sort','CTGT01','CHUONG2.3'),
                        ('CHUONG2.4','Định nghĩa bài toán tìm kiếm','CTGT01','CHUONG2'),
						('CHUONG2.5','Các giải thuật tìm kiếm nội','CTGT01','CHUONG2'),
                        ('CHUONG2.5.1','Giải thuật tìm kiếm tuyến tính','CTGT01','CHUONG2.5'),
						('CHUONG2.5.2','Giải thuật tìm kiếm nhị phân','CTGT01','CHUONG2.5');


-- Query kiểm tra
SELECT * FROM user_info;
select * from BOOK;
WITH RECURSIVE BookHierarchy AS (
  SELECT Id, Title, IdBook, ParentId, 1 AS Level
  FROM CHUONG
  WHERE ParentId IS NULL
  UNION ALL
  SELECT b.Id, b.Title, b.IdBook, b.ParentId, bh.Level + 1
  FROM CHUONG b
  INNER JOIN BookHierarchy bh ON b.ParentId = bh.Id
)
SELECT Id, Title, IdBook, ParentId, Level
FROM BookHierarchy
ORDER BY Id;
