#pragma once

enum class eSEM_ID {                                    // 同期オブジェクトID
    eFGBG,                                              // foreground/background thread
    ePARAMS,                                            // パラメータ用(共有オブジェクト)
    eLOGFILE,                                           // ログファイル用

    // ここに追加

    // 予約
    eTBD,                                               // 未知のセマフォID
    eNUMB
};

#define ESEMID                  (int)eSEM_ID
#define SEM_TIMEOUT				(2 * (1000))			// 20秒(LANケーブル未接続時、connect()から制御が戻ってくる時間が20秒)
//#define SEM_TIMEOUT				(20 * (1000))			// 20秒(LANケーブル未接続時、connect()から制御が戻ってくる時間が20秒)
