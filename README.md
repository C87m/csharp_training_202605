# csharp_training_202605
Web開発演習

## 進捗
### 5/20
- スケジュール立案
- 要件定義
- 設計  
  - ER図：完成
  - クラス図：途中まで

### 5/21
- 設計
  - クラス図：完成
  - シーケンス図：完成
  - テストケース：要修正
  - テーブル定義書：完成  

☆ログイン機能は後日加筆予定

### 5/22
- 設計
  - テストケース：完成
- 実装
  - 環境構築
  - データベース 
  - 社員一覧表示機能：未完成
  - 部署一覧表示機能：未完成

### 5/25
- 部署登録機能：テストまで完了
- 部署一覧機能：テストまで完了
- 社員登録機能：フィールド追加途中(ほぼ完成)

### 5/26
- 社員登録機能：実装完了
- 部署更新機能：実装完了
- 部署削除機能：実装完了
- 社員削除機能：途中

### 5/27
- 実装
  - 社員削除機能：実装完了
  - 社員更新機能：実装完了
  - ログイン機能：途中
- テスト
  - 社員登録機能
  - 社員一覧機能
  - DepartmentRepository

## 社員情報管理システム
社員情報と部門情報の管理を行う。
人事部など管理者用のシステム。
### 機能
- ログイン/ログアウト：ユーザ認証を行う
  - ユーザ認証できた人のみ他の機能を利用できる
- 社員一覧：登録されている社員全員を一覧形式で表示する
- 社員登録：社員を新規登録する
  - 新入社員などの登録をする
- 社員更新：登録されている社員の情報を変更する
  - 部署異動、名字の変更などに対応する
- 社員削除：登録されている社員の情報を削除する
  - 社員の退社に対応
  - 削除フラグで管理しデータベースからは完全に削除しない
- 部門一覧：登録されている部門を一覧形式で表示する
- 部門登録：部門を新規登録する
- 部門更新：登録されている部門の情報を変更する
  - 部署名変更など
- 部門削除：登録されている部門を削除する
  - その部門にいた社員は部門未登録となる
  - 削除フラグは使用せずデータベースから完全に削除する

## スケジュール
### 5/20
- スケジュール立案
- 要件定義
- 設計  
  - テーブル定義書
  - クラス図

### 5/21
- 設計
  - クラス図(2h)
  - シーケンス図(1.5h)
  - テストケース(1.5h)
- ~~実装(1h)~~
  - ~~環境構築~~
  - ~~データベース~~
  - ~~アプリケーション層~~
    - ~~ドメインオブジェクト~~
    - ~~Adapter~~
    - ~~Repository~~

### 5/22(5/25更新)

- **実装(5/21未完了分)**
  - **環境構築**
  - **データベース**
  - **アプリケーション層**
    - **ドメインオブジェクト**
    - **Adapter**
    - **Repository**
- 実装
  - インフラストラクチャ層(1h)
    - Entity
    - DbContext
    - Repository
    - Adapter
  - プレゼンテーション層(1h)
    - ViewModel
    - DIコンテナ
    - MiddleWare
  - 社員一覧表示機能(2h)
  - ~~部門一覧表示機能(2h)~~
  
☆機能を実装したら都度テストを行う

### 5/25
- 実装
  - ~~社員一覧機能(5/22未完了分)~~
  - 部署一覧機能(5/22未完了分)
  - 社員登録機能
  - 部門登録機能
  - ~~社員更新機能~~(5/25修正)
  - ~~部門更新機能~~(5/25修正)
  
### 5/26
- 実装
  - 社員登録機能(5/25未完了分)
  - 社員一覧機能(5/25未完了分)
  - ~~社員更新機能~~(5/26)修正
  - 部門更新機能
  - 社員削除機能
  - 部門削除機能

### 5/27
- 実装
　- 社員削除機能
  - 社員更新機能  
  - ログイン機能
- テスト

### 5/28
- 実装
  - 微修正
  - UIの調整
- プレゼンテーション資料作成

### 5/29
- 成果発表
  
## DB
![テーブル](docs/01_db/DBimage.png)
![ER図](docs/01_db/ERimage.png)
![テーブル定義書](docs/01_db/table.png)

### クエリ
```sql
drop table employee;
drop table department;
drop table login;

-- ============================
-- 部門 department
-- ============================
CREATE TABLE department
(
  dept_id   integer GENERATED ALWAYS AS IDENTITY,
  dept_name varchar(10) NOT NULL,
  CONSTRAINT pk_dept_id PRIMARY KEY (dept_id),
  CONSTRAINT nn_dept_name CHECK (dept_name <> '')
);

INSERT INTO department (dept_name) VALUES ('無所属');
INSERT INTO department (dept_name) VALUES ('総務部');
INSERT INTO department (dept_name) VALUES ('経理部');
INSERT INTO department (dept_name) VALUES ('人事部');
INSERT INTO department (dept_name) VALUES ('開発部');
INSERT INTO department (dept_name) VALUES ('営業部');

-- ============================
-- 社員 employee
-- ============================
CREATE TABLE employee
(
  emp_id      integer GENERATED ALWAYS AS IDENTITY,
  emp_name    varchar(10) NOT NULL,
  dept_id integer NOT NULL,
  birthday date NOT NULL,
  gender integer NOT NULL,
  phone_number varchar(20) NOT NULL,
  email varchar(100) NOT NULL,
  address varchar(100) NOT NULL,
  delete_flag boolean NOT NULL DEFAULT FALSE,
  CONSTRAINT pk_emp_no PRIMARY KEY (emp_id),
  CONSTRAINT fk_dept_no FOREIGN KEY (dept_id)
      REFERENCES department(dept_id)
);

INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('田中太郎',2,'2001-08-08',0,'11122223333','taro@sample.com','東京都');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('鈴木三郎',6,'1984-08-08',0,'11122323333','sab@sample.com','神奈川県');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('佐藤花子',4,'2012-08-08',1,'11124223333','hana5@sample.com','北海道');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('中田彩子',5,'2011-08-08',1,'11125223333','aya@sample.com','アメリカ合衆国');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('加藤圭太',3,'2004-08-08',0,'11126223333','keita@sample.com','中華人民共和国');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('松本良太',4,'2003-08-08',0,'11127223333','ryota@sample.com','大韓民国');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('山下孝輔',5,'1991-08-08',2,'11127223333','kosuke@sample.com','沖縄県');
INSERT INTO employee (emp_name, dept_id, birthday, gender, phone_number, email, address) VALUES ('渡辺大輔',4,'2000-08-08',2,'11128223333','daisuke@sample.com','東京都');

-- ============================
-- ユーザー login
-- ============================
CREATE TABLE login
(
  login_id   varchar(10) NOT NULL,
  login_password varchar(10) NOT NULL,
  CONSTRAINT pk_login_id PRIMARY KEY (login_id)
);
INSERT INTO login (login_id, login_password) VALUES ('user','password');
INSERT INTO login (login_id, login_password) VALUES ('fullness','fullness');
```
