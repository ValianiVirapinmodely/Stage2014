

<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>
<%@ taglib uri="http://www.springframework.org/tags/form" prefix="sf" %>
<%@ page session="false" %>

<h3>Connect to LinkedIn</h3>

<form action="<c:url value="/signin/linkedin" />" method="POST">
	<p><button type="submit">Connect with LinkedIn</button></p>
</form>

