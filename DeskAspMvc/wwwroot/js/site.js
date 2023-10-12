// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetAJAX(url, httptype,datatype, onsuccess, onfail) {
    $.ajax({
        url: url,
        type: httptype,
        dataType: datatype,
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
