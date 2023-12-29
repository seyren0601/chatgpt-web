-- Tạo database gpt_user và table user_info
DROP DATABASE IF EXISTS gpt_user;

CREATE DATABASE gpt_user;
USE gpt_user;

CREATE TABLE user_info(
	id int auto_increment,
	email char(50),
    hashed_pw char(50),
    salt char(50),
    PRIMARY KEY (id)
);

-- Query kiểm tra
SELECT * FROM user_info