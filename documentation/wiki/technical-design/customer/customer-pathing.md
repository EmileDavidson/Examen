# Customer Pathing

## What we want

For our customers we had one 'main' goal go from your current position to the given position and if you are there trigger an event saying 'pathing finished' so we can then change the state of the customer&#x20;

## How we've done it.

### Fixed Path

### A\* Path

## Some problems we predict(ed) and solutions&#x20;

> By fixing one problem you can create a new problem, so if it looks like there are 'duplicates' or close to being 'duplicates' it's probably not a duplicate if you take all other solutions in to account &#x20;

* Customers pushing each other when walking
* Customers wanting the same destination try to both get there and walk into each other
* Customers that walk towards each other will collide &#x20;
* When customers stand still on their destination for multiple seconds and another customer also want to go there or has pathing over that point&#x20;

##

##
