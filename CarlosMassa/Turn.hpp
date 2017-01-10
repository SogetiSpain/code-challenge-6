#ifndef _TURN_HPP_
#define _TURN_HPP_

#include<list>
#include<string>
#include<regex>
#include "Move.hpp"
#include "Player.hpp"

class Turn{
	
	private:
		list<Move> moves;
		string player;
		
	public:
		Turn(string moves);
		
		string get_player();
		void set_player(string player);
		void add_move(Move move);
		list<Move> get_moves();
		void set_moves(list<Move> moves);
		int get_number_of_unknown();
};

#endif
