// AJAX for Course Add, Update and Delete can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddCourse() {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/CourseData/AddCourse
	//with POST data of class code, name, teacher, etc.

	var URL = "http://localhost:50664/api/CourseData/AddCourse/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var ClassCode = document.getElementById('ClassCode').value;
	var TeacherID = document.getElementById('TeacherID').value;
	var StartDate = document.getElementById('StartDate').value;
	var FinishDate = document.getElementById('FinishDate').value;
	var ClassName = document.getElementById('ClassName').value;



	var CourseData = {
		"ClassCode": ClassCode,
		"TeacherID": TeacherID,
		"StartDate": StartDate,
		"FinishDate": FinishDate,
		"ClassName": ClassName
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(CourseData));

}



function UpdateCourse(CourseID) {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/CourseData/UpdateCourse/{id}
	//with POST data of Course code, name, teacher, etc.

	var URL = "http://localhost:50664/api/CourseData/UpdateCourse/" + CourseID;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var ClassCode = document.getElementById('ClassCode').value;
	var TeacherID = document.getElementById('TeacherID').value;
	var StartDate = document.getElementById('StartDate').value;
	var FinishDate = document.getElementById('FinishDate').value;
	var ClassName = document.getElementById('ClassName').value;



	var CourseData = {
		"ClassCode": ClassCode,
		"TeacherID": TeacherID,
		"StartDate": StartDate,
		"FinishDate": FinishDate,
		"ClassName": ClassName
	};



	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(CourseData));

}


function DeleteCourse(CourseID) {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/CourseData/DeleteCourse/{id}
	//with POST data of Course code, name, teacher, etc.

	var URL = "http://localhost:50664/api/CourseData/DeleteCourse/" + CourseID;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var ClassCode = document.getElementById('ClassCode').value;
	var TeacherID = document.getElementById('TeacherID').value;
	var StartDate = document.getElementById('StartDate').value;
	var FinishDate = document.getElementById('FinishDate').value;
	var ClassName = document.getElementById('ClassName').value;



	var CourseData = {
		"ClassCode": ClassCode,
		"TeacherID": TeacherID,
		"StartDate": StartDate,
		"FinishDate": FinishDate,
		"ClassName": ClassName
	};



	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(CourseData));

}