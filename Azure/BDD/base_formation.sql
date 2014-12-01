CREATE SCHEMA formation
GO

CREATE TABLE formation.DIPLOME
(
	IdDiplome NVARCHAR(20) PRIMARY KEY, 
    Intitule NVARCHAR(150) NOT NULL
);	

CREATE TABLE formation.UE
(
	IdUE integer PRIMARY KEY, 
    Intitule NVARCHAR(150) NOT NULL,
    Descriptif NVARCHAR(150) NULL,
    Semestre integer NULL
);	

CREATE TABLE formation.MODULE
(
    IdModule integer PRIMARY KEY, 
    Intitule NVARCHAR(150) NOT NULL, 
	ModuleChoix BIT NOT NULL,
    IdUE integer REFERENCES formation.UE (IdUE) ON DELETE CASCADE
    
);

CREATE TABLE formation.MATIERE
(
	IdMatiere integer PRIMARY KEY,
	Intitule NVARCHAR(150) NOT NULL,
    NbCM integer NULL,
	NbTD integer NULL,
    NbTP integer NULL,
    IdModule integer REFERENCES formation.MODULE(IdModule) ON DELETE CASCADE
);

CREATE TABLE formation.CONSTITUTION_DIPLOME
(
    IdDiplome NVARCHAR(20) REFERENCES formation.DIPLOME(IdDiplome) ON DELETE CASCADE,
    IdUE integer REFERENCES formation.UE(IdUE) ON DELETE CASCADE,
    PRIMARY KEY (IdDiplome,IdUE)
);
GO
