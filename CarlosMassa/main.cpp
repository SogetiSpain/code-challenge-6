#include<iostream>
#include<list>
#include<string>
#include<vector>
#include<stack>
#include<utility>
#include "Game.hpp"
using namespace std;


int main(){
	
	const string INPUT_FILE = "samples/input.txt";
	const string OUTPUT_FILE = "samples/output.txt";
	
	Game game;
	game.read_rounds(INPUT_FILE);
	game.process_rounds(OUTPUT_FILE);
}
