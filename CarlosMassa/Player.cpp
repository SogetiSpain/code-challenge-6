#include "Player.hpp"

Player::Player(){}

Player::Player(string name){
	this->name = name;
	this->unknown = 0;
}

string Player::get_name(){
	return name;
}

void Player::add_card(string card){
	if(card == "??")
		++unknown;
	else
		hand.push_back(card);
}

void Player::remove_card(string card){
	if(card == "??")
		--unknown;
	else
		hand.erase(std::remove(hand.begin(), hand.end(), card), hand.end());
}

vector<string> Player::get_hand(){
	return hand;
}

bool Player::has_card(string card){
	return find(hand.begin(), hand.end(), card) != hand.end();
}

int Player::get_number_of_unknown(){
	return unknown;
}
