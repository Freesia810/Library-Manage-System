CREATE database library;

CREATE table account(
    username VARCHAR(30) NOT NULL,
    pwMD5 CHAR(32) NOT NULL,
    userType VARCHAR(6) NOT NULL,
    PRIMARY KEY(username),
    CONSTRAINT user_chk CHECK(userType='admin' OR userType='reader')
);


CREATE TABLE books(
   book_ID VARCHAR(60) NOT NULL,
   category VARCHAR(60),
   book_name VARCHAR(60),
   press VARCHAR(60),
   year int,
   author VARCHAR(60),
   ISBN CHAR(13),
   price DECIMAL(7,2),
   totalNum INT,
   curNum INT,
   PRIMARY KEY(book_ID),
   CONSTRAINT Num_chk CHECK(totalNum > 0 AND curNum >= 0 AND year > 0 AND totalNum >= curNum)
);




CREATE TABLE readers(
    reader_ID VARCHAR(60) NOT NULL,
    reader_name VARCHAR(60),
    department varchar(60),
    reader_Type char(1),
    cur_num INT,
    credit INT,
    PRIMARY KEY(reader_ID),
    CONSTRAINT curnum_chk CHECK(cur_num <= 10 AND cur_num >= 0),
    CONSTRAINT credit_chk CHECK(credit <= 5 AND credit >= 0),
    CONSTRAINT type_chk CHECK(reader_Type = 'T' OR reader_Type = 'S')
);




CREATE table borrow(
    borrow_ID INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    reader_ID VARCHAR(60),
    book_ID VARCHAR(60),
    borrow_date DATE,
    planned_date DATE,
    acutal_date DATE,
    FOREIGN KEY (reader_ID) REFERENCES readers(reader_ID) ON UPDATE CASCADE ON DELETE SET NULL, 
    FOREIGN KEY (book_ID) REFERENCES books(book_ID) ON UPDATE CASCADE ON DELETE SET NULL
);

Insert INTO account values
('admin','E10ADC3949BA59ABBE56E057F20F883E','admin');



create user 'library'@'%' identified by '123456'; 
grant select on library.* to 'library'@'%';
grant update on library.* to 'library'@'%';
grant insert on library.* to 'library'@'%';
grant delete on library.books to 'library'@'%';
grant delete on library.readers to 'library'@'%';
grant file on *.* to 'library'@'%';
flush privileges;