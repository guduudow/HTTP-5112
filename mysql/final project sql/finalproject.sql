/* SQL PROJECT - EDERES GURE */

-- PART ONE: CODE FOR MAKING TABLES --

/* CODE FOR ANIMAL TABLE */
CREATE TABLE animals (
    animal_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    animal_name VARCHAR(50) NOT NULL,
    animal_type VARCHAR(50) NOT NULL,
    species VARCHAR(50) NOT NULL,
    entry_date DATE,
    exit_date DATE
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;

/* CODE FOR ENTRY/EXIT TABLE */
CREATE TABLE entry_and_exit (
    port_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    animal_id VARCHAR(50) REFERENCES animals(animal_id),
    entry_date DATE,
    exit_date DATE
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;

/* CODE FOR DONOR TABLE */
CREATE TABLE donors (
    donor_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;

/* CODE FOR FUNDING TABLE */
CREATE TABLE funding (
    funding_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    animal_id VARCHAR(50) REFERENCES animals(animal_id),
    species VARCHAR(50) NOT NULL,
    donor_id INT(3) REFERENCES donors(donor_id),
    amount DECIMAL(10,2),
    date_given DATE
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;


-- PART TWO: CREATING PROCEDURES FOR ANIMAL, DONOR AND FUNDING TABLES --

/* CREATING A PROCEDURE THAT WILL ALLOW ME TO AUTOMATICALLY ADD ANIMALS TO THE TABLE, RATHER THAN WRITE OUT EVERYTHING ALL THE TIME */
DELIMITER //
CREATE PROCEDURE add_animal (IN p_animal_name VARCHAR(50), IN p_animal_type VARCHAR(50), IN p_species VARCHAR(50), IN p_entry_date DATE, IN p_exit_date DATE)
BEGIN
	DECLARE var_animal_id INT;
    SELECT MAX(animal_id) + 1 INTO var_animal_id FROM animals;
    INSERT INTO animals(animal_id, animal_name, animal_type, species, entry_date, exit_date)
    VALUES (var_animal_id, p_animal_name, p_animal_type, p_species, p_entry_date, p_exit_date);
END; //
DELIMITER ;

/*PROCEDURE FOR DONOR TABLE */
DELIMITER //
CREATE PROCEDURE add_donor (IN p_first_name VARCHAR(50), IN p_last_name VARCHAR(50))
BEGIN
	DECLARE var_donor_id INT;
    SELECT MAX(donor_id) + 1 INTO var_donor_id FROM donors;
    INSERT INTO donors(donor_id, first_name, last_name)
    VALUES (var_donor_id, p_first_name, p_last_name);
END; //
DELIMITER ;

/*PROCEDURE FOR FUNDING TABLE*/
DELIMITER //
CREATE PROCEDURE add_funding (IN p_animal_id INT(3), IN p_species VARCHAR(50), IN p_donor_id INT(3), IN p_amount DECIMAL(10,2), IN p_date_given DATE)
BEGIN
	DECLARE var_funding_id INT;
    SELECT MAX(funding_id) + 1 INTO var_funding_id FROM funding;
    INSERT INTO funding(funding_id, animal_id, species, donor_id, amount, date_given)
    VALUES (var_funding_id, p_animal_id, p_species, p_donor_id, p_amount, p_date_given);
END; //
DELIMITER ;


-- PART THREE: CREATING TRIGGER FOR ANIMAL AND ENTRY/EXIT TABLE --

/* CREATING A TRIGGER TO AUTO RECORD THE ENTRY DATE (WHEN ANIMAL IS ADDED TO TABLE) AND EXIT DATE */
DELIMITER //
CREATE TRIGGER animal_entry
AFTER INSERT
ON animals
FOR EACH ROW
BEGIN
	INSERT INTO entry_and_exit(entry_date, animal_id) --TRIGGER ISSUE IS ANIMAL ID NOT SHOWING ON TABLE--
    VALUES (CURRENT_DATE(), animal_id);
END; //
DELIMITER ;

/*A TRIGGER THAT WOULD UPDATE ANIMAL TABLE TO RECORD ENTRY DATE AUTOMATICALLY*/

/* A TRIGGER THAT WOULD ADD EXIT DATE (CURRENT DAY) TO ENTRY/EXIT TABLE WHEN ANIMAL IS DROPPED */


-- PART FOUR: VIEW FOR SEEING TOTAL DONATIONS FOR EACH SPECIES --

/* VIEW FOR FUNDING TABLE TO SHOW SPECIES BY TOTAL FUNDING SORTED BY MOST TO LEAST */
-- SET sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''));

CREATE VIEW total_funding_species 
AS SELECT species, amount FROM funding GROUP BY species ORDER BY amount DESC; -- GETTING SQL ERROR CODE #1055