CREATE SCHEMA inscription; 

CREATE TABLE SuitMatiere
( 
    Annee date,
    IdEtudiant integer REFERENCES Etudiant(IdEtudiant),
    IdMatiere integer REFERENCES Matiere(IdMatiere),
    constraint primaryKey PRIMARY KEY (IdEtudiant,IdMatiere),

);