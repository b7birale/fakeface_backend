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





