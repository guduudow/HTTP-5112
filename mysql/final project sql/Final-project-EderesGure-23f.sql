/* SQL PROJECT - EDERES GURE */

-- PART ONE: CODE FOR MAKING TABLES --

/* CODE FOR ANIMAL TABLE */
CREATE TABLE animals (
    animal_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    animal_name VARCHAR(50) NOT NULL,
    animal_species VARCHAR(50) NOT NULL,
    family VARCHAR(50) NOT NULL,
    check_status VARCHAR(50)
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;

/* CODE FOR ENTRY/EXIT TABLE */
CREATE TABLE entry_and_exit (
    port_id INT(3) PRIMARY KEY AUTO_INCREMENT,
    animal_id INT(3), FOREIGN KEY(animal_id) REFERENCES animals(animal_id),
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
    animal_id INT(3), FOREIGN KEY(animal_id) REFERENCES animals(animal_id),
    species VARCHAR(50) NOT NULL,
    donor_id INT(3), FOREIGN KEY (donor_id) REFERENCES donors(donor_id),
    amount DECIMAL(10,2),
    date_given DATE
) ENGINE= INNODB DEFAULT CHARSET=utf8 ;


-- PART TWO: CREATING PROCEDURES FOR ANIMAL, DONOR AND FUNDING TABLES --

/* CREATING A PROCEDURE THAT WILL ALLOW ME TO AUTOMATICALLY ADD ANIMALS TO THE TABLE, RATHER THAN WRITE OUT EVERYTHING ALL THE TIME */
DELIMITER //
CREATE PROCEDURE add_animal (IN p_animal_name VARCHAR(50), IN p_animal_species VARCHAR(50), IN p_family VARCHAR(50), IN p_check_status VARCHAR(50))
BEGIN
	DECLARE var_animal_id INT;
    SELECT MAX(animal_id) + 1 INTO var_animal_id FROM animals;
    INSERT INTO animals(animal_id, animal_name, animal_species, family, check_status)
    VALUES (var_animal_id, p_animal_name, p_animal_species, p_family, p_check_status);
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


-- PART THREE: CREATING TRIGGERS FOR ANIMAL AND ENTRY/EXIT TABLE --

/* CREATING A TRIGGER TO AUTO RECORD THE ENTRY DATE (WHEN ANIMAL IS ADDED TO TABLE) AND EXIT DATE */
DELIMITER //
CREATE TRIGGER animal_entry
AFTER INSERT
ON animals
FOR EACH ROW
BEGIN
	INSERT INTO entry_and_exit(entry_date, animal_id) 
    VALUES (CURRENT_DATE(), NEW.animal_id);
END; //
DELIMITER ;

/* ANOTHER TRIGGER THAT ADDS EXIT DATE UPON CHECK STATUS CHANGE */

DELIMITER //
CREATE TRIGGER add_exit_animal
AFTER INSERT
ON entry_and_exit
BEGIN
	INSERT INTO animals(exit_date)
    VALUES (CURRENT_DATE);
END ; //
DELIMITER ;

/* CHANGING ANIMAL STATUS, WILL UPDATE add_exit_animal trigger */
UPDATE animals SET check_status = "left" WHERE animal_id =INT; 



/* A TRIGGER THAT WOULD ADD EXIT DATE (CURRENT DAY) TO ENTRY/EXIT TABLE WHEN ANIMAL STATUS IS CHANGED */
DELIMITER //
CREATE TRIGGER animal_exit
AFTER UPDATE
ON animals
FOR EACH ROW
    BEGIN
        IF NEW.check_status = "left"
        THEN
            UPDATE entry_and_exit
            SET exit_date = CURRENT_DATE
            WHERE animal_id = NEW.animal_id;
        END IF;
    END ; //
DELIMITER ;



-- PART FOUR: VIEW FOR SEEING TOTAL DONATIONS FOR EACH SPECIES --

/* VIEW FOR FUNDING TABLE TO SHOW SPECIES BY TOTAL FUNDING SORTED BY MOST TO LEAST */
CREATE VIEW species_donation_summary AS
SELECT
    a.family,
    SUM(f.amount) AS total_donation
FROM
    animals a
JOIN
    funding f ON a.animal_id = f.animal_id
GROUP BY
    a.family
ORDER BY
    total_donation DESC;