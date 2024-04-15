


var tasksTableId = "tasksTable";
var editedRowId;
var pickedRowData;
var tasksDataTable = $(`#${tasksTableId}`).DataTable({

});
var modal = document.getElementById("editRowData");
var modalEditButton = document.getElementById("editUserTask");
var modalAddButton = document.getElementById("addUserTask");
var tabDat;



function init() {
    

    $(".getUserTasks").on("click", function () {
        GetUserTasksForDate();
    });
    $(".openModalAddUser").on("click", function () {
        pickedRowData = null;
        OpenModalEmpty();
        modalEditButton.style.display = "none";
        modalAddButton.style.display = "";
        
    });

    $("#addUserTask").on("click", function () {
        AddUserTask();
    })

    $("#tasksTable").on("click", "tr .openEditRowDataModal", function (element) {
        pickedRowData = GetRowData(element);
        OpenModal(pickedRowData);
        modalEditButton.style.display = "";
        modalAddButton.style.display = "none";
    });

    $("#tasksTable").on("click", "tr .deleteUserTask", function (element) {
        pickedRowData = GetRowData(element);
        DeleteUserTask(pickedRowData.userTaskId);
    });

    $("#closeEditModal").on("click", function () {
        modal.style.display = "none";
    })

    $(".editUserTask").on("click", function () {
        EditUserTask();
    })

    function GetUserTasksForDate() {
        var passedDate = document.getElementById("getDate").value;
        

        if (passedDate == null) {
            
        }
        $.ajax({
            url: `ToDoApp/tasks/${passedDate}`,
            type: "POST",
            data: { passedDate: passedDate },
            async: false,
            success: function (data) {
                tabDat = data;
            },
            error: function (request, message, error) {
                HandleException(request, message, error);
                alert("Pick correct date!");
            }
        });

        tasksDataTable = $(`#${tasksTableId}`).DataTable({
            bDestroy: true,
            data: tabDat,
            columns: [
                { data: 'name' }
                , { data: 'description' }
                , { data: 'date' }
                , {
                    data: null,
                    render: function () {
                        return `<button type='button' class='btn btn-primary openEditRowDataModal'>Edit</button>`
                    }
                }
                , {
                    data: null,
                    render: function () {
                        return `<button type='button' class='btn btn-primary deleteUserTask'>Delete</button>`
                    }
                }
            ]

        });

    }

    function AddUserTask() {
        var rowDataName = document.getElementById("editRowName").value;
        var rowDataDescription = document.getElementById("editRowDescription").value;
        var rowDataDate = document.getElementById("editRowDate").value;

        var userTaskViewModel = new Object();
        userTaskViewModel.UserTaskId = null;
        userTaskViewModel.Name = rowDataName;
        userTaskViewModel.Description = rowDataDescription;
        userTaskViewModel.Date = rowDataDate;

        $.ajax({
            url: "ToDoApp/tasks",
            type: "POST",
            data: userTaskViewModel,
            async: false,
            success: function (data) {
                GetUserTasksForDate();
                modal.style.display = "none";
            },
            error: function (request, message, error) {
                HandleException(request, message, error);
            }
        });
    }
    
    

    function EditUserTask() {
        var rowDataName = document.getElementById("editRowName").value;
        var rowDataDescription = document.getElementById("editRowDescription").value;
        var rowDataDate = document.getElementById("editRowDate").value;

        var userTaskViewModel = new Object();
        userTaskViewModel.UserTaskId = pickedRowData.userTaskId;
        userTaskViewModel.Name = rowDataName;
        userTaskViewModel.Description = rowDataDescription;
        userTaskViewModel.Date = rowDataDate;



        $.ajax({
            url: "ToDoApp/tasks",
            type: "PATCH",
            async: false,
            data: userTaskViewModel,
            success: function (data) {
                editedRowId = null;
                modal.style.display = "none";
                GetUserTasksForDate();
            },
            error: function (request, message, error) { HandleException(request, message, error); }
        });
    }

    function DeleteUserTask(userTaskId) {
        $.ajax({
            url: `ToDoApp/tasks/${userTaskId}`,
            type: "DELETE",
            async: false,
            success: function (data) {
                GetUserTasksForDate();
            },
            error: function (request, message, error) {  HandleException(request, message, error); }
        });
    }

    function GetRowData(element) {
        var rowData = tasksDataTable.row(element.target.closest('tr')).data();
        return rowData;
    }
    function OpenModal(rowData) {
        modal.style.display = "block";

        document.getElementById("editRowName").value = rowData.name;
        document.getElementById("editRowDescription").value = rowData.description;
        document.getElementById("editRowDate").value = rowData.date;
    }
    function OpenModalEmpty() {
        document.getElementById("editRowName").value = null;
        document.getElementById("editRowDescription").value = null;
        document.getElementById("editRowDate").value = null;
        modal.style.display = "block";
        
    }

    function HandleException(request, message, error) {
        var message = "";
        message += "Status: " + request.status + "\n";
        message += "Status text: " + request.statusText + "\n";
        if (request.responseJSON != null) {
            message += "Message: " + request.responseJSON + "\n";
        }
        message += "Error: " + error;
        alert(message);
    }
}



init();

