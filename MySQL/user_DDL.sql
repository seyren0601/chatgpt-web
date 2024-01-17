-- Tạo database gpt_user và table user_info
DROP DATABASE IF EXISTS gpt_user;

CREATE DATABASE gpt_user;
USE gpt_user;

CREATE TABLE user_info(
	id int auto_increment,
    mssv int,
    full_name char(50),
    gender char(50),
    birthday date,
	email char(50) unique,
    address char(50),
    nationality char(50),
    religion char(50),
    entry_date date,
    faculty char(50),
    major char(50),
    class_sv char(50),
    hashed_pw char(50),
    salt char(50),
    PRIMARY KEY (id)
);

-- Query kiểm tra
SELECT * FROM user_info