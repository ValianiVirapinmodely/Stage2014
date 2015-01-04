CREATE SCHEMA student; 
GO 

CREATE TABLE student.Etudiant 
(
    IdEtudiant INT NOT NULL,
    Erasmus BIT,
    DernierDiplomeObtenu NVARCHAR(150),
    OrigineDiplome NVARCHAR(150), 
    Nationalite NVARCHAR(150),
    CONSTRAINT primaryKey PRIMARY KEY (IdEtudiant)
);
GO 