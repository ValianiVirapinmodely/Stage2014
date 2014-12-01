CREATE SCHEMA student; 
GO 

CREATE TABLE student.Etudiant 
(
    IdEtudiant integer PRIMARY KEY,
    Erasmus BIT,
    DernierDiplomeObtenu NVARCHAR(150),
    OrigineDiplome NVARCHAR(150), 
    Nationalite NVARCHAR(150)
);
GO 