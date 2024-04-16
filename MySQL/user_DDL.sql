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
    picture text,
    PRIMARY KEY (id)
);
CREATE TABLE CONVERSATION(
	idConversation int auto_increment,
	id int,
    Conversation text,
    CONSTRAINT PK_CID PRIMARY KEY  (IdConversation ASC),
    CONSTRAINT FK_CID FOREIGN KEY (id) REFERENCES user_info(id)
);

CREATE TABLE MONHOC(
	IdMonhoc nvarchar(50),
    TitleMonhoc text,
    ContentMonhoc longtext,
    CONSTRAINT PK_MID PRIMARY KEY  (IdMonhoc ASC)
);

CREATE TABLE CHUONG(
	Id nvarchar(50),
    Title text,
    IdMonhoc nvarchar(50),
    ParentId nvarchar(50),
    CONSTRAINT PK_CHID PRIMARY KEY  (Id ASC),
	CONSTRAINT FK1 FOREIGN KEY (ParentId) REFERENCES CHUONG(Id),
    CONSTRAINT FK2 FOREIGN KEY (IdMonhoc) REFERENCES MONHOC(IdMonhoc)
    
);
insert into MONHOC values ('CTGT01','CẤU TRÚC DỮ LIỆU VÀ GIẢI THUẬT','');

INSERT INTO CHUONG VALUES ('CHƯƠNG_1', 'TỔNG QUAN', 'CTGT01', NULL);
INSERT INTO CHUONG VALUES ('1.1', 'Vai trò của thuật toán và cấu trúc dữ liệu', 'CTGT01', 'CHƯƠNG_1');
INSERT INTO CHUONG VALUES ('1.2', 'Thuật toán', 'CTGT01', 'CHƯƠNG_1');
INSERT INTO CHUONG VALUES ('1.2.1', 'Định nghĩa thuật toán', 'CTGT01', '1.2');
INSERT INTO CHUONG VALUES ('1.2.2', 'Đặc trưng của thuật toán', 'CTGT01', '1.2');
INSERT INTO CHUONG VALUES ('1.2.3', 'Biểu diễn thuật toán', 'CTGT01', '1.2');
INSERT INTO CHUONG VALUES ('1.2.4', 'Độ phức tạp thuật toán', 'CTGT01', '1.2');
INSERT INTO CHUONG VALUES ('1.2.5', 'Các chiến lược thiết kế thuật toán', 'CTGT01', '1.2');
INSERT INTO CHUONG VALUES ('1.3', 'Kiểu dữ liệu, Cấu trúc dữ liệu', 'CTGT01', 'CHƯƠNG_1');
INSERT INTO CHUONG VALUES ('1.3.1', 'Kiểu dữ liệu', 'CTGT01', '1.3');
INSERT INTO CHUONG VALUES ('1.3.2', 'Cấu trúc dữ liệu', 'CTGT01', '1.3');
INSERT INTO CHUONG VALUES ('Bài tập chương','', 'CTGT01', 'CHƯƠNG_1');

INSERT INTO CHUONG VALUES ('CHƯƠNG_2','SẮP XẾP VÀ TÌM KIẾM','CTGT01', NULL);
INSERT INTO CHUONG VALUES ('2.1', 'Mối quan hệ giữa nhu cầu sắp xếp và tìm kiếm dữ liệu', 'CTGT01', 'CHƯƠNG_2');
INSERT INTO CHUONG VALUES ('2.2', 'Định nghĩa bài toán sắp xếp', 'CTGT01', 'CHƯƠNG_2');
INSERT INTO CHUONG VALUES ('2.3', 'Các giải thuật sắp xếp nội', 'CTGT01', 'CHƯƠNG_2');
INSERT INTO CHUONG VALUES ('2.3.1', 'Các giải thuật sắp xếp cơ bản', 'CTGT01', '2.3');
INSERT INTO CHUONG VALUES ('2.3.2', 'Giải thuật sắp xếp cây – Heap sort', 'CTGT01', '2.3');
INSERT INTO CHUONG VALUES ('2.3.3', 'Giải thuật sắp xếp độ phức tạp giảm dần – Shell sort', 'CTGT01', '2.3');
INSERT INTO CHUONG VALUES ('2.3.4', 'Giải thuật sắp xếp dựa trên phân hoạch – Quick sort', 'CTGT01', '2.3');
INSERT INTO CHUONG VALUES ('2.3.5', 'Giải thuật sắp xếp trộn trực tiếp – Merge sort', 'CTGT01', '2.3');
INSERT INTO CHUONG VALUES ('2.4', 'Định nghĩa bài toán tìm kiếm', 'CTGT01', 'CHƯƠNG_2');
INSERT INTO CHUONG VALUES ('2.5', 'Các giải thuật tìm kiếm nội', 'CTGT01', 'CHƯƠNG_2');
INSERT INTO CHUONG VALUES ('2.5.1', 'Giải thuật tìm kiếm tuyến tính', 'CTGT01', '2.5');
INSERT INTO CHUONG VALUES ('2.5.2', 'Giải thuật tìm kiếm nhị phân', 'CTGT01', '2.5');

-- Insert Chapter 3
INSERT INTO CHUONG VALUES ('CHƯƠNG_3', 'DANH SÁCH LIÊN KẾT', 'CTGT01', NULL);
INSERT INTO CHUONG VALUES ('3.1', 'Danh sách tuyến tính', 'CTGT01', 'CHƯƠNG_3');
INSERT INTO CHUONG VALUES ('3.1.1', 'Định nghĩa', 'CTGT01', '3.1');
INSERT INTO CHUONG VALUES ('3.1.2', 'Các thao tác trên danh sách', 'CTGT01', '3.1');
INSERT INTO CHUONG VALUES ('3.1.3', 'Biểu diễn danh sách dưới dạng mảng', 'CTGT01', '3.1');
INSERT INTO CHUONG VALUES ('3.2', 'Danh sách liên kết', 'CTGT01', 'CHƯƠNG_3');
INSERT INTO CHUONG VALUES ('3.2.1', 'Định nghĩa', 'CTGT01', '3.2');
INSERT INTO CHUONG VALUES ('3.2.2', 'Danh sách liên kết đơn', 'CTGT01', '3.2');
INSERT INTO CHUONG VALUES ('3.2.3', 'Danh sách liên kết kép', 'CTGT01', '3.2');
INSERT INTO CHUONG VALUES ('Bài tập chương 3', '', 'CTGT01', 'CHƯƠNG_3');

-- Insert Chapter 4
INSERT INTO CHUONG VALUES ('CHƯƠNG_4', 'NGĂN XẾP, HÀNG ĐỢI', 'CTGT01', NULL);
INSERT INTO CHUONG VALUES ('4.1', 'Ngăn xếp', 'CTGT01', 'CHƯƠNG_4');
INSERT INTO CHUONG VALUES ('4.1.1', 'Định nghĩa', 'CTGT01', '4.1');
INSERT INTO CHUONG VALUES ('4.1.2', 'Các thao tác trên ngăn xếp', 'CTGT01', '4.1');
INSERT INTO CHUONG VALUES ('4.1.3', 'Ứng dụng với ngăn xếp', 'CTGT01', '4.1');
INSERT INTO CHUONG VALUES ('4.1.4', 'Cài đặt ngăn xếp', 'CTGT01', '4.1');
INSERT INTO CHUONG VALUES ('4.2', 'Hàng đợi', 'CTGT01', 'CHƯƠNG_4');
INSERT INTO CHUONG VALUES ('4.2.1', 'Định nghĩa', 'CTGT01', '4.2');
INSERT INTO CHUONG VALUES ('4.2.2', 'Các thao tác trên hàng đợi', 'CTGT01', '4.2');
INSERT INTO CHUONG VALUES ('4.2.3', 'Cài đặt hàng đợi', 'CTGT01', '4.2');
INSERT INTO CHUONG VALUES ('Bài tập chương 4', '', 'CTGT01', 'CHƯƠNG_4');

-- Insert Chapter 5
INSERT INTO CHUONG VALUES ('CHƯƠNG_5', 'CÂY', 'CTGT01', NULL);
INSERT INTO CHUONG VALUES ('5.1', 'Định nghĩa và các khái niệm', 'CTGT01', 'CHƯƠNG_5');
INSERT INTO CHUONG VALUES ('5.1.1', 'Định nghĩa cây', 'CTGT01', '5.1');
INSERT INTO CHUONG VALUES ('5.1.2', 'Các khái niệm', 'CTGT01', '5.1');
INSERT INTO CHUONG VALUES ('5.1.3', 'Cây có thứ tự', 'CTGT01', '5.1');
INSERT INTO CHUONG VALUES ('5.1.4', 'Duyệt cây theo thứ tự', 'CTGT01', '5.1');
INSERT INTO CHUONG VALUES ('5.2', 'Biểu diễn cây trong máy tính', 'CTGT01', 'CHƯƠNG_5');
INSERT INTO CHUONG VALUES ('5.2.1', 'Biểu diễn cây sử dụng mảng', 'CTGT01', '5.2');
INSERT INTO CHUONG VALUES ('5.2.2', 'Biểu diễn cây sử dụng danh sách liên kết', 'CTGT01', '5.2');
INSERT INTO CHUONG VALUES ('5.3', 'Cây nhị phân', 'CTGT01', 'CHƯƠNG_5');
INSERT INTO CHUONG VALUES ('5.3.1', 'Định nghĩa và tính chất cây nhị phân', 'CTGT01', '5.3');
INSERT INTO CHUONG VALUES ('5.3.2', 'Tính chất của cây nhị phân', 'CTGT01', '5.3');
INSERT INTO CHUONG VALUES ('5.3.3', 'Biểu diễn cây nhị phân', 'CTGT01', '5.3');
INSERT INTO CHUONG VALUES ('5.3.4', 'Duyệt cây nhị phân', 'CTGT01', '5.3');
INSERT INTO CHUONG VALUES ('5.3.5', 'Một số ứng dụng cây nhị phân trong thực tế', 'CTGT01', '5.3');
INSERT INTO CHUONG VALUES ('5.4', 'Cây nhị phân tìm kiếm', 'CTGT01', 'CHƯƠNG_5');
INSERT INTO CHUONG VALUES ('5.4.1', 'Định nghĩa', 'CTGT01', '5.4');
INSERT INTO CHUONG VALUES ('5.4.2', 'Các thao tác trên cây nhị phân tìm kiếm', 'CTGT01', '5.4');
INSERT INTO CHUONG VALUES ('5.5', 'Cây cân bằng', 'CTGT01', 'CHƯƠNG_5');
INSERT INTO CHUONG VALUES ('5.5.1', 'Định nghĩa', 'CTGT01', '5.5');
INSERT INTO CHUONG VALUES ('5.5.2', 'Các trường hợp mất cân bằng', 'CTGT01', '5.5');
INSERT INTO CHUONG VALUES ('5.5.3', 'Thêm một phần tử trên cây', 'CTGT01', '5.5');
INSERT INTO CHUONG VALUES ('5.5.4', 'Hủy một phần tử trên cây', 'CTGT01', '5.5');
INSERT INTO CHUONG VALUES ('Bài tập chương 5', '', 'CTGT01', 'CHƯƠNG_5');

-- Insert Chapter 6
INSERT INTO CHUONG VALUES ('CHƯƠNG_6', 'BẢNG BĂM', 'CTGT01', NULL);
INSERT INTO CHUONG VALUES ('6.1', 'Bảng băm', 'CTGT01', 'CHƯƠNG_6');
INSERT INTO CHUONG VALUES ('6.1.1', 'Định nghĩa', 'CTGT01', '6.1');
INSERT INTO CHUONG VALUES ('6.1.2', 'Quy trình thực hiện lưu trữ trong bảng băm', 'CTGT01', '6.1');
INSERT INTO CHUONG VALUES ('6.1.3', 'Các thao tác trên bảng băm', 'CTGT01', '6.1');
INSERT INTO CHUONG VALUES ('6.2', 'Hàm băm', 'CTGT01', 'CHƯƠNG_6');
INSERT INTO CHUONG VALUES ('6.3', 'Các phương pháp giải quyết đụng độ', 'CTGT01', 'CHƯƠNG_6');
INSERT INTO CHUONG VALUES ('6.3.1', 'Phương pháp nối kết', 'CTGT01', '6.3');
INSERT INTO CHUONG VALUES ('6.3.2', 'Phương pháp địa chỉ mở', 'CTGT01', '6.3');
INSERT INTO CHUONG VALUES ('6.3.3', 'Phương pháp nhân', 'CTGT01', '6.3');
INSERT INTO CHUONG VALUES ('6.4', 'Hạn chế của bảng băm', 'CTGT01', 'CHƯƠNG_6');
INSERT INTO CHUONG VALUES ('Bài tập chương 6', '', 'CTGT01', 'CHƯƠNG_6');










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
