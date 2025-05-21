# Fakeface - Indítási és telepítési útmutató

## Bevezetés

A szakdolgozat 3 részből áll:

1. Frontend
2. Backend
3. Adatbázis

A frontend és a backend két külön repositoryban található. Az adatbázishoz szükséges parancsokat ebben a readme-ben találja meg!<br>

## CREATE TABLES

## Táblatervek

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

```
