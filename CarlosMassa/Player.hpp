#ifndef _PLAYER_HPP_
#define _PLAYER_HPP_

#include<string>
#include<vector>
#include<algorithm>
#include<set>
using namespace std;

class Player{
	
	private:
		string name;
		vector<string> hand;
		int unknown;
		
	public:
		Player();
	
		Player(string name);
		
		string get_name();
		
		void add_card(string card);
		
		void remove_card(string card);
		
		vector<string> get_hand();
		
		bool has_card(string card);
		
		int get_number_of_unknown();
	
};

#endif
