Ce dépot ne représente que la partie serveur d'un projet basé sur Unity, le but du projet est de créer un jeu multijoueur sur Unity pour lequel les joueurs utilisent leur télé^hone en guise de manette.
Il y a donc une partie serveur SocketIo (io) pour tout la communication Web/Serveur et une partie serveur WebSocket (ws) pour la communication Unity/serveur.

## Fonctionnalités

- Système de salons
- Mise à jour en temps réel de l'état des salons (Nombre joueurs et En jeu ou non)
- Existance d'un Hôte et transfert de l'hôte lors de la déconnexion du salon
- Joueur numérotés dans le salon grâce à un Id


## Installation

[Node.js](https://nodejs.org/) est requis pour pouvoir exécuter cette application

```sh
npm install
```
Attention à bien effectuer la commande dans le terminal ayant pour chemin le dossier roomServer

#### Exécuter l'application web

```sh
npm run start
```

   [Visual Studio Code]: <https://code.visualstudio.com/>
   [Node.JS]: <http://nodejs.org>
   [Bootstrap 5]: <https://getbootstrap.com//>
   [jQuery]: <http://jquery.com>
   [Express JS]: <http://expressjs.com>
