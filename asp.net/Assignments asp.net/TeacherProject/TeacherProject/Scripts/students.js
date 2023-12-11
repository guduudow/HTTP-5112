// AJAX for student Add, Delete and Update can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddStudent() {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/StudentData/AddStudent
	//with POST data of studentname, number, etc.

	var URL = "http://localhost:50664/api/StudentData/AddStudent/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;
	var EnrolDate = document.getElementById('EnrolDate').value;



	var StudentData = {
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber,
		"EnrolDate": EnrolDate
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
	rq.send(JSON.stringify(StudentData));

}



function UpdateStudent(StudentID) {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/StudentData/UpdateStudent/{id}
	//with POST data of Studentname, number, etc.

	var URL = "http://localhost:50664/api/StudentData/UpdateStudent/" + StudentID;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;
	var EnrolDate = document.getElementById('EnrolDate').value;



	var StudentData = {
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber,
		"EnrolDate": EnrolDate
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
	rq.send(JSON.stringify(StudentData));

}



function DeleteStudent(StudentID) {

	//goal: send a request which looks like this:
	//POST : http://localhost:50664/api/StudentData/DeleteStudent/{id}
	//with POST data of Studentname, number, etc.

	var URL = "http://localhost:50664/api/StudentData/DeleteStudent/" + StudentID;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;
	var EnrolDate = document.getElementById('EnrolDate').value;



	var StudentData = {
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber,
		"EnrolDate": EnrolDate
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
	rq.send(JSON.stringify(StudentData));

}