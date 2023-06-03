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

> Didn't really say all his toughts even reminded him sometimes to keep talking but nothing i can do about it anymore since doing it again wouldn't give me the same results as letting a complete nooby play.&#x20;

Video: (url)\
WebCam: (url)\
Gameplay: (url)

#### Player(s)

* name: Sascha
* PlayedBefore: false
* Age: 22&#x20;

#### What we wanted to test

"Pickup and play" so we told him nothing except that the start button is not "A" but "B" since we used a different type of controller, besides the controls we also wanted to see a "singleplayer" view does it feel the same as playing with someone, do we see them same kind of enjoyment?&#x20;

#### Things we noticed

* Started by looking at the control scheme and tested them out immidialty after starting&#x20;
* Dind't know what he had to do at first and customers where not spawning yet. and he tried taking items from the shelves (asked afterwards since he forgot to talk out loud sometimes)&#x20;
* Grabbed a customer (maybe by accident? while trying the controls)
* noticed the customers waiting at the cash register and knew how to pick up the item(s) / scan them &#x20;
* he noticed that customers can be scanned and we got a small smile from it&#x20;
* immediately grabbed the "truck" so it is clear how to interact with it but it was not clear what it was for.&#x20;
* tried recycling the products that he didn't scan (because he didn't know how to order and get new products)&#x20;
* Did notice he got money from scanning products&#x20;

#### Points of interest (video)&#x20;

> Small clips from the video(s) that are interesting&#x20;

#### Dev Action points:&#x20;

1. Saw products flying off the map after he ran in to it&#x20;
   * fix: adding a collider around the map&#x20;
2. make it easier to understand how to order new products.&#x20;
   * fix: maybe add some text above the screen with money saying "order products" _(team meeting to find a solution)_

#### &#x20;UserTest6:

{% hint style="danger" %}
Unfortunately, the webcam footage messed up and ended up capturing the device audio, making it nearly impossible to hear the microphone. I toggled off the device audio beforehand, but somehow it magically turned itself back on during the user test. and I only noticed after the recordings. By that point, there wasn't much I could do to fix it
{% endhint %}

> Didn't really say all his thoughts even reminded him sometimes to keep talking&#x20;

Video: (url)\
WebCam: (url)\
Gameplay: (url)

#### Player(s)

* name: Calvin
* PlayedBefore: True (co-op)&#x20;
* Age: 20

#### What we wanted to test

This player did play before in the early stages of the game and only with someone else so there where 2 things we wanted from this user-test, getting feedback on the improvements we made and the difference between co-op and singleplayer what is more enjoyable.&#x20;

#### Things we noticed

> A lot harder to find things worth mentioning since he knew most things so we can't really say " oh he didn't know this he knew to do the cashregister etc.. " since well he already knew. "

* immediately tried to break the game by buggs he previously found or heard of.&#x20;
* Did notice you needed to grab the truck but didn't know how to order products at first.
* Broke the customers and tried to " fix " them but couldn't figure out why they where broken.&#x20;

#### Points of interest (video)&#x20;

> Small clips from the video(s) that are interesting&#x20;

#### Dev Action points:&#x20;

1. Fixing existing buggs&#x20;
   * Customers out of the map will never return so the game never ends.
   * Customers may wait indefinitly for eachother at cashregister or when the paths overlap&#x20;
2. The truck is confusing. did not know that you need to order before grabbing the truck&#x20;
   * fix: maybe add the items you ordered on the side of the truck? _(team meeting to find a solution)_
3. Order screen in the back (noting the items you have ordered is the small)&#x20;
   * fix: move it closer or think of another solution _(team meeting to find a solution)_

#### UserTest7:

{% hint style="danger" %}
Unfortunately, the webcam footage messed up and ended up capturing the device audio, making it nearly impossible to hear the microphone. I toggled off the device audio beforehand, but somehow it magically turned itself back on during the user test. and I only noticed after the recordings. By that point, there wasn't much I could do to fix it
{% endhint %}

Video: (url)\
WebCam: (url)\
Gameplay: (url)

#### What we wanted to test

These 2 players did play before but only "singleplayer" so what we wanted to do with this is test if its more fun together, singleplayer, or about the same and since one of them is somewhat of a 'veteran' does that change how they play? does the veteran do more or will he help the other player.

#### Player(s):

* name: Calvin
* PlayedBefore: True (Singleplayer)&#x20;
* Age: 20



* name: Sascha
* PlayedBefore: True (singleplayer)
* Age: 22

#### Things we noticed:

* player(s) saying "oh jij bent de coole bij" translating to "oh you are the cool bee"&#x20;
* stopped all customers at first but creating a wave of customers and calling it the "midday-rush"&#x20;
* broke the game again by throwing customers out of the map
* walked out of the map trying to recover the customers&#x20;
* (bugg) whilse walking out of the map to recover the customer they grabbed the truck and triggered the "order deliver" logic and so getting there order (they didn't notice that happened)&#x20;
* they found out how to order (but the control scheme isn't the best they button smashed the controller trying to figure it out unfortunately i don't have a controller recording. )

#### Points of interest (video)&#x20;

> Small clips from the video(s) that are interesting&#x20;

#### Dev Action points:&#x20;

1. Players walking out of the map&#x20;
   * fix: add a colider arround the map? or maybe something more subtle&#x20;
2. customers thrown out of the map break the game&#x20;
   * fix: either make it so they can't leave the store or if this happens remove them and they become unhappy lowering your score (this would also require a visual effect) _(team meeting to find a solution)_
3. ordering button sceme is confusing&#x20;
   * fix: ordering needs a rework. _(team meeting to find a solution)_



\




