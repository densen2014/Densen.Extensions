#include <Arduino.h>
#include <TFT_eSPI.h>
#include <SPI.h>
#include "WiFi.h"
#include <Wire.h>
#include <esp_adc_cal.h>

#include "driver/rtc_io.h"

String Str; // 声明变量 Str ，用于存储串口输入的数据

TFT_eSPI tft = TFT_eSPI(135, 240); // Invoke custom library

void setup() {
  Serial.begin(115200); // 设置串口波特率

  
  tft.init();
  tft.setRotation(1);
  tft.fillScreen(TFT_BLACK);
  tft.setTextSize(2);
  tft.setTextColor(TFT_GREEN);
  tft.setCursor(0, 0);
  tft.setTextDatum(MC_DATUM);
  tft.setTextSize(1);
  
  tft.setSwapBytes(true);
  delay(5000);


  tft.setRotation(0);
  tft.fillScreen(TFT_RED);
  delay(100);
  tft.fillScreen(TFT_BLUE);
  delay(100);
  tft.fillScreen(TFT_GREEN);
  delay(100);

  

  tft.fillScreen(TFT_BLACK);
  tft.setTextDatum(MC_DATUM);

  
  tft.drawString("Blazor", tft.width() / 2, 2,4);
  tft.drawString("LeftButton:", 2, tft.height() / 3*2 ,4);
  tft.drawString("[WiFi Scan]", 0, tft.height() / 2 ,1);
  tft.setTextDatum(TL_DATUM);
}

// 获取串口数据函数
void GetSerialStuff(String& Str) {
  String tempStr = ""; // 声明变量 tempStr，用于临时存储串口输入的数据
  while(Serial.available()) { // 当串口有数据时，循环执行
    tempStr += (char)Serial.read();  // 把读取的串口数据，逐个组合到inStr变量里
  }
  Str = tempStr; // 把引用指针的变量赋值为 tempStr
}

void loop() {
  GetSerialStuff(Str);
  if(Str != "") {
    // 在串口中输出我们输入的数据，方便观察
    Serial.print("input: ");
    Serial.println(Str); 

    tft.setTextDatum(MC_DATUM);
    tft.fillScreen(TFT_RED);
    delay(200);
    tft.fillScreen(TFT_BLACK);

    tft.drawString("input: ", tft.width() / 2, 2,4);
    tft.drawString(Str,  2, tft.height() / 3 ,4);

    String StrA;
    switch ((byte)Str[0]) { // 把输入的字符转换成byte类型，进行对应指令的判断
      case 'a': // 串口输入 h 时，输出以下信息
        StrA="arduino serial";
        break;
      case 'h': // 串口输入 h 时，输出以下信息
        StrA="hello every body!";
        break;
      case 'b': // 串口输入 b 时，输出以下信息
        StrA="https://www.blazor.zone";
        break;
      case 't':  // 串口输入 t 时，输出以下信息
        StrA="Thanks for watch"; 
        break;
      default: // 没有对应的case时，则输出一下默认信息
        StrA="Please enter the correct command";
    }
    
    Serial.println(StrA); 
    tft.drawString(StrA, 2, tft.height() / 3*2 ,2);
  }

  delay(100); // 延时 100 毫秒
}
 
