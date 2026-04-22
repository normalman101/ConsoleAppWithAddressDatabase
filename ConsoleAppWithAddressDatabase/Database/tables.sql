CREATE TABLE table_persons (
                                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                   Name TEXT NOT NULL,
                                   IsDeleted INTEGER NOT NULL DEFAULT 0,
                                   Type INTEGER NOT NULL CHECK ( Type == 0 or Type == 1)
);

CREATE TABLE table_addresses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Region TEXT NOT NULL,
    Locality TEXT NOT NULL,
    PlanningElement TEXT NOT NULL,
    Street TEXT NOT NULL,
    Building TEXT NOT NULL,
    Room TEXT NOT NULL,
    IsDeleted INTEGER NOT NULL DEFAULT 0,
    PersonId INTEGER NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES table_persons (Id)
);