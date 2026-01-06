<?php
header("Content-Type: application/json; charset=utf-8");

$file = '../data/charts_state.json';

if (file_exists($file)) {
    $content = file_get_contents($file);
    $chartsState = json_decode($content, true);
    if ($chartsState !== null && json_last_error() === JSON_ERROR_NONE) {
        echo json_encode(['success' => true, 'data' => $chartsState]);
    } else {
        echo json_encode(['success' => false, 'message' => 'Failed to parse charts state JSON.', 'error' => json_last_error_msg()]);
    }
} else {
    echo json_encode(['success' => false, 'message' => 'Charts state file not found.']);
}
?>
