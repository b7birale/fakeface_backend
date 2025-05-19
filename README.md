# Telepítési és indítási útmutató
## Bevezetés
A szakdolgozat 3 részből áll:
1. Frontend
2. Backend
3. Adatbázis

A frontend és a backend két külön repositoryban található. Az adatbázishoz szükséges parancsokat ebben a readme-ben találja meg!

## CREATE TABLES

### Users
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(250) NOT NULL,
  `password` varchar(1000) NOT NULL,
  `birthdate` date NOT NULL,
  `profile_picture` mediumtext,
  `first_name` varchar(250) NOT NULL,
  `last_name` varchar(250) NOT NULL,
  `qr_code` varchar(1000) DEFAULT NULL,
  `salt` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Posts
CREATE TABLE `posts` (
  `post_id` int NOT NULL AUTO_INCREMENT,
  `picture` mediumtext,
  `content` varchar(1000) NOT NULL,
  `date` datetime NOT NULL,
  `user_id` int NOT NULL,
  `title` varchar(500) NOT NULL,
  PRIMARY KEY (`post_id`),
  KEY `fk_post_user` (`user_id`),
  CONSTRAINT `fk_post_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Comments
CREATE TABLE `comments` (
  `comment_id` int NOT NULL AUTO_INCREMENT,
  `date` datetime NOT NULL,
  `user_id` int NOT NULL,
  `post_id` int NOT NULL,
  `content` varchar(1000) NOT NULL,
  PRIMARY KEY (`comment_id`),
  KEY `fk_comment_user` (`user_id`),
  KEY `fk_comment_post` (`post_id`),
  CONSTRAINT `fk_comment_post` FOREIGN KEY (`post_id`) REFERENCES `posts` (`post_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_comment_post_id` FOREIGN KEY (`post_id`) REFERENCES `posts` (`post_id`),
  CONSTRAINT `fk_comment_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_comment_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Chatrooms
CREATE TABLE `chatrooms` (
  `chatroom_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `user_id_one` int DEFAULT NULL,
  `user_id_two` int DEFAULT NULL,
  PRIMARY KEY (`chatroom_id`),
  UNIQUE KEY `unique_user_pair` (`user_id_one`,`user_id_two`),
  CONSTRAINT `check_user_ids` CHECK ((`user_id_one` < `user_id_two`))
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Messages
 CREATE TABLE `messages` (
  `message_id` int NOT NULL AUTO_INCREMENT,
  `content` varchar(1000) NOT NULL,
  `chatroom_id` int NOT NULL,
  `sender_user_id` int NOT NULL,
  `reciever_user_id` int NOT NULL,
  `message_datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`message_id`),
  KEY `fk_chatroom` (`chatroom_id`),
  KEY `fk_message_sender` (`sender_user_id`),
  KEY `fk_message_reciever` (`reciever_user_id`),
  CONSTRAINT `fk_chatroom` FOREIGN KEY (`chatroom_id`) REFERENCES `chatrooms` (`chatroom_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_message_chatroom_id` FOREIGN KEY (`chatroom_id`) REFERENCES `chatrooms` (`chatroom_id`),
  CONSTRAINT `fk_message_reciever` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_message_reciever_user_id` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_message_sender` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Friend Relations
CREATE TABLE `friend_relations` (
  `relation_id` int NOT NULL AUTO_INCREMENT,
  `user_id_one` int NOT NULL,
  `user_id_two` int NOT NULL,
  PRIMARY KEY (`relation_id`),
  UNIQUE KEY `unique_user_pair` (`user_id_one`,`user_id_two`),
  KEY `fk_user_two` (`user_id_two`),
  CONSTRAINT `fk_friend_relation_user_id_one` FOREIGN KEY (`user_id_one`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_friend_relation_user_id_two` FOREIGN KEY (`user_id_two`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_user_one` FOREIGN KEY (`user_id_one`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_user_two` FOREIGN KEY (`user_id_two`) REFERENCES `users` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

### Friend Requests
CREATE TABLE `friend_requests` (
  `request_id` int NOT NULL AUTO_INCREMENT,
  `accepted` tinyint(1) NOT NULL,
  `rejected` tinyint(1) NOT NULL,
  `sender_user_id` int NOT NULL,
  `reciever_user_id` int NOT NULL,
  PRIMARY KEY (`request_id`),
  KEY `fk_sender` (`sender_user_id`),
  KEY `fk_reciever` (`reciever_user_id`),
  CONSTRAINT `fk_friend_request_reciever_user_id` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_friend_request_sender_user_id` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_reciever` FOREIGN KEY (`reciever_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_sender` FOREIGN KEY (`sender_user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci





