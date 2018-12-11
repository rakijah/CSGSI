# CSGSI
A simple C# library to interface with Counter-Strike: Global Offensive *Game State Integration*

## Table of Contents  
[What is Game State Integration](#what-is-game-state-integration)  
[About CSGSI](#about-csgsi)  
[Installation](#installation)  
[Usage](#usage)  
[Events](#events)  
[Null value handling](#null-value-handling)  
[Example program](#example-program)  

## What is Game State Integration

[This wiki page by Valve](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) explains the concept of GSI.


## About CSGSI

This library provides means to listen to a specific port for the game clients HTTP POST request. Once a request is received, the game state is parsed and can then be analyzed.

CSGSI uses Newtonsoft's [JSON.Net Framework](http://www.newtonsoft.com/json) to parse JSON.

Once a `GameStateListener` instance is running, it continuously listens for incoming HTTP requests.  
Every time a request is received, it's JSON content is used to construct a new `GameState` object.  
The `GameState` class represents the entire game state as it was sent by the client.  
It also provides access to all rootnodes (see Usage).


## Installation
Via NuGet:

```
Install-Package CSGSI
```

Manual installation:

1. Get the [latest binaries](https://github.com/rakijah/CSGSI/releases/latest)  
2. Get the [JSON Framework .dll by Newtonsoft](https://github.com/JamesNK/Newtonsoft.Json/releases)  
3. Extract Newtonsoft.Json.dll from `Bin\Net45\Newtonsoft.Json.dll`  
4. Add a reference to both CSGSI.dll and Newtonsoft.Json.dll in your project  

**Hint:** The NuGet package will always be up to date with the latest commit, but I will only publish a new binary release every major update.

## Usage
1. Create a `GameStateListener` instance by providing a port or passing a specific URI:

```C#
GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/  
GameStateListener gsl = new GameStateListener("http://127.0.0.1:81/");
```

**Please note**: If your application needs to listen to a URI other than `http://localhost:*/` (for example `http://192.168.2.2:100/`), you need to ensure that it is run with administrator privileges.  
In this case, `http://127.0.0.1:*/` is **not** equivalent to `http://localhost:*/`.

2. Create a handler:

```C#
void OnNewGameState(GameState gs)
{
    //do stuff
}
```

3. Subscribe to the `NewGameState` event:

```C#
gsl.NewGameState += OnNewGameState;
```

4. Use `GameStateListener.Start()` to start listening for HTTP POST requests from the game client. This method will return `false` if starting the listener fails (most likely due to insufficient privileges).

##### Examples:
```C#
int Health = gs.Player.State.Health;
//100

string activeWep = gs.Player.Weapons.ActiveWeapon.JSON
//{
//  "name": "weapon_knife",
//  "paintkit": ...
//  ...
//}
```

## Events

CSGSI provides a few helpful events that you can subscribe to avoid having to implement the logic yourself. These events are experimental and not entirely reliable (mostly because CSGO's Game State Integration itself sends inconsistent data).

Raising these events is disabled by default. To enable this feature, set `GameStateListener.EnableRaisingIntricateEvents = true`.

Available events: 

* `RoundPhaseChanged` - Is raised when the round phase changes (for example "Live", "FreezeTime" etc.).
* `PlayerFlashed` - Is raised when a player is flashed. Includes information about how much the player was flashed (0 - 255).
* `BombPlanted` - Is raised when the bomb is planted.
* `BombDefused` - Is raised when the bomb is defused.
* `BombExploded` - Is raised when the bomb explodes.
* `RoundEnd` - Is raised when the round ends.
* `RoundBegin` - Is raised when a round begins (i.e. exits FreezeTime).

## Null value handling

In case the JSON did not contain the requested information or parsing the node failed, these values will be returned instead:

Type|Default value
----|-------------
int|-1
float|-1
string| String.Empty
Vector3| (X: 0, Y: 0, Z: 0)

All Enums have a value `enum.Undefined` that serves the same purpose.

## Example program

Prints "Bomb has been planted", every time you plant the bomb:

```C#
using System;
using CSGSI;
using CSGSI.Nodes;

namespace CSGSI_Test
{
    class Program
    {
        static GameStateListener gsl;
        static void Main(string[] args)
        {
            gsl = new GameStateListener(3000);
            gsl.NewGameState += new NewGameStateHandler(OnNewGameState);
            if (!gsl.Start())
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Listening...");
        }
        
        static void OnNewGameState(GameState gs)
        {
            if(gs.Round.Phase == RoundPhase.Live &&
               gs.Bomb.State == BombState.Planted &&
               gs.Previously.Bomb.State == BombState.Planting)
            {
                Console.WriteLine("Bomb has been planted.");
                IsPlanted = true;
            }
        }
    }
}
```

Note that you can now also use events provided by the GameStateListener to achieve this:

```C#
using System;
using CSGSI;
using CSGSI.Nodes;

namespace CSGSI_Test
{
    class Program
    {
        static GameStateListener gsl;
        static void Main(string[] args)
        {
            gsl = new GameStateListener(3000);
            gsl.BombPlanted += BombPlanted;
            gsl.EnableRaisingIntricateEvents = true;
            if (!gsl.Start())
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Listening...");
        }

        private static void BombPlanted(CSGSI.Events.BombPlantedEventArgs e)
        {
            Console.WriteLine("Bomb has been planted.");
        }
    }
}
```

You will also need to create a custom `gamestate_integration_*.cfg` in `/csgo/cfg`, for example:  
`gamestate_integration_test.cfg`:  
```
"CSGSI Example"
{
	"uri" "http://localhost:3000"
	"timeout" "5.0"
	"auth"
	{
		"token"				"CSGSI Test"
	}
	"data"
	{
		"provider"              	"1"
		"map"                   	"1"
		"round"                 	"1"
		"player_id"					"1"
		"player_weapons"			"1"
		"player_match_stats"		"1"
		"player_state"				"1"
		"allplayers_id"				"1"
		"allplayers_state"			"1"
		"allplayers_match_stats"	"1"
	}
}
```

**Please note**: In order to run this test application without explicit administrator privileges, you need to use the URI `http://localhost:<port>` in this configuration file.
