--YOUR NAME HERE	ASSIGNMENT 5 JOINING TABLES
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
--1 
-- A
SELECT sales.date , stock_items.item FROM sales JOIN stock_items ON sales.item = stock_items.id WHERE sales.item = 1014; 
-- B
SELECT sales.item, stock_items.item, stock_items.category, sales.employee FROM sales INNER JOIN stock_items ON stock_items.id = sales.item WHERE stock_items.id = 1003; 
--2
-- A
SELECT s.date, e.first_name, e.last_name, s.item FROM sales s JOIN employees e ON s.employee = e.id WHERE employee = 114; 
-- B
SELECT e.first_name, e.last_name, e.role, e.sin, s.item FROM employees e RIGHT JOIN sales s ON e.id = s.employee WHERE e.sin LIKE "258%" OR e.sin LIKE "456%" OR e.sin LIKE "758%"; 

--3
-- A
SELECT s.date, s.item, e.first_name FROM sales s JOIN employees e ON s.employee = e.id WHERE date BETWEEN "2021-06-12" AND "2021-06-18"; 
-- B
SELECT concat(employees.first_name, " ", employees.last_name), COUNT(sales.id) FROM sales JOIN employees ON sales.employee = employees.id GROUP BY employees.first_name, employees.last_name ORDER BY COUNT(sales.id) DESC; 
--4
-- A

-- B



