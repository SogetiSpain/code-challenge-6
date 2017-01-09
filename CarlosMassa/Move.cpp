#include "Move.hpp"

Move::Move(string action){
	this->kind = (char) action[0];
	int pos = action.find(":");
	if(pos != string::npos){
		this->card = action.substr(1,pos-1);
		this->extra = action.substr(pos+1, action.size());
	}
	else{
		this->card = action.substr(1,action.size());
		this->extra = "";
	}
}

char Move::get_kind(){
	return kind;
}

string Move::get_card(){
	return card;
}

void Move::set_card(string card){
	this->card = card;
}

string Move::get_extra(){
	return extra;
}

bool Move::is_unknown(){
	return card=="??";
}
