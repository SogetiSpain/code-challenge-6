#ifndef _ROUND_HPP_
#define _ROUND_HPP_

#include<string>
#include<map>
#include "Turn.hpp"
using namespace std;

class Round{
	
	private:
		list<Turn> turns;
		list<Turn> signals;
		
	public:
		void add_turn(Turn turn);
		void add_signal(Turn turn);
		list<Turn> get_turns();
		list<Turn> get_signals();
};

#endif
