<%@ page session="false" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<html>
	<head>
		<title>Home</title>
	</head>
	<body>

	Hi <c:out value="${user.name}"/>, please provide the following information:<br/>
	
	<hr/>
	<h3>Load Groups and Contacts</h3>
	<form action="loadContacts">
	    MyNetContacts URI: <input type="text" name="myNetContactsUri" size="60"/> <input type="submit" value="Submit">
	    
	</form>
	<hr/>
	
	<h3>Load Contacts Information</h3>
	<form action="loadBasicInfo">
	    MyNet URI: <input type="text" name="myNetUri" size="60"/> <input type="submit" value="Submit">
	    
	</form>
	<hr/>
	
	<h3>Load Posts</h3>
	<form action="loadPosts">
	    MyNet URI: <input type="text" name="myNetUri" size="60"/> <input type="submit" value="Submit">
	</form>
	<hr/>
	
	<a href="<c:url value="/signout" />">Sign Out</a>
	
	</body>
</html>