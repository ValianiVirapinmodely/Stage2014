CREATE SCHEMA formation
GO

CREATE TABLE formation.DIPLOME
(
	IdDiplome NVARCHAR(20) PRIMARY KEY, 
    Intitule NVARCHAR(150)
);	

CREATE TABLE formation.UE
(
	IdUE NVARCHAR(20) PRIMARY KEY, 
    Intitule NVARCHAR(150),
    Descriptif NVARCHAR(150) NULL,
    Semestre integer NULL
);	

CREATE TABLE formation.MODULES
(
    IdModules NVARCHAR(20) PRIMARY KEY, 
    Intitule NVARCHAR(150), 
	ModuleChoix BIT,
    IdUE NVARCHAR(20) REFERENCES formation.UE (IdUE)
    
);

CREATE TABLE formation.MATIERE
(
	IdMatiere NVARCHAR(20) PRIMARY KEY,
	Intitule NVARCHAR(150)
);

CREATE TABLE formation.CONSTITUTION_DIPLOME
(
    IdDiplome NVARCHAR(20) REFERENCES formation.DIPLOME(IdDiplome),
    IdUE NVARCHAR(20) REFERENCES formation.UE(IdUE),
    PRIMARY KEY (IdDiplome,IdUE)
);

CREATE TABLE formation.CONSTITUTION_MODULES
(
    IdModules NVARCHAR(20) REFERENCES formation.MODULES(IdModules),
    IdMatiere NVARCHAR(20) REFERENCES formation.MATIERE(IdMatiere),
    NbCM integer,
	NbTD integer,
    NbTP integer, 
    PRIMARY KEY (IdModules,IdMatiere)
);
GO