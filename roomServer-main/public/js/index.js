// Objet player
const player = {
    playerId :"",
    roomId: null,
    username: "",
    host: false,
    socketId: "",
};

const socket = io();
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const roomId = urlParams.get('room');

if (roomId) {
    document.getElementById('start').innerText = "Rejoindre";
}
// Définition dee tous les éléments
const usernameInput = document.getElementById('username');
const userCard = document.getElementById('user-card');
const waitingArea = document.getElementById('waiting-area');
const spin = document.getElementById('spinner');
const roomsCard = document.getElementById('rooms-card');
const roomsList = document.getElementById('rooms-list');
const playerList = document.getElementById('players-list');
const disconnect_btn = document.getElementById('disconnect-player');
const waitingMessage = document.getElementById('waiting-game');
const acceptGame = document.getElementById('accept');
const refuseGame = document.getElementById('refuse');
const manette = document.getElementById('manette');
const page = document.getElementById('page');

//Envoi de la requête des rooms
socket.emit('get rooms');

//Récupération des rooms
socket.on('list rooms', (rooms) => {
    let html = "";
    if (rooms.length > 0) {
        roomsCard.classList.remove('d-none');
        rooms.forEach(room => {
            html += `<li class="list-group-item d-flex justify-content-between">
                    <p class="p-0 m-0 flex-grow-1 fw-bold">Salon de ${room.players[0].username} (${room.players.length}/4)</p>`;
                if (room.players.length !== 4 && room.inGame === false) {
                    html+= `<button class="btn btn-sm btn-success join-room" data-room="${room.id}">Rejoindre</button>`;
                }
                else if(room.inGame){
                    html+= `<button class="btn btn-sm btn-refuse">En Jeu</button>`;
                }
            html +=`</li>`;

        });
    }

    if (html !== "") {
        roomsList.innerHTML = html;

        for (const element of document.getElementsByClassName('join-room')) {
            element.addEventListener('click', joinRoom, false)
        }
    }
});

// Mise à jour des rooms
socket.on('update rooms', (rooms) => {
    updateRooms(rooms);
});

function updateRooms(rooms){
    if(!player.roomId){
        if(rooms.length === 0){
            roomsCard.classList.add('d-none');
        }
        else{
            let html = "";
            roomsCard.classList.remove('d-none');
            rooms.forEach(room => {
                html += `<li class="list-group-item d-flex justify-content-between">
                        <p class="p-0 m-0 flex-grow-1 fw-bold">Salon de ${room.players[0].username} (${room.players.length}/4)</p>`;
                if (room.players.length !== 4 && room.inGame === false) {
                    html+= `<button class="btn btn-sm btn-success join-room" data-room="${room.id}">Rejoindre</button>`;
                }
                else if(room.inGame){
                    html+= `<button class="btn btn-sm btn-refuse">En Jeu</button>`;
                }
                html +=`</li>`;    
            });
    
            if (html !== "") {
                roomsList.innerHTML = html;
    
                for (const element of document.getElementsByClassName('join-room')) {
                    element.addEventListener('click', joinRoom, false)
                }
            }
        };
    };
};

// Mise à jour
socket.on('update player', (players)=>{
    let html ="";
    players.forEach(player =>{
        player.playerId = players.indexOf(player)+1;
        html +=`<li class="list-group-item d-flex justify-content-between">${player.playerId} - ${player.username}`
        if(player.host === true){ //Si l'utilisateur est hôte
            html += `<i class="fa-regular fas fa-crown"></i>`;
        }
        html+=`</li>`;
    })
    playerList.innerHTML = html;
});

socket.on("update id", (playerid)=>{
    player.playerId = playerid;
});

// Attribution de l'hôte
socket.on('new host',() => {
    player.host = true;
})

// Créer ou rejoindre une room

$("#form").on('submit', function (e) {
    e.preventDefault();

    player.username = usernameInput.value;

    if (roomId) {
        player.roomId = roomId;
    } else {
        player.host = true;
    }

    player.socketId = socket.id;

    userCard.hidden = true;
    waitingArea.classList.remove('d-none');
    roomsCard.classList.add('d-none');

    socket.emit('playerData', player);
});


// ------- Room interaction avec joueurs 
//Connexion à une room
socket.on('join room', (room) => {
    player.roomId = room.id;
    player.playerId = room.players.length;

});

//Deconnexion à une room
socket.on('leave room', ()=>{
    player.roomId = null;
    if(player.host === true){
        player.host = false;
    }
});

socket.on('full', () =>{
    spin.classList.add('d-none');
});


//--------- Quitter une room
disconnect_btn.addEventListener('click', ()=>{
    socket.emit("disconect player", player);
    //On revient sur la page des rooms
    waitingArea.classList.add('d-none');
    usernameInput.innerHTML = player.username;
    socket.emit('get rooms');
    userCard.hidden = false;
});

// Rejoindre une room
const joinRoom = function () {
    if (usernameInput.value !== "") {
        player.username = usernameInput.value;
        player.socketId = socket.id;
        player.roomId = this.dataset.room;

        socket.emit('playerData', player);

        userCard.hidden = true;
        waitingArea.classList.remove('d-none');
        roomsCard.classList.add('d-none');

    }
}



// ------ Jouabilité
socket.on('waiting players', () =>{
    waitingMessage.classList.remove('d-none');
    let html ="";
    if(player.host === true){
        html += "Invitation à une partie";
        refuseGame.classList.remove('d-none');
        acceptGame.classList.remove('d-none');
    }
    else{
        html += "En attente de l'hôte ...";
    }
    document.getElementById('message').innerHTML = html;
});

socket.on('waiting ended', () =>{
    waitingMessage.classList.add('d-none');
    if(player.host === true){
        refuseGame.classList.add('d-none');
        acceptGame.classList.add('d-none');
    }
})

refuseGame.addEventListener('click', () => {
    socket.emit('refuse game', player.roomId);
})

acceptGame.addEventListener('click', () =>{
    socket.emit('accept game', player.roomId);

})

socket.on('display manette', ()=>{
    page.classList.add('d-none');
    manette.classList.remove('d-none');
    toggleFullScreen();
    updateManetteBackground(player.playerId);

})

socket.on('undisplay manette', ()=>{
    page.classList.remove('d-none');
    manette.classList.add('d-none');
})

socket.on('back to home', ()=>{
    if(!manette.classList.contains('d-none')){
        location.reload();
    }
    else if(!waitingArea.classList.contains('d-none')){
        location.reload();
    }
})
//-------------------------------------------------------//
//--------------- Fonctionnement manettes ---------------//
//-------------------------------------------------------//

const up = document.getElementById('up');
const left = document.getElementById('left');
const right = document.getElementById('right');
const down = document.getElementById('down');
const A = document.getElementById('A');
const B = document.getElementById('B'); 

function toggleFullScreen() {
    if (!document.fullscreenElement) {
        let elem = document.documentElement; // The entire page
        if (elem.requestFullscreen) {
            elem.requestFullscreen();
        } else if (elem.mozRequestFullScreen) { // Firefox
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullscreen) { // Chrome, Safari, Opera
            elem.webkitRequestFullscreen();
        } else if (elem.msRequestFullscreen) { // IE/Edge
            elem.msRequestFullscreen();
        };
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) { // Firefox
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) { // Chrome, Safari, Opera
            document.webkitExitFullscreen();
        } else if (document.msExitFullscreen) { // IE/Edge
            document.msExitFullscreen();
        }
    }
}

function handlePointerDown(button, message) {
    button.onpointerdown = () => {
        socket.emit('mouvement' ,`${message}Pressed`,player.playerId);
        console.log(message);
    };
}

function handlePointerUp(button, message) {
    button.onpointerup = () => {
        socket.emit('mouvement', `${message}Released`,player.playerId);
    };
    button.onpointerleave = () => {
        socket.emit('mouvement',`${message}Released`,player.playerId);
    };
    button.onpointercancel = () => {
        socket.emit('mouvement',`${message}Released`,player.playerId);
    };
}

handlePointerDown(up, 'Up');
handlePointerUp(up, 'Up');
handlePointerDown(left, 'Left');
handlePointerUp(left, 'Left');
handlePointerDown(right, 'Right');
handlePointerUp(right, 'Right');
handlePointerDown(down, 'Down');
handlePointerUp(down, 'Down');
handlePointerUp(A, 'A');
handlePointerDown(A, 'A');
handlePointerUp(B, 'B');
handlePointerDown(B,'B');


function updateManetteBackground(playerId) {
    // Retirer les classes existantes
    manette.classList.remove('player-1', 'player-2', 'player-3', 'player-4');
    // Ajouter la nouvelle classe
    manette.classList.add(`player-${playerId}`);
}

socket.on('join room', (room) => {
    player.roomId = room.id;
    player.playerId = room.players.length;
    updateManetteBackground(player.playerId);
});

function vibrateDevice() {
    if (navigator.vibrate) {
        // Vibrate for 200 milliseconds
        navigator.vibrate(200);
    }}