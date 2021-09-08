CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS groups(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Group Name',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  img varchar(255) COMMENT 'Group Primary Image Url',
  description VARCHAR(255) COMMENT 'Group Description',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE groupMembers(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  groupId int NOT NULL comment 'Group Id',
  accountId VARCHAR(255) NOT NULL comment 'account id',
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (groupId) REFERENCES groups(id) ON DELETE CASCADE
) default charset utf8 comment '';