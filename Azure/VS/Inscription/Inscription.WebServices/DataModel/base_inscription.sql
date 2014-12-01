CREATE SCHEMA inscription; 
GO

CREATE TABLE inscription.SuitMatiere
( 
    Annee NVARCHAR(4),
    IdEtudiant INT NOT NULL,
    IdMatiere INT NOT NULL,
    CONSTRAINT primaryKey PRIMARY KEY (IdEtudiant,IdMatiere)
);
GO