# CSGSI
A simple C# library to interface with Counter-Strike: Global Offensive *Game State Integration*

## What is Game State Integration

[This wiki page by Valve](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) explains the concept of GSI.

## Installation
1. Get the [latest binaries](https://github.com/rakijah/CSGSI/releases/latest)
2. Get the [JSON Framework .dll by Newtonsoft](https://github.com/JamesNK/Newtonsoft.Json/releases)
3. Extract Newtonsoft.Json.dll from `Bin\Net45\Newtonsoft.Json.dll`
4. Add a reference to both CSGSI.dll and Newtonsoft.Json.dll in your project

## About CSGSI

This library provides means to listen to a specific port for the game clients HTTP POST request. Once a request is received, the game state is parsed and can then be analyzed to display the players' current condition, create bomb timers etc...

CSGSI uses Newtonsoft's [JSON.Net Framework](http://www.newtonsoft.com/json) to parse JSON.

The main class `GSIListener` continuously listens for incoming HTTP requests.  
Once a request is made, it's JSON content is used to construct a new `GameState` object.  
The `GameState` class represents the entire game state as it was sent by the client.  
It also provides access to all rootnodes (see Usage).  
These rootnodes and every subnode of them are of type `GameStateNode`, which encapsulates the underlying JSON data.

## Usage

Use `GSIListener.Start(int port)` to start listening for HTTP POST requests from the game client. This method will return *false* if starting the listener fails.

**!! This step fails if your application is not run with administrator privileges !!**  
**Listening to a URI/port is not possible with regular privileges**

The GSIListener class provides the `NewGameState` event that occurs after a new game state has been received:
```
GSIListener.NewGameState += new EventHandler(OnNewGameState);
...

private static void OnNewGameState(object sender, EventArgs e)
{
	GameState gs = (GameState) sender; //the newest GameState
    //...
}
```

The GameState object provides access to these rootnodes that may be transmitted by the game:

* gs.Provider
* gs.Map
* gs.Round
* gs.Player
* gs.Auth
* gs.Added
* gs.Previously

The rootnodes `allplayers_*` have not yet been implemented and `GameState` currently lacks a `GetRootNode` method (even though `HasRootNode` already exists).

##### Example:

```
GameStateNode playerNode = gs.Player;
```

You can then access further subnodes by calling `playerNode.GetNode(string node);` (returns a `GameStateNode` object).  
You can retrieve a nodes value (as a string) by calling `playerNode.GetValue(string node);`  

##### Example:
```
playerNode.GetNode("state").GetValue("health");
//100

playerNode.GetNode("weapons").GetValue("weapon_0");
//{
//  "name": "weapon_knife",
//  "paintkit": ...
//  ...
//}
```

If the node you are trying to access using `GetValue` does not exist, it returns an empty string (`""`).

## Example program

This snippet will start listening for HTTP POST requests and print out the transmitted JSON data:

```
using System;
using CSGSI;

namespace GSI_Test
{
    class Program
    {
        static void Main(string[] args)
        {
        	//subscribe to the NewGameState event
            GSIListener.NewGameState += new EventHandler(OnNewGameState);
            
            //start listening on http://127.0.0.1:3000/
            if (GSIListener.Start(3000))
            {
                Console.WriteLine("Listening...");
            }
            else
            {
                Console.WriteLine("Error starting GSIListener.");
            }
        }

        private static void OnNewGameState(object sender, EventArgs e)
        {
        	//the newest GameState object is provided as the sender
            GameState gs = (GameState) sender;
            
            //gs.JSON returns the JSON string that was used to create this GameState object
            Console.WriteLine(gs.JSON);
        }
    }
}
```

You will also need to create a custom `gamestate_integration_*.cfg` in `/csgo/cfg`, for example:  
`gamestate_integration_test.cfg`:  
```
"CSGSI Test"
{
 "uri" "http://127.0.0.1:3000"
 "timeout" "5.0"
 "buffer"  "0.1"
 "throttle" "0.5"
 "heartbeat" "60.0"
 "data"
 {
   "provider"           "1"
   "map"            	"1"
   "round"            	"1"
   "player_id"          "1"
   "player_weapons"     "1"
   "player_match_stats" "1"
   "player_state"       "1"
 }
}
```
After starting the application and the game, this setup should output something like this:
![output example](http://i.imgur.com/baTaI0a.png)