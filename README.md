# RainText

Summary:

A game where players try to avoid the words that fall from the sky.

Game Description:

In RainText players are able to choose text from different online sources such as movie scripts and songs. The chosen text is then used as input for the game which drops the words from the sky in the order they occur in the chosen text. Some words will be colored in red, these words will spawn pools of fire at the spot they hit the ground. Players are given three lives - which are represented as hearts in the bottom right of the game window - at the start of the game. Players will lose a life when they are hit by a falling word or step in a pool of fire. Upon losing a life players will have a three second grace period during which they cannot lose additional lives, during this time the player’s avatar will flash rapidly. When the player has no lives remaining the game ends. Players are able to use power-ups that will periodically fall from the sky. The three power-ups that can spawn are the: Extra Life, Alarm Clock, and the Water Bucket, each of which provides a different benefit. The Extra Life is the only power-up that automatically activates when collected; the other two will be put in the player inventory if the player is not already holding another power-up. Power-ups held by the player will appear in the bottom left of the game window and can be activated by the player at any time.

Power-Up Effects:

Extra Life:
The Extra Life power-up, represented by a white pill bottle with a red cross and green cap, will refund one of the player's lost lives. Note however that picking up the Extra life power-up with all three lives does not grant a fourth life.

Alarm Clock:
The Alarm Clock power-up, represented by an analog clock with a white clock face with a black rim and clock hands and two yellow alarm bells, will briefly stop time. While time is stopped all falling words will be frozen in place, meanwhile the player will still be able to move as normal. Note that players will still lose a life if they collide with a word or step in a fire while time is stopped.

Water Bucket:
Water Bucket power-up, represented by a brown bucket with a gray handle, will extinguish all active fires on the ground as well as changing all currently active red words - which would have spawned fires when they hit the ground - into regular words which will not spawn fires.

Game Settings:

Text Selection:

If the player does not choose a text source to use the game will use the text from the previous session. To Select a different text source the player should navigate to: “Text Selection” which is found in the Game Settings Menu. Once there the player can select what media type their text is from, and type the title of the text they want to use. 

(Note: searching for obscure titles or for titles that share common terms may result in the incorrect source being used. For instance if a player wanted to use the script for Star Wars: A New Hope then searching for “A New Hope” is more likely to return the desired text than searching for “Star Wars”.)

Before clicking the search button players should ensure they have the Scraper.py file running in the background. If the player attempts to search for text without having Scraper.py running in the background then the request will eventually timeout. If the text is successfully loaded the loading icon should disappear and no error message should appear in its place. If an error message does appear you may need to modify your search based on what it says.

Gameplay Sliders and Key Rebinding:

In the Key Rebinding submenu of Game Settings, players can view all current key bindings as well as rebind keys to fit their preferences. To view what key an action is currently bound to, the user should hover over the button labeled with that action. If the user wants to remap an action to a different key then the user should click on the button with that action’s label at which point they should see a message saying that the action is listening for a new binding, When the user sees this message they can press the key they would like to rebind the action to. If at that point the user sees a message saying the action was rebound to the key they pressed the action remapping was successful. However if the user sees a message saying that key is already bound they will need to remap the action currently bound to that key to another key before trying to remap another action to the key. For instance if “Jump” is bound to key: “A” and the player wants to remap “UsePowerUp” to key: “A”, then the player first must rebind “Jump” to an available key other than key: “A” before rebinding “UsePowerUp” to key: “A”. If any actions are not bound to their default keys then the Reset Bindings button will become available to click. If clicked the Reset Bindings button will return all key bindings to their default values.

In the Gameplay Sliders submenu of Game Settings, the player can view and change the properties of various gameplay mechanics such as the probability that power-ups will spawn. As well as settings related to the user experience such as the game music volume. As with the Key Rebinding submenu, if all settings are not their default values, the player will be able to click a reset button to return all settings controlled by the sliders to their default value.

Known Issues:

When remapping “PrevSong”, “NextSong”, and/or “Modify” the new key bindings do not take effect until the player navigates to a different menu.

Cause of Issue: Unlike other actions which are only used during actual gameplay and thus can be set once at the start of each game in the Awake() method of the gameobjects that use those actions, “PrevSong”, “NextSong”, and “Modify” are all actions that can be used at any time. This means that the script that uses “PrevSong”, “NextSong”, and “Modify” is attached to an object that persists between different scenes so it’s Awake() method will only be called when the application is started and can’t be used as a vector for updating these action mappings.

Proposed Solution: To circumvent this issue, the script that uses “PrevSong”, “NextSong”, and “Modify” could check to see if any of these actions had been changed in its Update() method. While simple to implement, checking for this on every frame update is a computationally expensive solution. Instead this issue could be resolved by adding an additional public method to the script that uses the “PrevSong”, “NextSong”, and “Modify” actions; this method would be called when one of these settings is changed and update the three of them.

Pools of fire spawn with a width equivalent to the next word in the text rather than the width of the word that actually spawns them.

Cause of Issue: Currently believed to be caused by either a race condition between the thread that generates the next falling word and the thread that spawns a pool of fire, or by the width of the fire pool being defined after the width of the collider for the current word is updated.

Proposed Solution: None currently, however this issue will be addressed at a later date once it has been investigated further.

Searching for text using “Wikipedia Page” or “Book” as the media type does not work in game.

Cause of Issue: Unknown at present although the cause of this issue has been isolated to the interaction between the Python flask application and Unity. This can be determined because although these two media types do not work properly in game, they do return the expected results when interacting with the python flask application through a web browser.
Proposed Solution: None currently, more investigation is required to determine if this is an issue that can be resolved or if it is intrinsic to the unity framework.
