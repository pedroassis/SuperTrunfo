var ws = require("nodejs-websocket")

var server = ws.createServer(function (conn) {
    console.log("New connection");

    var string = '';

    conn.on("text", function (str) {
        console.log("Received "+str);
        string = str;
        conn.sendText(str);
    });

    conn.on("close", function (code, reason) {
        console.log("Connection closed");
    });

    conn.on("error", function (code, reason) {
        console.log("Connection error");
    });

	function echo() {

		if(string && conn.readyState === conn.OPEN)
	  		conn.sendText(string, function(){
	  			console.log(arguments)
	  		})

	  	setTimeout(echo, 2000);
	  }

  echo();


}).listen(81);