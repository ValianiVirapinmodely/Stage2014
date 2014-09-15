
use federation mynetfederation(contactlanguageid=0) with reset, filtering=off
go


---------------------------------------------
--  Drop Schema
---------------------------------------------


IF OBJECT_ID(N'mynet.speaksLanguage', N'U') IS NOT NULL 
	DROP TABLE mynet.speaksLanguage

IF OBJECT_ID(N'mynet.Language', N'U') IS NOT NULL 
	DROP TABLE mynet.Language

IF OBJECT_ID(N'mynet.hasEmail', N'U') IS NOT NULL 
	DROP TABLE mynet.hasEmail	

IF OBJECT_ID(N'mynet.Email', N'U') IS NOT NULL 
	DROP TABLE mynet.Email

IF OBJECT_ID(N'mynet.hasAddress', N'U') IS NOT NULL 
	DROP TABLE mynet.hasAddress

IF OBJECT_ID(N'mynet.Address', N'U') IS NOT NULL 
	DROP TABLE mynet.Address

IF OBJECT_ID(N'mynet.SocialNetworkBasicInfo', N'U') IS NOT NULL 
	DROP TABLE mynet.SocialNetworkBasicInfo
	
IF OBJECT_ID(N'mynet.Post', N'U') IS NOT NULL 
	DROP TABLE mynet.Post		

IF OBJECT_ID(N'mynet.Content', N'U') IS NOT NULL 
	DROP TABLE mynet.Content	
	
IF OBJECT_ID(N'mynet.IsContactOf', N'U') IS NOT NULL 
	DROP TABLE mynet.IsContactOf	
	
IF OBJECT_ID(N'mynet.Contact', N'U') IS NOT NULL 
	DROP TABLE mynet.Contact
	
IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'mynet')
	DROP SCHEMA mynet;
GO
	
	
---------------------------------------------
--	Create MyNet Schema	
---------------------------------------------	


CREATE SCHEMA mynet

CREATE TABLE mynet.Contact (
    ID	      INT PRIMARY KEY,
    Lastname  NVARCHAR(MAX) NOT NULL,
    Firstname NVARCHAR(MAX) NOT NULL,
    Society   NVARCHAR (MAX) NULL
)

CREATE TABLE mynet.SocialNetworkBasicInfo ( 
	SocialNetworkID		NVARCHAR(100) PRIMARY KEY,
	WebSite				NVARCHAR(MAX) NULL,
	ContactID			INT NOT NULL,
	FOREIGN KEY (ContactID) REFERENCES mynet.Contact(ID) ON DELETE CASCADE
)

CREATE TABLE mynet.Address (
	ID		INT PRIMARY KEY,
	Street	NVARCHAR(MAX) NULL,
	Number	INT NULL,
	City	NVARCHAR(MAX) NULL,
	ZipCode	INT NULL,
)

CREATE TABLE mynet.hasAddress ( 
    AddressID		INT NOT NULL,
    SocialNetworkID NVARCHAR(100) NOT NULL,
    FOREIGN KEY (AddressID) REFERENCES mynet.Address(ID) ON DELETE CASCADE, 
    FOREIGN KEY (SocialNetworkID) REFERENCES mynet.SocialNetworkBasicInfo(SocialNetworkID) ON DELETE CASCADE,
    PRIMARY KEY ( AddressID, SocialNetworkID )
) 

CREATE TABLE mynet.Email (
	EmailAddress	NVARCHAR(100) PRIMARY KEY,
	EmailType		CHAR(12) CHECK( EmailType in ('Personal', 'Profesional'))
)

CREATE TABLE mynet.hasEmail (
	EmailAddress	NVARCHAR(100) NOT NULL,
	SocialNetworkID NVARCHAR(100) NOT NULL,
    FOREIGN KEY (EmailAddress) REFERENCES mynet.Email(EmailAddress) ON DELETE CASCADE, 
    FOREIGN KEY (SocialNetworkID) REFERENCES mynet.SocialNetworkBasicInfo(SocialNetworkID) ON DELETE CASCADE, 
    PRIMARY KEY ( EmailAddress, SocialNetworkID )
)


CREATE TABLE mynet.Language( 
    LanguageID	 INT PRIMARY KEY, 
    LanguageName NVARCHAR(MAX) NOT NULL
) FEDERATED ON( ContactLanguageID = LanguageID )


CREATE TABLE mynet.speaksLanguage(
    LanguageID				  INT NOT NULL,
    ContactSocialNetworkID	  NVARCHAR(100) NOT NULL,
    SocialNetworkLanguageID   BIGINT NOT NULL,
    SocialNetworkLanguageName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (LanguageID) REFERENCES mynet.Language(LanguageID) ON DELETE CASCADE,
    FOREIGN KEY (ContactSocialNetworkID) REFERENCES mynet.SocialNetworkBasicInfo(SocialNetworkID) ON DELETE CASCADE,
    PRIMARY KEY (LanguageID, ContactSocialNetworkID, SocialNetworkLanguageID )
) FEDERATED ON( ContactLanguageID = LanguageID )
  

CREATE TABLE mynet.IsContactOf ( 
	ContactID_A	INT FOREIGN KEY REFERENCES mynet.Contact(ID),
	ContactID_B	INT FOREIGN KEY REFERENCES mynet.Contact(ID),
	PRIMARY KEY(ContactID_A, ContactID_B)
)

CREATE TABLE mynet.Content ( 
	ID		INT PRIMARY KEY,
	Text	NVARCHAR(MAX) NULL,
)

CREATE TABLE mynet.Post ( 
	ID			INT PRIMARY KEY,
	Timestamp	DATE NULL,
	GeoStamp	NVARCHAR(MAX) NULL,
	ContactID	INT FOREIGN KEY REFERENCES mynet.Contact(ID),
	ContentID	INT FOREIGN KEY REFERENCES mynet.Content(ID)
)
GO

INSERT INTO mynet.Language ( LanguageID, LanguageName ) 
VALUES (1, 'English'), (2, 'French'), (3, 'Spanish'), (4, 'Other') 
                                 