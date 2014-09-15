
USE FEDERATION MyNetFederation(ContactLanguageID=0) WITH RESET, FILTERING=OFF
GO

---
-- Get John Doe EMails 
---
SELECT Email.EmailAddress, Email.EmailType

FROM   mynet.SocialNetworkBasicInfo AS basicInfo,
       mynet.hasEmail AS hasEmail,
       mynet.Email AS Email

WHERE  basicInfo.SocialNetworkID = 'johndoe' AND
       basicInfo.SocialNetworkID = hasEmail.SocialNetworkID AND
       hasEmail.EmailAddress = Email.EmailAddress


---
-- Get John Doe Addresses 
---
SELECT address.ID, address.Street, address.Number, address.ZipCode

FROM   mynet.SocialNetworkBasicInfo AS basicInfo,
       mynet.hasAddress AS hasAddress,
       mynet.Address AS address

WHERE  basicInfo.SocialNetworkID = 'johndoe' AND
       basicInfo.SocialNetworkID = hasAddress.SocialNetworkID AND
       hasAddress.AddressID = address.ID


---
-- Get John Doe Languages
---
SELECT speaks.SocialNetworkLanguageID, speaks.SocialNetworkLanguageName

FROM   mynet.SocialNetworkBasicInfo AS basicInfo,
       mynet.speaksLanguage AS speaks

WHERE  basicInfo.SocialNetworkID = 'johndoe' AND
       basicInfo.SocialNetworkID = speaks.ContactSocialNetworkID


---
-- Get John Doe Contacts 
-- Assumme John Doe ID is 9
---
SELECT friend.ID, friend.Firstname, friend.Lastname, friend.Society, basicInfo.WebSite, basicInfo.SocialNetworkID

FROM   mynet.Contact contact, 
       mynet.Contact friend, 
       mynet.IsContactOf isContactOf,
       mynet.SocialNetworkBasicInfo basicInfo       

WHERE  contact.ID = 9 AND
       contact.ID = isContactOf.ContactID_A AND 
       friend.ID  = isContactOf.ContactID_B AND 
       friend.ID  = basicInfo.ContactID


---
-- Get John Doe Information 
-- Assume John Doe ID is 9
---
SELECT contact.ID, contact.Firstname, contact.Lastname, contact.Society, basicInfo.WebSite, basicInfo.SocialNetworkID

FROM   mynet.Contact contact, 
       mynet.SocialNetworkBasicInfo basicInfo       

WHERE  contact.ID = basicInfo.ContactID AND
       contact.ID = 9
