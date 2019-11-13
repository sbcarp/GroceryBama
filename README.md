# Requirements
- Visual Studio 2019 [[Download](https://visualstudio.microsoft.com/vs/ "Download")]
	- With ASP.NET and web development Workload installed
	- GitHub extension for Visual Studio (Optional)
- MySQL 8.0 [[Download](https://dev.mysql.com/downloads/windows/installer/8.0.html "Download")]
	- MySQL 8.0 Workbench CE (Optional)

# Configurations
### MySQL Configuration
It is recommended to use the following settings during installation
`High Availability` Standalone MySQL Server / Classic MySQL Replication

`Authentication Method`  Use Legacy Authentication Method

Then use the script to create a new user and database, if you want to use names or credentials other than default, remember to change the file `AppInformation.cs` under project root folder and exclude it in `.gitignore`.
```sql
DROP DATABASE  IF EXISTS grocerybama;
CREATE DATABASE grocerybama;
USE grocerybama;

DROP USER IF EXISTS 'grocerybama.admin'@'localhost';
CREATE USER  'grocerybama.admin'@'localhost' IDENTIFIED WITH mysql_native_password BY 'password';
GRANT ALL PRIVILEGES ON grocerybama . * TO 'grocerybama.admin'@'localhost';
FLUSH PRIVILEGES;


CREATE TABLE userexample
(
    fname VARCHAR(20) NOT NULL,
    lname VARCHAR(20) NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    role VARCHAR(15) NOT NULL,
    CONSTRAINT uepk PRIMARY KEY(username)
) Engine=InnoDB;

INSERT INTO userexample VALUES('John', 'Smith', 'jSmith', '123456', 'user');
INSERT INTO userexample VALUES('Franklin', 'Wong', 'fWong', 'qwerty', 'user');
INSERT INTO userexample VALUES('Alicia', 'Zelaya', 'aZelaya', '000000', 'user');
INSERT INTO userexample VALUES('Jennifer', 'Wallace', 'jWallace', 'eyr#{2 %DDk--=?=23fere', 'admin');
```
