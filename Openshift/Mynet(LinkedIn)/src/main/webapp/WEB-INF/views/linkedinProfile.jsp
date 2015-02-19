<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="sf" %>
<%@ page session="false" %>

<p>Hello, <c:out value="${personne.prenom} ${personne.nom}"/>!</p>
<img src="<c:out value="${personne.urlPhoto}"/>"/>
<br \>
<p>identifiant: <c:out value="${personne.identifiant}"/></p>
<p>email: <c:out value="${personne.email}"/></p>
<p>titre: <c:out value="${personne.titre}"/></p>
<p>industrie: <c:out value="${personne.industrie}"/></p>
<p>résumé: <c:out value="${personne.resume}"/></p>
<br \>
<p>URL publique: <c:out value="${personne.urlPublique}"/></p>
<p>URL standard (name): <c:out value="${personne.standardURLName}"/></p>
<p>URL standard (url): <c:out value="${personne.standardURLBrowser}"/></p>
<br \>
<p>Extra Languages : <c:out value="${personne.extraDataContenu}"/></p>
<br \>
<p>Skills : <c:out value="${personne.stringSkills}"/></p>
<p>Nombre de Connexions : <c:out value="${personne.connectionNumber}"/></p>
<p>Telephones : <c:out value="${personne.phones}"/></p>
<p>Education : <c:out value="${personne.education}"/></p>
<p>Name : <c:out value="${entreprise0.name}"/></p>
<p>Founded year : <c:out value="${entreprise0.foundedYear}"/></p>
<br \>
<p>Nom plage de comptage des employés : <c:out value="${entreprise0.employeeCountRangeName}"/></p>
<p>Code plage de comptage des employés : <c:out value="${entreprise0.employeeCountRangeCode}"/></p>
<br \>
<p>Industry : <c:out value="${entreprise0.industry}"/></p>
<p>Company Description : <c:out value="${entreprise0.description}"/></p>
<br \>
<p>Positions : <c:out value="${personne.positions}"/><p>