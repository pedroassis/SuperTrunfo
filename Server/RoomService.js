

function RoomService(){

	var rooms = [];

	this.className = "SuperTrunfo.Room";

	this.addRoom = function addRoom(room){
		rooms.push(room);
	}

	this.rooms = function rooms(room){
		return rooms;
	}
}