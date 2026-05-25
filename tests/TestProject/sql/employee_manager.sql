TRUNCATE TABLE
    employee,
    department
RESTART IDENTITY CASCADE;

INSERT INTO department (dept_name) VALUES ('総務部');
INSERT INTO department (dept_name) VALUES ('経理部');
INSERT INTO department (dept_name) VALUES ('人事部');


INSERT INTO employee (emp_name, dept_id) VALUES ('田中太郎',2);
INSERT INTO employee (emp_name, dept_id) VALUES ('鈴木三郎',1);
INSERT INTO employee (emp_name, dept_id) VALUES ('加藤圭太',3);