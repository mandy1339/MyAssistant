function showDueDatePickerCalendar()
{
    document.getElementById('CalendarDueDatePicker').hidden = false;
}

function toggleShowTodoForm()
{
    var div = document.getElementById('AddTodoForm');
    var but = document.getElementById('ButtonShowTodoForm');
    if (but.innerText == 'Add New | +') {
        div.classList.remove('Hidden');
        but.innerText = 'Add New | -';
    }
    else {
        div.classList.add('Hidden');
        but.innerText = 'Add New | +';
    }
    //document.getElementById('ButtonShowTodoForm').innerHTML = 'Add New | +';
    //document.getElementById('AddTodoForm').classList.remove('Hidden');
}



// Script to open and close sidebar
function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}


function datePickerInputJSHandler(element) {
    document.getElementById("HiddenField_DueDate").value = element.value;
}