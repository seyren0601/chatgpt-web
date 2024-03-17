-- Tạo database gpt_user và table user_info
DROP DATABASE IF EXISTS gpt_user;

CREATE DATABASE gpt_user;
USE gpt_user;

CREATE TABLE user_info(
	id int auto_increment,
    full_name char(50),
    mssv int,
    gender char(50),
    birthday date,
    faculty char(50),
    major char(50),
    nationality char(50),
    religion char(50),
    idcard int,
    dateofissue date,
    placeofissue char(50),
    myphone int,
    parentphone int,
	email char(50) unique,
    address char(50),
    aboutstudent char(255),
    hashed_pw char(50),
    salt char(50),
    PRIMARY KEY (id)
);

-- Query kiểm tra
SELECT * FROM user_info