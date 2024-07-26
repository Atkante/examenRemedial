<?php
// Incluye los archivos de configuración y utilidades
include "config.php";
include "utils.php";

// Establece la conexión a la base de datos
$dbConn = connect($db);

// Verifica si la conexión fue exitosa
if (!$dbConn) {
    echo json_encode(['success' => false]);
    exit;
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $input = $_POST;

    // Prepara la consulta SQL
    $sql = "SELECT COUNT(*) FROM estudiantes_login WHERE usuario = :usuario AND contraseña = :contrasena";
    $statement = $dbConn->prepare($sql);
    
    // Enlaza los valores
    bindAllValues($statement, $input);
    
    try {
        // Ejecuta la consulta
        $statement->execute();
        $count = $statement->fetchColumn();

        // Devuelve true si hay coincidencias, false si no
        if ($count > 0) {
            echo true;
        } else {
            echo json_encode(['success' => false]);
        }
    } catch (PDOException $e) {
        // En caso de error en la consulta, devuelve false
        echo json_encode(['success' => $e]);
    }
} else {
    // Si el método no es POST, devuelve false
    echo json_encode(['success' => false]);
}
?>
