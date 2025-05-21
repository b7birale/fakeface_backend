# Fakeface - Indítási és telepítési útmutató

## Bevezetés

A szakdolgozat 3 részből áll:

1. Frontend
2. Backend
3. Adatbázis

A frontend és a backend két külön repositoryban található. Az adatbázishoz szükséges parancsokat ebben a readme-ben találja meg!<br>

## CREATE TABLES

### Users

```sql
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
```

### Posts

```sql
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
```

### Comments
```sql
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
```

### Friend Requests
```sql
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
```

### Friend Relations
```sql
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
```

### Chatrooms
```sql
CREATE TABLE `chatrooms` (
  `chatroom_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `user_id_one` int DEFAULT NULL,
  `user_id_two` int DEFAULT NULL,
  PRIMARY KEY (`chatroom_id`),
  UNIQUE KEY `unique_user_pair` (`user_id_one`,`user_id_two`),
  CONSTRAINT `check_user_ids` CHECK ((`user_id_one` < `user_id_two`))
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```

### Messages
```sql
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
```


## Táblatervek

### Users

| Column          | Type          | Null | Key | Default | Extra          |
|-----------------|---------------|------|-----|---------|----------------|
| user_id         | int           | NO   | PRI | NULL    | auto_increment |
| email           | varchar(250)  | NO   |     | NULL    |                |
| password        | varchar(1000) | NO   |     | NULL    |                |
| birthdate       | date          | NO   |     | NULL    |                |
| profile_picture | mediumtext    | YES  |     | NULL    |                |
| first_name      | varchar(250)  | NO   |     | NULL    |                |
| last_name       | varchar(250)  | NO   |     | NULL    |                |
| qr_code         | varchar(1000) | YES  |     | NULL    |                |
| salt            | varchar(50)   | YES  |     | NULL    |                |

### Posts

| Column   | Type          | Null | Key | Default | Extra          |
|----------|---------------|------|-----|---------|----------------|
| post_id  | int           | NO   | PRI | NULL    | auto_increment |
| picture  | mediumtext    | YES  |     | NULL    |                |
| content  | varchar(1000) | NO   |     | NULL    |                |
| date     | datetime      | NO   |     | NULL    |                |
| user_id  | int           | NO   | MUL | NULL    |                |
| title    | varchar(500)  | NO   |     | NULL    |                |

### Comments

| Column     | Type          | Null | Key | Default | Extra          |
|------------|---------------|------|-----|---------|----------------|
| comment_id | int           | NO   | PRI | NULL    | auto_increment |
| date       | datetime      | NO   |     | NULL    |                |
| user_id    | int           | NO   | MUL | NULL    |                |
| post_id    | int           | NO   | MUL | NULL    |                |
| content    | varchar(1000) | NO   |     | NULL    |                |

### Friend Requests

| Column           | Type       | Null | Key | Default | Extra          |
|------------------|------------|------|-----|---------|----------------|
| request_id       | int        | NO   | PRI | NULL    | auto_increment |
| accepted         | tinyint(1) | NO   |     | NULL    |                |
| rejected         | tinyint(1) | NO   |     | NULL    |                |
| sender_user_id   | int        | NO   | MUL | NULL    |                |
| reciever_user_id | int        | NO   | MUL | NULL    |                |

### Friend Relations

| Column       | Type | Null | Key | Default | Extra          |
|--------------|------|------|-----|---------|----------------|
| relation_id  | int  | NO   | PRI | NULL    | auto_increment |
| user_id_one  | int  | NO   | MUL | NULL    |                |
| user_id_two  | int  | NO   | MUL | NULL    |                |

### Chatrooms

| Column       | Type          | Null | Key | Default | Extra          |
|--------------|---------------|------|-----|---------|----------------|
| chatroom_id  | int           | NO   | PRI | NULL    | auto_increment |
| name         | varchar(100)  | NO   |     | NULL    |                |
| user_id_one  | int           | YES  | MUL | NULL    |                |
| user_id_two  | int           | YES  |     | NULL    |                |

### Messages

| Column           | Type          | Null | Key | Default | Extra          |
|------------------|---------------|------|-----|---------|----------------|
| message_id       | int           | NO   | PRI | NULL    | auto_increment |
| content          | varchar(1000) | NO   |     | NULL    |                |
| chatroom_id      | int           | NO   | MUL | NULL    |                |
| sender_user_id   | int           | NO   | MUL | NULL    |                |
| reciever_user_id | int           | NO   | MUL | NULL    |                |


## Tárolt eljárások

### Users

```sql
DELIMITER //

CREATE PROCEDURE GetAllUsers(
    IN p_user_id INT
)
BEGIN
    SELECT user_id, last_name, first_name, profile_picture FROM users
    WHERE user_id <> p_user_id;

END //
DELIMITER ;



DELIMITER //

CREATE PROCEDURE ModifyUserData(
    IN p_user_id INT,
    IN p_password VARCHAR(1000),
    IN p_first_name VARCHAR(250),
    IN p_last_name VARCHAR(250),
    IN p_birthdate VARCHAR(100),
    IN p_profile_picture MEDIUMTEXT,
    IN p_salt VARCHAR(50)
)
BEGIN
    UPDATE users
    SET
	password = CASE WHEN p_password <> '' THEN p_password ELSE password END,
	first_name = CASE WHEN p_first_name <> '' THEN p_first_name ELSE first_name END,
	last_name = CASE WHEN p_last_name <> '' THEN p_last_name ELSE last_name END,
	birthdate = CASE WHEN p_birthdate <> '' THEN  STR_TO_DATE(TRIM(TRAILING '-' FROM REPLACE(REPLACE(p_birthdate, ' ', ''), '.', '-')), '%Y-%m-%d') ELSE birthdate END,
	profile_picture = CASE WHEN p_profile_picture <> '' THEN p_profile_picture ELSE profile_picture END,
	salt = CASE WHEN p_password <> '' THEN p_salt ELSE salt END
    WHERE user_id = p_user_id;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetUserToProfile(IN user_id INT)
BEGIN
    SELECT user_id, email, birthdate, profile_picture, first_name, last_name FROM users WHERE users.user_id = user_id;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE UploadProfilePicture (
    IN p_user_id INT,
    IN p_profile_picture_path VARCHAR(1000)
)
BEGIN
     UPDATE users
     SET profile_picture = p_profile_picture_path
     WHERE user_id = p_user_id;
    
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetUserByName (
    IN first_name VARCHAR(250),
    IN last_name VARCHAR(250)
)
BEGIN
    SELECT user_id, birthdate, profile_picture, first_name, last_name 
    FROM users
    WHERE (first_name = first_name OR first_name IS NULL) AND (last_name = last_name OR last_name IS NULL);
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetUserById(IN user_id INT)
BEGIN
    SELECT * FROM users WHERE users.user_id = user_id;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE SignUp(IN email VARCHAR(250), IN password VARCHAR(1000), IN salt VARCHAR(50), IN birthdate VARCHAR(50), IN profile_picture VARCHAR(1000), IN first_name VARCHAR(250), IN last_name VARCHAR(250), IN qr_code VARCHAR(1000))
BEGIN
    INSERT INTO users (email, password, salt, birthdate, profile_picture, first_name, last_name, qr_code) VALUES (email, password, salt, STR_TO_DATE(TRIM(TRAILING '-' FROM REPLACE(REPLACE(birthdate, ' ', ''), '.', '-')), '%Y-%m-%d'), profile_picture, first_name, last_name, qr_code);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE Login(IN email VARCHAR(250))
BEGIN
    SELECT user_id, first_name, last_name, birthdate, password, salt FROM users WHERE users.email LIKE email;
END //

DELIMITER;
```

### Posts

```sql

DELIMITER //

CREATE PROCEDURE CreatePost (
    IN p_picture mediumtext,
    IN p_content varchar(1000),
    IN p_user_id INT,
    IN p_title varchar(500)
)
BEGIN
    INSERT INTO posts (picture, content, date, user_id, title)
    VALUES (p_picture, p_content, NOW(), p_user_id, p_title);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE DeletePost (
    IN p_post_id INT
)
BEGIN
    DELETE FROM posts
    WHERE post_id = p_post_id;
END //

DELIMITER ;



DELIMITER //
CREATE PROCEDURE GetPostsByUserIds (
    IN p_user_ids VARCHAR(1000)
)
BEGIN
    set @userids = p_user_ids;
    DROP TEMPORARY TABLE IF EXISTS TempUserIds;

    CREATE TEMPORARY TABLE TempUserIds (user_id VARCHAR(1000));

    INSERT INTO TempUserIds (user_id)
    SELECT TRIM(SUBSTRING_INDEX(SUBSTRING_INDEX(@userids, ',', numbers.n), ',', -1)) AS user_id
    FROM (
        SELECT ROW_NUMBER() OVER () AS n
        FROM information_schema.columns
    ) numbers

    WHERE numbers.n <= 1 + CHAR_LENGTH(@userids) - CHAR_LENGTH(REPLACE(@userids, ',', ''))
    AND TRIM(SUBSTRING_INDEX(SUBSTRING_INDEX(@userids, ',', numbers.n), ',', -1)) <> '';

    SELECT p.*, u.first_name, u.last_name, u.profile_picture FROM posts AS p
    INNER JOIN users AS u ON p.user_id = u.user_id
    WHERE p.user_id IN (SELECT user_id FROM TempUserIds)
    ORDER BY p.date DESC;


END //

DELIMITER ;

```

### Comments

```sql

DELIMITER //

CREATE PROCEDURE AddComment (
    p_content VARCHAR(1000),
    p_user_id INT,
    p_post_id INT
)
BEGIN
    INSERT INTO comments (date, user_id, post_id, content)
    VALUES (NOW(), p_user_id, p_post_id, p_content);
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetCommentsByPostId (
    p_post_id INT
)
BEGIN
    SELECT c.*, u.first_name, u.last_name, u.profile_picture FROM comments as c
    INNER JOIN users as u ON
    u.user_id = c.user_id
    WHERE c.post_id = p_post_id;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE DeleteComment (
    IN p_comment_id INT
)
BEGIN
    DELETE FROM comments
    WHERE comment_id = p_comment_id;
END //

DELIMITER ;

```

### Friend Requests

```sql

DELIMITER //

CREATE PROCEDURE GetFriendRequests (
    IN p_user_id INT
)
BEGIN
    SELECT request_id, sender_user_id, reciever_user_id
    FROM friends_requests
    WHERE reciever_user_id = p_user_id AND accepted = 0 AND rejected = 0;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE AcceptRequest(
    IN p_user_id_sender INT,
    IN p_user_id_reciever INT
)
BEGIN
    UPDATE friend_requests
    SET accepted = 1
    WHERE sender_user_id = p_user_id_sender
    AND reciever_user_id = p_user_id_reciever;
	CALL CreateRelation(p_user_id_sender , p_user_id_reciever);
END //
DELIMITER ;



DELIMITER //

CREATE PROCEDURE CreateRelation (
    IN p_user_id_one INT,
    IN p_user_id_two INT
)
BEGIN
        INSERT INTO friend_relations (user_id_one, user_id_two)
        VALUES (p_user_id_one, p_user_id_two);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE RejectRequest(
    IN p_user_id_sender INT,
    IN p_user_id_reciever INT
)
BEGIN
    UPDATE friend_requests
    SET rejected = 1
    WHERE sender_user_id = p_user_id_sender
    AND reciever_user_id = p_user_id_reciever;

END //
DELIMITER ;


DELIMITER //

CREATE PROCEDURE CreateRequest (
    IN p_sender_user_id INT,
    IN p_reciever_user_id INT
)
BEGIN
    INSERT INTO friend_requests (sender_user_id, reciever_user_id, accepted, rejected)
    VALUES (p_sender_user_id, p_reciever_user_id, 0, 0);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE DeleteRequest (
    IN p_request_id INT
)
BEGIN
    DELETE FROM friend_requests
    WHERE request_id = p_request_id;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetIncomingFriendRequests (
    IN p_user_id INT
)
BEGIN
    SELECT request_id, sender_user_id, reciever_user_id, first_name, last_name, profile_picture
    FROM friend_requests
	INNER JOIN users ON users.user_id = friend_requests.sender_user_id
    WHERE reciever_user_id = p_user_id AND accepted = 0 AND rejected = 0;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetOutgoingFriendRequests (
    IN p_user_id INT
)
BEGIN
    SELECT request_id, sender_user_id, reciever_user_id
    FROM friends_requests
    WHERE sender_user_id = user_id AND accepted = 0 AND rejected = 0;
END //

DELIMITER ;

```

### Friend Relations

```sql

DELIMITER //
CREATE PROCEDURE GetFriendsIdsByUserId (
    IN p_user_id INT
)
BEGIN
    SELECT 
        CASE 
            WHEN p_user_id = user_id_one THEN user_id_two
            WHEN p_user_id = user_id_two THEN user_id_one
        END AS friend_id
    FROM 
        friend_relations
    WHERE 
        p_user_id = user_id_one OR p_user_id = user_id_two;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetFriendsByUserId (
    IN p_user_id INT
)
BEGIN
    SELECT 
        u.*
    FROM (
        SELECT 
            CASE 
                WHEN p_user_id = user_id_one THEN user_id_two
                WHEN p_user_id = user_id_two THEN user_id_one
            END AS friend_id
        FROM 
            friend_relations
        WHERE 
            p_user_id = user_id_one OR p_user_id = user_id_two
    ) AS friends
    INNER JOIN users u ON u.user_id = friends.friend_id;
	
END //
DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetFriendsByUserId (
    IN p_user_id INT
)
BEGIN
    
    SELECT * FROM friend_relations WHERE p_user_id = friend_relations.user_id_one OR p_user_id = friend_relations.user_id_two;

END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE CreateRelation (
    IN p_user_id_one INT,
    IN p_user_id_two INT
)
BEGIN
        INSERT INTO friend_relations (user_id_one, user_id_two)
        VALUES (p_user_id_one, p_user_id_two);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE DeleteRelation (
    IN p_user_id_one INT,
    IN p_user_id_two INT
)
BEGIN
    DELETE FROM friend_relations
    WHERE (user_id_one = p_user_id_one AND user_id_two = p_user_id_two)
    OR (user_id_one = p_user_id_two AND user_id_two = p_user_id_one);
END //

DELIMITER ;

```

### Chatrooms

```sql

DELIMITER //

CREATE PROCEDURE GetChatroomsByUserId (
    IN p_user_id INT
)
BEGIN
    SELECT * FROM chatrooms
	WHERE chatrooms.user_id_one = p_user_id OR
	chatrooms.user_id_two = p_user_id;

END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetChatroomsByUserId (
	IN p_user_id INT
)
BEGIN
	SELECT c.*, u.profile_picture
	FROM chatrooms c
	LEFT JOIN users u ON (
		(c.user_id_one = p_user_id AND u.user_id = c.user_id_two) OR
		(c.user_id_two = p_user_id AND u.user_id = c.user_id_one)
	) 
	WHERE c.user_id_one = p_user_id OR
	c.user_id_two = p_user_id;
	
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE CreateChatroom (
    IN p_name VARCHAR(100),
	IN p_user_id_one INT,
	IN p_user_id_two INT
)
BEGIN
    INSERT INTO chatrooms (name, user_id_one, user_id_two) VALUES (p_name, p_user_id_one, p_user_id_two);
	SELECT LAST_INSERT_ID() AS chatroom_id;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE DeleteChatroom (
    IN p_room_id INT
)
BEGIN
    DELETE FROM chatrooms
    WHERE chatroom_id = p_room_id;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE GetChatroomByUserIds (
    IN p_user_id_one INT,
	IN p_user_id_two INT
)
BEGIN
    SELECT * FROM chatrooms
	WHERE user_id_one = p_user_id_one
	AND user_id_two = p_user_id_two;
END //

DELIMITER ;


```

### Messages

```sql

DELIMITER //

CREATE PROCEDURE GetMessagesByChatroomId (
    IN p_chatroom_id INT
)
BEGIN
    SELECT * FROM messages
	WHERE chatroom_id = p_chatroom_id
	ORDER BY messages.message_datetime;
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE SendMessage (
    IN p_content VARCHAR(1000),
    IN p_chatroom_id INT,
    IN p_sender_user_id INT,
    IN p_reciever_user_id INT,
	IN p_message_datetime DATETIME
)
BEGIN
    INSERT INTO messages (content, chatroom_id, sender_user_id, reciever_user_id, message_datetime)
    VALUES (p_content, p_chatroom_id, p_sender_user_id, p_reciever_user_id, p_message_datetime);
END //

DELIMITER ;



DELIMITER //

CREATE PROCEDURE DeleteMessage (
    IN p_message_id INT
)
BEGIN
    DELETE FROM messages
    WHERE message_id = p_message_id;
END //

DELIMITER ;

```


## KÜLSŐ KULCSOK

### Posts
```sql
ALTER TABLE posts
ADD CONSTRAINT fk_post_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE;
```

### Comments
```sql
ALTER TABLE comments
ADD CONSTRAINT fk_comment_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
ADD CONSTRAINT fk_comment_post FOREIGN KEY (post_id) REFERENCES posts(post_id) ON DELETE CASCADE;
```

### Friend Requests
```sql
ALTER TABLE friend_requests
ADD CONSTRAINT fk_sender FOREIGN KEY (sender_user_id) REFERENCES users(user_id) ON DELETE CASCADE,
ADD CONSTRAINT fk_reciever FOREIGN KEY (reciever_user_id) REFERENCES users(user_id) ON DELETE CASCADE;
```

### Friend Relations
```sql
ALTER TABLE friend_relations
ADD CONSTRAINT fk_user_one FOREIGN KEY (user_id_one) REFERENCES users(user_id) ON DELETE CASCADE,
ADD CONSTRAINT fk_user_two FOREIGN KEY (user_id_two) REFERENCES users(user_id) ON DELETE CASCADE;
```


### Messages

```sql
ALTER TABLE messages
ADD CONSTRAINT fk_chatroom FOREIGN KEY (chatroom_id) REFERENCES chatrooms(chatroom_id) ON DELETE CASCADE,
ADD CONSTRAINT fk_message_sender FOREIGN KEY (sender_user_id) REFERENCES users(user_id) ON DELETE CASCADE,
ADD CONSTRAINT fk_message_reciever FOREIGN KEY (reciever_user_id) REFERENCES users(user_id) ON DELETE CASCADE;
```

## Példa rekordok (INSERT INTOs)

### Users
```sql
INSERT INTO users (email, password, birthdate, profile_picture, first_name, last_name, qr_code, salt) 
VALUES 
('john.doe@example.com', 'hashed_password_123', '1990-05-15', 'profile1.jpg', 'John', 'Doe', 'qr123456', 'salt1'),
('jane.smith@example.com', 'hashed_password_456', '1985-08-22', 'profile2.jpg', 'Jane', 'Smith', 'qr789012', 'salt2'),
('mike.johnson@example.com', 'hashed_password_789', '1995-03-10', NULL, 'Mike', 'Johnson', NULL, 'salt3');
```

### Posts
```sql
INSERT INTO posts (picture, content, date, user_id, title)
VALUES
('post1.jpg', 'Enjoying a beautiful day at the beach!', '2023-06-10 14:30:00', 1, 'Beach Day'),
('post2.jpg', 'Just finished this amazing book!', '2023-06-12 09:15:00', 2, 'Book Recommendation'),
(NULL, 'Working on a new project, excited to share soon!', '2023-06-15 18:45:00', 3, 'New Project');
```

### Comments
```sql
INSERT INTO comments (date, user_id, post_id, content)
VALUES
('2023-06-10 15:05:00', 2, 1, 'Looks amazing! Which beach is this?'),
('2023-06-10 15:30:00', 3, 1, 'Wish I was there!'),
('2023-06-12 10:00:00', 1, 2, 'What book is this? I need recommendations!');
```

### Friend requests
```sql
INSERT INTO friend_requests (accepted, rejected, sender_user_id, reciever_user_id)
VALUES
(1, 0, 1, 2),  -- Accepted request
(0, 1, 3, 1),  -- Rejected request
(0, 0, 2, 3);  -- Pending request
```

### Friend relations
```sql
INSERT INTO friend_relations (user_id_one, user_id_two)
VALUES
(1, 2),  -- John and Jane are friends
(1, 3);  -- John and Mike are friends
```

### Chatrooms
```sql
INSERT INTO chatrooms (name, user_id_one, user_id_two)
VALUES
('John & Jane', 1, 2),
('Work Group', 1, 3),
('Project Team', 2, 3);
```

### Messages
```sql
INSERT INTO messages (content, chatroom_id, sender_user_id, reciever_user_id, message_datetime)
VALUES
('Hey Jane, how are you?', 1, 1, 2, '2023-06-10 10:00:00'),
('Hi John! I''m good, thanks!', 1, 2, 1, '2023-06-10 10:02:00'),
('Mike, about that project deadline...', 2, 1, 3, '2023-06-11 15:30:00'),
('I''ll send you the files soon', 3, 2, 3, '2023-06-12 11:45:00');
```
