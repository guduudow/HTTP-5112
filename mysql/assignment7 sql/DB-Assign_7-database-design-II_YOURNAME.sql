--EDERES GURE	ASSIGNMENT 7 GROUPING RESULTS
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
-- 1 
--A
CREATE TABLE authors (
    author_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    username VARCHAR(100),
);

CREATE TABLE posts (
    post_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(100),
    post_content TEXT,
    author_id INT,
    date_published DATETIME,
    FOREIGN KEY (author_id) REFERENCES authors(author_id)
);
--B
INSERT INTO authors (author_id, first_name, last_name, email, username)
VALUES 
    (1, "James", "Jordan", "jjordan@gmail.com", "jjordan"),
    (2, "Bill", "Watkins", "billwat43@outlook.com", "whatwatkins"),
    (3, "Veronica", "Smith", "veronsmith@hotmail.com", "veronika");

INSERT INTO posts (post_id, title, post_content, author_id, date_published)
VALUES 
    (),
    (),
    ();

-- 2
--A
CREATE TABLE Comments (
    comment_id INT PRIMARY KEY AUTO_INCREMENT,
    post_id INT,
    author_id INT,
    comment_text TEXT,
    time_posted DATETIME,
    FOREIGN KEY (post_id) REFERENCES posts(post_id),
    FOREIGN KEY (author_id) REFERENCES authors(author_id)
);
--B
INSERT INTO Comments (post_id, author_id, comment_text, created_at)
VALUES 
    (),
    (),
    ();

-- 3
--A

--B

