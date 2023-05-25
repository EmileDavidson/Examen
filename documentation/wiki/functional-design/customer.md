# Customer

Customers

In the game there will be customers coming to your store. How customers work and how they affect the player will be explained here.

#### Pathfinding

When a customer enters the store they will decide on a product which they will try and get. As the customer navigates through the store they can be grabbed by the player, which will disable their AI. This AI will be re-enabled once the player lets go of the customer again. (More explanation on the customer AI can be found [here](customer.md#pathfinding))

#### Gathering a Product

Once the customer arrives at the destined shelf which contains their product they will start a timer, this timer indicates how long it takes for a customer to grab an item. After the customer has grabbed their item they will navigate to the checkout area.

#### Checkout

When a customer gets to the checkout area there is a chance the customer will have to wait in line. Once this line is empty and the customer can get their product scanned they will place their product on the conveyor belt. Though named a conveyor belt, this belt does not move and the players will have to grab the object and scan it. Once the product has been successfully scanned, the player will receive the according amount of money and the customer will leave the store.

#### Happiness

Some of the actions the customers do affect the happiness of the customer. The customer’s happiness can be 1 of 3 states, these states are the following: “Happy, Neutral and Angry.” At the start of the game this state will be neutral. When a customer wants to grab a product from the shelf their happiness gets affected. If the product they are looking for is there, the customer will be happy and will grab the product. If the product they are looking for is not there, the customer will be angry and won’t have any items to scan at the checkout, thus leaving the store. When a customer does have a product and gets their product successfully scanned at the checkout, the customer will be happy. But if their product does not get scanned, their happiness state will go down and become neutral again.
