

function Room(){

	var players = [];

	this.addPlayer = function addPlayer(player){
		players.push(player);
	}

	this.players = function getPlayers(){
		return players;
	}
}