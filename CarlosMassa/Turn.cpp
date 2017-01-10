#include "Turn.hpp"

Turn::Turn(string moves){
	regex deal_regex("[^\\s]+");
	for(sregex_iterator it(moves.begin(), moves.end(), deal_regex), it_end; it != it_end; ++it){
		smatch match = *it;
		string part = match.str();
		if(match.position() == 0)
			set_player(part);
		if(match.position() != 0){
			Move m(part);
			add_move(m);
		}
	}	
}

string Turn::get_player(){
	return player;
}

void Turn::set_player(string player){
	this->player = player;
}

void Turn::add_move(Move move){
	this->moves.push_back(move);
}

list<Move> Turn::get_moves(){
	return moves;
}

void Turn::set_moves(list<Move> moves){
	this->moves = moves;
}

int Turn::get_number_of_unknown(){
	int num_unknown = 0;
	for(Move move: moves)
		if(move.is_unknown())
			++num_unknown;
	return num_unknown;
}
