// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetAJAX(url, httptype, onsuccess, onfail) {
    $.ajax({
        url: url,
        type: httptype,
        dataType: "JSON",
        success: function (data) { onsuccess(data) },
        error: function (data) { onfail(data) }
    })
}

function PostAJAX(url, httptype, onsuccess, onfail,data) {
    $.ajax({
        url: url,
        type: httptype,
        dataType: "JSON",
        success: function (data) { onsuccess(data) },
        error: function (data) { onfail(data) },
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8"
    })
}

function FillSelectizeContent(data) {
    console.log(data);
    for (var i = 0; i < data.length; i++) {
        $selectLocs[0].selectize.addOption(data[i]);
        console.log(data[i])
    }
}
function FailEmpty(data) {
    console.log("error running ajax method")
}
function Log1() {
    console.log("hellp from attached js file")
}
/*$(document).ready(function () {
    $("#locbutton").on("click", function () {
        postAJAX();
    });
    $("#removelocbutton").on("click", function () {
        cleanAJAX();
    });
    $selectLocs = $('#locations').selectize({
        valueField: 'id',
        labelField: 'name',
        searchField: ['name'],
        sortField: 'name',
        onInitialize: function () {
            loadLocationsAJAX();
            postAJAX('Admin/Desk2/AjaxGetDesks', 'GET', fillSelectizeContent, failEmpty);
            console.log("hello from selectize")
        },
        render: {
            option: function (item, escape) {
                console.log("i am in selectize render function")
                return '<div class="selectDiv">' +
                    '<span class="mainName">' + escape(item.name) + '</span > <br />' +
                    '</div>';
            }
        }
    })

    console.log("ready!");
    $("#jquerytestitem").html("hello from js");

});


function postAJAX() {
    var deskdto = {}
    deskdto.deskname = $("#desknameinput").val();
    deskdto.deskid = $("#deskidinput").val();
    deskdto.locationid = $selectLocs[0].selectize.getValue();

    console.log(deskdto);
    //return 
    $.ajax({
        url: '/Admin/Desk/AjaxUpdate',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deskdto)

    })
}
function cleanAJAX() {
    var deskdto = {}
    deskdto.deskname = $("#desknameinput").val();
    deskdto.deskid = $("#deskidinput").val();
    deskdto.locationid = null;

    console.log(deskdto);
    //return
    $.ajax({
        url: '/Admin/Desk/AjaxUpdate',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deskdto)

    })
}


function loadLocationsAJAX() {
    $.ajax({
        url: '/Admin/Desk/AjaxGetLocations',
        type: 'GET',
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                $selectLocs[0].selectize.addOption(data[i]);
                console.log(data[i])
            }


        },
        error: function (response) {
            alert('Błąd: ' + response);
        }
    })

}

*/