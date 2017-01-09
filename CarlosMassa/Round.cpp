#include "Round.hpp"

void Round::add_turn(Turn turn){
	this->turns.push_back(turn);
}

void Round::add_signal(Turn turn){
	this->signals.push_back(turn);
}

list<Turn> Round::get_turns(){
	return turns;
}

list<Turn> Round::get_signals(){
	return signals;
}

