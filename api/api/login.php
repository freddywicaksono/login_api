
<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
include_once '../../database.php';
include_once '../../users.php';
include_once '../../token/token.php';
mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
$security=getToken();
$token=$_POST['token'];
if($token==$security){

    $database = new Database();
    $db = $database->getConnection();
    $item = new Users($db);
    $item->username = isset($_POST['username']) ? $_POST['username'] : die();
    $item->password = isset($_POST['password']) ? $_POST['password'] : die();
    $item->getLogin();
    if($item->username != null && $item->password != null){

        // create array
        $data = array(
            "iduser" => $item->iduser,
            "username" => $item->username,
            "email" => $item->email,
            "password" => $item->password,
            "level" => $item->level,
            "status" => "success",
            "message" => "record found."
        );

        http_response_code(200);
        
    }else{
        
        $data['status']='empty';
        $data['message']='Data not found.';
        
    }
} else {
    $data['status']='denied';
    $data['message']='Unauthorize access.';
}
echo json_encode($data);
?>