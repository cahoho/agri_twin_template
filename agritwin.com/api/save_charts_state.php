<?php
header("Content-Type: application/json; charset=utf-8");

// 确保 data 目录存在
$dataDir = '../data';
if (!file_exists($dataDir)) {
    // 0777 是权限，true 表示递归创建
    if (!mkdir($dataDir, 0777, true)) {
        echo json_encode(['success' => false, 'message' => 'Failed to create data directory.']);
        exit;
    }
}

$file = $dataDir . '/charts_state.json';

// 获取原始 POST 数据（期望是 JSON 字符串）
$input = file_get_contents('php://input');
$chartsState = json_decode($input, true); // 解码为关联数组

// 检查 JSON 解析是否成功
if ($chartsState === null && json_last_error() !== JSON_ERROR_NONE) {
    echo json_encode(['success' => false, 'message' => 'Invalid JSON input.', 'error' => json_last_error_msg()]);
    exit;
}

if (is_array($chartsState) && empty($chartsState)) {
    $chartsState = new stdClass();
}
// --- 修复结束 ---

// 将整个 chartsState 对象保存到 charts_state.json
// JSON_UNESCAPED_UNICODE 确保中文不被转义
// JSON_PRETTY_PRINT 使 JSON 文件更易读
if (file_put_contents($file, json_encode($chartsState, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT)) !== false) {
    echo json_encode(['success' => true, 'message' => 'Charts state saved successfully.']);
} else {
    echo json_encode(['success' => false, 'message' => 'Failed to write charts state to file. Check directory permissions.']);
}
?>
