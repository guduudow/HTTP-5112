--EDERES GURE	ASSIGNMENT 9 VIEWS AND TRIGGERS
--Put your answers on the lines after each letter. E.g. your query for question 1A should go on line 5; your query for question 1B should go on line 7...
-- 1 
--A
CREATE VIEW low_stock AS SELECT item, category, inventory FROM stock_items WHERE inventory <= 20;
--B
CREATE VIEW employees_that_sell AS
SELECT
    concat(e.first_name, " ", e.last_name) AS employee_name, COUNT(s.employee) AS total_sales
FROM
    employees e
LEFT JOIN
    sales s ON e.id = s.employee
GROUP BY
    e.id, employee_name
ORDER BY
    total_sales DESC;

-- 2
--TABLE CREATION
CREATE TABLE data_log (
    logid INT(3) NOT NULL AUTO_INCREMENT PRIMARY KEY,
    time_stamp TIMESTAMP,
    actions VARCHAR(50),
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    pay_per_hour DECIMAL(10,2))
    ENGINE=INNODB DEFAULT CHARSET=utf8
--A (AFTER UPDATE)
DELIMITER $$
CREATE TRIGGER employee_pay_per_hour_updates
AFTER UPDATE ON employees
FOR EACH ROW
BEGIN
  INSERT INTO data_log(time_stamp, actions, first_name, last_name, pay_per_hour)
  VALUES (CURRENT_TIMESTAMP(), 'Update', OLD.first_name, OLD.last_name, NEW.pay_per_hour);
END $$
DELIMITER ;
--B (AFTER DELETE)
DELIMITER $$
CREATE TRIGGER data_log_audit
AFTER DELETE ON employees
FOR EACH ROW
BEGIN
    INSERT INTO data_log(time_stamp, actions, first_name, last_name, pay_per_hour)
    VALUES (CURRENT_TIMESTAMP(), 'Removal', OLD.first_name, OLD.last_name, OLD.pay_per_hour);
END $$
DELIMITER ;


