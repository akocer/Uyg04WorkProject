var token = localStorage.getItem("token")
var userRoles = [];
var apiUrl = "https://localhost:7096/";
var userId=";"

if (token == null) {

    $(".NotLogined").show();
    $(".Logined").hide();

} else {
    $(".NotLogined").hide();
    $(".Logined").show();
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    var userPicUrl = apiUrl + "Profile/" + payload["userPicUrl"];
    userId = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    $("#userPic").attr("src", userPicUrl);
    $("#UserName").html(username);

    //console.log(userRoles);
}



function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}
$("#Logout").click(function () {
    localStorage.removeItem("token");
    location.href = "/Home/Login";
});