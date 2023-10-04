--YOUR NAME HERE	ASSIGNMENT 4 GROUPING RESULTS
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
-- 1 
--A
SELECT MIN(price) FROM stock_items;
--B
SELECT MAX(inventory) FROM stock_items;

-- 2
--A
SELECT COUNT(role), role FROM employees GROUP BY role;
--B
SELECT COUNT(role), COUNT(phone), role FROM employees GROUP BY role;

-- 3
--A
SELECT COUNT(item) AS "items", category AS "mammals" FROM stock_items WHERE category != "piscine" GROUP BY category;
--B
SELECT SUM(inventory) AS "in-stock", category AS "animal" FROM stock_items GROUP BY category ORDER BY SUM(inventory) ASC;
--C
SET sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''));

SELECT MAX(price), category FROM stock_items GROUP BY category ORDER BY MAX(price) DESC;
--D
SELECT MAX(price) AS "highest price", item, category FROM stock_items GROUP BY category DESC HAVING MAX(price) > 50;