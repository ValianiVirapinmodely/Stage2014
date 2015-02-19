<%@ page session="false" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
Success!
<hr/>
<a href="javascript:history.back(1)">Go back</a>
<a href="<c:url value="/signout" />">Sign Out</a>