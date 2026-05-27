TRUNCATE TABLE
    employee,
    department
RESTART IDENTITY CASCADE;

INSERT INTO department (dept_name) VALUES ('無所属');
INSERT INTO department (dept_name) VALUES ('総務部');
INSERT INTO department (dept_name) VALUES ('経理部');


INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('田中太郎',2,'2001-08-08',0,'11122223333','taro@sample.com','東京都');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('鈴木三郎',1,'1984-08-08',0,'11122323333','sab@sample.com','神奈川県');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('佐藤花子',2,'2012-08-08',1,'11124223333','hana5@sample.com','北海道');
