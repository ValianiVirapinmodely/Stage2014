CREATE SCHEMA formation
GO

CREATE TABLE formation.DIPLOME
(
	IdDiplome NVARCHAR(20) PRIMARY KEY, 
    Intitule NVARCHAR(150)
);	

CREATE TABLE formation.UE
(
	IdUE integer PRIMARY KEY, 
    Intitule NVARCHAR(150),
    Descriptif NVARCHAR(150) NULL,
    Semestre integer NULL
);	

CREATE TABLE formation.MODULE
(
    IdModule integer PRIMARY KEY, 
    Intitule NVARCHAR(150), 
	ModuleChoix BIT,
    IdUE integer REFERENCES formation.UE (IdUE)
    
);

CREATE TABLE formation.MATIERE
(
	IdMatiere integer PRIMARY KEY,
	Intitule NVARCHAR(150)
);

CREATE TABLE formation.CONSTITUTION_DIPLOME
(
    IdDiplome NVARCHAR(20) REFERENCES formation.DIPLOME(IdDiplome),
    IdUE integer REFERENCES formation.UE(IdUE),
    PRIMARY KEY (IdDiplome,IdUE)
);

CREATE TABLE formation.CONSTITUTION_MODULE
(
    IdModule integer REFERENCES formation.MODULE(IdModule),
    IdMatiere integer REFERENCES formation.MATIERE(IdMatiere),
    NbCM integer,
	NbTD integer,
    NbTP integer, 
    PRIMARY KEY (IdModule,IdMatiere)
);
GO
