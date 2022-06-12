$(document).ready(function() {
    $('.wine-box').on('click', function (e) {
        var sUrl = String("http://localhost:5106/Wines/id/" + $(this).data("iddata"));
        $.ajax({
            url: sUrl,
            type: 'GET',
            success: function(res) {
                var win = window.open(sUrl, '_blank');
                if (win) {
                    //Browser has allowed it to be opened
                    win.focus();
                } else {
                    //Browser has blocked it
                    alert('Please allow popups for this website');
                }
            }
        });      
    });  


    if($('.navi-bar').length > 0){
        $(window).on("scroll load resize", function(){
          checkScroll();
        });
      };

    
    
    
});

function checkScroll(){
    var startY = $('.navi-bar').height();
    if($(window).scrollTop() > startY){
      $('.navi-bar').addClass("scrolled");
    } else{
      $('.navi-bar').removeClass("scrolled");
    };
};

function sendComment() {
    var datas = {
        WineId :  $('#WineId').val(),
        ScoreValue :  $('#ScoreValue').val(),
        Comment : $('#Comment').val(),
        Author :  $('#Author').val()
    };
   
    $.ajax({
        url: "http://localhost:5106/Wines/Comment",
        type: 'POST',
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function() {   
            // realoadPage();
        }
    });     
    
   

    //location.reload();
}

function realoadPage() {
    location.reload();  
}

function addWIne() {
    var datas = {
        Winename :  $('#Winename').val(),
        Speciename :  $('#Speciename').val(),
        Description : $('#Description').val(),
        Brand :  $('#Brand').val()
    };
   
    $.ajax({
        url: "http://localhost:5106/Wines/Add",
        type: 'POST',
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function() {   
            // location.reload();  
        }
    });      

    //location.reload();
}

function addUser() {
    var datas = {
        Email : $('#Email').val(),
        Username : $('#Username').val(),
        Password : $('#Password').val()
    };

    $.ajax({
        url: "http://localhost:5106/Users/Add",
        type: 'POST',
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function() {   
            // location.reload();  
        }
    });      

}

function LogInUser() {
    var datas = {
        Email : $('#Email').val(),
        Password : $('#Password').val()
    };

    $.ajax({
        url: "http://localhost:5106/Users/LoggedIn",
        type: 'POST',
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function() {   
            location.href("http://localhost:5106/");  
        }
    });  

}
