
<?php
class Users{
    // db object
    private $db;
    // Table
    private $db_table = "users";
    // Columns
    public $iduser;
    public $username;
    public $email;
    public $password;
    public $level;
    
    // Db dbaction
    public function __construct($db){
        $this->db = $db;
    }

    // GET ALL
    public function getUsers(){
        $sqlQuery = "SELECT iduser,username,email,password,level FROM " . $this->db_table . "";
        $this->result = $this->db->query($sqlQuery);
        return $this->result;
    }
    // CREATE
    public function createUsers(){
        // sanitize
            $this->username=htmlspecialchars(strip_tags($this->username));
            $this->email=htmlspecialchars(strip_tags($this->email));
            $this->password=htmlspecialchars(strip_tags($this->password));
            $this->level=htmlspecialchars(strip_tags($this->level));
            
        $sqlQuery = "INSERT INTO ". $this->db_table ." SET username = '".$this->username."',email = '".$this->email."',password = '".$this->password."',level= '".$this->level."'";
        $this->db->query($sqlQuery);
        if($this->db->affected_rows > 0){
            return true;
        } else {
            return false;
        }
        
    }
    

    // FIND BY ID
    public function getSingleUsers(){
        $sqlQuery = "SELECT iduser,username,email,password,level FROM ". $this->db_table ." WHERE iduser ='".$this->iduser."'";
        $record = $this->db->query($sqlQuery);
        $dataRow=$record->fetch_assoc();
        $this->iduser = $dataRow['iduser'];
        $this->username = $dataRow['username'];
        $this->email = $dataRow['email'];
        $this->password = $dataRow['password'];
        $this->level = $dataRow['level'];
        
    }


    // FIND BY UNIQUE KEY
    public function getUsername(){
        $sqlQuery = "SELECT iduser,username,email,password,level FROM ". $this->db_table ." WHERE username ='".$this->username."'";
        $record = $this->db->query($sqlQuery);
        $dataRow=$record->fetch_assoc();
        $this->iduser = $dataRow['iduser'];
        $this->username = $dataRow['username'];
        $this->email = $dataRow['email'];
        $this->password = $dataRow['password'];
        $this->level = $dataRow['level'];
        
    }

    // UPDATE
    public function updateUsers(){
        $this->username=htmlspecialchars(strip_tags($this->username));
        $this->email=htmlspecialchars(strip_tags($this->email));
        $this->password=htmlspecialchars(strip_tags($this->password));
        $this->level=htmlspecialchars(strip_tags($this->level));
        $sqlQuery = "UPDATE ". $this->db_table ." SET username = '".$this->username."', email = '".$this->email."', password = '".$this->password."', level = '".$this->level."' WHERE iduser = ".$this->iduser;

        $this->db->query($sqlQuery);
        if($this->db->affected_rows > 0){
            return true;
        }
        return false;
    }

    // DELETE
    public function deleteUsers(){
        $sqlQuery = "DELETE FROM " . $this->db_table . " WHERE iduser = ".$this->iduser;
        $this->db->query($sqlQuery);
        if($this->db->affected_rows > 0){
            return true;
        }
        return false;
    }

    //Login
    public function getLogin(){
        $pwd=MD5($this->password);
        $sqlQuery = "SELECT `iduser`,`username`,`email`,`password`,`level` FROM ". $this->db_table ." WHERE `username` = '".$this->username."' AND `password` = '".$pwd."'";
        $record = $this->db->query($sqlQuery);
        $dataRow=$record->fetch_assoc();
        $this->iduser = $dataRow['iduser'];
        $this->username = $dataRow['username'];
        $this->email = $dataRow['email'];
        $this->password = $dataRow['password'];
        $this->level = $dataRow['level'];
        
    }
}
?>