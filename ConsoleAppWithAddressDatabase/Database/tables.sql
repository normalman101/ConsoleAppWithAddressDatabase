CREATE TABLE table_addresses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Region TEXT NOT NULL,
    Locality TEXT NOT NULL,
    PlanningElement TEXT NOT NULL,
    Street TEXT NOT NULL,
    Building TEXT NOT NULL,
    Room TEXT NOT NULL,
    IsDeleted INTEGER NOT NULL DEFAULT 0,
    IndividualId INTEGER NOT NULL,
    FOREIGN KEY (IndividualId) REFERENCES table_individuals (Id)
);

CREATE TABLE table_individuals (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    TypeId INTEGER NOT NULL,
    FOREIGN KEY (TypeId) REFERENCES table_individual_types (Id)
);

CREATE TABLE table_individual_types (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Type TEXT NOT NULL 
);

INSERT INTO table_individual_types (Type)
VALUES ('Legal'),
       ('Individual')