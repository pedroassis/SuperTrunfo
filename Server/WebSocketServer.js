

var RoomService = require("./RoomService.js");

var roomService = new RoomService();

function Server(){

	this.getRooms = function getRooms(connection, message){

		var message = {
			name 		: "rooms",
			message   	: roomService.rooms(),
			className 	: roomService.className
		};

		connection.sendText(JSON.stringify(message));
	}

	this.addRoom = function addRoom(connection, message){
		var room = message.message;
		roomService.addRoom(room);

		message.name = "roomAdded";

		connection.sendText(JSON.stringify(message));
	}

	this.joinRoom = function joinRoom(connection, message){
		var playerToRoom = message.message;

		var player = playerToRoom.player;

		var room = roomService.getRooms().filter(function(room){
			return room.id === playerToRoom.room.id;
		})[0];

		var playerAdded = {
			name : "playerAdded",
			message : player,
			className : "SuperTrunfo.Player"
		};

		room.players().forEach(function(player){
			player.connection.sendText(JSON.stringify(playerAdded));
		});

		room.addPlayer(player);
	}

}
