# UserTests

### UserTest 1:

{% file src="../.gitbook/assets/Usertest1.mp4" %}

* Controls are easy to understand
* Pre-spawned product on the ground were quite confusing
* Not clear they needed to attend the checkout area
* Not clear what to do at the checkout area
* Clear where product have to be brought to
* Visuals of the game looking good

### UserTest2:

{% file src="../.gitbook/assets/Usertest2.mp4" %}

* Players first had to look if they were player red or blue
* Didn’t know they had to attend to the checkout area
* Though they could only restock the shelves as a task
* Sometimes struggles with grabbing objects from the floor
* Thought they had to give the grabbed object to customers instead of restocking it in the shelf
* Wanted button prompts (didn’t know what buttons to press to interact with something)

### UserTest3:

* &#x20;Bug where the player ragdoll explodes
* More visual indications of where to go and what to do
* Not 100% clear that not helping a customer is a bad thing

### UserTest4:

* Bug where the customers get stuck in their pathfinding when walking against another customer
* Not clear where products have to brought to
* Were missing the element of working together
* Game went by fairly quickly
  * Feedback: slow the game down more so the player has more time to experiment with things.
* Customers need to walk slower so that the player has more time to experiment
* Not helping a customer didn’t give a punishing feeling
* Wasn’t that easy to grab objects
* Fun that players could grab the same object to sort of ‘fight’ over the object
* Possibly adding timer/speedrun element to the game

### UserTest5:

{% hint style="danger" %}
Unfortunately, the webcam footage messed up and ended up capturing the device audio, making it nearly impossible to hear the microphone. I toggled off the device audio beforehand, but somehow it magically turned itself back on during the user test. and I only noticed after the recordings. By that point, there wasn't much I could do to fix it.
{% endhint %}

Video: [https://youtu.be/tnGtOzQT65w](https://youtu.be/tnGtOzQT65w)\
WebCam: [https://youtu.be/tvrvasVcrMQ](https://youtu.be/tvrvasVcrMQ)\
Gameplay: [https://youtu.be/lt1lGlYMgaM](https://youtu.be/lt1lGlYMgaM)

#### Player(s)

* name: Sascha
* PlayedBefore: false
* Age: 22&#x20;

#### What we wanted to test

"Pickup and play" so we told him nothing except that the start button is not "A" but "B" since we used a different type of controller, besides the controls we also wanted to see a "singleplayer" view does it feel the same as playing with someone, do we see them same kind of enjoyment?&#x20;

#### Points of interest (video) & Things we noticed

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

1. Started by looking at the control scheme and tested them out immidialty after starting&#x20;
   * [https://youtu.be/tnGtOzQT65w](https://youtu.be/tnGtOzQT65w)
2. looks like he liked the grab "animation" (repeating it)&#x20;
   * [https://youtu.be/tnGtOzQT65w?t=18](https://youtu.be/tnGtOzQT65w?t=18)
3. "No idea what L1 and R1 do" (ordering screen cycle trough buttons)
   * [https://youtu.be/tnGtOzQT65w?t=26](https://youtu.be/tnGtOzQT65w?t=26)
4. Dind't know what he had to do at first and customers were not spawning yet. and he tried taking items from the shelves (asked afterward since he forgot to talk out loud sometimes)&#x20;
   * [https://youtu.be/tnGtOzQT65w?t=15](https://youtu.be/tnGtOzQT65w?t=15)
5. Grabbed a customer (maybe by accident? while trying the controls)
   * [https://youtu.be/tnGtOzQT65w?t=29](https://youtu.be/tnGtOzQT65w?t=29)
6. noticed the customers waiting at the cash register and knew how to pick up the item(s) / scan them &#x20;
   * [https://youtu.be/tnGtOzQT65w?t=39](https://youtu.be/tnGtOzQT65w?t=39)
7. the door doesn't open you go trough it&#x20;
   * [https://youtu.be/tnGtOzQT65w?t=58](https://youtu.be/tnGtOzQT65w?t=58)
8. dropped an item of the cash register but ignored it&#x20;
   1. [https://youtu.be/tnGtOzQT65w](https://youtu.be/tnGtOzQT65w)
9. he noticed that customers can be scanned (got a small from it)&#x20;
   * [https://youtu.be/tnGtOzQT65w?t=113](https://youtu.be/tnGtOzQT65w?t=113)
10. immediately grabbed the "truck" so it is clear how to interact with it but it was not clear what it was for.&#x20;
    * [https://youtu.be/tnGtOzQT65w?t=123](https://youtu.be/tnGtOzQT65w?t=123)
    * [https://youtu.be/tnGtOzQT65w?t=175](https://youtu.be/tnGtOzQT65w?t=175)
11. tried recycling the products that he didn't scan (because he didn't know how to order and get new products)&#x20;
    * [https://youtu.be/tnGtOzQT65w?t=215](https://youtu.be/tnGtOzQT65w?t=215)
12. Did notice he got money from scanning products but no idea what to do with it&#x20;
    * [https://youtu.be/tnGtOzQT65w?t=203](https://youtu.be/tnGtOzQT65w?t=203)

#### Dev Action points:&#x20;

1. Saw products flying off the map after he ran in to it&#x20;
   * fix: adding a collider around the map&#x20;
   * [https://youtu.be/tnGtOzQT65w?t=200](https://youtu.be/tnGtOzQT65w?t=200)
2. make it easier to understand how to order new products.&#x20;
   * fix: maybe add some text above the ordering screen saying "order products" _(team meeting to find a solution)_
3. _add a clear indication of what you need to do at the start of the game since just spawning them and then having to wait for customer(s) can be confusing_&#x20;
   * _possible fix would be to spawn 1 customer the moment the game starts (team meeting to find a solution)_
4. _Control scheme button is wrong this comes from what controller you are using_&#x20;
   * _either show the complete picture (eg. the arrows and highlight the arrow you need to use) or change the icon shown based on the (devices) connected but then you have the problem when there are multiple different devices_&#x20;



### &#x20;UserTest6:

{% hint style="danger" %}
Unfortunately, the webcam footage messed up and ended up capturing the device audio, making it nearly impossible to hear the microphone. I toggled off the device audio beforehand, but somehow it magically turned itself back on during the user test. and I only noticed after the recordings. By that point, there wasn't much I could do to fix it
{% endhint %}

> Didn't really say all his thoughts even reminded him sometimes to keep talking&#x20;

Video: [https://youtu.be/NDHn5BlFlN0](https://youtu.be/NDHn5BlFlN0)\
WebCam: [https://youtu.be/9IueCEBMZBI](https://youtu.be/9IueCEBMZBI)\
Gameplay: [https://youtu.be/dogD1GrD2uQ](https://youtu.be/dogD1GrD2uQ)

#### Player(s)

* name: Calvin
* PlayedBefore: True (co-op) / not the full game / old version
* Age: 20

#### What we wanted to test

This player did play before in the early stages of the game and only with someone else so there where 2 things we wanted from this user-test, getting feedback on the improvements we made and the difference between co-op and singleplayer what is more enjoyable. &#x20;

#### Points of interest (video) & Things we noticed

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

> A lot harder to find things worth mentioning since he knew most things so we can't really say " oh he didn't know this he knew to do the cashregister etc.. " since well he already knew. "

1. hitting the products of the cash register
   * &#x20;[https://youtu.be/NDHn5BlFlN0?t=78](https://youtu.be/NDHn5BlFlN0?t=78)
2. picking up the customers and throwing them out of the store (breaking the game)&#x20;
   * [https://youtu.be/NDHn5BlFlN0?t=12](https://youtu.be/NDHn5BlFlN0?t=12)
3. trying to fix broken customers "what do you want me to do"
   * &#x20;[https://youtu.be/NDHn5BlFlN0?t=122](https://youtu.be/NDHn5BlFlN0?t=122)
4. grabbing the truck without having ordered anything
   * [https://youtu.be/NDHn5BlFlN0?t=132](https://youtu.be/NDHn5BlFlN0?t=132)
5. tried ordering but had no idea how
   * [https://youtu.be/NDHn5BlFlN0?t=138](https://youtu.be/NDHn5BlFlN0?t=138)
6. speeds up customers by thowing them to the cash register
   * &#x20;[https://youtu.be/NDHn5BlFlN0?t=208](https://youtu.be/NDHn5BlFlN0?t=208)
7. finnaly fixed the customers&#x20;
   * [https://youtu.be/NDHn5BlFlN0?t=221](https://youtu.be/NDHn5BlFlN0?t=221)
8. immediately tried to break the game by buggs he previously found or heard of.&#x20;

#### Dev Action points:&#x20;

1. Fixing existing buggs&#x20;
   * Customers out of the map will never return so the game never ends. (known issue)&#x20;
   * Customers may wait indefinitly for eachother at cashregister or when the paths overlap (known issue)&#x20;
2. The truck is confusing. did not know that you need to order before grabbing the truck&#x20;
   * fix: maybe add the items you ordered on the side of the truck? _(team meeting to find a solution)_
3. Order screen in the back (noting the items you have ordered is the small)&#x20;
   * fix: move it closer, or make it bigger, or think of another solution _(team meeting to find a solution)_
4. _Order button scheme is confusing_&#x20;
   * _needs a rework (team meeting to find a better solution)_&#x20;
5. _Hitting products of the cash register can be frustrating since you barely have enough time to deal with_&#x20;
   * a fix could be adding a "jump" mechanic where you can jump over the cash register and or other elements of the game&#x20;

### UserTest7:

{% hint style="danger" %}
Unfortunately, the webcam footage messed up and ended up capturing the device audio, making it nearly impossible to hear the microphone. I toggled off the device audio beforehand, but somehow it magically turned itself back on during the user test. and I only noticed after the recordings. By that point, there wasn't much I could do to fix it
{% endhint %}

Video: [https://youtu.be/GMyIv6XsBeI](https://youtu.be/GMyIv6XsBeI)\
WebCam: [https://youtu.be/MjB-NySGW\_Y](https://youtu.be/MjB-NySGW\_Y)\
Gameplay: [https://youtu.be/cyl0cmX1jD4](https://youtu.be/cyl0cmX1jD4)

#### What we wanted to test

"Rookie with veteran" and "coop vs singleplayer", in this usertest we wanted to test 2 things what happens when someone that has only played once, plays with someone that has played multiple (multiple version) times, do they help eachother? does the veteran do everything? (this is moslty per person base and can change depending on who is the veteran but still interesting to see) besides that, we wanted to see if its "more fun" to play with someone instead of solo since they both played the latest version solo and not with someone &#x20;

#### Player(s):

* name: Calvin
* PlayedBefore: True (Singleplayer)&#x20;
* Age: 20



* name: Sascha
* PlayedBefore: True (singleplayer)
* Age: 22

#### Points of interest (video) & Things we noticed:

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

1. "Don't press "x" because that would start the game before the other player joins (did have to tell them)
   * &#x20;[https://youtu.be/GMyIv6XsBeI?t=1](https://youtu.be/GMyIv6XsBeI?t=1)
2. Trying to figure out wich character they are playing&#x20;
   * [https://youtu.be/GMyIv6XsBeI?t=9](https://youtu.be/GMyIv6XsBeI?t=9)
3. player(s) saying "oh jij bent de coole bij" translating to "oh you are the cool bee and "I'm something" (referring to a character in the game)"&#x20;
   * [https://youtu.be/GMyIv6XsBeI?t=9](https://youtu.be/GMyIv6XsBeI?t=9)
4. stopped all customers at first but creating a wave of customers and calling it the "midday-rush"&#x20;
   1. [https://youtu.be/GMyIv6XsBeI?t=19](https://youtu.be/GMyIv6XsBeI?t=19)
   2. [https://youtu.be/GMyIv6XsBeI?t=54](https://youtu.be/GMyIv6XsBeI?t=54)
5. "Wait I can hold the customers so you can help them easier"
   * [https://youtu.be/GMyIv6XsBeI?t=66](https://youtu.be/GMyIv6XsBeI?t=66)
6. "Previous play I thruw them out of the map here and they didn't return" saying to me "look at me i'm going to break your game again!" and ofcourse showing that they show things to eachother&#x20;
   * [https://youtu.be/GMyIv6XsBeI?t=25](https://youtu.be/GMyIv6XsBeI?t=25)
7. Didn't realise the truck needs all players before it works&#x20;
   * [https://youtu.be/GMyIv6XsBeI?t=82](https://youtu.be/GMyIv6XsBeI?t=82)
8. "they want the other cash register but they are stuck let me help them"
   * [https://youtu.be/GMyIv6XsBeI?t=94](https://youtu.be/GMyIv6XsBeI?t=94)
9. stressing because a lot of customers are at the cash register
   * [https://youtu.be/GMyIv6XsBeI?t=93](https://youtu.be/GMyIv6XsBeI?t=93)
10. "what products was here 0/4" so its not clear what product is where after the shelf is empty (they did try to remember by asking eachother) and ordering products&#x20;
    1. [https://youtu.be/GMyIv6XsBeI?t=120](https://youtu.be/GMyIv6XsBeI?t=120)
    2. [https://youtu.be/GMyIv6XsBeI?t=126](https://youtu.be/GMyIv6XsBeI?t=126)
11. stressing because there was a customer at the cash register but they where focussed on ordering&#x20;
    * [https://youtu.be/GMyIv6XsBeI?t=130](https://youtu.be/GMyIv6XsBeI?t=130)
12. walked out of the map trying to recover the customers and "fixing" the game&#x20;
    * [https://youtu.be/GMyIv6XsBeI?t=281](https://youtu.be/GMyIv6XsBeI?t=281)
13. hard time trying to pick up a product from the ground&#x20;
    * [https://youtu.be/GMyIv6XsBeI?t=222](https://youtu.be/GMyIv6XsBeI?t=222)



#### Dev Action points:&#x20;

1. Players walking out of the map&#x20;
   * fix: add a colider arround the map? or maybe something more subtle&#x20;
2. customers thrown out of the map break the game&#x20;
   * fix: either make it so they can't leave the store or if this happens remove them and they become unhappy lowering your score (this would also require a visual effect) _(team meeting to find a solution)_
3. ordering button sceme is confusing&#x20;
   * fix: we where already planning to take a look at ordering so its good to see the users also agree  _(team meeting to find a solution)_
4. (bugg) while walking out of the map to recover the customer they grabbed the truck and triggered the "order deliver" logic and so getting there order (they didn't notice that happened)&#x20;
   * fix: make it so the truck logic is "disabled" whilse inactive

### UserTest8:

video: [https://youtu.be/roXlpcWeRqo](https://youtu.be/roXlpcWeRqo)

#### What we wanted to test

“Pickup & Play.” This means that the person testing has never played the game before and us developers can see how easy the game and controls are to understand. Before testing we told the players nothing about the controls and what tasks the player needs to do in the game.

#### Player(s):

* name: Giel
* PlayedBefore: False
* Age: 20



* name: Luuk
* PlayedBefore: False
* Age: 19

#### Points of interest (video)  & Things we noticed:

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

* For a few seconds didn’t know what player he was controlling.
* Noticed that the checkout had to be attended to.
* Noticed that customers can get happy.
* Was confused on how to grab objects.
* Customer was stuck in place. The player was confused by this.
* Noticed that they can drag customers out of the store.
* Didn’t know what to do with the truck.

#### Dev Action points:&#x20;

1. &#x20;Create clearer button indications for grabbing, so the player knows what button to press.
2. Find out how customer can get stuck in place and fix this.
3. Clear indications for the truck
   * Perhaps big text that says “Grab!” with an amount of how many of the needed players have to grab it

### UserTest9:

video: [https://youtu.be/Pul6UgyJHJ8](https://youtu.be/Pul6UgyJHJ8)

#### What we wanted to test

“Grabbing.” We wanted to test the grabbing mechanic in our game. Checking if the buttons for grabbing feel right and whether it is easy to grab specific objects. Before the testing we told the players to mess around as much as possible to be able to give feedback on pickup range and button handling.

#### Player(s):

* name: Luuk
* PlayedBefore: True
* Age: 19



* name: Jaro
* PlayedBefore: False
* Age: 17

#### Points of interest (video)  & Things the player / we noticed:

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

1. Noticed he couldn’t grab cars.
   * [https://youtu.be/Pul6UgyJHJ8?t=108](https://youtu.be/Pul6UgyJHJ8?t=108)
2. Thought the pickup was very natural, range wasn’t too short but not too long either.
3. On keyboard you don’t have very good control over the direction you’re holding something.
4. Tried to grab the plant pot but was disappointed that they couldn’t.
   * [https://youtu.be/Pul6UgyJHJ8?t=99](https://youtu.be/Pul6UgyJHJ8?t=99)

####

#### Dev Action points:&#x20;

1. Either restrict the players to the store only or make cars grabbable. When the cars are grabbable the player must spawn back in the store when leaving the screen.
2. Add directional for keyboard as well by using the mouse movement.
3. Create more objects that are grabbable so that the player can mess around more.

### UserTest10:

video: [https://youtu.be/FAI2pQZRMvE](https://youtu.be/FAI2pQZRMvE)

#### What we wanted to test

“Product ordering” We want to test the ordering in the game. The main reason for testing this comes from the "pickup and play" where they did not know that they had to order, how it worked and that the truck is only usefull if you ordered. the other reason is because there are a lot of products and we needed some feedback on how the ordering system (how to cycle trough all products) works since it can be quite overwhelming if you need to look for an apple between \~18 products&#x20;

#### Player(s):

* name: Giel
* PlayedBefore: True
* Age: 20



* name: Luuk
* PlayedBefore: True
* Age: 19

#### Points of interest (video) & Things the player noticed:

> Small clips from the video(s) that are interesting and things we noticed the player do or player say

* It is not very clear what product has to be ordered.
  * [https://youtu.be/FAI2pQZRMvE?t=27](https://youtu.be/FAI2pQZRMvE?t=27)
* The opening of boxes wasn’t clear to the player.
  * [https://youtu.be/FAI2pQZRMvE?t=88](https://youtu.be/FAI2pQZRMvE?t=88)
* Truck could be opened by only one player the second time the truck arrived.
  * [https://youtu.be/FAI2pQZRMvE?t=183](https://youtu.be/FAI2pQZRMvE?t=183)
* The player had to keep good track of the shelves.
* It was quite easy to learn the list of products on the ordering screen.

#### Dev Action points:&#x20;

1. To make it clear what product must be ordered. We could add a highlight to the shelf of the currently selected product. This way the player can see whether the shelf is empty or not.
2. Create clearer button indications for the opening of boxes.
3. Fix the truck so that there is always two players needed to open it.

