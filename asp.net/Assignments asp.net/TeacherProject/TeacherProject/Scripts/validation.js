window.onload = domReady;

function domReady() {

    var formHandle = document.forms.AddTeacher;

    formHandle.onsubmit = processForm;
    function processForm() {
        var fnInput = formHandle.TeacherFname;
        if (fnInput.value === "") {
            fnInput.style.background = 'red';
            fnInput.focus();
            return false;
        }
        var lnInput = formHandle.TeacherLname;
        if (lnInput.value === "") {
            lnInput.style.background = 'red';
            lnInput.focus();
            return false;
        }
        var enInput = formHandle.EmployeeNumber;
        if (enInput.value === "") {
            enInput.style.background = 'red';
            enInput.focus();
            return false;
        }
        var hdInput = formHandle.HireDate;
        if (hdInput.value === "") {
            hdInput.style.background = 'red';
            hdInput.focus();
            return false;
        }
        var sInput = formHandle.Salary;
        if (sInput.value === "") {
            sInput.style.background = 'red';
            sInput.focus();
            return false;
        }
    }
}