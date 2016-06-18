
#include <Servo.h> // use the Servo library
#include <SoftwareSerial.h>

SoftwareSerial mySerial(7,6); // RX, TX
Servo Servo_up_down;
Servo Servo_left_right;
Servo Servo_deal;
int Players[4];
int Players_angles[4] = {0, 45, 90, 135};
int Burning_angle = 60;
int nof_players = 0;


/*********   application functions   *********/
void set_players(int num);
void deal_cards(void);
void flop(void);
void turn(void); // same as river
/**********************************************/



/*********    internal functions      *********/
void _set_servo_down(void);
void _set_servo_up(void);
void _deal_one_card(void);
void _flop_cards(int num_of_card);
void _deal_player(int player_id);
void _burn_one_card(void);
/**********************************************/


void setup() {
    Servo_deal.attach(9); // attaches the servo on pin 9 to the servo object
    Servo_up_down.attach(10);
    Servo_left_right.attach(11);

    Servo_deal.write(0);
    Servo_up_down.write(15);
    Servo_left_right.write(60);
    Serial.begin(9600);
    Serial.println("Welcome!");
    mySerial.begin(9600);

	/*
    set_players(1, 1, 1, 1);
    deal_cards();
	*/
}

void loop() {
    char c;
    int i;
    int p[4] = {0,0,0,0};
    if (mySerial.available()) {
        c = mySerial.read();
        switch(c) {
            case 's': // set_players
                for (i = 0; i < 4; i++) {
                    c = mySerial.read();
                    while (!(c == '0' || c == '1')) {
                      c = mySerial.read();
                    }
                    Serial.println(c);
                    p[i] = (c-'0');
                }
                set_players(p[0], p[1], p[2], p[3]);
                break;
            case 'd': // deal cards
                deal_cards();
                break;
            case 'f': // flop
                flop();
                break;
            case 't': // turn
            case 'r': // rivers
                turn();
                break;
        }
    }
   
}

void set_players(int p0, int p1, int p2, int p3) { // passing the player in binary form, for example 0110
    if (p0 == 1) Players[0] = 1; else Players[0] = 0;
    if (p1 == 1) Players[1] = 1; else Players[1] = 0;
    if (p2 == 1) Players[2] = 1; else Players[2] = 0;
    if (p3 == 1) Players[3] = 1; else Players[3] = 0;
}

void deal_cards(void) { // for each player
    int i = 0, j = 0;
    _set_servo_down();
    for (; j < 2; ++j) {
        for (i = 0; i < 4; i++) {
            if (Players[i] == 1)
              _deal_player(i);
        }        
    }
}

void flop(void) {
    _burn_one_card();
    _flop_cards(3);
}

void turn(void) {
    _burn_one_card();
    _flop_cards(1);
}

void _set_servo_down(void){
    Servo_up_down.write(5);
    delay(1000);
}

void _set_servo_up(void){
    Servo_up_down.write(15);
    delay(1000);
}

void _deal_one_card(void) {
    Servo_deal.write(0);
    delay(1000);
    Servo_deal.write(180);
    delay(1000);    
}

void _flop_cards(int num_of_card) {
    static int num = 0;
    int i;
    _set_servo_up();
    Servo_left_right.write(50 + num*5);
    for( i=0 ; i < num_of_card ;i++) {
        _deal_one_card();
    }
    num = (num + 1) % 5;
}

void _deal_player(int player_id) {
    Servo_left_right.write(Players_angles[player_id]);
    _deal_one_card();
}

void _burn_one_card(void) {
    _set_servo_down();
    Servo_left_right.write(Burning_angle);
    _deal_one_card();
}
