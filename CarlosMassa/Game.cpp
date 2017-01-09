#include "Game.hpp"
using namespace std;

Game::Game(){
	players.insert( pair<string,Player>("Shady"	, Player("Shady")) );
	players.insert( pair<string,Player>("Rocky"	, Player("Rocky")) );
	players.insert( pair<string,Player>("Danny"	, Player("Danny")) );
	players.insert( pair<string,Player>("Lil"	, Player("Lil")) );
}

void Game::read_rounds(string input_file){
	ifstream infile(input_file);
	
	Round round;
	bool first_round = true;
	while(!infile.eof()){
		string moves;
		getline(infile, moves);
		Turn turn(moves);
		if(moves[0] != '*'){
			if(!first_round && turn.get_player() == "Shady"){
				add_round(round);
				round = Round();
			}
			else first_round = false;
			
			round.add_turn(turn);
		}
		else
			round.add_signal(turn);
	}
	add_round(round);
}

void Game::process_rounds(string output_file){
	
	ofstream outfile;
    outfile.open(output_file, ios::out | ios::trunc);

	for(Round round : get_rounds()){
		for(Turn turn : round.get_turns()){
			if(turn.get_player() == "Lil"){ 
				for(Turn signal : round.get_signals()){
					if(is_valid_signal(turn, signal))
						break;
				}
			}
			for(Move move : turn.get_moves())
				process_hand(turn.get_player(), move);
		}
		
		// print to file
		for(string card : get_player("Lil").get_hand())
			outfile << card << " ";
		outfile << endl;
	}
}

void Game::add_round(Round round){
	rounds.push_back(round);
}

list<Round> Game::get_rounds(){
	return rounds;
}

Player Game::get_player(string name){
	return players.find(name)->second;
}

void Game::process_hand(string player, Move move){
	string extra = move.get_extra();
	string card = move.get_card();
	
	if(move.get_kind() == '+'){
		players.find(player)->second.add_card(card);
		if(!extra.empty())
			players.find(extra)->second.remove_card(card);
	}
	else{
		players.find(player)->second.remove_card(card);
		if(!extra.empty()){
			if(extra!="discard")
				players.find(extra)->second.add_card(card);
			else
				discard_pile.insert(card);
		}
	}

}

bool Game::is_discarded(string card){
	return discard_pile.count(card);
}

bool Game::is_valid_signal(Turn& turn, Turn signal){
	if(turn.get_number_of_unknown() != signal.get_moves().size())
		return false;
	
	list<Move> turn_moves = turn.get_moves();
	list<Move> signal_moves = signal.get_moves();
	
	list<Move>::iterator turn_it = turn_moves.begin();
	list<Move>::iterator signal_it = signal_moves.begin();
	while(turn_it != turn_moves.end() && signal_it != signal_moves.end()){
		if(turn_it->get_card()=="??"){
			if(turn_it->get_kind()!=signal_it->get_kind() || 
				turn_it->get_extra()!=signal_it->get_extra()) 
					return false;
			else if(is_discarded(signal_it->get_card())) 
				return false;
				
			/*else if(turn_it->get_kind()=='+'){ 
				if(game.get_player("Lil").has_card(signal_it->get_card()))
					return false;
				else if(!turn_it->get_extra().empty() && game.get_player(turn_it->get_extra()).has_card(signal_it->get_card()))
					return false;
			}*/
			/*else if(turn_it->get_kind()=='-'){
				if(!game.get_player("Lil").has_card(signal_it->get_card()))
					return false;
			}*/
			
			turn_it->set_card(signal_it->get_card());
			++signal_it;
		}
		++turn_it;
	}
	turn.set_moves(turn_moves);
	
	return true;
}
