#define SW 8
#define joy_x A0
#define joy_y A3

const int LED =  13;

void setup() {
  Serial.begin(115200);
  pinMode(SW, INPUT_PULLUP);
  pinMode(joy_x, INPUT);
  pinMode(joy_y, INPUT);
  pinMode(LED, OUTPUT);
}

void loop() {
  if (Serial.available() > 0) {
    String data = Serial.readStringUntil('\n');
    data.trim();
    
    if (data == "HIGH") {
      digitalWrite(LED, HIGH);
    } else if (data == "LOW") {
      digitalWrite(LED, LOW);
    }
  }
  
  float joy_rx = analogRead(joy_x);
  float joy_ry = analogRead(joy_y);

  joy_rx = map(joy_rx, 1, 1024, -500, 500);
  joy_ry = map(joy_ry, 1, 1024, -500, 500);

  Serial.print(joy_rx);
  Serial.print(",");
  Serial.print(joy_ry);
  Serial.print(",");
  Serial.println(!digitalRead(SW));
  delay(50);
}
