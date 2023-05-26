---
description: How did we create our ragdoll
---

# Ragdoll

#### how does the ragdoll get constructed&#x20;

\- rigged character&#x20;

\- every bone gets a 'configurable joint'&#x20;

\- this joint gets adjusted correctly&#x20;

\- colliders get added where necessary&#x20;

\- correct scripts get added to the correct parts&#x20;

\- copy-limb script for animation&#x20;

\- player-grab for hands

#### problems we encountered

&#x20;\- Player movement was done by changing the transform directly this gave a lot of problems with physics calculations making the player stretch and sometimes even explode.&#x20;

#### how we fixed those problems

&#x20;\- we changed the movement to use the correct type

&#x20;\- we made a script that increases the number of physics calculations so the player would not stretch&#x20;



<figure><img src="../../../.gitbook/assets/player_ragdoll (1).JPG" alt=""><figcaption></figcaption></figure>
