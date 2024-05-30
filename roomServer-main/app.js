const express = require('express');
const http = require('http');
const WebSocket = require('ws');
const path = require('path');
const SocketIO = require('socket.io');

const app = express();
const port = 8080;
// const ip = 'localhost';
const ip = '10.224.0.83';


const server = http.createServer(app);

const io = SocketIO(server);

const wss = new WebSocket.Server({noServer : true, path:'/ws' });

/** 
*@type {Socket}
*/

// Configurer les chemins pour servir les fichiers statiques
app.use('/bootstrap/css', express.static(path.join(__dirname, 'node_modules/bootstrap/dist/css')));
app.use('/bootstrap/js', express.static(path.join(__dirname, 'node_modules/bootstrap/dist/js')));
app.use('/jquery', express.static(path.join(__dirname, 'node_modules/jquery/dist')));
app.use('/public', express.static(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'templates/socket.io')));
app.use(express.static('public'));

app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'templates/index.html'));
});

app.get('/index', (req, res) => {
    res.sendFile(path.join(__dirname, 'templates/index.html'));
});

let rooms = [];


//----------------------------------------------//
//----------------------------------------------//
//----------- Partie Serveur SocketIO ----------//
//----------------------------------------------//
//----------------------------------------------//

io.on('connection', (socket) => {

    //On renvoit le joueur à la page d'accueil lors d'une reconnexion
    io.to(socket.id).emit('back to home');
    console.log(`[connection] ${socket.id}`);


    //Entrée du pseudo d'un joueur    
    socket.on('playerData', (player) => {
        console.log(`[playerData] ${player.username}`);

        let room = null;

        if (!player.roomId) {
            room = createRoom(player);
            console.log(`[create room ] - ${room.id} - ${player.username}`);
        } else {
            room = rooms.find(r => r.id === player.roomId);

            if (room === undefined) {
                return;
            }

            player.roomId = room.id;
            room.players.push(player);
        }

        socket.join(room.id);
        io.to(socket.id).emit('join room', room);
        player.playerId = room.players.length;
        io.to(room.id).emit('update player', room.players); //Update de la room pour tous les autres joueurs

        io.emit('update rooms', rooms); //Update de la liste des rooms pour tous les autres joueurs

        if (room.players.length === 4) {
            io.to(room.id).emit('full');
        }
    });

    // Affichage des rooms
    socket.on('get rooms', () => {
        io.to(socket.id).emit('list rooms', rooms);
    });


//---------- Déconnexions ----------//

// Clic sur le bouton déconnexion
    socket.on('disconect player', (player) => {
        let disconnectedRoom = null;
        let disconnectedPlayer = null;
        disconnectedRoom = rooms.find(r => r.id === player.roomId);
        disconnectedPlayer = player;
    
        if (disconnectedPlayer && disconnectedRoom) {
            disconnect_to_room(disconnectedPlayer, disconnectedRoom, socket);
        } 
    });
    

// Déconnexion par refresh de la page
    socket.on('disconnect', () => {
        console.log(`[disconnect] ${socket.id}`);
        rooms.forEach(r => {
            r.players.forEach(p => {
                if (p.socketId === socket.id) {
                    disconnect_to_room(p,r, socket);
                }
            })
        })
    });

//------------------------------------------------//
//------------------------------------------------//
//-------- Communication Unity et joueurs --------//
//------------------------------------------------//
//------------------------------------------------//

    //Acceptation de l'invitation à une partie -> affichage des manettes
    socket.on('accept game', (room) =>{
        console.log(room);
        let html ="usernames|";
        rooms.forEach(r => {
            r.players.forEach(p =>{
                io.to(p.socketId).emit('waiting ended');
            });
            if(r.id === room){
                const selectedroom = r;
                r.inGame = true;
                selectedroom.players.forEach(p =>{
                    html += `${p.username}|`
                    io.to(p.socketId).emit('display manette');
                });                
                wss.clients.forEach((client) =>{
                    if(client.readyState == WebSocket.OPEN){
                        client.send((`NBPlayers${selectedroom.players.length}`));
                        client.send((html));
                    }
                });
            }


        });
        io.emit('update rooms', rooms);
    });

    // Refus de l'invitation par l'hôte d'une room

    socket.on('refuse game', (room) =>{
        rooms.forEach(r => {
            if(r.id === room){
                r.players.forEach(p =>{
                    io.to(p.socketId).emit('waiting ended');
                })
            }
        });
    });

    // Envoi d'un input de mouvement de la part d'un joueur
    socket.on('mouvement', (mouvement, joueurId) =>{
        let message = "";
        wss.clients.forEach((client) =>{
            if(client.readyState == WebSocket.OPEN){
                message += `P${joueurId}_${mouvement}`;
                client.send(message);
                
                console.log(message);
            }

        });
    })
});

//-------------------------------------------//
//-------------------------------------------//
//---------------- Fonctions ----------------//
//-------------------------------------------//
//-------------------------------------------//


// Deconnexion d'un joueur 
function disconnect_to_room(disconnectedPlayer, disconnectedRoom, socket){
    if(disconnectedPlayer.host === true){  //Transfert du role d'hôte si l'hôte a quitté la partie
        if(disconnectedRoom.players[1]){
            disconnectedRoom.players[1].host = true;
            console.log(`[Transfert host] - ${disconnectedPlayer.username} -> ${disconnectedRoom.players[1].username}`)
            io.to(disconnectedRoom.players[1].socketId).emit('new host');

        }
    }
    disconnectedRoom.players = disconnectedRoom.players.filter(player => player.socketId !== socket.id); //Supression du joueur
    io.to(socket.id).emit('leave room');

    if(disconnectedRoom.players.length === 0){
        rooms = rooms.filter(room => room.id !== disconnectedRoom.id); //Supression de la room si il ne reste plus de joueurs
    }
    else{ // Sinon on uptade la liste des joueurs
        disconnectedRoom.players.forEach(p =>{
            p.playerId = disconnectedRoom.players.indexOf(p) +1;
            io.to(p.socketId).emit("update id",p.playerId);
        });
        io.to(disconnectedRoom.id).emit('update player', disconnectedRoom.players);
    }
    io.emit('update rooms', rooms);
}


// Création d'un room

function createRoom(player) {
    const room = { id: roomId(), players: [] , inGame: false};

    player.roomId = room.id;

    room.players.push(player);
    rooms.push(room);
    

    return room;
}

function roomId() {
    return Math.random().toString(36).substr(2, 9);
}


//----------------------------------------------//
//----------------------------------------------//
//---------- Partie Serveur Websocket ----------//
//----------------------------------------------//
//----------------------------------------------//

// Communication avec le Jeu

wss.on('connection', (ws)=>{
    console.log("Unity connected");
    
    ws.on('message', function incoming(message, isBinary){
        const mess = JSON.stringify(message)
        console.log(mess)
        if(mess === `"asking players"`){
            rooms.forEach(r =>{

                // Désactive les manettes pour les joueurs les ayant comme affichage
                if(r.inGame === true){
                    r.players.forEach(p =>{
                        io.to(p.socketId).emit('undisplay manette');
                    })
                    r.inGame = false; //On repasse le statut de la room comm n'étant pas en jeu
                    io.emit('update rooms', rooms);
                }
                // Affiche la demande d'attente de nouveaux joueurs
                if(r.players.length > 1){
                    r.players.forEach(p=>{
                        io.to(p.socketId).emit('waiting players');
                    })
                }
            })
        }
        if(mess === `"Nombre player"`){
            rooms.forEach(r =>{
                if(r.inGame === true){
                    wss.clients.forEach((client)=>{
                        if(client.readyState == WebSocket.OPEN){
                            client.send((`NBPlayers${r.players.length}`));
                        }
                    })

                }
            })
        }
    });
});



// Handle upgrade requests
server.on('upgrade', (request, socket, head) => {
    const pathname = new URL(request.url, `http://${request.headers.host}`).pathname;
  
    if (pathname === '/ws') {
      wss.handleUpgrade(request, socket, head, (ws) => {
        wss.emit('connection', ws, request);
      });
    } else {
      socket.destroy();
    }
  });

server.listen(port, ip, () => {
    console.log(`Server is running on http://${ip}:${port}`);
});