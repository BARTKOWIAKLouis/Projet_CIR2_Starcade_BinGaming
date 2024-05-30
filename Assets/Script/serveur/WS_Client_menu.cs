using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using WebSocketSharp;

public class WebSocketClient_menu : MonoBehaviour
{
    private WebSocket ws;

    public string ip = "192.168.43.50";
    public string PORT = "8080";

    public menu_move_player movementScript_P1;
    public planet0Script planet_ice;
    public planet0Script planet_tron;
    public planet0Script planet_war;
    public planet0Script planet_speed_fight;

    public int Nombre_player;

    void Start()
    {
        ws = new WebSocket("ws://" + ip + ":" + PORT + "/ws");

        ws.OnOpen += (sender, e) => Debug.Log("WebSocket connected");
        ws.OnMessage += (sender, e) => ProcessMessage(e.Data);
        //ws.OnError += (sender, e) => Debug.LogError("WebSocket error: " + e.Message);
        ws.OnClose += (sender, e) => Debug.Log("WebSocket closed");

        ws.Connect();

        //movementScript_P1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_1_MovementScript>();
        //movementScript_P2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_2_MovementScript>();
        //movementScript_P3 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_3_MovementScript>();
        //movementScript_P4 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_4_MovementScript>();
    }

    void OnDestroy()
    {
        ws.Close();
    }
    void Update()
    {
        ws.OnMessage += (sender, e) =>
        {

        };

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ws = new WebSocket("ws://" + ip + ":" + PORT + "/ws");

            ws.OnOpen += (sender, e) => Debug.Log("WebSocket connected");
            ws.OnMessage += (sender, e) => ProcessMessage(e.Data);
            ws.OnError += (sender, e) => Debug.LogError("WebSocket error: " + e.Message);
            ws.OnClose += (sender, e) => Debug.Log("WebSocket closed");

            ws.Connect();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            string message = "asking players";
            ws.Send(message);
        }
    }


    void ProcessMessage(string message)
    {
        // Traiter le message WebSocket et simuler des événements de PlayerInput
        Debug.Log("treating : " + message);

        //on interprète le nombre de joueurs de la room
        if(message.Contains("NBPlayers")) 
        {
            // /!\ à changer d'urgence /!\
            switch (message.Last())
            {
                case '2':
                    Debug.Log("2 joueurs");
                    Nombre_player = 2;
                    break;

                case '3':
                    Debug.Log("3 joueurs");
                    Nombre_player = 3;
                    break;

                case '4':
                    Debug.Log("4 joueurs");
                    Nombre_player = 4;
                    break;

                default:
                    Debug.Log(message.Last());
                    break;
            }
        }

        else if (message.Contains("usernames"))
        {
            string[] Usernames = message.Split('|'); //sépare tous les éléments du messages et les met dans le tableau Usernames
            Usernames = Usernames.Where((source,index) => index != 0).ToArray(); //retire le premier élément du tableau Usernames 
        }
        
        else if (message != null)
        {
            Debug.Log("data !null");

            // à modifier, regarde quel joueur bouge
            switch (message[1])
            {
                case '1':
                    switch (message) // à modifier, regarde quelle action le joueur effectue (P1)
                    {
                        case "P1_LeftPressed":
                            // Simuler un déplacement du joueur à gauche
                            Debug.Log("Player1 Go Left");
                            movementScript_P1.left("on");
                            break;
                        case "P1_LeftReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player1 Stop Left");
                            movementScript_P1.left("off");
                            break;

                        case "P1_RightPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player1 Go Right");
                            movementScript_P1.right("on");
                            break;
                        case "P1_RightReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player1 Stop Right");
                            movementScript_P1.right("off");
                            break;

                        case "P1_UpPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player1 Go Up");
                            movementScript_P1.up("on");
                            break;
                        case "P1_UpReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player1 Stop Up");
                            movementScript_P1.up("off");
                            break;

                        case "P1_DownPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player1 Go Down");
                            movementScript_P1.down("on");
                            break;
                        case "P1_DownReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player1 Stop Down");
                            movementScript_P1.down("off");
                            break;


                            
                        case "P1_APressed":
                            //Simule l'utilisation du bouton A
                            Debug.Log("Player1 Press A");
                            planet_ice.fonction_Act1("on");
                            planet_tron.fonction_Act1("on");
                            planet_war.fonction_Act1("on");
                            planet_speed_fight.fonction_Act1("on");
                            break;
                            /*
                        case "P1_BPressed":
                            //Simule l'utilisation du bouton B
                            Debug.Log("Player1 Press B");
                            movementScript_P1.fonction_Act2("on");
                            break;
                            */
                    }
                    break;
/*
                case '2':
                    switch (message)
                    {
                        case "P2_LeftPressed":
                            // Simuler un déplacement du joueur à gauche
                            Debug.Log("Player2 Go Left");
                            movementScript_P2.left("on");
                            break;
                        case "P2_LeftReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player2 Stop Left");
                            movementScript_P2.left("off");
                            break;

                        case "P2_RightPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player2 Go Right");
                            movementScript_P2.right("on");
                            break;
                        case "P2_RightReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player2 Stop Right");
                            movementScript_P2.right("off");
                            break;

                        case "P2_UpPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player2 Go Up");
                            movementScript_P2.up("on");
                            break;
                        case "P2_UpReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player2 Stop Up");
                            movementScript_P2.up("off");
                            break;

                        case "P2_DownPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player2 Go Down");
                            movementScript_P2.down("on");
                            break;
                        case "P2_DownReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player2 Stop Down");
                            movementScript_P2.down("off");
                            break;



                        case "P2_APressed":
                            //Simule l'utilisation du bouton A
                            Debug.Log("Player2 Press A");
                            movementScript_P2.fonction_Act1("on");
                            break;

                        case "P2_BPressed":
                            //Simule l'utilisation du bouton B
                            Debug.Log("Player2 Press B");
                            movementScript_P2.fonction_Act2("on");
                            break;
                    }
                    break;

                case '3':
                    switch (message)
                    {
                        case "P3_LeftPressed":
                            // Simuler un déplacement du joueur à gauche
                            Debug.Log("Player3 Go Left");
                            movementScript_P3.left("on");
                            break;
                        case "P3_LeftReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player3 Stop Left");
                            movementScript_P3.left("off");
                            break;

                        case "P3_RightPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player3 Go Right");
                            movementScript_P3.right("on");
                            break;
                        case "P3_RightReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player3 Stop Right");
                            movementScript_P3.right("off");
                            break;

                        case "P3_UpPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player3 Go Up");
                            movementScript_P3.up("on");
                            break;
                        case "P3_UpReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player3 Stop Up");
                            movementScript_P3.up("off");
                            break;

                        case "P3_DownPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player3 Go Down");
                            movementScript_P3.down("on");
                            break;
                        case "P3_DownReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player3 Stop Down");
                            movementScript_P3.down("off");
                            break;



                        case "P3_APressed":
                            //Simule l'utilisation du bouton A
                            Debug.Log("Player3 Press A");
                            movementScript_P3.fonction_Act1("on");
                            break;

                        case "P3_BPressed":
                            //Simule l'utilisation du bouton B
                            Debug.Log("Player3 Press B");
                            movementScript_P3.fonction_Act2("on");
                            break;
                    }
                    break;

                case '4':
                    switch (message)
                    {
                        case "P4_LeftPressed":
                            // Simuler un déplacement du joueur à gauche
                            Debug.Log("Player4 Go Left");
                            movementScript_P4.left("on");
                            break;
                        case "P4_LeftReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player4 Stop Left");
                            movementScript_P4.left("off");
                            break;

                        case "P4_RightPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player4 Go Right");
                            movementScript_P4.right("on");
                            break;
                        case "P4_RightReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player4 Stop Right");
                            movementScript_P4.right("off");
                            break;

                        case "P4_UpPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player4 Go Up");
                            movementScript_P4.up("on");
                            break;
                        case "P4_UpReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player4 Stop Up");
                            movementScript_P4.up("off");
                            break;

                        case "P4_DownPressed":
                            // Simuler un déplacement du joueur à droite
                            Debug.Log("Player4 Go Down");
                            movementScript_P4.down("on");
                            break;
                        case "P4_DownReleased":
                            // Simuler l'arrêt du déplacement du joueur
                            Debug.Log("Player4 Stop Down");
                            movementScript_P4.down("off");
                            break;



                        case "P4_APressed":
                            //Simule l'utilisation du bouton A
                            Debug.Log("Player4 Press A");
                            movementScript_P4.fonction_Act1("on");
                            break;

                        case "P4_BPressed":
                            //Simule l'utilisation du bouton B
                            Debug.Log("Player4 Press B");
                            movementScript_P4.fonction_Act2("on");
                            break;
                    }
                    break;
*/
            }

            //à supprimer plus tard quand on en a plus besoin
            //switch (message)
            //{
            //    case "LeftDown":
            //            // Simuler un déplacement du joueur à gauche
            //            Debug.Log("Player1 Go Left");
            //            movementScript_P1.left("on");
            //            break;
            //    case "LeftUp":
            //            // Simuler l'arrêt du déplacement du joueur
            //            Debug.Log("Player1 Stop Left");
            //            movementScript_P1.left("off");
            //            break;

            //    case "RightDown":
            //            // Simuler un déplacement du joueur à droite
            //            Debug.Log("Player1 Go Right");
            //            movementScript_P1.right("on");
            //            break;
            //    case "RightUp":
            //            // Simuler l'arrêt du déplacement du joueur
            //            Debug.Log("Player1 Stop Right");
            //            movementScript_P1.right("off");
            //            break;

            //    case "UpDown":
            //            // Simuler un déplacement du joueur à droite
            //            Debug.Log("Player1 Go Up");
            //            movementScript_P1.up("on");
            //            break;
            //    case "UpUp":
            //            // Simuler l'arrêt du déplacement du joueur
            //            Debug.Log("Player1 Stop Up");
            //            movementScript_P1.up("off");
            //            break;

            //    case "DownDown":
            //            // Simuler un déplacement du joueur à droite
            //            Debug.Log("Player1 Go Down");
            //            movementScript_P1.down("on");
            //            break;
            //    case "DownUp":
            //            // Simuler l'arrêt du déplacement du joueur
            //            Debug.Log("Player1 Stop Down");
            //            movementScript_P1.down("off");
            //            break;
            //}


        }
    }


    public void setScript(menu_move_player tmp_Player)
    {
        movementScript_P1 = tmp_Player;
    }
}






[System.Serializable]
public class WebSocketMessage
{
    public string inputType;
    public string inputDetails;
}
public class Wrapper<T>
{
    public T[] array;
}