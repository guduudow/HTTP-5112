--EDERES GURE	ASSIGNMENT 7 GROUPING RESULTS
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
-- 1 
--A
CREATE TABLE authors (
    author_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    username VARCHAR(100)
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
    (1, "Top 50 NES Games", "Here are the 50 best games on Nintendo's first console.", 2, "2023-10-30 08:00:00"),
    (2, "DOOM: Eternal Review on PC", "Bethesda's sequel to 2016's DOOM is extra gory one.", 3, "2020-03-15 12:35:21"),
    (3, "Our Favourite Gaming Secrets", "Easter eggs as they are called are as old as gaming itself, here are our favourites.", 1, "2014-05-17 15:43:05");

-- 2
--A
CREATE TABLE comments (
    comment_id INT PRIMARY KEY AUTO_INCREMENT,
    post_id INT,
    author_id INT,
    comment_text TEXT,
    FOREIGN KEY (post_id) REFERENCES posts(post_id),
    FOREIGN KEY (author_id) REFERENCES authors(author_id)
);
--B
INSERT INTO comments (post_id, author_id, comment_text)
VALUES 
    (1,2, "Yup, Super Mario Bros. 3 is the best game on the NES."),
    (2,3, "LMAO YOU GAVE IT 7?? DO YOU GUYS EVEN KNOW GAMING??"),
    (3,1, "Discovering the konami code as a kid was mind-blowing lol");

-- 3
--A
ALTER TABLE Comments
ADD comment_date DATETIME;
    (1,2, "Yup, Super Mario Bros. 3 is the best game on the NES.", "2023-10-30 11:41:30"),
    (2,3, "LMAO YOU GAVE IT 7?? DO YOU GUYS EVEN KNOW GAMING??", "2020-03-15 13:01:45"),
    (3,1, "Discovering the konami code as a kid was mind-blowing lol", "2014-05-17 20:32:15");

    UPDATE comments set comment_date = '2023-10-30 11:41:30' WHERE comment_id = 1;
    UPDATE comments set comment_date = '2020-03-15 13:01:45' WHERE comment_id = 2;
    UPDATE comments set comment_date = '2014-05-17 20:32:15' WHERE comment_id = 3;
--B
DELETE authors, posts, comments
FROM authors
LEFT JOIN posts ON authors.author_id = posts.author_id, comments on authors.author_id = comments.author_id
WHERE author_id IN (1,2);

DELETE authors, posts, comments
FROM authors
INNER JOIN posts ON authors.author_id = posts.author_id INNER JOIN comments ON authors.author_id = comments.author_id
WHERE authors.author_id IN (1,2);
