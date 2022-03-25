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
