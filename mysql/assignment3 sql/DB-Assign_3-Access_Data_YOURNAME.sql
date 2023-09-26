--EDERES GURE (no1630082)	ASSIGNMENT 3 REFINING DATA
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
-- 1 
--A
SELECT * from employees WHERE role = "Manager" OR role = "Assistant";
--B
SELECT item, price FROM stock_items WHERE category ="piscine" AND price <=24;

-- 2
--A
SELECT item, price FROM stock_items WHERE item LIKE "%cage";
--B
SELECT first_name, role FROM employees WHERE first_name LIKE "F%";

-- 3
--A
SELECT * FROM stock_items WHERE id BETWEEN 1010 AND 1015;
--B
SELECT item, price, category FROM stock_items WHERE category ="canine" AND price BETWEEN 10 AND 20;

-- 4
--A
SELECT last_name, first_name, role FROM employees WHERE role != "Manager" ORDER BY last_name;
--B
SELECT id, item, price, inventory, category FROM stock_items WHERE category ="murine" AND inventory >=20 ORDER BY price DESC;
