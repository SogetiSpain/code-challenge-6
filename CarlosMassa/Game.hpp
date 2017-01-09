#ifndef _GAME_HPP_
#define _GAME_HPP_

#include<list>
#include<map>
#include<utility>
#include<iostream>
#include<sstream>
#include<fstream>
#include "Round.hpp"

class Game{
	
	private:
		list<Round> rounds;
		map<string, Player> players;
		set<string> discard_pile;
	
		void process_hand(string player, Move move);
		bool is_valid_signal(Turn& turn, Turn signal);
	public:
		Game();
		
		void read_rounds(string input_file);
		void process_rounds(string output_file);
		void add_round(Round round);
		list<Round> get_rounds();
		Player get_player(string name);
		bool is_discarded(string card);
};

#endif
