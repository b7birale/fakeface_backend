# Telepítési és indítási útmutató
## Bevezetés
A szakdolgozat 3 részből áll:
1. Frontend
2. Backend
3. Adatbázis

A frontend és a backend két külön repositoryban található. Az adatbázishoz szükséges parancsokat ebben a readme-ben találja meg!

## CREATE TABLES

### Users
CREATE TABLE `users` (<br>
  `user_id` int NOT NULL AUTO_INCREMENT,<br>
  `email` varchar(250) NOT NULL,<br>
  `password` varchar(1000) NOT NULL,<br>
  `birthdate` date NOT NULL,<br>
  `profile_picture` mediumtext,<br>
  `first_name` varchar(250) NOT NULL,<br>
  `last_name` varchar(250) NOT NULL,<br>
  `qr_code` varchar(1000) DEFAULT NULL,<br>
  `salt` varchar(50) DEFAULT NULL,<br>
  PRIMARY KEY (`user_id`)<br>
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Posts
CREATE TABLE `posts` (<br>
  `post_id` int NOT NULL AUTO_INCREMENT,<br>
  `picture` mediumtext,<br>
  `content` varchar(1000) NOT NULL,<br>
  `date` datetime NOT NULL,<br>
  `user_id` int NOT NULL,<br>
  `title` varchar(500) NOT NULL,<br>
  PRIMARY KEY (`post_id`),<br>
  KEY `fk_post_user` (`user_id`),<br>
  CONSTRAINT `fk_post_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)<br>
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Comments
CREATE TABLE `comments` (<br>
  `comment_id` int NOT NULL AUTO_INCREMENT,<br>
  `date` datetime NOT NULL,<br>
  `user_id` int NOT NULL,<br>
  `post_id` int NOT NULL,<br>
  `content` varchar(1000) NOT NULL,<br>
  PRIMARY KEY (`comment_id`),<br>
  KEY `fk_comment_user` (`user_id`),<br>
  KEY `fk_comment_post` (`post_id`),<br>
  CONSTRAINT `fk_comment_post` FOREIGN KEY (`post_id`) REFERENCES `posts` (`post_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_comment_post_id` FOREIGN KEY (`post_id`) REFERENCES `posts` (`post_id`),<br>
  CONSTRAINT `fk_comment_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_comment_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)<br>
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Chatrooms<br>
CREATE TABLE `chatrooms` (<br>
  `chatroom_id` int NOT NULL AUTO_INCREMENT,<br>
  `name` varchar(100) NOT NULL,<br>
  `user_id_one` int DEFAULT NULL,<br>
  `user_id_two` int DEFAULT NULL,<br>
  PRIMARY KEY (`chatroom_id`),<br>
  UNIQUE KEY `unique_user_pair` (`user_id_one`,`user_id_two`),<br>
  CONSTRAINT `check_user_ids` CHECK ((`user_id_one` < `user_id_two`))<br>
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Messages<br>
 CREATE TABLE `messages` (<br>
  `message_id` int NOT NULL AUTO_INCREMENT,<br>
  `content` varchar(1000) NOT NULL,<br>
  `chatroom_id` int NOT NULL,<br>
  `sender_user_id` int NOT NULL,<br>
  `reciever_user_id` int NOT NULL,<br>
  `message_datetime` datetime DEFAULT NULL,<br>
  PRIMARY KEY (`message_id`),<br>
  KEY `fk_chatroom` (`chatroom_id`),<br>
  KEY `fk_message_sender` (`sender_user_id`),<br>
  KEY `fk_message_reciever` (`reciever_user_id`),<br>
  CONSTRAINT `fk_chatroom` FOREIGN KEY (`chatroom_id`) REFERENCES `chatrooms` (`chatroom_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_message_chatroom_id` FOREIGN KEY (`chatroom_id`) REFERENCES `chatrooms` (`chatroom_id`),<br>
  CONSTRAINT `fk_message_reciever` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_message_reciever_user_id` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`),<br>
  CONSTRAINT `fk_message_sender` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE<br>
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Friend Relations
CREATE TABLE `friend_relations` (<br>
  `relation_id` int NOT NULL AUTO_INCREMENT,<br>
  `user_id_one` int NOT NULL,<br>
  `user_id_two` int NOT NULL,<br>
  PRIMARY KEY (`relation_id`),<br>
  UNIQUE KEY `unique_user_pair` (`user_id_one`,`user_id_two`),<br>
  KEY `fk_user_two` (`user_id_two`),<br>
  CONSTRAINT `fk_friend_relation_user_id_one` FOREIGN KEY (`user_id_one`) REFERENCES `users` (`user_id`),<br>
  CONSTRAINT `fk_friend_relation_user_id_two` FOREIGN KEY (`user_id_two`) REFERENCES `users` (`user_id`),<br>
  CONSTRAINT `fk_user_one` FOREIGN KEY (`user_id_one`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_user_two` FOREIGN KEY (`user_id_two`) REFERENCES `users` (`user_id`) ON DELETE CASCADE<br>
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

### Friend Requests
CREATE TABLE `friend_requests` (<br>
  `request_id` int NOT NULL AUTO_INCREMENT,<br>
  `accepted` tinyint(1) NOT NULL,<br>
  `rejected` tinyint(1) NOT NULL,<br>
  `sender_user_id` int NOT NULL,<br>
  `reciever_user_id` int NOT NULL,<br>
  PRIMARY KEY (`request_id`),<br>
  KEY `fk_sender` (`sender_user_id`),<br>
  KEY `fk_reciever` (`reciever_user_id`),<br>
  CONSTRAINT `fk_friend_request_reciever_user_id` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`),<br>
  CONSTRAINT `fk_friend_request_sender_user_id` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`),<br>
  CONSTRAINT `fk_reciever` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,<br>
  CONSTRAINT `fk_sender` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE<br>
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci<br>

## Tárolt eljárások

### Users
#### GetAllUsers

DELIMITER //<br>
CREATE PROCEDURE GetAllUsers(<br>
    IN p_user_id INT<br>
)<br>
BEGIN<br>
    SELECT user_id, last_name, first_name, profile_picture FROM users<br>
    WHERE user_id <> p_user_id;<br>
END //<br>
DELIMITER;<br>

#### ModifyUserData
DELIMITER //<br>
CREATE PROCEDURE ModifyUserData(<br>
    IN p_user_id INT,<br>
    IN p_password VARCHAR(1000),<br>
    IN p_first_name VARCHAR(250),<br>
    IN p_last_name VARCHAR(250),<br>
    IN p_birthdate VARCHAR(100),<br>
    IN p_profile_picture MEDIUMTEXT,<br>
    IN p_salt VARCHAR(50)<br>
)<br>
BEGIN<br>
    UPDATE users<br>
    SET<br>
	password = CASE WHEN p_password <> '' THEN p_password ELSE password END,<br>
	first_name = CASE WHEN p_first_name <> '' THEN p_first_name ELSE first_name END,<br>
	last_name = CASE WHEN p_last_name <> '' THEN p_last_name ELSE last_name END,<br>
	birthdate = CASE WHEN p_birthdate <> '' THEN  STR_TO_DATE(TRIM(TRAILING '-' FROM REPLACE(REPLACE(p_birthdate, ' ', ''), '.', '-')), '%Y-%m-%d') ELSE birthdate END,<br>
	profile_picture = CASE WHEN p_profile_picture <> '' THEN p_profile_picture ELSE profile_picture END,<br>
	salt = CASE WHEN p_password <> '' THEN p_salt ELSE salt END<br>
    WHERE user_id = p_user_id;<br>
END //<br>
DELIMITER ;<br>

#### GetUserToProfile
DELIMITER //<br>
CREATE PROCEDURE GetUserToProfile(IN user_id INT)<br>
BEGIN<br>
    SELECT user_id, email, birthdate, profile_picture, first_name, last_name FROM users WHERE users.user_id = user_id;<br>
END //<br>
DELIMITER;<br>

#### UploadProfilePicture
DELIMITER //<br>
CREATE PROCEDURE UploadProfilePicture (<br>
    IN p_user_id INT,<br>
    IN p_profile_picture_path VARCHAR(1000)<br>
)<br>
BEGIN<br>
     UPDATE users<br>
     SET profile_picture = p_profile_picture_path<br>
     WHERE user_id = p_user_id;<br>
END //<br>
DELIMITER;<br>

#### GetUserByName
DELIMITER //<br>

CREATE PROCEDURE GetUserByName (<br>
    IN first_name VARCHAR(250),<br>
    IN last_name VARCHAR(250)<br>
)<br>
BEGIN<br>
    SELECT user_id, birthdate, profile_picture, first_name, last_name <br>
    FROM users<br>
    WHERE (first_name = first_name OR first_name IS NULL) AND (last_name = last_name OR last_name IS NULL);<br>
END //<br>

DELIMITER ;<br>


#### GetUserById
DELIMITER //<br>

CREATE PROCEDURE GetUserById(IN user_id INT)<br>
BEGIN<br>
    SELECT * FROM users WHERE users.user_id = user_id;<br>
END //<br>

DELIMITER ;<br>


#### SignUp
DELIMITER //<br>

CREATE PROCEDURE SignUp(<br>
	IN email VARCHAR(250),<br>
 	IN password VARCHAR(1000),<br>
  	IN salt VARCHAR(50),<br>
   	IN birthdate VARCHAR(50),<br>
    	IN profile_picture VARCHAR(1000),<br>
     	IN first_name VARCHAR(250),<br>
      	IN last_name VARCHAR(250),<br>
       	IN qr_code VARCHAR(1000)<br>
)<br>
BEGIN<br>
    INSERT INTO users (email, password, salt, birthdate, profile_picture, first_name, last_name, qr_code)<br>
    VALUES (email, password, salt, STR_TO_DATE(TRIM(TRAILING '-' FROM REPLACE(REPLACE(birthdate, ' ', ''), '.', '-')), '%Y-%m-%d'), profile_picture, first_name, last_name, qr_code);<br>
END //<br>

DELIMITER ;<br>


#### Login
DELIMITER //<br>

CREATE PROCEDURE Login(IN email VARCHAR(250))<br>
BEGIN<br>
    SELECT user_id, first_name, last_name, birthdate, password, salt FROM users <br>
    WHERE users.email LIKE email;<br>
END //<br>

DELIMITER;<br>





