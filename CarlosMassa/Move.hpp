#ifndef _MOVE_HPP_
#define _MOVE_HPP_

#include<string>
using namespace std;

class Move{
	
	private:
		char kind;
		string card; // value and suit
		string extra;
		
	public:
		Move(string action);
		
		char get_kind();
		string get_card();
		void set_card(string card);
		string get_extra();
		bool is_unknown();
};

#endif
